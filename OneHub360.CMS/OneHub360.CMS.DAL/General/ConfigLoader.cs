using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace OneHub360.CMS.DAL
{
    public class CMSConfigLoader
    {
        private static CMSConfigLoader instance;
        public CMSConfigData configData;

        public static CMSConfigLoader Generator
        {
            get
            {
                if (instance == null)
                {
                    instance = new CMSConfigLoader();
                }
                return instance;
            }
        }

        private CMSConfigLoader()
        {
            ReadConfig();
        }

        internal void ReadConfig()
        {
            configData = JsonConvert.DeserializeObject<CMSConfigData>(
                        File.ReadAllText(HostingEnvironment.MapPath("~/config/modules/cms/cmsconfig.json")));
        }
    }

    public class CMSConfigData
    {
        public string WordViewerUrl { get; set; }
        public string FilePreviewUrl { get; set; }
        public string ExcelViewerUrl { get; set; }
        public string PowerPointViewerUrl { get; set; }
        public string CMSServiceBaseUrl { get; set; }
        public string PdfConvertionUrl { get; set; }
        public string NumbersPrefix { get; set; }

        public string ImagePreviewUrl { get; set;}
    }
}
