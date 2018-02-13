using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginConfiguration.SeleniumFramework
{
    public class StagingConfiguration:SeleniumConfiguration
    {

        public StagingConfiguration()
        {
            Username = "yourusername";
            Password = "yourpassword";
            LoginURL = "https://www.mysite.com";
            BaseURL = "https://www.mysite.com";
            ProxyAuthentication = true;
            ProxyUsername = "prateekvats";
            ProxyPassword = "proxypass";
        }


    }
}
