using FileSystem.Enums;
using FileSystem.Interfaces;
using FileSystem.Models;

namespace FileSystem.Implementations
{
    internal class File : IFileEntity, IFIleOperations
    {
        private string name;
        private string path;
        private string content;
        private Metadata metadata;
        public File(string name, string path, string owner) 
        { 
            this.name = name;
            this.path = path;
            this.content = null;
            this.metadata = new Metadata()
            {
                createdOn = DateTime.Now.ToString(),
                lastModifiedOn = DateTime.Now.ToString(),
                owner = owner,
                fileEntityType = FileEntityType.File,
            };
        }
        public IFileEntity CreateClone(string name)
        {
            File clone = new File(name, this.path, this.GetMetadata().owner);
            clone.WriteContent(this.content);
            return clone;
        }

        public string GetContent()
        {
            return this.content;
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

        public void WriteContent(string content)
        {
            this.content = content;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetPath(string path)
        {
            this.path = path;
        }
    }
}
