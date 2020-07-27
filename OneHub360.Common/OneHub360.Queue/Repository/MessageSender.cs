using System;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Messaging;
using System.Reflection;

namespace OneHub360.Queue
{
    /// <summary>
    /// Generic Class to handle the required functionality for dealing with one specified MSMQ Queue,
    /// and only accept one message type, 
    /// any other messages will considered as corrupted messages and sent to the dead messages queue
    /// </summary>
    public class MessageSender<T> where T : IMessage
    {
        #region MessageSender Public Properties
        [DefaultValue(false)]
        public bool IsAuthenticated { get; set; }

        [DefaultValue(false)]
        public bool IsEncrypted { get; set; }

        /// <summary>
        /// Get the total number of Message within the Queue
        /// </summary>
        public long Count
        {
            get
            {
                // Initialize the MSMQ Queue Object 
                MessageQueue messageQueue = new MessageQueue(messageQueuePath, QueueAccessMode.Peek);

                // Initialize Counter and Queue Cursor
                long count = 0;
                Cursor cursor = messageQueue.CreateCursor();

                Message m = messageQueue.Peek(new TimeSpan(1), cursor, PeekAction.Current);
                if (m != null)
                {
                    count = 1;
                    try
                    {
                        while ((m = messageQueue.Peek(new TimeSpan(1), cursor, PeekAction.Next)) != null)
                        {
                            count++;
                        }
                    }
                    catch { }
                }
                return count;
            }
        }
        #endregion

        #region MessageRepository Protected Properties
        /// <summary>
        /// Arrab include all the Messages types included supported
        /// </summary>
        protected Type[] messagesTypes;
        #endregion

        #region MessageSender Private Fields
        /// <summary>
        /// Private Message Queue Path (Locally / Remotely)
        /// </summary>
        protected string messageQueuePath;

        /// <summary>
        /// The Acknowledgement Message will be saved in that Queue Path 
        /// </summary>
        protected string administrationQueuePath;
        #endregion

        #region MessageSender Event Handlers
        #region MessageSendingFailed Event Handlers
        /// <summary>
        /// Fired after Sending a message failed.
        /// </summary>
        public event EventHandler MessageSendingFailed;

        /// <summary>
        /// Execute the Message Sending Failed Handler
        /// </summary>
        protected virtual void OnMessageSendingFailed(MessageEventArgs e)
        {
            EventHandler messageSendingFailedHandler = MessageSendingFailed;

            // Execute the Event Handler if any
            if (messageSendingFailedHandler != null)
                messageSendingFailedHandler(this, e);
        }
        #endregion

        #region MessageSent Event Handlers
        /// <summary>
        /// Fired after commit the message sending.
        /// </summary>
        public event EventHandler MessageSent;

        /// <summary>
        /// Execute the Sent Handler
        /// </summary>
        protected virtual void OnMessageSent(EventArgs e)
        {
            EventHandler messageSentHandler = MessageSent;
            
            // Execute the Event Handler if any
            if (messageSentHandler != null)
                messageSentHandler(this, e);
        }
        #endregion

        //TODO : Add Custom EventArgs include the Exception and avoid any exceptions within this class
        #region Error Event Handler
        /// <summary>
        /// Fired on case of exceptions or errors.
        /// </summary>
        public event EventHandler Error;

        /// <summary>
        /// Execute the Error Occurred Handler
        /// </summary>
        protected virtual void OnError(EventArgs e)
        {
            EventHandler errorOccurred = Error;

            // Execute the Event Handler if any
            if (errorOccurred != null)
                errorOccurred(this, e);
        }
        #endregion
        #endregion

        #region MessageSender Constructor
        /// <summary>
        /// The Repository Constructor
        /// </summary>
        /// <param name="mqPath">MSMQ Private Messages Queue Path, "MachineName\QueueName"</param>
        /// <param name="adminqPath">MSMQ Private Administration Queue Path, "MachineName\QueueName"</param>
        public MessageSender(string mqPath, string adminqPath)
        {
            // https://msdn.microsoft.com/en-us/library/system.messaging.messagequeue.exists(v=vs.110).aspx

            // Set the Message Queue Paths
            this.messageQueuePath = mqPath;
            this.administrationQueuePath = adminqPath;

            // Initialize the Types
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["typesAssembly"]))
            {
                var typesAssesmlies = ConfigurationManager.AppSettings["typesAssembly"].Split(',');
                foreach (string typesAssembly in typesAssesmlies)
                {
                    Assembly messagesLibrary = Assembly.Load(typesAssembly);
                    messagesTypes = (from t in messagesLibrary.GetExportedTypes() where t.IsClass && (t.GetInterfaces().FirstOrDefault<Type>() == typeof(IMessage)) select t).ToArray<Type>();
                }
            }
            else
            {
                // If no registered Assembley in the configuration file will load the embeded types only
                messagesTypes = (from t in Assembly.GetExecutingAssembly().GetExportedTypes() where t.IsClass && (t.GetInterfaces().FirstOrDefault<Type>() == typeof(IMessage)) select t).ToArray<Type>();
            }

