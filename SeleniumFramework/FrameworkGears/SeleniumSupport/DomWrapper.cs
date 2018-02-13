using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumFramework.FrameworkGears.SeleniumSupport;

namespace SeleniumFramework.FrameworkGears
{
    
    public class DomWrapper
    {
       
        private readonly SeleniumContext _context;

        private readonly RemoteWebDriver _driver;

        public DomWrapper(RemoteWebDriver driver, SeleniumContext context)
        {
            _driver = driver;
            _context = context;
        }
        
        public DomElement Find(string selector, bool immediate = false)
        {
            Log("Looking for the element:"+selector);
            IWebElement element = immediate
                ? _driver.FindElement(By.CssSelector(selector))
                : _driver.WaitForElement(selector, SeleniumExtensions.ElementFindTimeout);
            return ToDomElement(element,selector);
        }

        public IEnumerable<DomElement> FindMany(string selector)
        {
            Log("Looking for the element:" + selector);
            IEnumerable<IWebElement> element =
                new WebDriverWait(_driver, TimeSpan.FromSeconds(SeleniumExtensions.ElementFindTimeout)).Until(
                    dr => dr.FindManyByCssSelector(selector));
            return element.Select(d => ToDomElement(d,selector));
        }


        public DomElement WaitFor(string selector)
        {
            Log("Waiting for the element:" + selector);
            IWebElement element = _driver.WaitForElement(selector);
            return ToDomElement(element, selector);
        }

        public void CloseLastTab()
        {
            IEnumerable<string> tabHandles = _driver.WindowHandles;

            Log("Switching to the tab with window handle id: " + tabHandles.Last());

            _driver.Close();
            _driver.SwitchTo().Window(tabHandles.First());
        }

        public void SwitchToLastTab()
        {
            IEnumerable<string> tabHandles = _driver.WindowHandles;

            Log("Switching to the tab with window handle id: " + tabHandles.Last());

            _driver.SwitchTo().Window(tabHandles.Last());
        }

        public string GetCurrentURL()
        {
            return _driver.Url;
        }

        public void ExecuteJavascript(String script)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            string title = (string)js.ExecuteScript(script);
        }

        private void Log(string entry)
        {
            _context.Log(entry);
        }

        
        private DomElement ToDomElement(IWebElement el, string searchedSelector)
        {
            return el == null ? null : new DomElement(el, _driver, _context, searchedSelector);
        }

    }
}
