using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using config.libs;

namespace OtagujPlikiTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\User\Desktop";
            string type = "*.txt";
            Searcher searcher = new Searcher();
            searcher.GetAllFiles(path, type);
        }
    }
}
