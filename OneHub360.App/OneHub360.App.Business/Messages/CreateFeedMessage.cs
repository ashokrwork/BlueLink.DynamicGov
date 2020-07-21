using OneHub360.App.Shared;
using OneHub360.Business;
using OneHub360.Queue;
using System;
using System.Threading.Tasks;

namespace OneHub360.App.Business.Messages
{
    public  class CreateFeedMessage : IMessage
    {

        public virtual Feeds Feed { get; set; }

        public virtual string CreatedBy
        {
            get; set;
        }

        public virtual DateTime CreationDate
        {
            get; set;
        }

        public virtual int ProcessingIterations
        {
            get; set;
        }

        public virtual Task<bool> Process()
        {
            var result = true;
            
            var feedWorker = new FeedWorker(WorkerMode.NonQueue);
            if (!feedWorker.Create(Feed))
            {
                result = false;
                Retry();
            }
            return Task<bool>.FromResult(result);
        }

        public virtual Task<bool> Retry()
        {
            var result = true;
            
            var feedWorker = new FeedWorker(WorkerMode.Queue);
            result = feedWorker.Create(Feed);

            return Task.FromResult(result);
        }
    }
}
