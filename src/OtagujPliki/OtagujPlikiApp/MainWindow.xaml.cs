﻿using config.libs;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OtagujPlikiApp
{
    public partial class MainWindow : Window
    {
        public class FileProp
        {
            public string File { get; set; }

            public string Path { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
            var gridView = new GridView();
            ListViewFiles.View = gridView;
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "File",
                DisplayMemberBinding = new Binding("File")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Path",
                DisplayMemberBinding = new Binding("Path")
            });
            
        }

        Searcher searcher = new Searcher();
        public string Path { get; set; }

        private void buttonPath_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                Path = dialog.SelectedPath;
                textBoxPath.Text = Path;
            }
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                searcher.searching_result.Clear();
                ListViewFiles.Items.Clear();
                string path = "";
                if (string.IsNullOrEmpty(Path))
                {
                    path = System.IO.Path.GetPathRoot(Environment.SystemDirectory);
                }
                else
                {
                    path = Path;
                }

                searcher.GetAllFiles(path, "*." + textBoxType.Text, textBoxName.Text);
                foreach (string pathEx in searcher.searching_result)
                {

                    string[] dir = pathEx.Split('\\');

                    ListViewFiles.Items.Add(new FileProp { File = dir[dir.Length - 1], Path = pathEx });
                }
            }
            catch (UnauthorizedAccessException uAex)
            {
                MessageBox.Show($"{uAex.Message} Proszę podać dokładniejsze parametry", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void textBoxPath_TextChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxPath.Text))
            {
                buttonSearch.IsEnabled = true;
            }
            else
            {
                buttonSearch.IsEnabled = false;
            }
        }

        private void buttonAddTag_Click(object sender, RoutedEventArgs e)
        {

            if (textBoxAddTag.Text != "")
            {
                addTag(((FileProp)ListViewFiles.SelectedItem).Path, ((FileProp)ListViewFiles.SelectedItem).File, textBoxAddTag.Text);
                getTags(((FileProp)ListViewFiles.SelectedItem).Path);
            }
        }

        private void buttonOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", $@"{((FileProp)ListViewFiles.SelectedItem).Path}");
            }
            catch
            {
                MessageBox.Show("Zaznacz plik przed otwarciem", "Błąd programu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        

        private void buttonSearchTag_Click(object sender, RoutedEventArgs e)
        {
            getFilesByTag(textBoxSearchTag.Text);
        }
        private void ListViewFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ListViewFiles_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            GetTableDB();
            if (ListViewFiles.SelectedItems.Count > 0)
            {
                getTags(((FileProp)ListViewFiles.SelectedItem).Path);
            }
        }

        private void addTag(string path, string file, string tag)
        {
            SQLiteConnection sqlc = new SQLiteConnection("Data Source=" + Environment.CurrentDirectory + "/tagi.sqlite");
            sqlc.Open();
            string sql = "INSERT INTO files(Path,Name,Tag,WhenInserted) VALUES(@param1,@param2,@param3,@param4)";


            SQLiteParameter param1 = new SQLiteParameter("param1", DbType.String);
            SQLiteParameter param2 = new SQLiteParameter("param2", DbType.String);
            SQLiteParameter param3 = new SQLiteParameter("param3", DbType.String);
            SQLiteParameter param4 = new SQLiteParameter("param4", DbType.DateTime.ToString());

            SQLiteCommand cmd = new SQLiteCommand(sql, sqlc);
            cmd.Parameters.Add(param1);
            cmd.Parameters.Add(param2);
            cmd.Parameters.Add(param3);
            cmd.Parameters.Add(param4);

            param1.Value = path;
            param2.Value = file;
            param3.Value = tag;
            param4.Value = DateTime.Now.ToString();

            try
            {
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.HResult + "\n" + ex.Message, "Błąd programu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            sqlc.Close();


        }
      

        //połączenia z bazą danych
        private void getTags(string path)
        {
            SQLiteConnection sqlc = new SQLiteConnection("Data Source=" + Environment.CurrentDirectory + "/tagi.sqlite");
            ListViewFiles.Items.Clear();
            sqlc.Open();
            string sql = "SELECT * FROM files where Path = @param1 ";
            //string sql = "SELECT * FROM files  ";
            SQLiteParameter param1 = new SQLiteParameter("param1", DbType.String);
            SQLiteCommand cmd = new SQLiteCommand(sql, sqlc);
            cmd.Parameters.Add(param1);
            param1.Value = path;
            try
            {
                SQLiteDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ListViewFiles.Items.Add(dr[2].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.HResult + "\n" + ex.Message, "Błąd programu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            sqlc.Close();
        }


        private void deleteTag(string path, string tag)
        {
            SQLiteConnection sqlc = new SQLiteConnection("Data Source=" + Environment.CurrentDirectory + "/tagi.sqlite");
            sqlc.Open();
            string sql = "DELETE FROM files WHERE Path=@param1 and Tag=@param2";
            SQLiteParameter param1 = new SQLiteParameter("param1", DbType.String);
            SQLiteParameter param2 = new SQLiteParameter("param2", DbType.String);
            SQLiteCommand cmd = new SQLiteCommand(sql, sqlc);
            cmd.Parameters.Add(param1);
            cmd.Parameters.Add(param2);
            param1.Value = path;
            param2.Value = tag;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.HResult + "\n" + ex.Message, "Błąd programu", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            sqlc.Close();
        }

        private void buttonRemoveTag_Click(object sender, RoutedEventArgs e)
        {
            if ((ListViewFiles.SelectedItems.Count > 0) && (ListViewFiles.SelectedItems.Count > 0))
                try
                {
                    deleteTag(((FileProp)ListViewFiles.SelectedItem).Path, ((FileProp)ListViewFiles.SelectedItem).File);
                    getTags(((FileProp)ListViewFiles.SelectedItem).Path);
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Musisz zaznaczyć tagi do usunięcia");
                }
        }



        private void getFilesByTag(string tag)
        {
            SQLiteConnection sqlc = new SQLiteConnection("Data Source=" + Environment.CurrentDirectory + "/tagi.sqlite");
            ListViewFiles.Items.Clear();
            sqlc.Open();
            string sql = "SELECT * FROM files where Tag = @param1 ";
            //string sql = "SELECT * FROM files  ";
            SQLiteParameter param1 = new SQLiteParameter("param1", DbType.String);
            SQLiteCommand cmd = new SQLiteCommand(sql, sqlc);
            cmd.Parameters.Add(param1);
            param1.Value = tag;

            ListViewFiles.Items.Clear();
            try
            {
                SQLiteDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ListViewFiles.Items.Add(new FileProp { File = dr[1].ToString(), Path = dr[0].ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.HResult + "\n" + ex.Message, "Błąd programu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            sqlc.Close();
        }

        private void GetTableDB()
        {
            SQLiteConnection con = new SQLiteConnection($"Data Source=" + Environment.CurrentDirectory + "/tagi.sqlite");
            con.Open();

            SQLiteCommand cmd = new SQLiteCommand(con)
            {
                CommandText = "CREATE TABLE IF NOT EXISTS files(Path NVARCHAR(255),Name NVARCHAR(255),Tag NVARCHAR(255),WhenInserted NVARCHAR(255))"
            };
            cmd.ExecuteNonQuery();

            con.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxType.Text))
            {
                buttonSearch.IsEnabled = true;
            }
            else
            {
                buttonSearch.IsEnabled = false;
            }
        }
    }
}
