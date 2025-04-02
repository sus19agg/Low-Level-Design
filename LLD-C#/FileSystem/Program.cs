using FileSystem.Implementations;
using FileSystem.Interfaces;

namespace FileSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IFileSystem fs = new FileSystem.Implementations.FileSystem();
            fs.CreateDirectory("sush/1");
            fs.CreateDirectory("sush/2");
            fs.CreateDirectory("sush/2/3");
            fs.CreateFile("sush/file1.txt", "this is the first file");
            fs.CreateFile("sush/1/file2.txt", "this is the second file");
            fs.CreateFile("sush/2/file3.txt", "this is the third file");
            fs.List("sush/2");
            fs.Move("sush/2/file3.txt", "sush/2/3/file3.txt");
            fs.List("sush/2");
            fs.List("sush/2/3");
            fs.List("sush/1");
            fs.List("sush");
        }
    }
}
