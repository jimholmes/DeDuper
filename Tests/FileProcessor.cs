using System.Collections.Generic;

namespace Tests
{
    public class FileProcessor
    {
        private IList<string> files;
        private string deleteList;

        public string DeleteList
        {
            get { return deleteList; }
        }

        public FileProcessor(IList<string> files)
        {
            this.files = files;
        }

        internal void CreateDeletionList()
        {
            deleteList = "01 - foo.mp3";

        }
    }
}