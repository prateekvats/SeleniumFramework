using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using LoginConfiguration.SeleniumFramework;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using SeleniumFramework.FrameworkGears.DataCommunication;
using SeleniumFramework.TestCases;

namespace SeleniumFramework.FrameworkGears.SeleniumSupport
{
    public class SeleniumTest:IDisposable
    {
        private ChromeDriver _driver;
        public static string RunID ;
        public DateTime StartTime;
        public static bool Status;

        private SeleniumContext Context;

        protected SeleniumTest()
        {
            //Setup();
        }
        [TestFixtureSetUp]
        public void Setup()
        {
            string _executionPath = Environment.CurrentDirectory;

            string _rootPath = Path.GetFullPath(Path.Combine(_executionPath, @"..\.."));

            _driver=new ChromeDriver(Path.Combine(_rootPath,@"Packages\"));
            //_driver.Manage().Window.Size = new Size(1500, 800);

            Context = SeleniumContext.CreateContext(_driver);
            StartTime = DateTime.Now;

            if(SeleniumConfiguration.DBLogging)
            RunID = Context.RegisterTest(GetType().Name,StartTime.ToString());
            else
            RunID = "1";

            Status = true;

            /*
            try
            {
                var Authenticate = new Authentication();
                Authenticate.BaseUrl = SeleniumConfiguration.Current.BaseURL;
                Authenticate.LoginUrl = SeleniumConfiguration.Current.LoginURL;
                Authenticate.UserName = SeleniumConfiguration.Current.Username;
                Authenticate.Password = SeleniumConfiguration.Current.Password;
                Authenticate.ProxyUserName = SeleniumConfiguration.Current.ProxyUsername;
                Authenticate.ProxyPassword = SeleniumConfiguration.Current.ProxyPassword;

                Authenticate.RunCase();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Authentication Failed.");
            }
            */
        }

        //Function to clear out all the files in the default download location
        private static void ClearDownloads()
        {
            string[] filePaths = Directory.GetFiles(@"C:\Temp\SeleniumDownloads\");
            foreach (string filePath in filePaths)
            {
                File.Delete(filePath);
            }
        }

        //Setting all the chrome options here
        private static ChromeOptions SetChromeOptions()
        {
            var chromeOptions = new ChromeOptions();

            var downloadPath = @"C:\Temp\SeleniumDownloads\";
            if (!Directory.Exists(downloadPath))
            {
                Directory.CreateDirectory(downloadPath);
            }

            //Chrome option to set up download directory path
            chromeOptions.AddUserProfilePreference("download.default_directory", downloadPath);
            chromeOptions.AddUserProfilePreference("intl.accept_languages", "nl");
            chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");

            //Chrome option to avoid the warning popup saying are you sure you want to download multiple files in chrome
            chromeOptions.AddUserProfilePreference("profile.content_settings.pattern_pairs.*.multiple-automatic-downloads", 1);
            return chromeOptions;
        }

        private static void ExecuteAll(params Action[] actions)
        {
            var errors = new List<Exception>();
            foreach (Action action in actions)
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    errors.Add(e);
                }
            }
            if (errors.Count > 0)
            {
                throw new AggregateException(errors);
            }
        }

        public void Dispose()
        {
            try
            {
                Cleanup();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Warning: Selenium dispose failed: {0}", ex));
            }
        }
        private void Finish()
        {
            ExecuteAll(
                () =>
                {
                    UpdateTestRun();

                },
                () =>
                {
                    //clear out the temporary downloads folder
                    ClearDownloads();
                },
                () =>
                {
                    
                        //if context was not initialized, but the driver was initialized, destroy the driver
                        _driver.Quit();
                        _driver.Dispose();
                    
                });
        }

        private void UpdateTestRun()
        {
            if (SeleniumConfiguration.DBLogging)
            {
                Context.UpdateTest(RunID, Status);
                Context.UpdateTest(RunID, DateTime.Now.ToString());
                DBOperations.CloseConnection();
            }
        }

        public void Cleanup()
        {
            try
            {
                // finalize the test (flush all files)
                Finish();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Warning: Selenium cleanup failed: {0}", ex));
            }
        }
    }
}
