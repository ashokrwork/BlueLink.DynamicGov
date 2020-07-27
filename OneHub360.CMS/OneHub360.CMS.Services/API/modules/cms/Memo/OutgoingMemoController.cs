using OneHub360.CMS.DAL;
using OneHub360.CMS.Services.Properties;
using System.Web.Http;
using System;
using System.Net.Http;
using OneHub360.CMS.Business;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;

namespace OneHub360.CMS.Services.Controllers
{
    /// <summary>
    /// 
    /// http://localhost:361
    /// </summary>
    public class OutgoingMemoController : ApiController
    {

        [HttpGet]
        [Route("cms/outgoingmemo/attachements/{id}")]
        public IEnumerable<DocumentsView> GetAttachements(Guid id)
        {
            var outgoingMemoWorker = new OutgoingMemoWorker(Settings.Default.WorkerMode);
            return outgoingMemoWorker.GetAttachements(id);
        }

        [HttpGet]
        [Route("cms/outgoingmemo/view/{id}")]
        public OutgoingMemoView GetView(Guid id)
        {
            var outgoingMemoWorker = new OutgoingMemoWorker(Settings.Default.WorkerMode);
            return outgoingMemoWorker.GetView(id);
        }

        //[HttpGet]
        //[Route("api/outgoingmemo/actions/{thread}")]
        //public IEnumerable<UserActionView> GetActions(Guid thread)
        //{
        //    var outgoingMemoWorker = new OutgoingMemoWorker(Settings.Default.WorkerMode);
        //    return outgoingMemoWorker.GetThreadActions(thread);
        //}

        [HttpGet]
        [Route("cms/outgoingmemo/download/{outgoingMemoId}")]
        public HttpResponseMessage DownloadOutgoingMemo(Guid outgoingMemoId)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

            var outgoingMemoWorker = new OutgoingMemoWorker(Settings.Default.WorkerMode);
            

                var zipContent = outgoingMemoWorker.GetCorrespondenceZip(outgoingMemoId);
                
                result.Content = new ByteArrayContent(zipContent);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = string.Format("{0}.zip", outgoingMemoId)
                };
            
            return result;
        }
    }
}
