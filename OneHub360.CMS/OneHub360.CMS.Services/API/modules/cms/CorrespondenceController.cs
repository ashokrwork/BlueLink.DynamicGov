using OneHub360.CMS.Business;
using OneHub360.CMS.Services.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace OneHub360.CMS.Services.API.modules.cms
{
    public class CorrespondenceController : ApiController
    {
        [HttpGet]
        [Route("cms/correspondence/download/{correspondenceId}")]
        public HttpResponseMessage DownloadDraftMemo(Guid correspondenceId)
        {
            var correspondneceWorker = new CorrespondeceWorker(Settings.Default.WorkerMode);

            var zipContent = correspondneceWorker.GetCorrespondenceZip(correspondenceId);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(zipContent);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = string.Format("{0}.zip", correspondenceId)
            };

            return result;
        }

        [HttpGet]
        [Route("cms/correspondence/preview/{correspondenceId}")]
        public string GetMainDocumentPreviewUrl(Guid correspondenceId)
        {
            var correspondneceWorker = new CorrespondeceWorker(Settings.Default.WorkerMode);

            return correspondneceWorker.GetMainDocument(correspondenceId).PreviewUrl;
        }
    }
}
