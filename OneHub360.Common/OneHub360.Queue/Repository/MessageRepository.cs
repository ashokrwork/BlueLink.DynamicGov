using System;
using System.Messaging;

namespace OneHub360.Queue
{
    /// <summary>
    /// Generic Class to handle the required functionality for dealing with one specified MSMQ Queue,
    /// and only accept one message type, 
    /// any other messages will considered as corrupted messages and sent to the dead messages queue
    /// </summary>
    public class MessageRepository<T> : MessageSender<T> where T : IMessage
    {
        #region MessageRepository Event Handlers
        #region MessageReceiving Event Handlers
        /// <summary>
        /// Fired before commit the receiving of a message, this handler shuold include the processing of the message
        /// </summary>
        public event EventHandler<MessageEventArgs> MessageReceiving;

        /// <summary>
        /// Execute the Receiving Handler
        /// </summary>
        /// <param name="e">Include a flag indicating if Processing Succeeded</param>
        protected virtual void OnMessageReceiving(MessageEventArgs e)
        {
            EventHandler<MessageEventArgs> messageReceivingHandler = MessageReceiving;

            // Execute the Event Handler if any, otherwise set the processing as succeeded
            if (messageReceivingHandler != null)
                messageReceivingHandler(this, e);
            else
                e.ProcessingSucceeded = true;
        }
        #endregion

        #region MessageReceived Event Handlers
        /// <summary>
        /// Fired after commit the receiving of a message.
        /// </summary>
        public event EventHandler MessageReceived;

        /// <summary>
        /// Execute the Received Handler
        /// </summary>
        /// <param name="e">Include a flag indicating if Processing Succeeded</param>
        protected virtual void OnMessageReceived(EventArgs e)
        {
            EventHandler messageReceivedHandler = MessageReceived;
            
            // Execute the Event Handler if any
            if (messageReceivedHandler != null)
                messageReceivedHandler(this, e);
        }
        #endregion
        #endregion

        

        #region MessageRepository Constructor
        /// <summary>
        /// The Repository Constructor
        /// </summary>
        /// <param name="mqPath">MSMQ Private Messages Queue Path, "MachineName\QueueName"</param>
        /// <param name="adminqPath">MSMQ Private Administration Queue Path, "MachineName\QueueName"</param>
        public MessageRepository(string mqPath, string adminqPath)
            : base(mqPath, adminqPath)
        {
            
        }
        #endregion

        #region MessageRepository Public Members
        /// <summary>
        /// Transactional Receiving from the MSMQ
        /// </summary>
        /// <returns>Return the Received Message from the MSMQ</returns>
        public T Receive()
        {
            // Initialize the MSMQ Queue Object 
            MessageQueue messageQueue = new MessageQueue(messageQueuePath, QueueAccessMode.SendAndReceive);
            MessageEventArgs messageReceivingEventArgs = new MessageEventArgs();
            object returnedMessage = null;
            Message message = new Message();

            try
            {
                // Initialize the Messages Formatter
                ((XmlMessageFormatter)messageQueue.Formatter).TargetTypes = messagesTypes;

                // Receive One Message from the MSMQ
                message = messageQueue.Receive();
                
                // Get the Message Body and Cast it to the specified type <T>
                returnedMessage = (T)message.Body;
            
                // Fire Receiving Event Handler if Any and initialize the Event Args
                messageReceivingEventArgs.Message = (T)returnedMessage;

                // Fire the Receiving Event
                this.OnMessageReceiving(messageReceivingEventArgs);

                // Check If Processing Succeeded
                if (messageReceivingEventArgs.ProcessingSucceeded)
                {
                    // Fire Sent Event Handler
                    EventArgs messageReceivedEventArgs = new EventArgs();

                    // Execute the Event handler(s)
                    this.OnMessageReceived(messageReceivedEventArgs);
                }
            }
            catch(InvalidCastException icex)
            {
                // Send a faliure response message 
            }
            catch (MessageQueueException mqex)
            {
                switch ((MessageQueueErrorCode)mqex.ErrorCode)
                {
                    case MessageQueueErrorCode.WriteNotAllowed:
                    case MessageQueueErrorCode.UnsupportedAccessMode:
                        // Can't Read/Write from this MSMQ
                        throw new System.Exception("Can't Read/Write within the Specified MSMQ Queue.");
                    case MessageQueueErrorCode.IllegalQueuePathName:
                        // Can't Read/Write from this MSMQ
                        throw new System.Exception("Specified MSMQ Queue path is incorrect.");
                    case MessageQueueErrorCode.IOTimeout:
                        // Connectivity or Performance Issues 
                        throw new System.Exception("Specified MSMQ Queue path is incorrect.");
                    case MessageQueueErrorCode.MachineNotFound:
                    case MessageQueueErrorCode.QueueNotAvailable:
                    case MessageQueueErrorCode.QueueNotFound:
                    case MessageQueueErrorCode.RemoteMachineNotAvailable:
                        throw new System.Exception("Specified MSMQ Queue path is not available.");
                    case MessageQueueErrorCode.AccessDenied:
                        throw new System.Exception("Current Context has no Permission to access the Specified MSMQ Queue path.");
                    default:
                        break;
                }
            }
            
            // Close the MSMQ Queue Connection
            messageQueue.Close();

            // If Operation Completed Succefully
            if (!messageReceivingEventArgs.ProcessingSucceeded)
            {
                // Adjust the processin iterations for this message 
                ((T)returnedMessage).ProcessingIterations++;

                // Insert the message back
                this.Send((T)returnedMessage);

                // Can't Complete the processing of this Message
                throw new System.Exception("Can't complete message processing.");
            }
            else
                // Return the Final result
                return (T)returnedMessage;
        }
        #endregion
    }
}
