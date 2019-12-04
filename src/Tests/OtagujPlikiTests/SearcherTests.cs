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
    }
}
