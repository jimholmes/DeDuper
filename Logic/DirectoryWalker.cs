using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class DirectoryWalker
    {
        private string startPath;

        public DirectoryWalker(string startPath)
        {
            this.startPath = startPath;
        }

        public void ProcessDirectoryTree()
        {
            DirectoryInfo rootFolder = new DirectoryInfo(startPath);
            ProcessFiles(rootFolder);
        }

        private void ProcessFiles(DirectoryInfo currentFolder)
        {
            var fileList = currentFolder.GetFiles("*.*");
            string[] names = fileList.Select(file => file.FullName).ToArray();

            FileProcessor proc = new FileProcessor(names);
            proc.CreateDeletionList();
            var filesToDelete = proc.DeleteList;

        }
    }
}
