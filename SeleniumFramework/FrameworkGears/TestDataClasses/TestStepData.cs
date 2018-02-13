using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using SeleniumFramework.Helpers;

namespace SeleniumFramework.FrameworkGears.TestDataClasses
{
    public class TestStepData
    {
        public string Description { get; set; }
        public string ImageLocation { get; set; }
        public bool IsSuccessful { get; set; }
        public string TestCaseID { get; set; }
        public string Error { get; set; }
        public DateTime Timestamp { get; set; }
        public List<string> Log; 
        private ChromeDriver _driver;

        public TestStepData(string TestcaseID,ChromeDriver driver)
        {
            TestCaseID = TestcaseID;
            _driver = driver;
            IsSuccessful = true;
            Error = "";
        }


        


    }
}
