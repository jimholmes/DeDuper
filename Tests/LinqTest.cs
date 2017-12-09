using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    internal class LinqTest
    {
        private IList<string> items;

        [SetUp]
        public void test_setup()
        {
            items = new List<string>();
            items.Add("food");
            items.Add("foo");
            items.Add("bar");
            items.Add("gwatz");
        }

        
        [Test]
        public void works()
        {
            var stuff = items.Where(f => f.StartsWith("foo"));
            IList<string> list = stuff.ToArray();

            foreach (var thing in list)
            {
                items.Remove(thing);
            }

            CollectionAssert.DoesNotContain(items, "food");
            CollectionAssert.DoesNotContain(items, "foo");
        }
    }
}