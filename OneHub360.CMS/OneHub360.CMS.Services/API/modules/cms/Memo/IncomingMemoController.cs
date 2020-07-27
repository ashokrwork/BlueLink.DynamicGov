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
    public class IncomingMemoController : ApiController
    {

        [HttpGet]
        [Route("cms/incomingmemo/attachements/{id}")]
        public IEnumerable<DocumentsView> GetAttachements(Guid id)
        {
            var incomingMemoWorker = new IncomingMemoWorker(Settings.Default.WorkerMode);
            return incomingMemoWorker.GetAttachements(id);
        }

        [HttpGet]
        [Route("cms/incomingmemo/documents/{id}")]
        public IEnumerable<DocumentsView> GetDocuments(Guid id)
        {
            var incomingMemoWorker = new IncomingMemoWorker(Settings.Default.WorkerMode);
            return incomingMemoWorker.GetAllDocuments(id);
        }

        [HttpGet]
        [Route("cms/incomingmemo/view/{id}")]
        public IncomingMemoView GetView(Guid id)
        {
            var incomingMemoWorker = new IncomingMemoWorker(Settings.Default.WorkerMode);
            return incomingMemoWorker.GetView(id);
        }

        [HttpGet]
        [Route("cms/incomingmemo/download/{incomingMemoId}")]
        public HttpResponseMessage DownloadIncomingMemo(Guid incomingMemoId)
        {

            var incomingMemoWorker = new IncomingMemoWorker(Settings.Default.WorkerMode);
            var zipContent = incomingMemoWorker.GetCorrespondenceZip(incomingMemoId);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(zipContent);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = string.Format("{0}.zip", incomingMemoId)
            };

            return result;
        }

        [HttpPost]
        [Route("cms/incomingmemo/send")]
        public Guid Send(IncomingMemoAction action)
        {
            Guid result;
            using (var incomingMemoWorker = new IncomingMemoWorker(Settings.Default.WorkerMode))
            {
                result = incomingMemoWorker.Send(action).Result;
            }
            return result;        
        }


        [HttpGet]
        [Route("cms/incomingmemo/print/{incomingMemoId}")]
        public HttpResponseMessage PrintAll(Guid incomingMemoId)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);


            using (var incomingMemoWorker = new IncomingMemoWorker(Settings.Default.WorkerMode))
            {

                var documentsBinary = incomingMemoWorker.GetCorrespondenceForPrinting(incomingMemoId);

                result.Content = new ByteArrayContent(documentsBinary);

                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "طباعة.pdf"
                };


                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            }
            return result;
        }

        //[HttpGet]
        //[Route("api/incomingmemo/actions/{thread}")]
        //public IEnumerable<UserActionView> GetActions(Guid thread)
        //{
        //    var incomingMemoWorker = new IncomingMemoWorker(Settings.Default.WorkerMode);
        //    return incomingMemoWorker.GetThreadActions(thread);
        //}
    }
}
