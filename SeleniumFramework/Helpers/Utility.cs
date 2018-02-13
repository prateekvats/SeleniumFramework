using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFramework.Helpers
{
    public static class Utility
    {
        public static string RandomNumber()
        {
            return new Random().Next(0, int.MaxValue).ToString();
        }

        public static string RemoveUnsafeChars(string fileName)
        {
            return new string(fileName.Select(c => char.IsLetterOrDigit(c) || c == '.' || c == '-' ? c : '_').ToArray());
        }

        public static string GetStepImageLocation()
        {
            return @"\\filesvr3\Photo & Party Pics\TestStepImages";
        }

       
    }
}
