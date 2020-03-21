using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BackApis.Helpers
{
    public static class Settings
    {
        public static string ApiKey
        {
            get { return ConfigurationManager.AppSettings["ApiKey"]; }
        }


        public static string ApiSecret
        {
            get { return ConfigurationManager.AppSettings["ApiSecret"]; }
        }

        public static string ZoomApiUrl
        {
            get { return ConfigurationManager.AppSettings["ZoomApiUrl"]; }
        }
    }
}