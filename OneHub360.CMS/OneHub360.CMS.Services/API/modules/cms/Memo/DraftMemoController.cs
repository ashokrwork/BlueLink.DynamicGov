using OneHub360.CMS.DAL;
using OneHub360.CMS.Services.Properties;
using System.Web.Http;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;
using System.IO;
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
    public class DraftMemoController : ApiController
    {

        [HttpPost]
        [Route("cms/draftmemo/create")]
        public Guid Create(DraftMemo draftMemo)
        {
            var result = Guid.Empty;
            using (var draftMemoWorker = new DraftMemoWorker(Settings.Default.WorkerMode))
            {
                result = draftMemoWorker.ProcessCreate(draftMemo).Result;
            }

            return result;
        }

        [HttpPost]
        [Route("cms/draftmemo/update")]
        public bool Update(DraftMemo draftMemo)
        {
            var result = false;
            using (var draftMemoWorker = new DraftMemoWorker(Settings.Default.WorkerMode))
            {
                result = draftMemoWorker.Update(draftMemo).Result;
            }

            return result;
        }

        [HttpPost]
        [Route("cms/draftmemo/updatemultisend")]
        public bool UpdateMultiSend(MultiSendView multiSendView)
        {
            var result = false;
            using (var draftMemoWorker = new DraftMemoWorker(Settings.Default.WorkerMode))
            {
                result = draftMemoWorker.UpdateMultiSendView(multiSendView);
            }

            return result;
        }

        [HttpGet]
        [Route("cms/draftmemo/getmultisend/{id}")]
        public MultiSendView GetMultiSend(Guid id)
        {
            MultiSendView result;
            using (var draftMemoWorker = new DraftMemoWorker(Settings.Default.WorkerMode,true))
            {
                result = draftMemoWorker.GetMultiSendView(id);
            }

            return result;
        }

        [HttpPost]
        [Route("cms/draftmemo/updateaddtionalrecipients/{id}")]
        public bool UpdateAddtionalRecipients(DraftMemo draftMemo)
        {
            var result = false;
            using (var draftMemoWorker = new DraftMemoWorker(Settings.Default.WorkerMode))
            {
                result = draftMemoWorker.Update(draftMemo).Result;
            }

            return result;
        }

        [HttpGet]
        [Route("cms/draftmemo/send/{id}/{userid}")]
        public Task<bool> Send(Guid id,Guid userid)
        {
            var result = false;
            using (var draftMemoWorker = new DraftMemoWorker(Settings.Default.WorkerMode))
            {
                result = draftMemoWorker.Send(id, userid, string.Empty).Result;
            }
            return Task<bool>.FromResult(result);
        }

        [HttpGet]
        [Route("cms/draftmemo/attachements/{id}")]
        public IEnumerable<DocumentsView> GetAttachements(Guid id)
        {
            var draftMemoWorker = new DraftMemoWorker(Settings.Default.WorkerMode);
            return draftMemoWorker.GetAttachements(id);
        }

        [HttpGet]
        [Route("cms/{random}/draftmemo/view/{id}")]
      
        public DraftMemoView GetView(string random,Guid id)
        {
            //var user = HttpContext.Current.User;
            using (var draftMemoWorker = new DraftMemoWorker(Settings.Default.WorkerMode,true))
            {
                
                return draftMemoWorker.GetView(id);
            }
        }

        [HttpPost]
        [Route("cms/draftmemo/{id}/attachement/add")]
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
                Id= Guid.NewGuid(),
                FK_Template = ModuleConstants.AttachementDocumentTemplate,
                Title = fileTitle,
                FileName = fileName,
                FileBinary = fileBinary,
                CreatedBy = "E0050903-2B1A-4DBB-A5AC-0015F36C0F25"
            };

            try
            {
                using (var draftMemoWorker = new DraftMemoWorker(Settings.Default.WorkerMode))
                {
                    draftMemoWorker.AddAttachement(id, document);
                }

                return Ok(new { Message = "Uploaded" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPost]
        [Route("cms/draftmemo/{id}/attachement/addfromonedrive")]
        public async Task<IHttpActionResult> AddAttachementFromOneDrive(Guid id)
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return BadRequest("Unsupported media type");
            }

            var provider = await Request.Content.ReadAsMultipartAsync();

            var oneDriveFileUrl = provider.Contents[0].ReadAsStringAsync().Result;

            var webClient = new WebClient();
            
                var fileBinary = webClient.DownloadData(new Uri(oneDriveFileUrl));
            
                 

            var fileTitle = provider.Contents[1].ReadAsStringAsync().Result;
            var fileName = provider.Contents[3].ReadAsStringAsync().Result;

           

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
                using (var draftMemoWorker = new DraftMemoWorker(Settings.Default.WorkerMode))
                {
                    draftMemoWorker.AddAttachement(id, document);
                }

                return Ok(new { Message = "Uploaded" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet]
        [Route("cms/draftmemo/attachement/delete/{id}")]
        public bool DeleteAttachement(Guid id)
        {
            bool result;
            using (var draftMemoWorker = new DraftMemoWorker(Settings.Default.WorkerMode))
            {
                result = draftMemoWorker.DeleteAttachement(id);
            }
            return result;
        }

        [HttpGet]
        [Route("cms/draftmemo/download/{draftMemoId}")]
        public HttpResponseMessage DownloadDraftMemo(Guid draftMemoId)
        {
            var draftMemoWorker = new DraftMemoWorker(Settings.Default.WorkerMode);

            var zipContent = draftMemoWorker.GetCorrespondenceZip(draftMemoId);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(zipContent);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = string.Format("{0}.zip", draftMemoId)
            };

            return result;
        }

        [HttpGet]
        [Route("cms/draftmemo/print/{draftMemoId}")]
        public HttpResponseMessage PrintAll(Guid draftMemoId)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);


            using (var draftMemoWorker = new DraftMemoWorker(Settings.Default.WorkerMode))
            {

                var documentsBinary = draftMemoWorker.GetCorrespondenceForPrinting(draftMemoId);

                result.Content = new ByteArrayContent(documentsBinary);

                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "طباعة.pdf"
                };

                
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                
            }
            return result;
        }

        [HttpGet]
        [Route("cms/draftmemo/getforupdate/{draftMemoId}")]
        public DraftMemo GetForUpdate(Guid draftMemoId)
        {
            var draftMemoWorker = new DraftMemoWorker(Settings.Default.WorkerMode);
            return draftMemoWorker.GetMemo(draftMemoId);
        }
    }
}
