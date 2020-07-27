using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneHub360.CMS.WebDav.Handlers
{
    public class OptionsHandler : IMethodHandler
    {
        private HttpApplication _httpApplication;


        public OptionsHandler(HttpApplication HttpApplication)
        {
            _httpApplication = HttpApplication;
        }



        public string ErrorXml
        {
            get
            {
                return string.Empty;
            }
        }

        public string RequestXml
        {
            get
            {
                return string.Empty;
            }
        }

        public string ResponseXml
        {
            get
            {
                return string.Empty;
            }
        }

        public int Handle()
        {
            if (_httpApplication == null)
                return (int)ServerResponseCode.BadRequest;

            _httpApplication.Response.AppendHeader("DAV", "1,2,3");
            _httpApplication.Response.AppendHeader("MS-Author-Via", "DAV");
            _httpApplication.Response.AppendHeader("Versioning-Support", "DAV:basicversioning");
            _httpApplication.Response.AppendHeader("DASL", "<DAV:sql>");
            _httpApplication.Response.AppendHeader("Public", "COPY, DELETE, GET, HEAD, LOCK, MKCOL, MOVE, OPTIONS, PROPFIND, PROPPATCH, PUT, UNLOCK, REPORT, VERSION-CONTROL, CHECKOUT, CHECKIN, UNCHECKOUT");
            _httpApplication.Response.AppendHeader("Allow", "COPY, DELETE, GET, HEAD, LOCK, MKCOL, MOVE, OPTIONS, PROPFIND, PROPPATCH, PUT, UNLOCK, REPORT,  VERSION-CONTROL, CHECKOUT, CHECKIN, UNCHECKOUT");

            return (int)ServerResponseCode.Ok;
        }
    }
}