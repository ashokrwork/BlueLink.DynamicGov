using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneHub360.CMS.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.CMS.Business.Tests
{
    [TestClass()]
    public class FeedApiClientTests
    {
        [TestMethod()]
        public void UpdateFeedTest()
        {
            using (var feedApiClient = new FeedApiClient())
            {
                //feedApiClient.UpdateFeed(new { Id = Guid.Parse("1503ebf6-88ee-42d8-a3f8-a682017cffde"), Status = 0 });
            }
        }
    }
}