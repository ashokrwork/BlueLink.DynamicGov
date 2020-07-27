using OneHub360.App.Business;
using OneHub360.App.Shared;
using OneHub360.CMS.Business;
using OneHub360.CMS.DAL;
using OneHub360.CMS.Services.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace OneHub360.CMS.Services.Controllers.Letter
{
    /// <summary>
    /// 
    /// http://localhost:361
    /// </summary>
    public class IncomingLetterController : ApiController
    {
        [HttpPost]
        [Route("cms/incomingletter/register")]
        public Task<Guid> Register(IncomingLetter incomingLetter)
        {
            var result = Guid.Empty;
            using (var incominLetterWorker = new IncomingLetterWorker(Settings.Default.WorkerMode))
            {
                result = incominLetterWorker.Register(incomingLetter).Result;
            }

            return Task<Guid>.FromResult(result);
        }
        [HttpPost]
        [Route("cms/incomingletter/update")]
        public bool update(IncomingLetter incomingLetter)
        {
            var result = false;
            using (var incominLetterWorker = new IncomingLetterWorker(Settings.Default.WorkerMode))
            {
                result = incominLetterWorker.Update(incomingLetter);
            }

            return result;
        }
        [HttpPost]
        [Route("cms/incomingletter/Reject")]
        public Guid Reject(IncomingLetter incomingLetter)
        {
            var result = Guid.Empty;
            incomingLetter.Id = Guid.NewGuid();
            incomingLetter.ThreadId = Guid.NewGuid();
            incomingLetter.CreationDate = DateTime.Now;
            using (var incominLetterWorker = new IncomingLetterWorker(Settings.Default.WorkerMode))
            {
                result = incominLetterWorker.Reject(incomingLetter);

            }
            var comment = new Comment();
            comment.CreatedBy = incomingLetter.From;
            comment.FK_Feed = incomingLetter.Id;
            comment.ThreadId = incomingLetter.ThreadId;
            comment.CreatedBy = incomingLetter.CreatedBy;
            comment.CreationDate = incomingLetter.CreationDate;
            comment.FK_User =new Guid( incomingLetter.CreatedBy);

            comment.Body = "Rejected! " + incomingLetter.RejectedReason;

            using (var feedWorker = new FeedWorker(Settings.Default.WorkerMode))
            {
                feedWorker.AddComment(comment);
            }
            return result;
        }
        [HttpPost]
        [Route("cms/incomingmemo/{id}/main/add")]
        public async Task<IHttpActionResult> AddMainDocument(Guid id)
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
                using (var incomingLetterWorker = new IncomingLetterWorker(Settings.Default.WorkerMode))
                {
                    incomingLetterWorker.AddMainDocument(id, document,true);
                    incomingLetterWorker.UpdateLetterStatus(id, IncomingLetterStatus.Scanned);
                }

                return Ok(new { Message = "Uploaded" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }


        [HttpGet]
        [Route("cms/incomingletter/getregistered")]
        public IList<IncomingLetterView> GetRegisteredLetters()
        {
            IList<IncomingLetterView> result = new List<IncomingLetterView>();

            using (var incominLetterWorker = new IncomingLetterWorker(Settings.Default.WorkerMode))
            {
                result = incominLetterWorker.GetRegisteredLetters();
            }

            return result;
        }
        [Route("cms/incomingletter/get/{status}")]
        public IList<IncomingLetterView> GetLetters(int status)
        {
            IList<IncomingLetterView> result = new List<IncomingLetterView>();

            using (var incominLetterWorker = new IncomingLetterWorker(Settings.Default.WorkerMode))
            {
                result = incominLetterWorker.GetLetters(status);
            }

            return result;
        }
        [Route("cms/incomingLetter/getforupdate/{id}")]
        public IncomingLetterView getforupdate(string id)
        {
            var result = new IncomingLetterView();

            using (var incominLetterWorker = new IncomingLetterWorker(Settings.Default.WorkerMode))
            {
                result = incominLetterWorker.GetLetter(id);
            }

            return result;
        }
        public string filterstring(filteroption filteroption)
        {
           var whereClause = string.Format("(1 = 1)");
            if (filteroption != null)
            {
                if (!filteroption.isfirstload)
                {
                    if (!string.IsNullOrEmpty(filteroption.Fromuser))
                        whereClause += string.Format(" and ([From] = '{0}')", filteroption.Fromuser);
                    if (!string.IsNullOrEmpty(filteroption.Touser))
                        whereClause += string.Format(" and ([To] = '{0}')", filteroption.Touser);
                    if (filteroption.Fromdate > DateTime.Now.AddYears(-1))
                        whereClause += string.Format(" and (CreationDate >= '{0}')", filteroption.Fromdate);
                    if (filteroption.Todate > filteroption.Fromdate)
                        whereClause += string.Format(" and (CreationDate <= '{0}')", filteroption.Todate);
                    if (filteroption.status != 0)
                        whereClause += string.Format(" and (Status ={0})", filteroption.status);
                }
            }

            return whereClause;
        }
        [HttpPost]
        [Route("cms/incomingletter/getcount")]
        public virtual double Getcount(filteroption filteroption)
        {
            using (var feedWorker = new IncomingLetterWorker(Settings.Default.WorkerMode))
            {
                //return feedWorker.GetTimeLineView(whereClause);
                double Count = feedWorker.GetPagesCount(filterstring(filteroption));
                double pagescount = Math.Ceiling(Count / filteroption.PageLength);
                if (pagescount < 0)
                    pagescount = 0;
                return pagescount;
            }
        }
        [HttpGet]
        
        [HttpPost]
        [Route("cms/incomingletter/filter")]
        [Authorize]
        public virtual IEnumerable<IncomingLetterView> filter(filteroption filteroption)
        {
            using (var feedWorker = new IncomingLetterWorker(Settings.Default.WorkerMode))
            {
                //return feedWorker.GetTimeLineView(whereClause);
                return feedWorker.GetPagedTimeLineView(filterstring(filteroption), filteroption.pageNumber, filteroption.PageLength);
            }
        }
        [HttpGet]
        [Route("cms/incomingletter/view/{id}")]
        public IncomingLetterView GetView(Guid id)
        {
            var incomingLetterWorker = new IncomingLetterWorker(Settings.Default.WorkerMode);
            return incomingLetterWorker.GetView(id);
        }

        [HttpGet]
        [Route("cms/incomingletter/attachements/{id}")]
        public IEnumerable<DocumentsView> GetAttachements(Guid id)
        {
            var incomingLetterWorker = new IncomingLetterWorker(Settings.Default.WorkerMode);
            return incomingLetterWorker.GetAttachements(id);
        }
        [HttpPost]
        [Route("cms/incomingletter/complete/{letterId}/{status}")]
        public virtual void UpdateLetterStatus(Guid letterId, int status)
        {
            using (var feedWorker = new IncomingLetterWorker(Settings.Default.WorkerMode))
            {
                //return feedWorker.GetTimeLineView(whereClause);
                feedWorker.UpdateLetterStatus(letterId, status);
            }
        }
        [HttpPost]
        [Route("cms/incomingletter/delete/{letterId}")]
        public virtual void Delete(Guid letterId)
        {
            using (var feedWorker = new IncomingLetterWorker(Settings.Default.WorkerMode))
            {
                var letter = feedWorker.GetLetter(letterId);
                letter.IsDeleted = true;

                //return feedWorker.GetTimeLineView(whereClause);
                feedWorker.Update(letter);
            }
        }
        [HttpPost]
        [Route("cms/incomingletter/forward")]
        public Guid Send(IncomingLetterAction action)
        {
            Guid result;
            using (var incomingLetterWorker = new IncomingLetterWorker(Settings.Default.WorkerMode))
            {
                result = incomingLetterWorker.ForwardIncomingLetter(action).Result;
            }
            return result;
        }

        [HttpGet]
        [Route("cms/reg/incomingletter/forward/{incomingLetterId}")]
        public bool ForwardFromReg(Guid incomingLetterId)
        {
            var result = false;

            using (var incomingLetterWorker = new IncomingLetterWorker(Settings.Default.WorkerMode))
            {
                result = incomingLetterWorker.SendFromReg(incomingLetterId).Result;
            }

            return result;
        }

        [HttpPost]
        [Route("cms/reg/incomingletter/update")]
        public bool Update(IncomingLetter incomingLetter)
        {
            bool result;
            using (var incomingLetterWorker = new IncomingLetterWorker(Settings.Default.WorkerMode))
            {
                result = incomingLetterWorker.Update(incomingLetter);
            }
            return result;
        }


        [HttpGet]
        [Route("cms/reg/incomingletter/{incomingLetterId}")]
        public IncomingLetter GetIncomingLetter(Guid incomingLetterId)
        {
            IncomingLetter result;
            using (var incomingLetterWorker = new IncomingLetterWorker(Settings.Default.WorkerMode))
            {
                result = incomingLetterWorker.GetLetter(incomingLetterId);
            }
            return result;
        }


        [HttpGet]
        [Route("cms/incomingletter/download/{incomingLetterId}")]
        public HttpResponseMessage DownloadIncomingLetter(Guid incomingLetterId)
        {
            var incomingLetterWorker = new IncomingLetterWorker(Settings.Default.WorkerMode);

            var zipContent = incomingLetterWorker.GetCorrespondenceZip(incomingLetterId);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(zipContent);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = string.Format("{0}.zip", incomingLetterId)
            };

            return result;
        }

        [HttpPost]
        [Route("cms/incomingletter/{id}/attachement/add")]
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
                using (var correspondenceWorker = new CorrespondeceWorker(Settings.Default.WorkerMode))
                {
                    correspondenceWorker.AddAttachement(id, document);
                }

                return Ok(new { Message = "Uploaded" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

    }
}
