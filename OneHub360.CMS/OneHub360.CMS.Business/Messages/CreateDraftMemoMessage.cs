using OneHub360.Business;
using OneHub360.CMS.DAL;
using OneHub360.Queue;
using System;
using System.Threading.Tasks;

namespace OneHub360.CMS.Business.Messages
{
    public class CreateDraftMemoMessage : IMessage
    {
        public DraftMemo DraftMemo { get; set; }
        public string CreatedBy
        {
            get; set;
        }

        public DateTime CreationDate
        {
            get; set;
        }

        public int ProcessingIterations
        {
            get; set;
        }

        public async Task<bool> Process()
        {
            var result = true;

            

            var draftMemoWorker = new DraftMemoWorker(WorkerMode.NonQueue);
            if (true)//!draftMemoWorker.ProcessCreate(DraftMemo).Result)
            {
                result = false;
                await Retry();
            }
            else
            {
                //var feed = new Feeds();

                //feed.CreatedBy = DraftMemo.CreatedBy;
                //feed.CreationDate = DateTime.Now;
                //feed.Title = DraftMemo.Subject;
                //feed.FeedId = DraftMemo.Id.ToString();
                //feed.Status = 1;
                //feed.Scope = 1;
                //feed.Pinned = false;
                //feed.Date = DateTime.Now;
                //feed.Language = DB.Language.AR;
                //feed.Priority = 1;
                //feed.FK_From = Guid.Parse(DraftMemo.From);
                //feed.FK_To = Guid.Parse(DraftMemo.To);
                //feed.FeedTypes = new FeedTypes { CreatedBy = DraftMemo.CreatedBy, Id = Guid.Parse("23722766-6DD5-4CB3-8989-22655038300E") };

                //var client = new HttpClient() { Timeout = TimeSpan.FromMinutes(3) };

                //client.BaseAddress = new Uri("http://localhost:363/");
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //try
                //{
                //    var feedPostResponse = client.PostAsJsonAsync("api/feed/create", feed).Result;

                //    result = feedPostResponse.IsSuccessStatusCode;
                //}
                //catch (Exception ex)
                //{
                //    Debug.WriteLine(string.Format("Shokr : {0}", ex.Message));
                //}


            }
            return result;
        }

        public Task<bool> Retry()
        {
            throw new NotImplementedException();
            
            //var draftMemoWorker = new DraftMemoWorker(WorkerMode.Queue);
            //return draftMemoWorker.ProcessCreate(DraftMemo);
            
        }
    }
}
