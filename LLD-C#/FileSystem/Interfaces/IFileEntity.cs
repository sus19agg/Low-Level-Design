using FileSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Interfaces
{
    internal interface IFileEntity
    {
        string GetName();
        string GetPath();
        void SetName(string name);
        void SetPath(string path);
        Metadata GetMetadata();
    }
}
