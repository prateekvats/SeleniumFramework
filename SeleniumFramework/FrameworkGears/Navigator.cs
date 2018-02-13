using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Remote;
using SeleniumFramework.FrameworkGears.SeleniumSupport;

namespace SeleniumFramework.FrameworkGears
{
    public class Navigator
    {

        private readonly RemoteWebDriver _driver;
        private readonly SeleniumContext _seleniumContext;

        public Navigator(RemoteWebDriver driver, SeleniumContext seleniumContext)
        {
            _driver = driver;
            _seleniumContext = seleniumContext;
        }

        public void Refresh()
        {
            _seleniumContext.LogStep("Refreshing Page...");
            _driver.Navigate().Refresh();
            _seleniumContext.LogStep("Page refreshed");
        }

        public void Url(string url)
        {
            _seleniumContext.LogStep("Going to url " + url);
            _driver.Navigate().GoToUrl(url);
            _seleniumContext.LogStep("Page loaded");
        }
    }
}
