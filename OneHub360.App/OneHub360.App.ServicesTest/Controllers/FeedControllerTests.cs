using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneHub360.App.DAL;
using OneHub360.App.Services.Controllers;
using OneHub360.App.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.App.Services.Controllers.Tests
{
    [TestClass()]
    public class FeedControllerTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            var controller = new FeedController();

            var feed = new Feeds();

            feed.CreatedBy = "Ahmed";
            feed.Title = "Test";
            feed.FeedId = Guid.NewGuid().ToString();
            feed.Status = 1;
            feed.Scope = 1;
            feed.Pinned = false;
            feed.Date = DateTime.Now;
            feed.Language = DB.Language.AR;
            feed.Priority = 1;
            feed.FK_UserInfo = new UserInfos { CreatedBy = "Ahmed", Id = Guid.Parse("E0050903-2B1A-4DBB-A5AC-0015F36C0F25") };
            feed.FeedTypes = new FeedTypes { CreatedBy = "Ahmed", Id = Guid.Parse("23722766-6DD5-4CB3-8989-22655038300E") };


            //controller.Create(feed);

            var client = new HttpClient() { Timeout = TimeSpan.FromMinutes(3) };

            client.BaseAddress = new Uri("http://localhost:363/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var feedPostResponse = client.PostAsJsonAsync("api/feed/create", feed).Result;

                var result = feedPostResponse.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Shokr : {0}", ex.Message));
            }
        }


    }
}