using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumFramework.FrameworkGears.SeleniumSupport.CustomSeleniumExceptions
{
    public class ElementNotVisible:ElementNotVisibleException
    {

        public override string Message
        {
            get { return "The element you were trying to find is not visible."; }
        }
    }
}
