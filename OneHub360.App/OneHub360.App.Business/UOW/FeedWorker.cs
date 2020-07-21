using OneHub360.App.DAL;
using OneHub360.App.Shared;
using System.Collections.Generic;
using OneHub360.Business;
using OneHub360.App.Business.Messages;
using System;
using NHibernate.Transform;

namespace OneHub360.App.Business
{
    public class FeedWorker : AppWorkerBase
    {


        public FeedWorker(WorkerMode mode) : base(mode)
        {
        }


        #region Actions

        public bool AddAction(UserAction action)
        {

            var userActionRepository = new UserActionRepository(Context);
            userActionRepository.Insert(action);

            return true;
        }

        public IList<UserActionView> GetThreadActions(Guid threadId)
        {
            long totalCount;
            IList<UserActionView> userActionsView;

            userActionsView = new UserActionViewRepository(Context).GetPaged(string.Format("ThreadId = '{0}'", threadId), "CreationDate", 0, int.MaxValue, out totalCount);

            return userActionsView;
        }

        #endregion

        #region Comments

        public Comment GetCommentModel()
        {
            return new Comment();
        }

        public bool AddComment(Comment comment)
        {

            comment.LastModified = DateTime.Now;
            comment.CreationDate = DateTime.Now;
            var commentRepository = new CommentRepository(Context);
            commentRepository.Insert(comment);

            

            return true;
        }

        public IList<CommentsView> GetFeedComments(Guid feedId, Guid userId)
        {
            long totalCount;
            IList<CommentsView> commentsView;

            var commentRepository = new CommentsViewRepository(Context);
            commentsView = commentRepository.GetPaged(string.Format("ThreadId = '{0}' and (Private = 0 or (Private = 1 and FK_User = '{1}'))", feedId.ToString().ToLower(), userId.ToString().ToLower()), "CreationDate", 0, int.MaxValue, out totalCount);



            return commentsView;
        }

        public long GetFeedCommentsCount(Guid feedId, Guid userId)
        {
            long feedCount;

            var commentRepository = new CommentsViewRepository(Context);
            feedCount = commentRepository.GetTotalItems(string.Format("ThreadId = '{0}' and (Private = 0 or (Private = 1 and FK_User = '{1}'))", feedId, userId));


            return feedCount;
        }
        #endregion

        #region Feed
        public virtual void UpdateFeed(Guid feedId, dynamic feedNewValues)
        {

            var feedRepository = new FeedRepository(Context);
            feedRepository.DynamicUpdate(feedId, feedNewValues);


        }

        public virtual bool Create(Feeds feed)
        {
            var result = false;

            if (string.IsNullOrEmpty(feed.SharedWith))
                feed.SharedWith = "[]";

            switch (Mode)
            {
                case WorkerMode.Queue:
                    var createFeedMessage = new CreateFeedMessage();
                    createFeedMessage.Feed = feed;
                    result = QueueWorkMessage(createFeedMessage);
                    break;
                case WorkerMode.NonQueue:

                    var feedRepository = new FeedRepository(Context);
                    feedRepository.Insert(feed);
                    result = true;


                    break;
            }

            return result;
        }
        public virtual IEnumerable<TimeLineView> GetPagedTimeLineView(string whereClause, int pagenum, int pagelength)
        {
            IEnumerable<TimeLineView> timeLineView;
            var timeLineViewRepository = new TimeLineViewRepository(Context);
            long totalCount;
            timeLineView = timeLineViewRepository.GetPaged(whereClause, "CreationDate desc", pagelength * (pagenum - 1), pagelength, out totalCount);
            return timeLineView;
        }
        public virtual long GetPagesCount(string whereClause)
        {
            var timeLineViewRepository = new TimeLineViewRepository(Context);
            long totalCount;
            totalCount = timeLineViewRepository.GetTotalItems(whereClause);
            return totalCount;
        }
        public virtual IEnumerable<TimeLineView> GetTimeLineView(string whereClause)
        {
            IEnumerable<TimeLineView> timeLineView;

            var timeLineViewRepository = new TimeLineViewRepository(Context);
            long totalCount;
            timeLineView = timeLineViewRepository.GetPaged(whereClause, "CreationDate desc", 0, int.MaxValue, out totalCount);



            return timeLineView;
        }
        public virtual Feeds Get()
        {
            return new Feeds() { Language = DB.Language.AR, Status = 1, CreatedBy = "Test user" };
        }

        public virtual IEnumerable<FeedTypes> GetNewItemFeedTypes()
        {
            IEnumerable<FeedTypes> feedTypes;

            var feedTypeRepository = new FeedTypeRepository(Context);
            long totalCount;
            feedTypes = feedTypeRepository.GetPaged("ShowInNewItemStrip=true", string.Empty, 0, 10, out totalCount);


            return feedTypes;
        }

        #endregion

        #region Sharing
        public virtual SharingView GetSharing(Guid feedItemId)
        {
            SharingView sharingView;

            var sharingViewRepository = new SharingViewRepository(Context);
            sharingView = sharingViewRepository.GetById(feedItemId);


            return sharingView;
        }

        public virtual void UpdateSharing(SharingView sharingData)
        {

            var sharingViewRepository = new SharingViewRepository(Context);
            sharingViewRepository.Update(sharingData);

            try
            {



                var message = "تمت مشاركة مراسلة معك, للإطلاع يرجي الدخول علي نظام المراسلات و التوقيع الإلكتروني (OneHub360)";

                var telegramClient = new TelegramAPIClient();
                telegramClient.SendMessage(message);
            }
            catch { }
        }


        #endregion

        #region Notifications

        public bool AddNotification(Notification notification)
        {
            var notificationRepository = new NotificationRepository(Context);

            notificationRepository.Insert(notification);

            return true;
        }

        public IList<Notification> GetUserNotifications(Guid userId)
        {
            long totalCount;
            IList<Notification> userNotifications;

            var notificationsRepository = new NotificationRepository(Context);

            userNotifications = notificationsRepository.GetPaged(string.Format("FK_User = '{0}'", userId), "CreationDate", 0, int.MaxValue, out totalCount);




            return userNotifications;
        }



        #endregion

        #region Search

        public IList<TimeLineView> KeyWordSearch(SearchCriteria searchCriteria)
        {
            var timeLineRepository = new TimeLineViewRepository(Context);
            return timeLineRepository.KeyWordSearch(searchCriteria);

        }

        #endregion

    }
}