            #region Not Support Implementation for Remote Queues
            /*if (MessageQueue.Exists(mqPath))
                this.messageQueuePath = mqPath;
            else
                throw new System.Exception("Invalid or Inaccessible MSMQ Messages Queue Path.");


            // Validate & Set the Administration Message Queue Path
            if (MessageQueue.Exists(adminqPath))
                this.administrationQueuePath = adminqPath;
            else
                throw new System.Exception("Invalid or Inaccessible MSMQ Administration Queue Path.");*/
            #endregion
        }
        #endregion

        #region MessageSender Public Methods
        /// <summary>
        /// Transactional Sending a new Message to the MSMQ
        /// </summary>
        /// <param name="message">The Message to be added into the MSMQ</param>
        /// <returns>True if Message sent to the queue, False if it failed</returns>
        public void Send(T message)
        {
            // Initialize the MSMQ Queue Object 
            MessageQueue messageQueue = new MessageQueue(messageQueuePath, QueueAccessMode.Send);

            // Initialize the Transaction Object 
            MessageQueueTransaction messageQueueTransaction = new MessageQueueTransaction();

            // If Can write to the MSMQ Start the Transaction
            if(messageQueue.CanWrite)
            {
                // Begin the Transaction
                messageQueueTransaction.Begin();

                // Build the Queue Message to send it to the MSMQ Queue
                Message queueMessage = new Message(message);
                queueMessage.Recoverable = true;
                queueMessage.AdministrationQueue = new MessageQueue(administrationQueuePath);
                queueMessage.AcknowledgeType = AcknowledgeTypes.FullReachQueue;
                queueMessage.UseDeadLetterQueue = true;
                queueMessage.UseAuthentication = this.IsAuthenticated;
                //queueMessage.UseEncryption = true;

                // Send the Full Message to the MSMQ
                messageQueue.Send(queueMessage, messageQueueTransaction);

                try
                {
                    // Commit the transaction
                    messageQueueTransaction.Commit();

                    // Close the MSMQ Queue Connection
                    messageQueue.Close();

                    // Fire Sent Event Handler
                    EventArgs messageSentEventArgs = new EventArgs();
                    this.OnMessageSent(messageSentEventArgs);
                }
                catch(Exception ex)
                {
                    // Roll Back the transaction
                    messageQueueTransaction.Abort();

                    // Close the MSMQ Queue Connection
                    messageQueue.Close();

                    // Can't Write to this MSMQ
                    throw new System.Exception("Error occurred when sending a Message.");
                }
                #region Sending & Sent Event Handlers
                /*
                // Fire Sending Event Handler if Any, otherwise set the processing as succeeded
                MessageEventArgs messageSendingEventArgs = new MessageEventArgs();
                this.OnMessageSending(messageSendingEventArgs);

                // Check If Processing Succeeded
                if (messageSendingEventArgs.ProcessingSucceeded)
                {
                    // Commit/Abort the transaction
                    messageQueueTransaction.Commit();

                    // Fire Sent Event Handler
                    EventArgs messageSentEventArgs = new EventArgs();
                    this.OnMessageSent(messageSentEventArgs);

                    // Set the returned variable to indicate that the Operation Completed Successfully
                    ret = true;
                }
                else
                {
                    // Roll Back all changes, message is not sent any more
                    messageQueueTransaction.Abort();
                    
                    // Set the returned variable to indicate that the Operation Failed
                    ret = false;
                }*/
                #endregion
            }
            else
            {
                // Close the MSMQ Queue Connection
                messageQueue.Close();

                // Can't Write to this MSMQ
                throw new System.Exception("Can't Write to the Specified MSMQ Queue.");
            }

            
        }
        
        /// <summary>
        /// Get the Number of Messages from one selected Type
        /// </summary>
        /// <param name="type">Message Type to be counted</param>
        /// <returns>Number of Messages</returns>
        public long GetCount(Type type)
        {
            // Initialize the MSMQ Queue Object 
            MessageQueue messageQueue = new MessageQueue(messageQueuePath, QueueAccessMode.Peek);

            // Initialize the Messages Formatter
            ((XmlMessageFormatter)messageQueue.Formatter).TargetTypes = messagesTypes;

            // Initialize Counter and Queue Cursor
            long count = 0;
            Cursor cursor = messageQueue.CreateCursor();
            Message m;
            try
            {
                m = messageQueue.Peek(new TimeSpan(1), cursor, PeekAction.Current);

                if (m != null)
                {
                    count = 1;
                    //try
                    {
                        while ((m = messageQueue.Peek(new TimeSpan(1), cursor, PeekAction.Next)) != null)
                        {
                            if (m.Body.GetType() == type)
                                count++;
                        }
                    }
                    //catch { }
                }
            }
            catch { }
            
            return count;
        }
        #endregion
    }
}
