using FileSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Interfaces
{
    internal interface IFileSystem
    {
        void CreateFile(string path, string content);
        void CreateDirectory(string path);
        IFileEntity GetFileEntity(string path);
        void List(string path);
        void Move(string from_path, string to_path);
        void Delete(string path);
        Metadata GetMetadata(string path);
    }
}
