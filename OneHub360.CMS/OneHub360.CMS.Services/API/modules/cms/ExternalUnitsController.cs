using OneHub360.CMS.Business;
using OneHub360.CMS.DAL;
using OneHub360.CMS.Services.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace OneHub360.CMS.Services.API.modules.cms
{
    public class ExternalUnitsController : ApiController
    {
        [HttpGet]
        [Route("cms/externalorganizations/getall")]
        public IList<ExternalOrganizations> GetExternalOrganizations()
        {
            IList<ExternalOrganizations> organizations = new List<ExternalOrganizations>();

            using (var letterWorker = new LetterWorker(Settings.Default.WorkerMode))
            {
                organizations = letterWorker.GetExternalOrganizations();
            }

            return organizations;
        }

        [HttpGet]
        [Route("cms/externalorganizations/logo/{id}")]
        public virtual HttpResponseMessage GetOrgUnitLogo(Guid id)
        {
            using (var letterWorker = new LetterWorker(Settings.Default.WorkerMode))
            {
                var organization = letterWorker.GetExternalOrganization(id);

                if (organization.Logo == null)
                {
                    organization.Logo = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/img/modules/cms/government.png"));
                }

                var stream = new MemoryStream(organization.Logo);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(stream);
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
                return response;
            }
        }
    }
}
