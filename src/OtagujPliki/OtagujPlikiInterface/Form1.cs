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
            if(!string.IsNullOrEmpty(textBoxName.Text))
            {
                searcher.GetFile(textBoxPath.Text, textBoxName.Text + "." + textBoxType.Text);
            }
            else
            {
                searcher.GetAllFiles(Path, "*." + textBoxType.Text);
                foreach (string file in searcher.searching_result)
                    listBoxResult.Items.Add(file);
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
            Searcher searcher = new Searcher();
            searcher.AddTagToFile(new string[] { "" },"test");
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", $@"{listBoxResult.SelectedItem}");
        }
    }
}
