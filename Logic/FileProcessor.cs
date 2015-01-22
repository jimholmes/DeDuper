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
                var songsWithSameNumericPrefix = 
                    files.Where(s => s.StartsWith(songPrefix.ToString("D2")));
                if (songsWithSameNumericPrefix.Count() == 0)
                {
                    break;
                }
                if (songsWithSameNumericPrefix.Count() > 1)
                {
                    HandleNumericDupes(songsWithSameNumericPrefix);
                }
                HandleNonNumericDupes(songsWithSameNumericPrefix);
            }
        }

        private void HandleNonNumericDupes(IEnumerable<string> songsWithSameNumericPrefix)
        {
            var songsMatchingWithoutPrefix =
                files.Where(s => s.StartsWith(
                    songsWithSameNumericPrefix.First().Substring(5)
                    ));
            foreach (var song in songsMatchingWithoutPrefix)
            {
                DeleteList.Add(song);
            }
        }

        private void HandleNumericDupes(IEnumerable<string> songsWithSameNumericPrefix)
        {
            Regex preferredPrefix = new Regex(PREFERRED_PREFIX);
            foreach (var song in songsWithSameNumericPrefix)
            {
                if (song.Contains(" - Copy"))
                {
                    DeleteList.Add(song);
                }
                if (! preferredPrefix.IsMatch(song))
                {
                    DeleteList.Add(song);
                }
            }
        }
    }
}