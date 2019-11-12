using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using config;

namespace OtagujPlikiTests
{
    [TestClass]
    public class OtagujPlikiTests
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

        [DataRow(1)]
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void Test_Konstruktora_Niepoprawny_Argument(string path)
        {
            Searcher searcher = new Searcher(path);
        }
        #endregion
    }
}
