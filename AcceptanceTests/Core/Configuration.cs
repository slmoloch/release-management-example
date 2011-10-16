using System;
using System.Configuration;

namespace AcceptanceTests.Core
{
    class Configuration
    {
        public static string SiteUrl
        {
            get { return ConfigurationManager.AppSettings["SiteUrl"]; }
        }

        public static string StorageAdminSiteUrl
        {
            get { return ConfigurationManager.AppSettings["StorageAdminSiteUrl"]; }
        }

        public static string SeleniumHost
        {
            get { return ConfigurationManager.AppSettings["SeleniumHost"]; }
        }

        public static int SeleniumPort
        {
            get { return Int32.Parse(ConfigurationManager.AppSettings["SeleniumPort"]); }
        }

        public static string ScriptsPath
        {
            get { return ConfigurationManager.AppSettings["ScriptsPath"]; }
        }
    }
}
