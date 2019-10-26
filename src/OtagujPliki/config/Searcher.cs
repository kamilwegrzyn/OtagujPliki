using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace config
{
    /// <summary>
    /// Biblioteka główna programu
    /// Dodane przydatne funkcje
    /// </summary>
    public class Searcher
    {
        public string Path { get; set; }

        public Searcher(string path)
        {
            try
            {
                Path = path;
            }
            catch(ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        //Listuje wszystkie ścieżki plików w podanym folderze o zadanym rozszerzeniu
        public string GetAllFiles(string path)
        {
            foreach (string files in Directory.EnumerateFiles(path, "*.txt", 
                SearchOption.AllDirectories))
            {
                Console.WriteLine(files);
            }
            return path;
        }
    }
}
