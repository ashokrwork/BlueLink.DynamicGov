using Newtonsoft.Json;
using OneHub360.App.Shared;
using OneHub360.CMS.DAL;
using OneHub360.DB;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace OneHub360.CMS.Business
{
    public class FeedApiClient : HttpClient
    {
        /// <summary>
        /// 
        /// Scope = 0 mean only creator
        /// Scope = 1 means only for From
        /// Scope = 2 means only for To
        /// Scope = 3 means for both
        /// </summary>
        public FeedApiClient()
        {
            BaseAddress = new Uri(CMSConfigLoader.Generator.configData.CMSServiceBaseUrl);
        }
        #region User
        public Task<UserInfoAutoComplete> GetUser(string userId)
        {
            var response = this.GetStringAsync(string.Format("api/user/aviator/{0}", userId));
            return Task.FromResult(JsonConvert.DeserializeObject<UserInfoAutoComplete>(response.Result));
        }

        public Task<Signature> GetUserSignature(string userId)
        {
            var response = this.GetStringAsync(string.Format("api/user/signature/{0}", userId));
            return Task.FromResult(JsonConvert.DeserializeObject<Signature>(response.Result));
        }

        #endregion
        #region Mappig
        public Feeds GenerateFromDraftMemo(DraftMemo draftMemo)
        {
            var feed = new Feeds();

            feed.Id = draftMemo.Id;
            feed.CreatedBy = draftMemo.CreatedBy;
            feed.CreationDate = draftMemo.CreationDate;
            feed.Brief = draftMemo.Brief;
            feed.Title = draftMemo.Subject;
            feed.FeedId = draftMemo.Id.ToString();
            feed.Status = 1;
            feed.Scope = 1;
            feed.Pinned = false;
            feed.Date = DateTime.Now;
            feed.Number1 = 0;
            feed.Language = DB.Language.AR;
            feed.Priority = 1;
            feed.FK_From = Guid.Parse(draftMemo.From);
            feed.FK_To = Guid.Parse(draftMemo.To);
            feed.FeedTypes = new FeedTypes { CreatedBy = draftMemo.CreatedBy, Id = ModuleConstants.DraftMemoType };
            feed.Text3 = draftMemo.ThreadId.ToString();
            return feed;
        }

        public Feeds GenerateFromDraftLetter(DraftLetter draftLetter)
        {
            var feed = new Feeds();

            feed.Id = draftLetter.Id;
            feed.CreatedBy = draftLetter.CreatedBy;
            feed.CreationDate = draftLetter.CreationDate;
            feed.Brief = draftLetter.Brief;
            feed.Title = draftLetter.Subject;
            feed.FeedId = draftLetter.Id.ToString();
            feed.Status = 1;
            feed.Scope = 1;
            feed.Pinned = false;
            feed.Date = DateTime.Now;
            feed.Number1 = 0;
            feed.Language = DB.Language.AR;
            feed.Priority = 1;
            feed.FK_From = Guid.Parse(draftLetter.From);
            feed.FK_To = Guid.Parse(draftLetter.To);
            feed.FeedTypes = new FeedTypes { CreatedBy = draftLetter.CreatedBy, Id = ModuleConstants.DraftLetterType };
            feed.Text3 = draftLetter.ThreadId.ToString();
            return feed;
        }

        public Feeds GenerateFromOutgoingLetter(OutgoingLetter outgoingLetter)
        {
            var feed = new Feeds();

            feed.Id = outgoingLetter.Id;
            feed.CreatedBy = outgoingLetter.CreatedBy;
            feed.CreationDate = outgoingLetter.CreationDate;
            feed.Brief = outgoingLetter.Brief;
            feed.Title = outgoingLetter.Subject;
            feed.FeedId = outgoingLetter.Id.ToString();
            feed.Status = 1;
            feed.Scope = 1;
            feed.Pinned = false;
            feed.Date = DateTime.Now;
            feed.Number1 = 0;
            feed.Language = DB.Language.AR;
            feed.Priority = 1;
            feed.FK_From = Guid.Parse(outgoingLetter.From);
            feed.FK_To = Guid.Parse(outgoingLetter.To);
            feed.FeedTypes = new FeedTypes { CreatedBy = outgoingLetter.CreatedBy, Id = ModuleConstants.OutgoingLetterType };
            feed.Text3 = outgoingLetter.ThreadId.ToString();
            return feed;
        }

        public Feeds GenerateFromIncomingLetter(IncomingLetter incomingLetter)
        {
            var feed = new Feeds();

            feed.Id = incomingLetter.Id;
            feed.CreatedBy = incomingLetter.CreatedBy;
            feed.CreationDate = incomingLetter.CreationDate;
            feed.Brief = incomingLetter.Brief;
            feed.Title = incomingLetter.Subject;
            feed.FeedId = incomingLetter.Id.ToString();
            feed.Status = incomingLetter.Status;
            feed.Scope = 0;
            feed.Pinned = false;
            feed.Date = DateTime.Now;
            feed.Number1 = 0;
            feed.Language = DB.Language.AR;
            feed.Priority = 1;
            feed.FK_From = Guid.Parse(incomingLetter.From);
            feed.FK_To = Guid.Parse(incomingLetter.To);
            feed.FeedTypes = new FeedTypes { CreatedBy = incomingLetter.CreatedBy, Id = ModuleConstants.IncomingLetterType };
            feed.Text3 = incomingLetter.ThreadId.ToString();
            return feed;
        }

        public UserAction InitAction(HttpRequest request,CreationInfo creationInfo)
        {
            var userAction = new UserAction
            {
                BrowserType = HttpContext.Current.Request.UserAgent,
                CreatedBy = creationInfo.CreatedBy,
                CreationDate = creationInfo.CreationDate,
                Language = creationInfo.Language,
                MachineName = request.UserHostName,
                MachineIP = request.UserHostAddress,
                ServerName = request.UserHostName,
                RequestUrl = request.Url.PathAndQuery,
                LastModified = creationInfo.LastModified,
                IsDeleted = creationInfo.IsDeleted
            };

            return userAction;
        }

        public Feeds GenerateFromOutgoingMemo(OutgoingMemo outgoingMemo)
        {
            var feed = new Feeds();
            feed.Id = outgoingMemo.Id;
            feed.CreatedBy = outgoingMemo.CreatedBy;
            feed.CreationDate = outgoingMemo.CreationDate;
            feed.Brief = outgoingMemo.Brief;
            feed.Title = outgoingMemo.Subject;
            feed.FeedId = outgoingMemo.Id.ToString();
            feed.Status = 1;
            feed.Scope = 1;
            feed.Pinned = false;
            feed.Date = DateTime.Now;
            feed.Text1 = outgoingMemo.OutgoingNumber;
            feed.Date1 = outgoingMemo.OutgoingDate;
            feed.Language = DB.Language.AR;
            feed.Priority = 1;
            feed.FK_From = Guid.Parse(outgoingMemo.From);
            feed.FK_To = Guid.Parse(outgoingMemo.To);
            feed.FeedTypes = new FeedTypes { CreatedBy = outgoingMemo.CreatedBy, Id = ModuleConstants.OutgoingMemoType };
            feed.Text3 = outgoingMemo.ThreadId.ToString();
            return feed;
        }

        public Feeds GenerateFromIncomingMemo(IncomingMemo incomingMemo)
        {
            var feed = new Feeds();
            feed.Id = incomingMemo.Id;
            feed.CreatedBy = incomingMemo.CreatedBy;
            feed.CreationDate = incomingMemo.CreationDate;
            feed.Brief = incomingMemo.Brief;
            feed.Title = incomingMemo.Subject;
            feed.FeedId = incomingMemo.Id.ToString();
            feed.Status = 1;
            feed.Scope = 2;
            feed.Pinned = false;
            feed.Date = DateTime.Now;
            feed.Text1 = incomingMemo.IncomingNumber;
            feed.Date1 = incomingMemo.IncomingDate;
            feed.Language = DB.Language.AR;
            feed.Priority = 1;
            feed.FK_From = Guid.Parse(incomingMemo.From);
            feed.FK_To = Guid.Parse(incomingMemo.To);
            feed.FeedTypes = new FeedTypes { CreatedBy = incomingMemo.CreatedBy, Id = ModuleConstants.IncomingMemoType };
            feed.Text3 = incomingMemo.ThreadId.ToString();
            return feed;
        }

        //public Feeds GenerateFromReport(Report report)
        //{

        //}

        #endregion
        #region Operations

        public Task<bool> InsertBatch(EntitiesBatchInsert entities)
        {
            var result = false;

            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var feedPostResponse = this.PostAsJsonAsync("api/feed/createbatch", entities).Result;

                result = feedPostResponse.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
            }

            return Task.FromResult(result);
        }

       


        public Task<bool> InsertFeed(Feeds feed)
        {
            var result = false;

            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var feedPostResponse = this.PostAsJsonAsync("api/feed/create", feed).Result;

                result = feedPostResponse.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return Task.FromResult(result);
        }

        public Task<bool> UpdateFeed(Dictionary<string, object> newValues)
        {
            var result = false;

            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var feedPostResponse = this.PostAsJsonAsync("api/feed/partialupdate", newValues).Result;

                result = feedPostResponse.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return Task.FromResult(result);
        }

        public Task<bool> InsertAction(UserAction action)
        {
            var result = false;

            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var feedPostResponse = this.PostAsJsonAsync("api/feed/action/add", action).Result;

                result = feedPostResponse.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return Task.FromResult(result);
        }

        #endregion
    }
}
