using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using config;

namespace OtagujPlikiTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\User\Desktop";
            Searcher searcher = new Searcher(path);
            searcher.GetAllFiles(path);
        }
    }
}
