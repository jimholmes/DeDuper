﻿using System.Collections;
using System.Collections.Generic;
using Logic;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Tests
{
    [Binding]
    public class When_Working_With_Multiple_DuplicatesSteps
    {
        private FileProcessor proc;
        private IList<string> files; 

        public When_Working_With_Multiple_DuplicatesSteps()
        {
           proc = new FileProcessor();
            files = new List<string>();
        }

        [Given(@"The list has '(.*)'")]
        public void GivenTheListHasAnd(string songname)
        {
            files.Add(songname);
        }

        [When(@"I select files for deletion")]
        public void WhenISelectFilesForDeletion()
        {
            proc = new FileProcessor(files);
            proc.CreateDeletionList();
        }

        [Then(@"the result should contain '(.*)'")]
        public void ThenTheResultShouldContain(string result)
        {
            CollectionAssert.Contains(proc.DeleteList, result);
        }

        [Then(@"the result should not contain '(.*)'")]
        public void ThenTheResultShouldNotContain(string result)
        {
            CollectionAssert.DoesNotContain(proc.DeleteList, result);
        }

        [Then(@"the result should contain nothing")]
        public void ThenTheResultShouldContainNothing()
        {
            CollectionAssert.IsEmpty(proc.DeleteList);
        }

        [Then(@"No exception should be generated")]
        public void ThenNoExceptionShouldBeGenerated()
        {
           Assert.DoesNotThrow(() => proc.CreateDeletionList());
        }


    }
}