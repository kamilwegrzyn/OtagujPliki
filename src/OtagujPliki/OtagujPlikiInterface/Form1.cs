using config.libs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtagujPlikiInterface
{
    public partial class Form1 : Form
    {
        public string Sciezka { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Sciezka = folderBrowserDialog1.SelectedPath;
                textBox1.Text = Sciezka;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Searcher searcher = new Searcher();
            searcher.GetAllFiles(Sciezka, "*.txt");
            foreach (string file in searcher.searching_result)
                listBox1.Items.Add(file);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBox1.Text))
            {
                button2.Enabled = true;
            } else
            {
                button2.Enabled = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Searcher searcher = new Searcher();
            searcher.AddTagToFile(new string[] { "" },"test");
        }
    }
}
