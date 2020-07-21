using OneHub360.App.Business;
using OneHub360.App.Services.Properties;
using OneHub360.App.Shared;
using OneHub360.Authentication.Context;
using System.Collections.Generic;
using System.Web.Http;
using System;
using Newtonsoft.Json.Linq;
using OneHub360.App.DAL;
using System.Security.Principal;

namespace OneHub360.App.Services.Controllers
{
    [RoutePrefix("api/feed")]
    public class FeedController : ApiController
    {
        [HttpGet]
        [Route("warm")]
        public virtual IEnumerable<TimeLineView> Warmup()
        {
            using (var feedWorker = new FeedWorker(Settings.Default.WorkerMode))
            {
                return feedWorker.GetTimeLineView(string.Empty);
            }
            //return DateTime.Now.ToString();
        }

        [HttpPost]
        [Route("search")]
        public virtual IEnumerable<TimeLineView> SimpleSearch(SearchCriteria searchCriteria)
        {
            var currentUserId = ClaimsContext.GetCurrentUserId(Request);

            searchCriteria.UserId = currentUserId;

            using (var feedWorker = new FeedWorker(Settings.Default.WorkerMode))
            {
                return feedWorker.KeyWordSearch(searchCriteria);
            }
            //return DateTime.Now.ToString();
        }

        [HttpGet]
        [Route("user")]
        public virtual string CurrentUser()
        {
            return this.User.Identity.Name;
        }
        [HttpPost]
        [Route("create")]
        public virtual bool Create(Feeds feed)
        {
            bool result;
            using (var feedWorker = new FeedWorker(Settings.Default.WorkerMode))
            {
                result = feedWorker.Create(feed);
            }
            return result;
        }



        [HttpPost]
        [Route("createbatch")]
        public virtual bool CreateBatch(EntitiesBatchInsert entities)
        {
            var context = new OneHubContext(true);
            using (var feedWorker = new FeedWorker(Settings.Default.WorkerMode))
            {

                foreach (var feed in entities.feeds)
                {
                    feedWorker.Create(feed);
                }

                foreach (var action in entities.actions)
                {
                    feedWorker.AddAction(action);
                }
            }
            return true;
        }

        [HttpGet]
        [Route("comment/getmodel")]
        public Comment GetCommentModel()
        {
            Comment result;
            using (var feedWorker = new FeedWorker(Settings.Default.WorkerMode))
            {
                result = feedWorker.GetCommentModel();
            }
            return result;
        }

        [HttpPost]
        [Route("action/add")]
        public bool AddAction(UserAction action)
        {
            bool result;
            using (var feedWorker = new FeedWorker(Settings.Default.WorkerMode))
            {
                result = feedWorker.AddAction(action);
            }
            return result;
        }

        [HttpGet]
        [Route("actions/{thread}")]
        public IEnumerable<UserActionView> GetActions(Guid thread)
        {
            IEnumerable<UserActionView> result;
            using (var feedWorker = new FeedWorker(Settings.Default.WorkerMode))
            {
                result = feedWorker.GetThreadActions(thread);
            }
            return result;
        }

        [HttpPost]
        [Route("comment/add")]
        public bool AddComment(Comment comment)
        {
            bool result;
            using (var feedWorker = new FeedWorker(Settings.Default.WorkerMode))
            {
                result = feedWorker.AddComment(comment);
            }
            return result;
        }

        [HttpGet]
        [Route("comment/getall/{feedId}/{userId}")]
        public IList<CommentsView> GetFeedComments(Guid feedId, Guid userId)
        {
            using (var feedWorker = new FeedWorker(Settings.Default.WorkerMode))
            {
                return feedWorker.GetFeedComments(feedId, userId);
            }
        }

        [HttpGet]
        [Route("comment/getcount/{feedId}/{userId}")]
        public long GetFeedCommentsCount(Guid feedId, Guid userId)
        {
            using (var feedWorker = new FeedWorker(Settings.Default.WorkerMode))
            {
                return feedWorker.GetFeedCommentsCount(feedId, userId);
            }
        }


