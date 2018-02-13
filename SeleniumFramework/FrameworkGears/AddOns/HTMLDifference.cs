using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlDiff = SeleniumFramework.Helpers.HtmlDiff;

namespace SeleniumFramework.FrameworkGears.AddOns
{
    public static class HTMLDifference
    {
        public static string SaveDifferenceOutput(string htmlString1, string htmlString2,string OutputFileLocation=null)
        {

            HtmlDiff difference = new HtmlDiff(htmlString1, htmlString2);
            if (String.IsNullOrEmpty(OutputFileLocation)) 
            { 
                OutputFileLocation = Directory.GetParent(Environment.CurrentDirectory.ToString()).ToString();
                OutputFileLocation = Path.Combine(OutputFileLocation, "HTMLDifferenceOutput");
            }

            if (!Directory.Exists(OutputFileLocation))
                Directory.CreateDirectory(OutputFileLocation);

            OutputFileLocation = Path.Combine(OutputFileLocation, "HTMLDifferenceOutput.html");
            string diff = difference.Build();
            File.WriteAllText(diff,OutputFileLocation);
            return OutputFileLocation;

        }

    }
}
