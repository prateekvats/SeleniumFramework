using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumFramework.FrameworkGears;
using SeleniumFramework.FrameworkGears.AddOns;
using SeleniumFramework.Helpers;


namespace SeleniumFramework.TestCases
{
    
    public class Htmldiffdemo:SeleniumTestCase
    {
        
        public override void RunCase()
        {
            string html1 = File.ReadAllText(@"C:\HTMLDiff\html1.html");
            string html2 = File.ReadAllText(@"C:\HTMLDiff\html1.html");

        }
    }
}
