using System;
using System.Collections.Generic;
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
using config.libs;
using System.Diagnostics;

namespace OtagujPlikiApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Click_search(object sender, RoutedEventArgs e)
        {
            string path = _path.Text;
            string type = "*." + _type.Text;
            Searcher searcher = new Searcher();
            searcher.GetAllFiles(path, type);

            foreach(string files in searcher.searching_result)
            {
                _results.Items.Add(files);
            }
        }

        private void Click_open(object sender, RoutedEventArgs e)
        {
            Process.Start($"{_results.SelectedItem}");
        }

        private void Click_view(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", @"Ten komputer");
        }
    }
}
