using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace OneHub360.App.Shared
{
    public class AppConfigLoader
    {
        private static AppConfigLoader instance;
        public AppConfigData configData;

        public static AppConfigLoader Generator
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppConfigLoader();
                }
                return instance;
            }
        }

        private AppConfigLoader()
        {
            ReadConfig();
        }

        internal void ReadConfig()
        {
            configData = JsonConvert.DeserializeObject<AppConfigData>(
                        File.ReadAllText(HostingEnvironment.MapPath("~/config/app/appConfig.json")));
        }
    }

    public class AppConfigData
    {
        public string AppApiBaseUrl { get; set; }
    }
}
