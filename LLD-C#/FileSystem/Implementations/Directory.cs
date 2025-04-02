using FileSystem.Enums;
using FileSystem.Interfaces;
using FileSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Implementations
{
    internal class Directory : IFileEntity, IDirectoryOperations
    {
        private string name;
        private string path;
        private Metadata metadata;
        private Dictionary<string, IFileEntity> fileEntities;
        public Directory(string name, string path, string owner)
        {
            this.name = name;
            this.path = path;
            this.metadata = new Metadata()
            {
                createdOn = DateTime.Now.ToString(),
                lastModifiedOn = DateTime.Now.ToString(),
                owner = owner,
                fileEntityType = FileEntityType.Directory,
            };
            this.fileEntities = new Dictionary<string, IFileEntity>();
        }
        public IFileEntity CreateClone(string name)
        {
            Directory clone = new Directory(name, this.path, this.GetMetadata().owner);
            return clone;
        }

        public string GetPath()
        {
            return this.path;
        }

        public Metadata GetMetadata()
        {
            return this.metadata;
        }

        public string GetName()
        {
            return this.name;
        }

        public bool AddFileEntity(IFileEntity entity)
        {
            return this.fileEntities.TryAdd(entity.GetName(), entity);
        }

        public List<IFileEntity> ListFileEntities()
        {
            return this.fileEntities.Values.ToList();
        }

        public bool RemoveFileEntity(IFileEntity entity)
        {
            return this.fileEntities.Remove(entity.GetName());
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetPath(string path)
        {
            this.path = path;
        }

        public bool HasFileEntity(string name) {
            return this.fileEntities.ContainsKey(name);
        }
        public IFileEntity GetFileEntity(string name) {  
            return this.fileEntities[name]; 
        }
    }
}
