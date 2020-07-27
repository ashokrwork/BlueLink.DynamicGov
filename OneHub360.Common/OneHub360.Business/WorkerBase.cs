using OneHub360.DB;
using OneHub360.Queue;
using System;

namespace OneHub360.Business
{
    public class WorkerBase
    {
        protected virtual string LOCAL_ACK_QUEUE { get; }// ".\\Private$\\appakc";
        protected virtual string LOCAL_QUEUE { get; } // ".\\Private$\\app";

        public WorkerMode Mode { get; set; }

        public IDBContext Context { get; set; }

        public WorkerBase(WorkerMode mode)
        {
            Mode = mode;
        }

        

        public bool FinishWork()
        {
            var result = true;
            try
            {
                Context.Transaction.Commit();
            }
            catch(Exception ex)
            {
                Context.Transaction.Rollback();
                result = false;
            }

            return result;
        }

        protected bool QueueWorkMessage(IMessage message)
        {
            var messageRepository = new MessageRepository<IMessage>(LOCAL_QUEUE, LOCAL_ACK_QUEUE);
            messageRepository.Send(message);
            return true;
        }

        protected IMessage RecieveWorkMessage()
        {
            var messageRepository = new MessageRepository<IMessage>(LOCAL_QUEUE, LOCAL_ACK_QUEUE);
            return messageRepository.Receive();
        }
    }
}
