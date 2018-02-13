using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumFramework.FrameworkGears.SeleniumSupport;

namespace SeleniumFramework.FrameworkGears
{
    public class DomElement
    {
        private readonly SeleniumContext _context;
        private readonly RemoteWebDriver _driver;
        private readonly IWebElement _element;
        private readonly string _searchedSelector;

        public DomElement(IWebElement element, RemoteWebDriver driver, SeleniumContext context,string searchedSelector = null)
        {
            _element = element;
            _driver = driver;
            _context = context;
            _searchedSelector = searchedSelector;

        }

        public string Class
        {
            get { return _element.GetAttribute("class"); }
        }

        public string HTMLId
        {
            get { return _element.GetAttribute("id"); }
        }

        public string Tag
        {
            get { return _element.TagName; }
        }

        public int Height
        {
            get { return _element.Size.Height; }
        }

        public int Width
        {
            get { return _element.Size.Width; }
        }

        public string HTMLText
        {
            get { return _element.GetAttribute("innerHTML"); }
        }

        public int XCoordinate
        {
            get { return _element.Location.X; }
        }

        public int YCoordinate
        {
            get { return _element.Location.Y; }
        }

        public string ElementSelector
        {
            get
            {
                string res = _element.TagName;
                string id = _element.GetAttribute("id");
                if (!string.IsNullOrEmpty(id))
                {
                    res += "#" + id;
                }
                string c = _element.GetAttribute("class");
                if (!string.IsNullOrEmpty(c))
                {
                    string[] classes = c.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    res += "." + string.Join(".", classes);
                }
                return res;
            }
        }

        public string Text
        {
            get { return _element.Text; }
        }

        public string TextboxValue
        {
            get { return _element.GetAttribute("value"); }
        }

        #region public methods

        public override string ToString()
        {
            if (_searchedSelector != null)
            {
                return _searchedSelector;
            }
            IWebElement parentElem = _element.FindElement(By.XPath(".."));
            DomElement parent = ToDomElement(parentElem);
            string parentSelector = parent == null ? "" : parent.ElementSelector + " ";
            return parentSelector + ElementSelector;
        }

        public string GetAttributeValue(string attributeName)
        {
            Log("Finding Attribute:" + attributeName + " of:" + this);
            return _element.GetAttribute(attributeName);
        }

        public bool HasClass(string className)
        {
            string classAttr = _element.GetAttribute("class") ?? "";
            string[] classes = classAttr.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return classes.Contains(className.ToLower());
        }

        #endregion


        #region public Action methods
        public DomElement Click()
        {
            Log("Clicking on the element:"+this.ElementSelector);
            _element.Click();
            return this;
        }

        public DomElement DoubleClick()
        {
            Log("Double Clicking on the element:" + this.ElementSelector);
            new Actions(_driver).DoubleClick(_element).Build().Perform();
            return this;
        }

        public DomElement ClearTextbox()
        {
            Log("Clearing textbox " + this);
            while (!string.IsNullOrEmpty(_element.GetAttribute("value")))
            {
                _element.SendKeys(Keys.Backspace);
                _element.SendKeys(Keys.Delete);
            }
            return this;
        }

        public void SendKeys(string text,bool clearFirst=false)
        {
            Log("Sending keys in the element:" + this.ElementSelector);

            if (clearFirst)
                this.ClearTextbox();

            _element.SendKeys(text);
        }

        public void HoverOver()
        {
            Log("Hovering over " + this);
            new Actions(_driver).MoveToElement(_element).Build().Perform();
        }

        public DomElement ScrollUp()
        {
            Log("Scrolling up the element:" + this.ElementSelector);
            this.SendKeys(Keys.PageUp);
            return this;
        }

        public DomElement ScrollDown()
        {
            Log("Scrolling up the element:" + this.ElementSelector);
            this.SendKeys(Keys.PageDown);
            return this;
        }

