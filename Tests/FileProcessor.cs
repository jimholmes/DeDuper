using System.Collections.Generic;

namespace Tests
{
    public class FileProcessor
    {
        private IList<string> files;
        private IList<string>deleteList;

        public IList<string> DeleteList
        {
            get { return deleteList; }
        }

        public FileProcessor()
        {
            files = new List<string>();
            deleteList = new List<string>();
        }

        public FileProcessor(IList<string> files)
        {
            this.files = files;
            deleteList = new List<string>();
        }

        internal void CreateDeletionList()
        {
            deleteList.Add("01 - foo.mp3");

        }
    }
}