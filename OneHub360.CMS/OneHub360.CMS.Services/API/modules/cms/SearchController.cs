using OneHub360.CMS.Business;
using OneHub360.CMS.DAL;
using OneHub360.CMS.Services.Properties;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace OneHub360.CMS.Services.API.modules.cms
{
    public class SearchController : ApiController
    {
        [HttpGet]
        [Route("cms/search/{id}/{keyword}")]
        public IList<Correspondence> Search(Guid id, string keyword)
        {
            IList<Correspondence> result = new List<Correspondence>();
            using (var correspondenceWorker = new CorrespondeceWorker(Settings.Default.WorkerMode))
            {
                result = correspondenceWorker.KeyWordSearch(keyword, id);
            }
            return result;
        }
    }
}
