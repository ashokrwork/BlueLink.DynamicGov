using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneHub360.App.Shared;
using System;

namespace OneHub360.App.DAL.Tests
{
    [TestClass()]
    public class FeedRepositoryTests
    {
        [TestMethod()]
        public void FeedRepositoryTest()
        {
            using (var context = new OneHubContext(true)) {
                var feedRepository = new FeedRepository(context);

                var feed = new Feeds
                {
                    CreatedBy = "Ahmed Shokr",
                    Title = "التسبب بقوع حادث",
                    FeedId = Guid.NewGuid().ToString(),
                    Status = 1,
                    Scope = 1,
                    Pinned = false,
                    Date = DateTime.Now,
                    Priority = 1,
                    //FK_UserInfo = new UserInfos {Language = DB.Language.AR, Id = Guid.Parse("E0050903-2B1A-4DBB-A5AC-0015F36C0F25") },
                    FeedTypes = new FeedTypes { Language = DB.Language.AR, Id = Guid.Parse("10AEFD01-BE05-4333-B160-C47D1D56617B") },
                    Brief = "التسبب بوقوع حادث وقيادة مركبة بدون تأمين"
                };

                feedRepository.Insert(feed);
                context.Transaction.Commit();
            }

            
        }

        [TestMethod()]
        public void UpdateFeedStatus()
        {
            //var feedRepository = new FeedRepository();

            //feedRepository.DynamicUpdate(Guid.Parse("3C37D33F-D113-4619-9DED-A682017C0B9C"), new { Status = 0 });
        }
    }
}