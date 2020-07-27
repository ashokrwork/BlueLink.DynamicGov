using OneHub360.Business;
using OneHub360.CMS.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Web;

namespace OneHub360.CMS.Services.WOPI.Word
{
    public class MSOfficeHandler : IHttpHandler
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {

            if (context.Request.HttpMethod.ToLower() == "head")
            {
                context.Response.StatusCode = 200;
                context.Response.StatusDescription = HttpWorkerRequest.GetStatusDescription(200);

                byte[] fileContent;

                using (var documentWorker = new DocumentWorker(WorkerMode.NonQueue))
                {
                    fileContent = documentWorker.GetFileContent(Guid.Parse("92184716-6BBD-41D2-85CF-0A66C8BEAE10"));
                }

                // set the headers of the response

                

                context.Response.Headers["Content-Type"] = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                context.Response.Headers["Last-Modified"] = DateTime.Now.ToString("r");
                context.Response.Headers["Content-Length"] = fileContent.Length.ToString();

                context.Response.AddHeader("Access-Control-Allow-Credentials", "true");

                context.Response.AddHeader("Access-Control-Allow-Headers", "Overwrite, Destination, Content-Type, Depth, User-Agent, Translate, Range, Content-Range, Timeout, X-File-Size, X-Requested-With, If-Modified-Since, X-File-Name, Cache-Control, Location, Lock-Token, If");

                context.Response.AddHeader("Access-Control-Allow-Methods", "ACL, CANCELUPLOAD, CHECKIN, CHECKOUT, COPY, DELETE, GET, HEAD, LOCK, MKCALENDAR, MKCOL, MOVE, OPTIONS, POST, PROPFIND, PROPPATCH, PUT, REPORT, SEARCH, UNCHECKOUT, UNLOCK, UPDATE, VERSION-CONTROL");

                context.Response.AddHeader("Access-Control-Allow-Origin", "*");

                context.Response.AddHeader("Access-Control-Expose-Headers", "DAV, content-length, Allow");
                context.Response.AddHeader("Access-Control-Max-Age", "2147483647");


            }

            if (context.Request.RequestType.ToLower() == "get")
            {
                byte[] fileContent;

                using (var documentWorker = new DocumentWorker(WorkerMode.NonQueue))
                {
                    fileContent = documentWorker.GetFileContent(Guid.Parse("92184716-6BBD-41D2-85CF-0A66C8BEAE10"));
                }

                // transmit file from local storage to the response stream.
                context.Response.BinaryWrite(fileContent);
                context.Response.StatusCode = 200;
                context.Response.StatusDescription = HttpWorkerRequest.GetStatusDescription(200);

                context.Response.Close();
            }

            if (context.Request.RequestType.ToLower() == "options")
            {
                

                context.Response.AddHeader("DASL","<DAV:basicsearch>");
                context.Response.AddHeader("DAV", "1, 2, 3");
                context.Response.AddHeader("MS-Author-Via", "DAV");
                context.Response.AddHeader("Public","COPY,DELETE,GET,HEAD,LOCK,MOVE,OPTIONS,POST,PROPFIND,PROPPATCH,PUT,REPORT,SEARCH,UNLOCK");
                context.Response.AddHeader("Accept-Ranges", "bytes");
                context.Response.AddHeader("Server", "Microsoft-HTTPAPI/1.0");
                context.Response.AddHeader("Access-Control-Allow-Credentials", "true");

                context.Response.AddHeader("Access-Control-Allow-Headers", "Overwrite, Destination, Content-Type, Depth, User-Agent, Translate, Range, Content-Range, Timeout, X-File-Size, X-Requested-With, If-Modified-Since, X-File-Name, Cache-Control, Location, Lock-Token, If");

                context.Response.AddHeader("Access-Control-Allow-Methods","ACL, CANCELUPLOAD, CHECKIN, CHECKOUT, COPY, DELETE, GET, HEAD, LOCK, MKCALENDAR, MKCOL, MOVE, OPTIONS, POST, PROPFIND, PROPPATCH, PUT, REPORT, SEARCH, UNCHECKOUT, UNLOCK, UPDATE, VERSION-CONTROL");

                context.Response.AddHeader("Access-Control-Allow-Origin", "*");

                context.Response.AddHeader("Access-Control-Expose-Headers","DAV, content-length, Allow");
                context.Response.AddHeader("Access-Control-Max-Age","2147483647");
                context.Response.AddHeader("Allow", "COPY, DELETE, GET, HEAD, LOCK, MOVE, OPTIONS, POST, PROPFIND, PROPPATCH, PUT, REPORT, SEARCH, UNLOCK");

                context.Response.StatusCode = 200;
                context.Response.StatusDescription = HttpWorkerRequest.GetStatusDescription(200);
                context.Response.Close();
            }

            if (context.Request.RequestType.ToLower() == "put")
            {
                StreamReader _inputStream = new StreamReader(context.Request.InputStream);

                long _inputSize = _inputStream.BaseStream.Length;

                byte[] _inputBytes = new byte[_inputSize];
                _inputStream.BaseStream.Read(_inputBytes, 0, (int)_inputSize);

                using (var documentWorker = new DocumentWorker(WorkerMode.NonQueue))
                {
                    documentWorker.SaveFileContent(Guid.Parse("92184716-6BBD-41D2-85CF-0A66C8BEAE10"), _inputBytes);
                }

                context.Response.StatusCode = 200;
            }

            if (context.Request.RequestType.ToLower() == "lock")
            {


                context.Response.AddHeader("DASL", "<DAV:basicsearch>");
                context.Response.AddHeader("DAV", "1, 2, 3");
                context.Response.AddHeader("MS-Author-Via", "DAV");
                context.Response.AddHeader("Public", "COPY,DELETE,GET,HEAD,LOCK,MOVE,OPTIONS,POST,PROPFIND,PROPPATCH,PUT,REPORT,SEARCH,UNLOCK");
                context.Response.AddHeader("Accept-Ranges", "bytes");
                context.Response.AddHeader("Server", "Microsoft-HTTPAPI/1.0");
                context.Response.AddHeader("Access-Control-Allow-Credentials", "true");

                context.Response.AddHeader("Access-Control-Allow-Headers", "Overwrite, Destination, Content-Type, Depth, User-Agent, Translate, Range, Content-Range, Timeout, X-File-Size, X-Requested-With, If-Modified-Since, X-File-Name, Cache-Control, Location, Lock-Token, If");

                context.Response.AddHeader("Access-Control-Allow-Methods", "ACL, CANCELUPLOAD, CHECKIN, CHECKOUT, COPY, DELETE, GET, HEAD, LOCK, MKCALENDAR, MKCOL, MOVE, OPTIONS, POST, PROPFIND, PROPPATCH, PUT, REPORT, SEARCH, UNCHECKOUT, UNLOCK, UPDATE, VERSION-CONTROL");

                context.Response.AddHeader("Access-Control-Allow-Origin", "*");

                context.Response.AddHeader("Access-Control-Expose-Headers", "DAV, content-length, Allow");
                context.Response.AddHeader("Access-Control-Max-Age", "2147483647");
                context.Response.AddHeader("Allow", "COPY, DELETE, GET, HEAD, LOCK, MOVE, OPTIONS, POST, PROPFIND, PROPPATCH, PUT, REPORT, SEARCH, UNLOCK");

                context.Response.StatusCode = 200;
                context.Response.StatusDescription = HttpWorkerRequest.GetStatusDescription(200);
                context.Response.Close();
            }

            #endregion
        }

