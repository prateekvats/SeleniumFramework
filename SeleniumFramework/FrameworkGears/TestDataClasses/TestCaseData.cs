using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginConfiguration.SeleniumFramework;
using NUnit.Core;
using SeleniumFramework.FrameworkGears.DataCommunication;

namespace SeleniumFramework.FrameworkGears.TestDataClasses
{
    public class TestCaseData
    {
        public string RunID { get; set; }
        public string TestCaseName { get; set; }
        public bool TestCaseStatus { get; set; }
        public string Error { get; set; }

        public List<TestStepData> StepList;
        private DBOperations DBCalls;

        public TestCaseData(string runid)
        {
            RunID = runid;
            if(SeleniumConfiguration.DBLogging)
            DBCalls = new DBOperations();
            StepList=new List<TestStepData>();
        }

        public string RegisterTestCase()
        {
           return DBCalls.RunInsertQuery("INSERT INTO TestCaseTable (RunName) Values('" + TestCaseName + "');");
        }

        public void UpdateTestCaseStatus(string TestCaseID)
        {
            if (TestCaseStatus)
                DBCalls.RunUpdateQuery("UPDATE TestCaseTable SET Status='Pass' where TestCaseID='"+TestCaseID+"';");
            else
                DBCalls.RunUpdateQuery("UPDATE TestCaseTable SET Status='Fail',ErrorMessage='" + Error + "' where TestCaseID='" + TestCaseID + "';");
            
        }

        public void AddTestCaseSteps(string TestCaseID)
        {
            string log = "";
            foreach (var stepdata in StepList)
            {
                log = String.Join("\n", stepdata.Log);
                DBCalls.RunInsertQuery("INSERT INTO TestStepTable (TestCaseID,StepDescription,ImageLocation,StepLog,Error,TimeStamp) Values('" +
                                       TestCaseID + "','" + stepdata.Description + "','"+stepdata.ImageLocation+"','"+log+"','"+stepdata.Error+"','"+stepdata.Timestamp.ToString()+"');");
            }
        }
    }
}
