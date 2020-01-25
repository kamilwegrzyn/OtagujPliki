using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using config.Interfaces;
using System.Windows;
using DocumentFormat.OpenXml.CustomProperties;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml.Wordprocessing;



namespace config.libs
{
    /// <summary>
    /// Biblioteka główna programu
    /// Dodane przydatne funkcje
    /// </summary>
    public class Searcher : ISearcherable
    {
        /// <summary>
        /// Publiczne properties
        /// <param name="Path"></param>
        /// <param name="Type"></param>
        /// </summary>
        public string Path { get; set; }
        public string Type { get; set; }
        public List<string> searching_result = new List<string>();

        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        /// <param name="path"></param>
        /// <param name="type"></param>
        public Searcher(string path = "", string type = "")
        {
            try
            {
                Path = path;
                Type = type;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// Listuje wszystkie ścieżki plików w podanym folderze o zadanym rozszerzeniu
        /// </summary>
        /// <param name="path"></param>
        /// <param name="type"></param>
        /// <returns>
        /// Funkcja zwraca ścieżki do plików jeśli argument filename nie jest pusty wyszukuje dokładnie takiego pliku
        /// </returns>
        public string GetAllFiles(string path, string type, string filename = "")
        {
            foreach (string files in Directory.GetFiles(path, type,
                 SearchOption.AllDirectories))
            {
                if (filename == "")
                {
                    string[] dir = files.Split('\\');
                    //searching_result.Add(dir[dir.Length - 1]);
                    searching_result.Add(files);
                }
                else
                {
                    string[] dir = files.Split('\\');
                    if (dir[dir.Length - 1].Equals((filename + type.Substring(1))))
                    {
                        //searching_result.Add(dir[dir.Length - 1]);
                        searching_result.Add(files);
                    }
                }
            }
            return path;
        }
    }
}
