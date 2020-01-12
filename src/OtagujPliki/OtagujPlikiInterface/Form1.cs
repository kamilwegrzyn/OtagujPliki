using config.libs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Data.SQLite;


namespace OtagujPlikiInterface
{
    public partial class Form1 : Form
    {
        Searcher searcher = new Searcher();
        public string Path { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonPath_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Path = folderBrowserDialog1.SelectedPath;
                textBoxPath.Text = Path;
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            searcher.searching_result.Clear();
            listView1.Items.Clear();
            string path = "";
            if(string.IsNullOrEmpty(Path))
            {
                path = System.IO.Path.GetPathRoot(Environment.SystemDirectory);
            } else
            {
                path = Path;
            }

            searcher.GetAllFiles(path, "*." + textBoxType.Text, textBoxName.Text);
            foreach (string file in searcher.searching_result)
            {



                string[] dir = file.Split('\\');
                ListViewItem item = new ListViewItem(new string[] { dir[dir.Length - 1], file });
                listView1.Items.Add(item);
            }

        }

        private void textBoxPath_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBoxPath.Text))
            {
                buttonSearch.Enabled = true;
            } else
            {
                buttonSearch.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "")
            {
                addTag(listView1.SelectedItems[0].SubItems[1].Text, listView1.SelectedItems[0].SubItems[0].Text, textBox1.Text);
                getTags(listView1.SelectedItems[0].SubItems[1].Text);
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", $@"{listView1.SelectedItems[0].SubItems[1].Text}");
        }

        private void textBoxType_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBoxType.Text))
            {
                buttonSearch.Enabled = true;
            } else
            {
                buttonSearch.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            getFilesByTag(textBox2.Text);
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                getTags(listView1.SelectedItems[0].SubItems[1].Text);
            }
        }

        private void addTag(string path, string file, string tag)
        {
            SQLiteConnection sqlc = new SQLiteConnection("Data Source="+ Environment.CurrentDirectory + "/tagi.sqlite");
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
                MessageBox.Show(ex.HResult + "\n" + ex.Message, "Błąd programu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            sqlc.Close();


        }

        private void ListView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {


        }

        //połączenia z bazą danych
        private void getTags(string path)
        {
            SQLiteConnection sqlc = new SQLiteConnection("Data Source=" + Environment.CurrentDirectory + "/tagi.sqlite");
            listViewTags.Items.Clear();
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
                        ListViewItem item = new ListViewItem(new string[] { dr[2].ToString() });
                        listViewTags.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.HResult + "\n" + ex.Message, "Błąd programu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            sqlc.Close();
        }


        private void deleteTag(string path, string tag)
        {
            SQLiteConnection sqlc = new SQLiteConnection("Data Source=" + Environment.CurrentDirectory + "/tagi.sqlite");
            sqlc.Open();
            SQLiteParameter param1 = new SQLiteParameter("param1", DbType.String);
            SQLiteParameter param2 = new SQLiteParameter("param2", DbType.String);
            string sql = "DELETE FROM files WHERE Path=@param1 and Tag=@param2";
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
                MessageBox.Show(ex.HResult + "\n" + ex.Message, "Błąd programu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            sqlc.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if ((listView1.SelectedItems.Count > 0) && (listViewTags.SelectedItems.Count > 0))
            {
                deleteTag(listView1.SelectedItems[0].SubItems[1].Text, listViewTags.SelectedItems[0].SubItems[0].Text);
                getTags(listView1.SelectedItems[0].SubItems[1].Text);
            }
        }



        private void getFilesByTag(string tag)
        {
            SQLiteConnection sqlc = new SQLiteConnection("Data Source=" + Environment.CurrentDirectory + "/tagi.sqlite");
            listViewTags.Items.Clear();
            sqlc.Open();
            string sql = "SELECT * FROM files where Tag = @param1 ";
            //string sql = "SELECT * FROM files  ";
            SQLiteParameter param1 = new SQLiteParameter("param1", DbType.String);
            SQLiteCommand cmd = new SQLiteCommand(sql, sqlc);
            cmd.Parameters.Add(param1);
            param1.Value = tag;

            listView1.Items.Clear();
            try
            {
                SQLiteDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ListViewItem item = new ListViewItem(new string[] { dr[1].ToString(), dr[0].ToString() });
                        listView1.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.HResult + "\n" + ex.Message, "Błąd programu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            sqlc.Close();
        }


    }
}
