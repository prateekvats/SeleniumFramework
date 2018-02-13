using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LoginConfiguration.SeleniumFramework;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using SeleniumFramework.FrameworkGears.SeleniumSupport;
using SeleniumFramework.FrameworkGears.SeleniumSupport.CustomSeleniumExceptions;
using SeleniumFramework.FrameworkGears.TestDataClasses;
using TestCaseData = SeleniumFramework.FrameworkGears.TestDataClasses.TestCaseData;

namespace SeleniumFramework.FrameworkGears
{
    [SetUpFixture]
    public abstract class SeleniumTestCase
    {
        private TestCaseData CaseData;
        private ChromeDriver _driver;
        public string TestCaseName { get; set; }
        private string TestCaseID;

        public SeleniumContext Context
        {
            get { return SeleniumContext.Current; }
        }

        public DomWrapper DOM
        {
            get
            {
                return Context.DOM;
            }
        }

        public Navigator GoTo
        {
            get
            {
                return Context.GoTo;
            }
        }
        protected SeleniumTestCase()
        {
            
        }

        public abstract void RunCase();

        [SetUp]
        public void TestStarting()
        {
            CaseData = new TestCaseData(SeleniumTest.RunID);

            if (SeleniumConfiguration.DBLogging)
            {
                string testcasename = (String.IsNullOrEmpty(TestCaseName) ? GetType().ToString() : TestCaseName);
                TestCaseID = Context.RegisterCase(testcasename, SeleniumTest.RunID);
                SeleniumContext.CurrentTestCaseID = TestCaseID;
            }

            Console.WriteLine("\n" + TestCaseName + " has started.");

            MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
            Assert.Throws(typeof(Exception),delegate
            {
                method.Invoke(null,null);
            });

        }

        [TearDown]
        public void TestEnding()
        {
            string TestStatus=TestContext.CurrentContext.Result.Status.ToString();
            string error = TestContext.CurrentContext.Result.State.ToString();

            //Update the log for the last step 
            CaseData.StepList.Last().Log = Context.StepLog;

            if (SeleniumConfiguration.DBLogging)
            {
                CaseData.UpdateTestCaseStatus(TestCaseID);
                CaseData.AddTestCaseSteps(TestCaseID);
            }

            
            

        }

        public void Run()
        {
           
            try
            {
                RunCase();
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                CaseData.TestCaseStatus = true;
                Console.WriteLine("\nTestcase:"+TestCaseName+" PASSED. \n");
                
            }
            catch (Exception e)
            {
                CaseData.TestCaseStatus = false;
                SeleniumTest.Status = false;
                CaseData.Error = e.Message;
                CaseData.StepList.Last().IsSuccessful = false;
                CaseData.StepList.Last().Error = e.Message;
                Assert.Fail(e.Message);
                Console.WriteLine("\n" + TestCaseName + " failed due to the following error/exception:"+e.Message+"\n");
                
            }

            
        }

        public void Step(string description)
        {
            TestStepData stepdata = Context.LogStep(TestCaseID, description);

            if (CaseData.StepList.Count > 0)
                CaseData.StepList.Last().Log = stepdata.Log.Select(item => (string)item.Clone()).ToList();

            CaseData.StepList.Add(stepdata);
            CaseData.StepList.Last().Log.Clear();
        }

        public void Error(string description)
        {
            throw new Exception(description);
        }

        public void Wait(int milliseconds)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(milliseconds));
        }
    }

}
