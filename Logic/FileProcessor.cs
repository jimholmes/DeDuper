using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Logic
{
    public class FileProcessor
    {
        private const string PREFERRED_PREFIX = "^\\d\\d\\s+-.*";
        private const string COPY_SUFFIX = " - Copy";
        private const int FILEEXT_LEN = 4;
        private readonly IList<string> files;

        public FileProcessor()
        {
            files = new List<string>();
            DeleteList = new List<string>();
        }

        public FileProcessor(IList<string> files)
        {
            this.files = new List<string>(files);
            DeleteList = new List<string>();
        }

        public IList<string> DeleteList { get; private set; }

        public void CreateDeletionList()
        {
            HandleDupes();
        }

        private void HandleDupes()
        {
            var moreSongsLeft = true;

            for (var songPrefix = 1; moreSongsLeft; songPrefix++)
            {
                var songsWithSameNumericPrefix = SongsWithDigitsAsPrefix(songPrefix);
                if (songsWithSameNumericPrefix.Count() == 0)
                {
                    break;
                }
                if (songsWithSameNumericPrefix.Count() > 1)
                {
                    ResolveNumericDupes(songsWithSameNumericPrefix);
                }
                ResolveNonNumericDupes(songsWithSameNumericPrefix);
            }
            ResolveCopyOfDupes();
            ResolveParensNumberDupes();
        }

        //Removes dupes which are in form '01 - foo(1).mp3'
        private void ResolveParensNumberDupes()
        {
            var parenFiles = GetParensNumFileList();
            if (parenFiles.Count()==0) { return;}
            else{
                IList<string> workingList = new List<string>();
                foreach (var parenFile in parenFiles)
                {
                    var fileRoot = GetRootOfParensFile(parenFile);
                    Regex targetName = new Regex(fileRoot + "\\.[a-zA-Z0-9]{3}");
                    foreach (var file in files)
                    {
                        var m = targetName.Match(file);
                        if (m.Success)
                        {
                            workingList.Add(parenFile);
                        }
                    }
                }
                UpdateLists(workingList);
            }
        }

        public string GetRootOfParensFile(string file)
        {
            var position = file.LastIndexOf("(");
            return  file.Substring(0, position);
        }

        public IEnumerable<string> GetParensNumFileList()
        {
            Regex parenNumRegex = new Regex("\\(\\d+\\)");

            var parensFiles = from file in files
                let matches = parenNumRegex.Match(file)
                where matches.Length > 0
                select file;

            return parensFiles;
        }

        private void ResolveCopyOfDupes()
        {
            IList<string> working = new List<string>();
            var songsWithCopy = files.Where(s => s.Contains(COPY_SUFFIX));
            foreach (var song in songsWithCopy)
            {
                if (song.Contains(COPY_SUFFIX))
                {
                    var songRoot = song.Substring(0, song.Length - (COPY_SUFFIX.Length + FILEEXT_LEN));

                    var similarSongs = files.Where(s => s.Contains(songRoot));
                    if (similarSongs.Count() == 2 )
                    {
                        working.Add(song);
                    }
                }
            }
            UpdateLists(working);
        }


        private void ResolveNonNumericDupes(IEnumerable<string> songsWithSameNumericPrefix)
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


        private void ResolveNumericDupes(IEnumerable<string> songsWithSameNumericPrefix)
        {
            IList<string> stuffToDelete = new List<string>();

            var preferredPrefix = new Regex(PREFERRED_PREFIX);
            foreach (var song in songsWithSameNumericPrefix)
            {
                if (song.Contains(" - Copy"))
                {
                    stuffToDelete.Add(song);
                }
                if (!preferredPrefix.IsMatch(song))
                {
                    stuffToDelete.Add(song);
                }
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
        private IEnumerable<string> SongsWithDigitsAsPrefix(int songPrefix)
        {
            return files.Where(s => s.StartsWith(songPrefix.ToString("D2")));
        }

    }
}