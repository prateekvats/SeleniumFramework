using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumFramework.FrameworkGears.SeleniumSupport.CustomSeleniumExceptions
{
    public class ElementTimedOut:WebDriverTimeoutException
    {
        public string timeoutnumber;

        public override string Message
        {
            get
            {
                timeoutnumber = Regex.Match(Message, @"\d+").Value;
                return "The element you were trying to could not be found even after "+timeoutnumber+" seconds.";
            }
        }
    }
}
