using OneHub360.App.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneHub360.App.Business.UOW;
using OneHub360.App.DAL;
using OneHub360.App.Shared;
using OneHub360.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.App.Business.Tests
{
    [TestClass()]
    public class FeedWorkerTests
    {
        [TestMethod()]
        public void GetTimeLineViewTest()
        {
            using (var context = new OneHubContext(false))
            {
                var result = context.Session.CreateQuery("from TimeLineView p where p.Title like :title")
    .SetParameter("title", "%مشروع%")
    .List<TimeLineView>();
                //var result = feedWorker.GetTimeLineView("Title like '%مشروع%'");
            }
        }

        [TestMethod()]
        public void KeyWordSearchTest()
        {
            var feedWroker = new FeedWorker(WorkerMode.NonQueue);
            var result = feedWroker.KeyWordSearch("مشروع", Guid.NewGuid());
        }
    }
}

namespace OneHub360.App.Business.UOW.Tests
{
    [TestClass()]
    public class FeedWorkerTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            var feed = new Feeds();

            feed.CreatedBy = "Ahmed Shokr";
            feed.CreationDate = DateTime.Now;
            feed.Title = "Test after Shared Storage";
            feed.FeedId = Guid.NewGuid().ToString();
            feed.Status = 1;
            feed.Scope = 1;
            feed.Pinned = false;
            feed.Date = DateTime.Now;
            feed.Language = DB.Language.AR;
            feed.Priority = 1;
            //feed.FK_UserInfo = new UserInfos { CreatedBy = "Ahmed", Id = Guid.Parse("E0050903-2B1A-4DBB-A5AC-0015F36C0F25") };
            feed.FeedTypes = new FeedTypes { CreatedBy = "Ahmed", Id = Guid.Parse("23722766-6DD5-4CB3-8989-22655038300E") };

            var feedWorker = new FeedWorker(WorkerMode.NonQueue);
            feedWorker.Create(feed);
        }
    }
}