        [FlagsAttribute]
        public enum HttpMethods : int
        {
            /// <summary>
            /// No supported methods
            /// </summary>
            None = 0,

            /// <summary>
            /// Supports OPTIONS Method
            /// </summary>
            Options = 1,

            /// <summary>
            /// Supports GET Method
            /// </summary>
            Get = 2,

            /// <summary>
            /// Supports HEAD Method
            /// </summary>
            Head = 4,

            /// <summary>
            /// Supports DELETE Method
            /// </summary>
            Delete = 8,

            /// <summary>
            /// Supports PUT Method
            /// </summary>
            Put = 16,

            /// <summary>
            /// Supports COPY Method
            /// </summary>
            Copy = 32,

            /// <summary>
            /// Supports MOVE Method
            /// </summary>
            Move = 64,

            /// <summary>
            /// Supports MKCOL Method
            /// </summary>
            MKCol = 128,

            /// <summary>
            /// Supports PROPFIND Method
            /// </summary>
            PropFind = 256,

            /// <summary>
            /// Supports PROPPATCH Method
            /// </summary>
            PropPatch = 512,

            /// <summary>
            /// Supports LOCK Method
            /// </summary>
            Lock = 1024,

            /// <summary>
            /// Supports UNLOCK Method
            /// </summary>
            Unlock = 2048,

            /// <summary>
            /// Supports VERSION-CONTROL Method
            /// </summary>
            VersionControl = 4096,

            /// <summary>
            /// Supports REPORT Method
            /// </summary>
            Report = 8192,

            /// <summary>
            /// Supports All Methods
            /// </summary>
            All = HttpMethods.Copy | HttpMethods.Delete | HttpMethods.Get | HttpMethods.Head | HttpMethods.Lock |
                HttpMethods.MKCol | HttpMethods.Move | HttpMethods.Options | HttpMethods.PropFind | HttpMethods.PropPatch | HttpMethods.Put |
                HttpMethods.Report | HttpMethods.Unlock | HttpMethods.VersionControl
        }
    }
}
