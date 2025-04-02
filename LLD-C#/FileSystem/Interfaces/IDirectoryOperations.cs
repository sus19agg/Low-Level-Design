using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Interfaces
{
    internal interface IDirectoryOperations
    {
        bool AddFileEntity(IFileEntity entity);
        bool RemoveFileEntity(IFileEntity entity);
        bool HasFileEntity(string name);
        IFileEntity GetFileEntity(string name);
        List<IFileEntity> ListFileEntities();
    }
}
