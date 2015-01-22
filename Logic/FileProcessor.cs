using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Logic
{
    public class FileProcessor
    {
        private const string PREFERRED_PREFIX = "^\\d\\d\\s+-.*";
        private IList<string> files;

        public FileProcessor()
        {
            files = new List<string>();
            DeleteList = new List<string>();
        }

        public FileProcessor(IList<string> files)
        {
            this.files = files;
            DeleteList = new List<string>();
        }

        public IList<string> DeleteList { get; private set; }

        public void CreateDeletionList()
        {
            HandleDupes();
        }


        private void HandleDupes()
        {
            bool moreSongsLeft = true;
           
            for (int songPrefix = 1; moreSongsLeft; songPrefix++)
            {
                var songsMatchingPrefix = 
                    files.Where(s => s.StartsWith(songPrefix.ToString("D2")));
                if (songsMatchingPrefix.Count() == 0)
                {
                    break;
                }
                if (songsMatchingPrefix.Count() > 1)
                {
                    DeleteNumericDupesFromDirectoryListAndAddToDeleteList(songsMatchingPrefix);
                }
                DeleteNonNumericDupesFromDirectoryListAndAddToDeleteList(songsMatchingPrefix);
            }
        }

        private void DeleteNonNumericDupesFromDirectoryListAndAddToDeleteList(IEnumerable<string> songsMatchingPrefix)
        {
            var songsMatchingWithoutPrefix =
                files.Where(s => s.StartsWith(
                    songsMatchingPrefix.First().Substring(5)
                    ));
            foreach (var song in songsMatchingWithoutPrefix)
            {
                DeleteList.Add(song);
            }
        }

        private void DeleteNumericDupesFromDirectoryListAndAddToDeleteList(IEnumerable<string> songsMatchingPrefix)
        {
            Regex preferredPrefix = new Regex(PREFERRED_PREFIX);
            foreach (var song in songsMatchingPrefix)
            {
                if (! preferredPrefix.IsMatch(song))
                {
                    DeleteList.Add(song);
                }
            }
        }
    }
}