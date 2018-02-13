using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumFramework.FrameworkGears;
using SeleniumFramework.FrameworkGears.SeleniumSupport;


namespace SeleniumFramework.TestCases
{
    
    public class Authentication:SeleniumTestCase
    {

        public string BaseUrl { get; set; }

        public string LoginUrl { get; set; }

        public bool ProxyEnabled { get; set; }

        public string Password { get; set; }

        public string ProxyPassword { get; set; }

        public string ProxyUserName { get; set; }

        public string UserName { get; set; }

        public override void RunCase()
        {
            
            

        }

       
    }
}
