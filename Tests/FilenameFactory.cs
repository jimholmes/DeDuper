using System.Collections.Generic;

namespace Tests
{
    public class FilenameFactory
    {
        internal static IList<string> OneNumericPrefixDupe()
        {
            List<string> simple = new List<string>();
            simple.Add("01 - foo.mp3");
            simple.Add("01 foo.mp3");
            return simple;
        }
    }
}