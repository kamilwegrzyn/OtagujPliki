using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using config.libs;

namespace OtagujPlikiTests
{
    [TestClass]
    public class SearcherTests
    {
        #region Test Konstruktora
        [DataRow(@"C:\\Users\User\Desktop", "*.txt")]
        [TestMethod]
        public void Test_Konstruktora_Poprawny_Argument(string path, string type)
        {
            Searcher searcher = new Searcher(path, type);
            Assert.AreEqual(searcher.Path, path);
            Assert.AreEqual(searcher.Type, type);
        }

        [TestMethod]
        public void Konstruktor_Domyslny_Test()
        {
            Searcher searcher = new Searcher();
            string path = searcher.Path;
            string type = searcher.Type;

            Assert.AreEqual(searcher.Path, path);
            Assert.AreEqual(searcher.Type, type);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Konstruktora_Niepoprawny_Argument(string path)
        {
            Searcher searcher = new Searcher(path);
        }
        #endregion

        #region Testy funkcji wyszukiwania
        [DataTestMethod]
        [DataRow(@"C:\\Users\Sebastian\Desktop", "*.txt", "aa")]
        [DataRow(@"C:\\Users\Sebastian\Desktop", "*.pdf", "bilet")]
        public void GetAllFiles_Tests_OK(string path, string type, string filename)
        {
            Searcher searcher = new Searcher(path, type);
            foreach (string files in searcher.searching_result)
            {
                searcher.searching_result.Add(files);

                Assert.AreEqual(searcher.Path, path);
                Assert.AreEqual(searcher.Type, type);
                Assert.AreEqual(files, filename);
            }
        }
        #endregion
    }
}
