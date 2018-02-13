using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginConfiguration.SeleniumFramework
{
    public class ProductionConfiguration:SeleniumConfiguration
    {

        public ProductionConfiguration()
        {
            Username = "yourusername";
            Password = "yourpassword";
            BaseURL = "https://www.gmail.com";
        }


    }
}
