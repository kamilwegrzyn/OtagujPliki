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
        /// Funkcja zwraca ścieżki do plików
        /// </returns>
        public string GetAllFiles(string path, string type)
        {
            foreach (string files in Directory.GetFiles(path, type,
                 SearchOption.AllDirectories))
            {
                string[] dir = files.Split('\\');
                searching_result.Add(dir[dir.Length - 1]);

            }
            return path;
        }

        /// <summary>
        /// Wyciąga plik z podanej lokalizacji
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns>
        /// Funkcja zwraca jeden plik o podanej nazwie
        /// </returns>
        public string GetFile(string path, string name)
        {
            return File.ReadAllText(path+ "\\"+name);
        }

        /// <summary>
        /// Dodaje tagi do wybranych plików
        /// </summary>
        /// <param name="files"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public string AddTagToFile(string[] files, string tag)
        {
            using (WordprocessingDocument document = WordprocessingDocument.Open($@"{Directory.GetCurrentDirectory()}\\tagi.odt", true))
            {
                document.PackageProperties.Keywords += string.IsNullOrEmpty(document.PackageProperties.Keywords) ? tag : "; "+tag;
            }
            return "";
        }
    }
}
