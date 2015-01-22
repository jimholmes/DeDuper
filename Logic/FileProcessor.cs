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
                var songsWithSameNumericPrefix = SongsWithDigitsAsPrefix(songPrefix);
                if (songsWithSameNumericPrefix.Count() == 0)
                {
                    break;
                }
                if (songsWithSameNumericPrefix.Count() > 1)
                {
                    IdentifyNumericDupes(songsWithSameNumericPrefix);
                }
                IdentifyNonNumericDupes(songsWithSameNumericPrefix);
            }
        }

        private IEnumerable<string> SongsWithDigitsAsPrefix(int songPrefix)
        {
            return files.Where(s => s.StartsWith(songPrefix.ToString("D2")));
        }

        private void IdentifyNonNumericDupes(IEnumerable<string> songsWithSameNumericPrefix)
        {
            IList<string> stuffToDelete = new List<string>();
            var songsMatchingWithoutPrefix =
                files.Where(s => s.StartsWith(
                    songsWithSameNumericPrefix.First().Substring(5)
                    ));
            foreach (var song in songsMatchingWithoutPrefix)
            {
                stuffToDelete.Add(song);
            }

            UpdateLists(stuffToDelete);
        }

        private void UpdateLists(IList<string> stuffToDelete)
        {
            foreach (var song in stuffToDelete)
            {
                DeleteList.Add(song);
                files.Remove(song);
            }
        }

        private void IdentifyNumericDupes(IEnumerable<string> songsWithSameNumericPrefix)
        {
            IList<string> stuffToDelete = new List<string>();

            Regex preferredPrefix = new Regex(PREFERRED_PREFIX);
            foreach (var song in songsWithSameNumericPrefix)
            {
                if (song.Contains(" - Copy"))
                {
                    stuffToDelete.Add(song);
                }
                if (! preferredPrefix.IsMatch(song))
                {
                    stuffToDelete.Add(song);
                }
            }
            UpdateLists(stuffToDelete);
        }
    }
}