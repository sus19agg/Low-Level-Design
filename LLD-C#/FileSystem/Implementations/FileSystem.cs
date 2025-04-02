using FileSystem.Enums;
using FileSystem.Interfaces;
using FileSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FileSystem.Implementations
{
    internal class FileSystem : IFileSystem
    {
        private Directory root;
        private Dictionary<string, IFileEntity> cache;

        public FileSystem()
        {
            root = new Directory("root", "", "admin");
            cache = new Dictionary<string, IFileEntity>();
        }

        private void RemoveAllChildrenFromCache(IFileEntity entity)
        {

        }

        private Directory GetParentDirectory(IFileEntity fileEntity)
        {
            List<string> pathList = fileEntity.GetPath().Split('/').ToList();
            if (pathList.Count > 0)
            {
                Directory tempRoot = root;
                foreach (string name in pathList)
                {
                    if (tempRoot.HasFileEntity(name))
                    {
                        tempRoot = tempRoot.GetFileEntity(name) as Directory;
                    }
                    else
                    {
                        throw new Exception("invalid path");
                    }
                }
                return tempRoot;
            }
            return root;
        }

        public void CreateDirectory(string path)
        {
            List<string> pathList = path.Split('/').ToList();
            Directory tempRoot = root;
            string newPath = "/";
            foreach (string name in pathList) {
                if (tempRoot.HasFileEntity(name))
                {
                    tempRoot = tempRoot.GetFileEntity(name) as Directory;
                } else
                {
                    IFileEntity newDir = new Directory(name, newPath, "admin");
                    tempRoot.AddFileEntity(newDir);
                    cache.Add(newPath+name, newDir);
                    tempRoot = newDir as Directory;
                }
                newPath = newPath + name + "/";
            }
        }

        public void CreateFile(string path, string content)
        {
            List<string> pathList = path.Split('/').ToList();
            string fileName = pathList.Last();
            pathList.RemoveAt(pathList.Count - 1);
            Directory tempRoot = root;
            string newPath = "/";
            foreach (string name in pathList)
            {
                if (tempRoot.HasFileEntity(name))
                {
                    tempRoot = tempRoot.GetFileEntity(name) as Directory;
                }
                else
                {
                    IFileEntity newDir = new Directory(name, newPath, "admin");
                    tempRoot.AddFileEntity(newDir);
                    cache.Add(newPath + name, newDir);
                    tempRoot = newDir as Directory;
                }
                newPath = newPath + name + "/";
            }
            File newFile = new File(fileName, newPath, "admin");
            newFile.WriteContent(content);
            tempRoot.AddFileEntity(newFile);
            cache.Add(newPath + fileName, newFile);
        }

        public void Delete(string path)
        {
            List<string> pathList = path.Split('/').ToList();
            string fileEntityName = pathList.Last();
            pathList.RemoveAt(pathList.Count - 1);
            Directory tempRoot = root;
            foreach (string name in pathList)
            {
                if (tempRoot.HasFileEntity(name))
                {
                    tempRoot = tempRoot.GetFileEntity(name) as Directory;
                }
                else
                {
                    throw new Exception("invalid path");
                }
            }
            IFileEntity fileEntity = tempRoot.GetFileEntity(fileEntityName);
            cache.Remove(path);
            if (fileEntity.GetMetadata().fileEntityType == FileEntityType.Directory)
            {
                RemoveAllChildrenFromCache(fileEntity);
                fileEntity = null;
            }
            fileEntity = null;
        }

        public IFileEntity GetFileEntity(string path)
        {
            if(cache.ContainsKey(path)) { return cache[path]; }
            return null;
        }

        public Metadata GetMetadata(string path)
        {
            if (cache.ContainsKey(path)) { return cache[path].GetMetadata(); }
            return null;
        }

        public void List(string path)
        {
            List<string> pathList = path.Split('/').ToList();
            Directory tempRoot = root;
            foreach (string name in pathList)
            {
                if (tempRoot.HasFileEntity(name))
                {
                    tempRoot = tempRoot.GetFileEntity(name) as Directory;
                }
                else
                {
                    throw new Exception("invalid path");
                }
            }
            List<IFileEntity> files = tempRoot.ListFileEntities();
            Console.WriteLine("Executing List for "+tempRoot.GetName());
            foreach (IFileEntity fileEntity in files) {
                Console.WriteLine("Name -> " + fileEntity.GetName() + " IsDirectory -> " + (fileEntity.GetMetadata().fileEntityType == FileEntityType.Directory));
            }
        }

        public void Move(string from_path, string to_path)
        {
            File curr = GetFileEntity(from_path) as File;
            if (curr != null) { 
                Directory currParent = GetParentDirectory(curr);
                if (currParent != null) { 
                    currParent.RemoveFileEntity(curr);
                }
                CreateFile(to_path, curr.GetContent());
                File newFile = cache[to_path] as File;
            }
        }
    }
}
