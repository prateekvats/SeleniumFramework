using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumFramework.FrameworkGears;


namespace SeleniumFramework.TestCases
{
    
    public class ExampleTestCase1:SeleniumTestCase
    {
        public class NewUserDetails
        {
            public String FirstName
            {
                get; set;
            }
            public String LastName
            {
                get; set;
            }
            public String UserName
            {
                get; set;
            }
            public String Password
            {
                get; set;
            }
            public int BirthMonth
            {
                get; set;
            }
            public String BirthDate
            {
                get; set;
            }
            public String BirthYear
            {
                get; set;
            }
            public String Gender
            {
                get; set;
            }
            public String Phone
            {
                get; set;
            }
            public String CurrentEmail
            {
                get; set;
            }
            public String Location
            {
                get; set;
            }

        }
        public override void RunCase()
        {
            GoTo.Url("https://accounts.google.com/SignUp?service=mail&continue=https%3A%2F%2Fmail.google.com%2Fmail%2F&ltmpl=default");
            FillDetails();
        }

        public void FillDetails()
        {
            NewUserDetails u = new NewUserDetails();
            u.FirstName = "Random";
            u.LastName = "Person";
            u.UserName = "rp" + DateTime.Now.Millisecond;
            u.CurrentEmail = "ithril12@gmail.com";
            u.Password = "NewAutomatedUser321!";
            u.BirthMonth = 1;
            u.BirthDate = "01";
            u.BirthYear = "1980";
            u.Phone = "+1-432-132-4552";


            DOM.Find("#FirstName").SendKeys(u.FirstName);
            DOM.Find("#LastName").SendKeys(u.LastName);
            DOM.Find("#GmailAddress").SendKeys(u.UserName);
            DOM.Find("#Passwd").SendKeys(u.Password);
            DOM.Find("#PasswdAgain").SendKeys(u.Password);
            DOM.Find("#BirthMonth").SendKeys(u.BirthMonth.ToString());
            DOM.Find("#BirthDay").SendKeys(u.BirthDate);
            DOM.Find("#BirthYear").SendKeys(u.BirthYear);
            DOM.Find("#RecoveryEmailAddress").SendKeys(u.CurrentEmail);
            DOM.Find("#submitbutton").Click();

        }
    }
}
