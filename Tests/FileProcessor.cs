using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Tests
{
    public class FileProcessor
    {
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

        internal void CreateDeletionList()
        {
            HandleNumericDupes();
        }

        private void HandleNumericDupes()
        {
            bool moreSongsLeft = true;
           

            for (int songPrefix = 1; moreSongsLeft; songPrefix++)
            {
                var currentSongs = 
                    files.Where(s => s.StartsWith(songPrefix.ToString("D2")));
                if (currentSongs.Count() > 1)
                {
                    AddDupesToDeleteList(currentSongs);
                }
                else if (currentSongs.Count()==0)
                {
                    moreSongsLeft = false;
                }
            }
        }

        private void AddDupesToDeleteList(IEnumerable<string> currentSongs)
        {
            Regex preferredPrefix = new Regex("^\\d\\d\\s+-.*");
            foreach (var song in currentSongs)
            {
                if (! preferredPrefix.IsMatch(song))
                {
                    DeleteList.Add(song);
                }
            }
        }
    }
}