        public void SelectDropdownOption(string visibleText)
        {
            Log("Selecting dropdown option '" + visibleText + "' of " + this.ElementSelector);

            SelectElement dropdown = new SelectElement(_element);
            dropdown.SelectByText(visibleText);
        }

        public void SelectDropdownOption(int index)
        {
            Log("Selecting option '" + index+1 + "' of " + this.ElementSelector);
            SelectElement dropdown = new SelectElement(_element);
            dropdown.SelectByIndex(index);
        }

        public void SelectDropdownOptionByValue(string value)
        {
            Log("Selecting dropdown option with value= '" + value + "' of " + this);
            SelectElement dropdown = new SelectElement(_element);
            dropdown.SelectByValue(value);
        }
        
        public void SendKeys(string text)
        {
            Log("Sending keys to: " + this + " - " + text);
            _element.SendKeys(text);
        }

        public void Submit()
        {
            _element.Submit();
        }
        #endregion

        #region public Element search methods

        public DomElement GetParent()
        {
            Log("Getting parent of: " + this);
            IWebElement elem = _element.FindElement(By.XPath(".."));
            return ToDomElement(elem);
        }

        public DomElement GetParent(string tag)
        {
            Log("Getting parent of: " + this);
            IWebElement parentElement = _element.FindElement(By.XPath("./parent::*"));
            do
            {
                parentElement = parentElement.FindElement(By.XPath("./parent::*")); //parent relative to current element

            } while (parentElement.TagName != tag);

            return ToDomElement(parentElement);
        }

        

        public DomElement FindChild(string selector)
        {
            return FindChild(By.CssSelector(selector));
        }

        public DomElement FindChild(By by)
        {
            Log("Looking for: " + by + " in: " + this);
            for (var i = 0; i < SeleniumExtensions.ElementFindTimeout; i++)
            {
                IWebElement elem = _element.TryGetChild(by);
                if (elem != null)
                {
                    return ToDomElement(elem);
                }
                Thread.Sleep(1000);
            }

            throw new Exception(string.Format("Couldn't find child after {0} seconds", SeleniumExtensions.ElementFindTimeout));
        }

        public DomElement WaitForChild(string selector)
        {
            return WaitForChild(By.CssSelector(selector));
        }

        public DomElement WaitForChild(By by)
        {
            Log("Looking for: " + by + " in: " + this);
            for (var i = 0; i < SeleniumExtensions.ElementWaitTimeout; i++)
            {
                IWebElement elem = _element.TryGetChild(by);
                if (elem != null)
                {
                    return ToDomElement(elem);
                }
                Thread.Sleep(1000);
            }

            throw new Exception(string.Format("Couldn't find child after {0} seconds", SeleniumExtensions.ElementFindTimeout));
        }

        public string WaitForText(int time)
        {
            Log("Waiting for the text under:" + this + " to appear.");
            for (var i = 0; i < time; i++)
            {
                if (!string.IsNullOrEmpty(Text) || !string.IsNullOrEmpty(TextboxValue))
                {
                    return Text;
                    break;
                }
                Thread.Sleep(500);
            }
            return string.Empty;
        }

        public DomElement TryFindChild(string selector)
        {
            Log("Checking whether " + selector + " exists in: " + this);
            IWebElement elem = _element.TryGetVisibleElement(By.CssSelector(selector));
            return ToDomElement(elem, selector);
        }

        public DomElement TryFindChildXpath(string xpathSelector)
        {
            Log("Checking whether " + xpathSelector + " (xpath) exists in: " + this);
            IWebElement elem = _element.TryGetVisibleElement(By.XPath(xpathSelector));
            return ToDomElement(elem, xpathSelector);
        }

        #endregion


        private void Log(string entry)
        {
            _context.Log(entry);
        }

        private DomElement ToDomElement(IWebElement el, string searchedSelector = null)
        {
            return el == null ? null : new DomElement(el, _driver, _context, searchedSelector);
        }
    }
}
