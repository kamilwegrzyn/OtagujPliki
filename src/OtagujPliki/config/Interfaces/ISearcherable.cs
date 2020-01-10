using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace config.Interfaces
{
    public interface ISearcherable
    {
        string GetAllFiles(string path, string type);
        string GetFile(string path, string name);
    }
}