        [HttpGet]
        [Route("{option}/getall")]
        [Authorize]
        public virtual IEnumerable<TimeLineView> GetAll(int option)
        {
            var currentUserId = ClaimsContext.GetCurrentUserId(Request);
            //Scope = 0 السرية
            //
            var whereClause = string.Format("(SharedWith like '%{0}%') or (Scope = 0 and CreatedBy = '{0}') or (Scope = 1 and FK_From = '{0}') or (Scope=2 and FK_To = '{0}') or (Scope=3 and(FK_From = '{0}' or FK_To = '{0}'))", currentUserId);

            switch (option)
            {
                case 0: // Load Drafts
                    whereClause = string.Format("({0}) and FeedTypeId='23722766-6DD5-4CB3-8989-22655038300E'", whereClause);
                    break;
                case 1: // Load Incoming
                    whereClause = string.Format("({0}) and FeedTypeId='98B2986D-542D-4959-9317-3F3B398CCF3E'", whereClause);
                    break;
                case 2: // Load Outgoing
                    whereClause = string.Format("({0}) and FeedTypeId='38C15649-3D62-42A7-8B53-979A763F4055'", whereClause);
                    break;
            }

            //Add your Code for filter and paging

            using (var feedWorker = new FeedWorker(Settings.Default.WorkerMode))
            {
                return feedWorker.GetTimeLineView(whereClause);
            }

        }
        public string filterstring(filteroption filteroption)
        {
            var currentUserId = ClaimsContext.GetCurrentUserId(Request);
            var whereClause = string.Format("(SharedWith like '%{0}%') or (Scope = 0 and CreatedBy = '{0}') or (Scope = 1 and FK_From = '{0}') or (Scope=2 and FK_To = '{0}') or (Scope=3 and(FK_From = '{0}' or FK_To = '{0}'))", currentUserId);
            if (filteroption != null)
            {
                switch (filteroption.datatype)
                {
                    case 0: // Load Drafts
                        if (filteroption.filtertype.ToUpper() == "ALL")
                            whereClause = string.Format("({0}) and FeedTypeId in ('23722766-6DD5-4CB3-8989-22655038300E','3D2ABCA4-40DD-4B07-8782-B287F2002CD3')", whereClause);
                        else if (filteroption.filtertype.ToUpper() == "EXTERNAL")
                            whereClause = string.Format("({0}) and FeedTypeId in ('3D2ABCA4-40DD-4B07-8782-B287F2002CD3')", whereClause);
                        else if (filteroption.filtertype.ToUpper() == "INTERNAL")
                            whereClause = string.Format("({0}) and FeedTypeId in ('23722766-6DD5-4CB3-8989-22655038300E')", whereClause);
                        break;
                    case 1: // Load Incoming
                        if (filteroption.filtertype.ToUpper() == "ALL")
                            whereClause = string.Format("({0}) and FeedTypeId in ('98B2986D-542D-4959-9317-3F3B398CCF3E','C6C08AF1-DDB2-4ED0-A0B3-F5C79BD85F29')", whereClause);
                        else if (filteroption.filtertype.ToUpper() == "EXTERNAL")
                            whereClause = string.Format("({0}) and FeedTypeId in ('C6C08AF1-DDB2-4ED0-A0B3-F5C79BD85F29')", whereClause);
                        else if (filteroption.filtertype.ToUpper() == "INTERNAL")
                            whereClause = string.Format("({0}) and FeedTypeId in ('98B2986D-542D-4959-9317-3F3B398CCF3E')", whereClause);

                        break;
                    case 2: // Load Outgoing
                        if (filteroption.filtertype.ToUpper() == "ALL")
                            whereClause = string.Format("({0}) and FeedTypeId in ('38C15649-3D62-42A7-8B53-979A763F4055','16E26CF7-39DC-4962-859C-B6A20B4D0FFF')", whereClause);
                        else if (filteroption.filtertype.ToUpper() == "EXTERNAL")
                            whereClause = string.Format("({0}) and FeedTypeId in ('16E26CF7-39DC-4962-859C-B6A20B4D0FFF')", whereClause);
                        else if (filteroption.filtertype.ToUpper() == "INTERNAL")
                            whereClause = string.Format("({0}) and FeedTypeId in ('38C15649-3D62-42A7-8B53-979A763F4055')", whereClause);
                        break;
                }
                //if (!filteroption.isfirstload)
                //{
                //    if (!string.IsNullOrEmpty(filteroption.Fromuser))
                //        whereClause += string.Format(" and (FK_From = '{0}')", filteroption.Fromuser);
                //    if (!string.IsNullOrEmpty(filteroption.Touser))
                //        whereClause += string.Format(" and (FK_To = '{0}')", filteroption.Touser);
                //    if (filteroption.Fromdate > DateTime.Now.AddYears(-1))
                //        whereClause += string.Format(" and (CreationDate >= '{0}')", filteroption.Fromdate);
                //    if (filteroption.Todate > DateTime.Now.AddDays(1))
                //        whereClause += string.Format(" and (CreationDate <= '{0}')", filteroption.Todate);
                //    if (filteroption.status != 0)
                //        whereClause += string.Format(" and (Status ={0})", filteroption.status);
                //}
            }

            return whereClause;
        }
        [HttpPost]
        [Route("filter")]
        [Authorize]
        public virtual IEnumerable<TimeLineView> filter(filteroption filteroption)
        {
            using (var feedWorker = new FeedWorker(Settings.Default.WorkerMode))
            {
                //return feedWorker.GetTimeLineView(whereClause);
                return feedWorker.GetPagedTimeLineView(filterstring(filteroption), filteroption.pageNumber, filteroption.PageLength);
            }
        }
        [HttpPost]
        [Route("getcount")]
        public virtual double Getcount(filteroption filteroption)
        {
            using (var feedWorker = new FeedWorker(Settings.Default.WorkerMode))
            {
                //return feedWorker.GetTimeLineView(whereClause);
                double Count = feedWorker.GetPagesCount(filterstring(filteroption));
                double pagescount = Math.Ceiling(Count / filteroption.PageLength);
                if (pagescount < 0)
                    pagescount = 0;
                return pagescount;
            }
        }
        [HttpGet]
        [Route("getsharing/{feedItemId}")]
        public virtual SharingView GetSharing(Guid feedItemId)
        {
            using (var feedWorker = new FeedWorker(Settings.Default.WorkerMode))
            {
                return feedWorker.GetSharing(feedItemId);
            }
        }
        [HttpPost]
        [Route("updatesharing")]
        public virtual bool UpdateSharing(SharingView feedSharingData)
        {
            using (var feedWorker = new FeedWorker(Settings.Default.WorkerMode))
            {
                feedWorker.UpdateSharing(feedSharingData);
            }
            return true;
        }

        [HttpPost]
        [Route("partialupdate")]
        public virtual void UpdateFeed(Dictionary<string, object> values)
        {
            using (var feedWorker = new FeedWorker(Settings.Default.WorkerMode))
            {
                var feedId = Guid.Parse(values["Id"].ToString());
                values.Remove("Id");
                feedWorker.UpdateFeed(feedId, values);
            }
        }

    }
}
