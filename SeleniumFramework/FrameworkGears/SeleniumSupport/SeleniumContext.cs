using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using SeleniumFramework.FrameworkGears.DataCommunication;
using SeleniumFramework.FrameworkGears.TestDataClasses;
using SeleniumFramework.Helpers;

namespace SeleniumFramework.FrameworkGears.SeleniumSupport
{
    public class SeleniumContext
    {
        private ChromeDriver _driver;
        public static SeleniumContext Current;

        public DomWrapper DOM { get; set; }
        public Navigator GoTo { get; set; }
        public List<string> StepLog;

        public static string CurrentTestCaseID { get; set; }

        protected SeleniumContext(ChromeDriver driver)
        {
            _driver = driver;
            DOM=new DomWrapper(_driver,this);
            GoTo=new Navigator(_driver,this);
            StepLog=new List<string>();

        }

        public TestStepData LogStep(string TestCaseID,string Description, bool TakeScreenshot = true)
        {
            string ImageName = Utility.RemoveUnsafeChars(String.Format("{0}_{1}.png", DateTime.Now.ToString(), Description));
            
            string ImageLocation="Failed To capture image.";
            if (TakeScreenshot)
            {
                try
                {
                    _driver.TakeScreenshot()
                        .SaveAsFile(Path.Combine(Utility.GetStepImageLocation(), ImageName), ImageFormat.Png);

                    ImageLocation = Path.Combine(Utility.GetStepImageLocation(), ImageName);
                }
                catch (Exception e)
                {
                    //Not something Fatal
                }
            }

            
            TestStepData StepData=new TestStepData(TestCaseID,_driver);
            StepData.Description = Description;
            StepData.TestCaseID = TestCaseID;
            StepData.ImageLocation = ImageLocation;
            StepData.Timestamp = DateTime.Now;
            StepData.Log = StepLog.Select(item => (string) item.Clone()).ToList();
            
            Console.WriteLine("\n" + StepData.Timestamp.ToString("g") + ":Step-" + StepData.Description);
            Console.WriteLine("\nScreenshot:" + StepData.ImageLocation);
            StepLog.Clear();
            return StepData;

        }

        public TestStepData LogStep(string Description, bool TakeScreenshot = true)
        {
            string ImageName = Utility.RemoveUnsafeChars(String.Format("{0}_{1}.png", DateTime.Now.ToString(), Description));

            string ImageLocation = "Failed To capture image.";
            if (TakeScreenshot)
            {
                try
                {
                    _driver.TakeScreenshot()
                        .SaveAsFile(Path.Combine(Utility.GetStepImageLocation(), ImageName), ImageFormat.Png);

                    ImageLocation = Path.Combine(Utility.GetStepImageLocation(), ImageName);
                }
                catch (Exception e)
                {
                    //Not something Fatal
                }
            }

            
            TestStepData StepData = new TestStepData(CurrentTestCaseID, _driver);
            StepData.Description = Description;
            StepData.TestCaseID = CurrentTestCaseID;
            StepData.ImageLocation = ImageLocation;
            StepData.Timestamp = DateTime.Now;
            StepData.Log = StepLog;

            
            Console.WriteLine("\n"+StepData.Timestamp.ToString("g")+":Step-"+StepData.Description);
            Console.WriteLine("\nScreenshot:"+StepData.ImageLocation);
            StepLog.Clear();
            return StepData;

        }

        public void Log(string message)
        {
            Console.WriteLine("\n"+DateTime.Now.ToString("g")+":"+message);
            StepLog.Add(message);
        }

        public string LogCase(string RunID, string testCaseName)
        {
            StepLog.Clear();
            return null;
        }

        public string RegisterTest(string RunName,string startTime)
        {
            DBOperations DBCalls=new DBOperations();
            return DBCalls.RunInsertQuery("INSERT INTO TestRunTable (RunName,Status,StartTime) Values('" + RunName + "','Pass','" + startTime + "');");
        }

        public void UpdateTest(string RunID,bool status)
        {
            DBOperations DBCalls = new DBOperations();


            if(status)
                DBCalls.RunUpdateQuery("UPDATE TestRunTable SET Status='Pass' where RunID='" + RunID + "';");
            else
                DBCalls.RunUpdateQuery("UPDATE TestRunTable SET Status='Fail' where RunID='" + RunID + "';");
        }

        public bool UpdateTest(string RunID,string endTime)
        {
            DBOperations DBCalls=new DBOperations();
            return DBCalls.RunUpdateQuery("UPDATE TestRunTable SET EndTime='"+endTime+"' where RunID='" + RunID + "';");
        }

        public string RegisterCase(string Testcasename,string Runid)
        {
            DBOperations DBCalls = new DBOperations();
            return DBCalls.RunInsertQuery("INSERT INTO TestCaseTable (TestCaseName,RunID,Status) Values('" + Testcasename + "','"+Runid+"','Pass');");
        }

        
        




       


        public static SeleniumContext CreateContext(ChromeDriver driver)
        {
            Current=new SeleniumContext(driver);
            return Current;
        }
    }
}
