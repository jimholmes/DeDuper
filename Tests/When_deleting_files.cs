using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    class When_deleting_files
    {
        [Test]
        public void driver()
        {
            var path = @"D:\Music\Amazon MP3\Bad Company\10 From 6";
            DirectoryWalker walker = new DirectoryWalker(path);
            walker.ProcessDirectoryTree();
        }
    }
}
