using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using config;

namespace OtagujPlikiTests
{
    [TestClass]
    public class OtagujPlikiTests
    {
        [DataRow(@"C:\Users\User\Desktop")]
        [TestMethod]
        public void Test_Konstruktora_Poprawny_Argument(string path)
        {
            Searcher searcher = new Searcher(path);
            Assert.AreEqual(searcher.Path, path);
        }

        [DataRow(1)]
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void Test_Konstruktora_Niepoprawny_Argument(string path)
        {
            Searcher searcher = new Searcher(path);
        }
    }
}
