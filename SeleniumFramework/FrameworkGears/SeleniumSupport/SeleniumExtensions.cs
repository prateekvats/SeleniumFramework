using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginConfiguration.SeleniumFramework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumFramework.FrameworkGears
{
    public static class SeleniumExtensions
    {
        public const int ElementWaitTimeout = SeleniumConfiguration.ElementWaitTimeout;
        public const int ElementFindTimeout = SeleniumConfiguration.ElementFindTimeout;

        public static IWebElement WaitForElement(this IWebDriver d, string selector, int? timeout = null)
        {
            return d.WaitForElement(By.CssSelector(selector), timeout);
        }

        public static IWebElement WaitForElement(this IWebDriver d, By by, int? timeout = null)
        {
            int seconds = timeout.HasValue ? timeout.Value : ElementWaitTimeout;
            return new WebDriverWait(d, TimeSpan.FromSeconds(seconds)).Until(dr => dr.TryGetVisibleElement(by));
        }

        public static IEnumerable<IWebElement> FindManyByCssSelector(this IWebDriver d, string selector)
        {
            return d.FindElements(By.CssSelector(selector));
        }

        public static IWebElement TryGetVisibleElement(this ISearchContext searchContext, By by)
        {
            try
            {
                ReadOnlyCollection<IWebElement> e = searchContext.FindElements(by);
                if (e == null)
                {
                    return null;
                }

                return e.FirstOrDefault(d => d.Displayed);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IWebElement TryGetChild(this IWebElement element, By by)
        {
            try
            {
                return element.FindElement(by);
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
