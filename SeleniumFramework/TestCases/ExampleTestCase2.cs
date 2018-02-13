using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumFramework.FrameworkGears;
using SeleniumFramework.Helpers;


namespace SeleniumFramework.TestCases
{
    
    public class ExampleTestCase2:SeleniumTestCase
    {
        
        public override void RunCase()
        {
            /*Step("Hovering over Portfolios");
            DOM.Find("#tmbTab9").HoverOver();
            Step("Clicking on create portfolio");
            DOM.Find("#__tmbFlyout9 a[href*=CreateEditPortfolio]").Click();
            Step("Waiting for the Create portfolio page to load.");
            DomElement portfolioInput = DOM.Find("#summarySection_Toggle1_portfolioName");
            Step("Create portfolio page loaded.");
            
            string portfolioname="SeleniumPortfolio_"+Utility.RandomNumber();
            Step("Entering portfolio name as "+portfolioname);
            portfolioInput.SendKeys(portfolioname);
            Step("Now clicking on Save button");
            DOM.Find("#bbBottom_mySave__saveBtn").Click();

            Step("Verifying if the portfolio got created.");

            if(!DOM.Find("#ctl27__textLabel").Text.Contains(portfolioname))
               Error("Portfolio did not get created!");*/
            String hashTagsToLike = "#hotwomen";
            String profilesFollowersToFollow = "https://www.instagram.com/thenotoriousmma/";
            GoTo.Url("https://www.instagram.com");
            
            DOM.Find("._fcn8k").Click() ;
            DOM.Find("input[name=username]").SendKeys("prateekvats2@gmail.com");
            DOM.Find("input[name=password]").SendKeys("Coolermaster24");
            DOM.Find("button._ah57t._84y62._i46jh").Click();
            //follow(profilesFollowersToFollow);
            Unfollow();


        }
        public void follow(String urlForFollowers)
        {
            GoTo.Url(urlForFollowers);
            DOM.Find("a[href*=followers]").Click();
            String st = "._4gt3b";
            String script = String.Format("var objDiv = document.getElementsByClassName(\"_4gt3b\")[0];objDiv.scrollTop = objDiv.scrollHeight;");

            for (int i = 0; i < 300; i++)
                DOM.ExecuteJavascript(script);


            List<DomElement> followButtons = DOM.FindMany("._ah57t._84y62._i46jh._rmr7s").ToList();
            Random rnd = new Random();
            int counter = 0;
            foreach (var btn in followButtons)
            {
                counter++;
                btn.Click();
                Wait(rnd.Next(10, 15) * 1000);
                Console.WriteLine("Unfollowed " + counter + " accounts.");
            }
        }
        public void Unfollow()
        {
            DOM.Find(".coreSpriteDesktopNavProfile").Click();
            DOM.Find("a[href*=following]").Click();
            String st = "._4gt3b";
            String script = String.Format("var objDiv = document.getElementsByClassName(\"_4gt3b\")[0];objDiv.scrollTop = objDiv.scrollHeight;");

            for (int i = 0; i < 50; i++)
            {
                DOM.ExecuteJavascript(script);
                Wait(1000);
            }


            List<DomElement> followButtons = DOM.FindMany("._ah57t._6y2ah._i46jh._rmr7s").ToList();
            Random rnd = new Random();
            int counter = 0;
            foreach (var btn in followButtons)
            {
                counter++;
                btn.Click();
                Wait(rnd.Next(10, 15) * 1000);
                Console.WriteLine("Unfollowed " + counter + " accounts.");
            }
            
        }
    }
}
