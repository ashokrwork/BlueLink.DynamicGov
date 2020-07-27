using OneHub360.CMS.Business;
using OneHub360.CMS.DAL;
using OneHub360.CMS.Services.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace OneHub360.CMS.Services.API.modules.cms.Letter
{
    public class DraftLetterController : ApiController
    {
        [HttpPost]
        [Route("cms/draftletter/create")]
        public Guid Create(DraftLetter draftLetter)
        {
            var result = Guid.Empty;
            using (var draftLetterWorker = new DraftLetterWorker(Settings.Default.WorkerMode))
            {
                result = draftLetterWorker.Create(draftLetter).Result;
            }

            return result;
        }

        [HttpGet]
        [Route("cms/draftletter/send/{id}/{userid}")]
        public Task<bool> Send(Guid id, Guid userid)
        {
            var result = false;
            using (var draftLetterWorker = new DraftLetterWorker(Settings.Default.WorkerMode))
            {
                result = draftLetterWorker.Send(id, userid, string.Empty).Result;
            }
            return Task<bool>.FromResult(result);
        }

        [HttpGet]
        [Route("cms/draftletter/view/{id}")]
        public DraftLetterView GetView(Guid id)
        {
            var draftMemoWorker = new DraftLetterWorker(Settings.Default.WorkerMode);
            return draftMemoWorker.GetView(id);
        }

        [HttpGet]
        [Route("cms/draftletter/attachements/{id}")]
        public IEnumerable<DocumentsView> GetAttachements(Guid id)
        {
            var draftletterWorker = new DraftLetterWorker(Settings.Default.WorkerMode);
            return draftletterWorker.GetAttachements(id);
        }

        [HttpPost]
        [Route("cms/draftletter/{id}/attachement/addmain")]
        public async Task<IHttpActionResult> Addmaindocument(Guid id)
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return BadRequest("Unsupported media type");
            }

            var provider = await Request.Content.ReadAsMultipartAsync();

            var fileBinaryStream = provider.Contents[0].ReadAsStreamAsync().Result;

            var fileTitle = provider.Contents[1].ReadAsStringAsync().Result;
            var fileName = provider.Contents[3].ReadAsStringAsync().Result;

            byte[] fileBinary;

            using (var memoryStream = new MemoryStream())
            {
                fileBinaryStream.CopyTo(memoryStream);
                fileBinary = memoryStream.ToArray();
            }

            var document = new Document
            {
                Id = id,
                FK_Template = ModuleConstants.AttachementDocumentTemplate,
                Title = fileTitle,
                FileName = fileName,
                FileBinary = fileBinary,
                CreatedBy = "E0050903-2B1A-4DBB-A5AC-0015F36C0F25"
            };

            try
            {
                using (var draftLetterWorker = new DraftLetterWorker(Settings.Default.WorkerMode))
                {
                    draftLetterWorker.AddMainDocument(id, document, true);
                    //draftLetterWorker.AddAttachement(id, document);
                }

                return Ok(new { Message = "Uploaded" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }
        [HttpPost]
        [Route("cms/draftletter/{id}/attachement/add")]
        public async Task<IHttpActionResult> AddAttachement(Guid id)
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return BadRequest("Unsupported media type");
            }

            var provider = await Request.Content.ReadAsMultipartAsync();

            var fileBinaryStream = provider.Contents[0].ReadAsStreamAsync().Result;

            var fileTitle = provider.Contents[1].ReadAsStringAsync().Result;
            var fileName = provider.Contents[3].ReadAsStringAsync().Result;

            byte[] fileBinary;

            using (var memoryStream = new MemoryStream())
            {
                fileBinaryStream.CopyTo(memoryStream);
                fileBinary = memoryStream.ToArray();
            }

            var document = new Document
            {
                Id = Guid.NewGuid(),
                FK_Template = ModuleConstants.AttachementDocumentTemplate,
                Title = fileTitle,
                FileName = fileName,
                FileBinary = fileBinary,
                CreatedBy = "E0050903-2B1A-4DBB-A5AC-0015F36C0F25"
            };

            try
            {
                using (var draftLetterWorker = new DraftLetterWorker(Settings.Default.WorkerMode))
                {
                    draftLetterWorker.AddAttachement(id, document);
                }

                return Ok(new { Message = "Uploaded" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }
        [HttpGet]
        [Route("cms/draftletter/getforupdate/{draftLetterId}")]
        public DraftLetter GetForUpdate(Guid draftletterId)
        {
            var draftLetterWorker = new DraftLetterWorker(Settings.Default.WorkerMode);
            return draftLetterWorker.GetLetter(draftletterId);
        }
        [HttpPost]
        [Route("cms/draftLetter/update")]
        public bool Update(DraftLetter draftLetter)
        {
            var result = false;
            using (var draftLetterWorker = new DraftLetterWorker(Settings.Default.WorkerMode))
            {
                result = draftLetterWorker.Update(draftLetter).Result;
            }

            return result;
        }

        [HttpGet]
        [Route("cms/draftletter/download/{draftLetterId}")]
        public HttpResponseMessage DownloadDraftLetter(Guid draftLetterId)
        {
            var draftLetterWorker = new DraftLetterWorker(Settings.Default.WorkerMode);

            var zipContent = draftLetterWorker.GetCorrespondenceZip(draftLetterId);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(zipContent);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = string.Format("{0}.zip", draftLetterId)
            };

            return result;
        }

    }
}
