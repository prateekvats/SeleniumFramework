using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Web;

namespace SeleniumFramework.FrameworkGears.SeleniumSupport
{
    public class AutoItWrapper
    {

        private static readonly string _binaryPath = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory.ToString()).ToString()).ToString()+"\\Packages\\AutoIt3.exe";
       

        public static void EnterProxyCredentials(string expectedUrl, string username, string password)
        {
            var ub = new UriBuilder(expectedUrl);
            if (ub.Scheme == "http" && ub.Port == 80 || ub.Scheme == "https" && ub.Port == 443)
            {
                ub.Port = -1; //default port
            }

            expectedUrl = HttpUtility.UrlDecode(ub.ToString().Replace("http://", ""));
            string script = string.Format(
                @" WinWaitActive(""{0} - Google Chrome"","""", 20)
 Send(""{1}{{TAB}}"")
 Send(""{2}{{Enter}}"")
",
                expectedUrl,
                username,
                password);
            RunScript(script);
        }

        public static void RunScript(string script)
        {
            string scriptPath = Path.GetTempFileName();
            File.WriteAllText(scriptPath, script);
            Process.Start(_binaryPath, '"' + scriptPath + '"').WaitForExit();
            File.Delete(scriptPath);
        }

    }
}
