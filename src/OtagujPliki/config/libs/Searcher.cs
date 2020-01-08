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
        public string Path { get; set; }
        public string Type { get; set; }
        public List<string> searching_result = new List<string>();

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

        //Listuje wszystkie ścieżki plików w podanym folderze o zadanym rozszerzen
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

        //Wyciąga plik z podanej lokalizacji
        public string GetFile(string path, string name)
        {
            return File.ReadAllText(path+ "\\"+name);
        }

        public string AddTagToFile(string[] files, string tag)
        {
            using (WordprocessingDocument document = WordprocessingDocument.Open(@"C:\Users\Agnieszka\Desktop\C#\test\Nowy Dokument programu Microsoft Word.docx", true))
            {
                document.PackageProperties.Keywords += string.IsNullOrEmpty(document.PackageProperties.Keywords) ? tag : "; "+tag;
                Console.WriteLine();
            }
            return "";
        }
    }
}
