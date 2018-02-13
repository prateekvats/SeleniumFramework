using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace LoginConfiguration.SeleniumFramework
{
    public class SeleniumConfiguration
    {
        static SeleniumConfiguration()
        {
            Current=new ProductionConfiguration();
        }

        public const int ElementWaitTimeout = 100;
        public const int ElementFindTimeout = 20;
        public const bool DBLogging = false;

        public string Username { get; set; }
        public string Password { get; set; }
        public string BaseURL { get; set; }
        public string LoginURL { get; set; }
        public bool ProxyAuthentication { get; set; }
        public string ProxyUsername { get; set; }
        public string ProxyPassword { get; set; }
        public static SeleniumConfiguration Current { get; set; }
       
        public string Name
        {
            get
            {
                return GetType().FullName.Replace("Configuration", "");
            }
        }


    }
}
