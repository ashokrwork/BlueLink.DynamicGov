using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneHub360.CMS.WebDav.Handlers
{
    public class MKCOLHandler : IMethodHandler
    {
        private HttpApplication _httpApplication;

        public MKCOLHandler(HttpApplication HttpApplication)
        {

            _httpApplication = HttpApplication;

        }

        public string ResponseXml
        {
            get
            {
                return "";
            }
        }
        public string RequestXml
        {
            get
            {
                return "";
            }
        }
        public string ErrorXml
        {
            get
            {
                return "";
            }
        }

        public int Handle()
        {
            return 201;
        }
    }
}