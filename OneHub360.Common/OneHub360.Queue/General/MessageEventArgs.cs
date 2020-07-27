using System;

namespace OneHub360.Queue
{
    public class MessageEventArgs : EventArgs
    {
        public bool ProcessingSucceeded { get; set; }
        public IMessage Message { get; set; }

        #region MessageReceivingEventArgs Public Methods
        /// <summary>
        /// Constructore to set the default Processing status
        /// </summary>
        public MessageEventArgs()
        {
            ProcessingSucceeded = false;
        }
        public MessageEventArgs(bool processingSucceeded)
        {
            ProcessingSucceeded = processingSucceeded;
        }
        #endregion
    }
}
