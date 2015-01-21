using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Tests
{
    [Binding]
    public class When_Working_With_Multiple_DuplicatesSteps
    {
        private FileProcessor proc;

        public When_Working_With_Multiple_DuplicatesSteps()
        {
           
        }

        [Given(@"The list has '(.*)' and '(.*)'")]
        public void GivenTheListHasAnd(string p0, string p1)
        {
            IList<string> files = FilenameFactory.OneNumericPrefixDupe();
            proc = new FileProcessor(files);
        }

        [When(@"I select files for deletion")]
        public void WhenISelectFilesForDeletion()
        {
            proc.CreateDeletionList();
        }

        [Then(@"the result should contain '(.*)'")]
        public void ThenTheResultShouldContain(string result)
        {
            Assert.AreEqual(result, proc.DeleteList);
        }

        [Given(@"The list has '(.*)', '(.*)', '(.*)', and '(.*)'")]
        public void GivenTheListHasAnd(string p0, string p1, string p2, string p3)
        {
            ScenarioContext.Current.Pending();
        }

    }
}