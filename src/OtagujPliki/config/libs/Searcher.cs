using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using config.Interfaces;

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

        //Listuje wszystkie ścieżki plików w podanym folderze o zadanym rozszerzeniu
        public string GetAllFiles(string path, string type)
        {
            try
            {
                foreach (string files in Directory.EnumerateFiles(path, type,
                SearchOption.AllDirectories))
                {
                    searching_result.Add(files);
                }
                return path;
            }
            catch(UnauthorizedAccessException uAex)
            {
                Console.WriteLine(uAex.Message);
            }
            return path;
        }

        //Wyciąga plik z podanej lokalizacji
        public string GetFile(string path, string name) => throw new NotImplementedException();
    }
}
