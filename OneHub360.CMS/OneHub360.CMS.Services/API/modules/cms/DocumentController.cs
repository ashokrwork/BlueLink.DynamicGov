using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using OneHub360.CMS.Services.Properties;
using OneHub360.CMS.Business;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace OneHub360.CMS.Services.Controllers
{

    public class DocumentController : ApiController
    {
        [HttpGet]
        [Route("cms/document/file/{id}")]
        public HttpResponseMessage GetContent(Guid id)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);


            using (var documentWorker = new DocumentWorker(Settings.Default.WorkerMode)) { 

                var document = documentWorker.GetDocument(id);

                result.Content = new ByteArrayContent(documentWorker.GetFileContent(id));

                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = string.Format("{0}{1}",document.Title, Path.GetExtension(document.FileName))
                };

                if (Path.GetExtension(document.FileName) == ".docx")
                {
                    result.Content.Headers.ContentType =
                        new MediaTypeHeaderValue("application/octet-stream");
                }
                else if (Path.GetExtension(document.FileName) == ".pdf")
                {
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                }
            }
            return result;
        }

        [HttpGet]
        [Route("cms/document/file/download/{filename}")]
        public HttpResponseMessage GetDocumentContentForWord(string filename)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);


            using (var documentWorker = new DocumentWorker(Settings.Default.WorkerMode))
            {
                var id = Guid.Parse(filename.Split('.')[0]);

                var document = documentWorker.GetDocument(id);

                result.Content = new ByteArrayContent(documentWorker.GetFileContent(id));

                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = string.Format("{0}{1}", document.Title, Path.GetExtension(document.FileName))
                };

                if (Path.GetExtension(document.FileName) == ".docx")
                {
                    result.Content.Headers.ContentType =
                        new MediaTypeHeaderValue("application/octet-stream");
                }
                else if (Path.GetExtension(document.FileName) == ".pdf")
                {
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                }
            }
            return result;
        }

        [HttpGet]
        [Route("cms/template/file/download/{filename}")]
        public HttpResponseMessage GetTemplateContentForWord(string filename)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);


            using (var documentWorker = new DocumentWorker(Settings.Default.WorkerMode))
            {
                var id = Guid.Parse(filename.Split('.')[0]);

                var template = documentWorker.GetTemplate(id);

                result.Content = new ByteArrayContent(template.File);

                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = string.Format("{0}.docx", template.Title)
                };

                
                    result.Content.Headers.ContentType =
                        new MediaTypeHeaderValue("application/octet-stream");
                
            }
            return result;
        }

        [HttpPost]
        [Route("cms/document/word/update")]
        public virtual bool SaveDocumentFromWord([FromBody]JObject postData) 
        {
            dynamic data = postData;

            var documentId = data.id.ToString();
            var wordXml = data.contents.ToString();

            var result = false;

            using (var documentWorker = new DocumentWorker(OneHub360.Business.WorkerMode.NonQueue))
            {
                result = documentWorker.SaveDocumentFromWordOpenXml(Guid.Parse(documentId), wordXml);
            }

            return result;
        }

        [HttpPost]
        [Route("cms/template/word/update")]
        public virtual bool SaveTemplateFromWord([FromBody]JObject postData)
        {
            dynamic data = postData;

            var templateId = data.id.ToString();
            var wordXml = data.contents.ToString();

            var result = false;

            using (var documentWorker = new DocumentWorker(OneHub360.Business.WorkerMode.NonQueue))
            {
                result = documentWorker.SaveTemplateFromWordOpenXml(Guid.Parse(templateId), wordXml);
            }

            return result;
        }

        [HttpGet]
        [Route("cms/warm")]
        public virtual string Warmup()
        {
            return DateTime.Now.ToString();
        }

        
    }
}
