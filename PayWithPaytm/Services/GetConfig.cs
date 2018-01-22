using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PayWithPaytm.Services
{
    public static class GetConfig
    {
        public static string Get(string configName)
        {
            return ConfigurationManager.AppSettings[configName].ToString();
        }
    }
}