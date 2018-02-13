using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SeleniumFramework.FrameworkGears;
using SeleniumFramework.FrameworkGears.SeleniumSupport;
using SeleniumFramework.TestCases;

namespace SeleniumFramework.TestRun
{
    [TestFixture]
    public class ExampleRun:SeleniumTest
    {
        
        [Test]
        public void ExampleTestCase()
        {
            var example = new ExampleTestCase1();
            //example.TestCaseName = "Creating a new Gmail Account.";
            example.Run();
        }

        [Ignore]
        [Test]
        public void AnotherTest()
        {
            var example = new ExampleTestCase2();
            example.TestCaseName = "Creating a new Portfolio";
            example.Run();
        }

    }
}
