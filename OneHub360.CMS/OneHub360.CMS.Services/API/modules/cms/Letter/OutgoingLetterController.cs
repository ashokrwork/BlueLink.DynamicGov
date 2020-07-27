using OneHub360.App.Shared;
using OneHub360.CMS.Business;
using OneHub360.CMS.DAL;
using OneHub360.CMS.Services.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace OneHub360.CMS.Services.API.modules.cms.Letter
{
    public class OutgoingLetterController : ApiController
    {

        [HttpGet]
        [Route("cms/outgoingletter/get/{status}")]
        public IList<OutgoingLetterView> GetLetters(int status)
        {
            IList<OutgoingLetterView> result = new List<OutgoingLetterView>();

            using (var outgoingLetterWorker = new OutgoingLetterWorker(Settings.Default.WorkerMode))
            {
                result = outgoingLetterWorker.GetLetters(status);
            }

            return result;
        }

        [HttpGet]
        [Route("cms/outgoingletter/view/{id}")]
        public OutgoingLetterView GetView(Guid id)
        {
            var outgoingLetterWorker = new OutgoingLetterWorker(Settings.Default.WorkerMode);
            return outgoingLetterWorker.GetView(id);
        }

        [HttpGet]
        [Route("cms/outgoingletter/attachements/{id}")]
        public IEnumerable<DocumentsView> GetAttachements(Guid id)
        {
            var outgoingLetterWorker = new OutgoingLetterWorker(Settings.Default.WorkerMode);
            return outgoingLetterWorker.GetAttachements(id);
        }

        [HttpGet]
        [Route("cms/outgoingletter/outgoingnumbers")]
        public IEnumerable<OutgoingLetterNumberAutoCompelete> GetOutgoingNumbers()
        {
            var outgoingLetterWorker = new OutgoingLetterWorker(Settings.Default.WorkerMode);
            return outgoingLetterWorker.GetOutgoingNumbers();
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
                    if (filteroption.Todate > DateTime.Now.AddDays(1))
                        whereClause += string.Format(" and (CreationDate <= '{0}')", filteroption.Todate);
                    if (filteroption.status != 0)
                        whereClause += string.Format(" and (Status ={0})", filteroption.status);
                }
            }

            return whereClause;
        }
        [HttpPost]
        [Route("cms/outgoingletter/getcount")]
        public virtual double Getcount(filteroption filteroption)
        {
            using (var feedWorker = new OutgoingLetterWorker(Settings.Default.WorkerMode))
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
        [Route("cms/outgoingletter/complete/{letterId}/{status}")]
        public virtual void UpdateLetterStatus(Guid letterId, int status)
        {
            using (var feedWorker = new OutgoingLetterWorker(Settings.Default.WorkerMode))
            {
                //return feedWorker.GetTimeLineView(whereClause);
                feedWorker.UpdateLetterStatus(letterId,status);
            }
        }

        [HttpPost]
        [Route("cms/outgoingletter/filter")]
        [Authorize]
        public virtual IEnumerable<OutgoingLetterView> filter(filteroption filteroption)
        {
            using (var feedWorker = new OutgoingLetterWorker(Settings.Default.WorkerMode))
            {
                //return feedWorker.GetTimeLineView(whereClause);
                return feedWorker.GetPagedTimeLineView(filterstring(filteroption), filteroption.pageNumber, filteroption.PageLength);
            }
        }
        [HttpGet]
        [Route("cms/outgoingletter/getall")]
        public IList<OutgoingLetterView> GetAll()
        {
            IList<OutgoingLetterView> result = new List<OutgoingLetterView>();

            using (var outgoingLetterWorker = new OutgoingLetterWorker(Settings.Default.WorkerMode))
            {
                result = outgoingLetterWorker.GetAll();
            }

            return result;
        }

        [HttpGet]
        [Route("cms/outgoingletter/signed")]
        public IList<OutgoingLetterView> GetSignedLetters()
        {
            IList<OutgoingLetterView> result = new List<OutgoingLetterView>();

            using (var outgoingLetterWorker = new OutgoingLetterWorker(Settings.Default.WorkerMode))
            {
                result = outgoingLetterWorker.GetSigned();
            }

            return result;
        }

        [HttpGet]
        [Route("cms/outgoingletter/export/{id}")]
        public void ExportManually(Guid id)
        {
            using (var outgoingLetterWorker = new OutgoingLetterWorker(Settings.Default.WorkerMode))
            {
                outgoingLetterWorker.ExportManually(id);
            }

            
        }

        [HttpGet]
        [Route("cms/outgoingletter/export/g2g/{id}")]
        public void ExportG2G(Guid id)
        {
            using (var outgoingLetterWorker = new OutgoingLetterWorker(Settings.Default.WorkerMode))
            {
                outgoingLetterWorker.ExportG2G(id);
            }


        }
        [HttpGet]
        [Route("cms/outgoingletter/download/{outgoingletterId}")]
        public HttpResponseMessage DownloadOutgoingletter(Guid outgoingletterId)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

            var outgoingletterWorker = new OutgoingLetterWorker(Settings.Default.WorkerMode);


            var zipContent = outgoingletterWorker.GetCorrespondenceZip(outgoingletterId);

            result.Content = new ByteArrayContent(zipContent);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = string.Format("{0}.zip", outgoingletterId)
            };

            return result;
        }
    }
}
