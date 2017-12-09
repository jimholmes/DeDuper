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
    class FileProcessorTests
    {
        private IList<string> files;
        private FileProcessor proc;
        [OneTimeSetUp]
        public void Fixture_Setup()
        {
            files = new List<string>()
            {
                "01 foo.bar",
                "foo.bar",
                "foo(1).bar",
                "01 - food.bar",
                "food.bar",
                "food(2).bar",
                "food(blah).bar",
            };

            proc = new FileProcessor(files);
        }
        [Test]
        public void build_list_of_parens_filenames()
        {
            var list = proc.GetParensNumFileList();
            Assert.AreEqual(2, list.Count());
            CollectionAssert.Contains(list, "foo(1).bar");
            CollectionAssert.Contains(list, "food(2).bar");
        }

        [Test]
        public void can_extract_root_of_parens_file()
        {
            var root = proc.GetRootOfParensFile("foo(1).bar");
            Assert.AreEqual("foo", root);
        }
    }
}
