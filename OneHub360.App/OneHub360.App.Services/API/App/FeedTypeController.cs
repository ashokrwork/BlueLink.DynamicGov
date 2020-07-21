using OneHub360.App.Business;
using OneHub360.App.Services.Properties;
using OneHub360.App.Shared;
using System.Collections.Generic;
using System.Web.Http;

namespace OneHub360.App.Services.Controllers
{
    [RoutePrefix("api/feedtype")]
    public  class FeedTypeController : ApiController
    {
        [HttpGet]
        [Route("newitemslist")]
        public  IEnumerable<FeedTypes> GetAll()
        {using (
            var feedWorker = new FeedWorker(Settings.Default.WorkerMode))
            {
                var feedTypes = feedWorker.GetNewItemFeedTypes();
                return feedTypes;
            }
        }
    }
}
