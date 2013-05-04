using System.Diagnostics;
using System.ServiceModel;

namespace DiscoveryChat
{
    class ChatServer : IChatServer
    {
        /// <summary>
        /// The window to update on new messages.
        /// </summary>
        //public static ChatWindow ChatWindow { get; set; }

        public delegate void GotMessageEventHandler(object sender, MessageEventArgs eventArgs);
        public static event GotMessageEventHandler GotMessage;

        /// <summary>
        /// Callback for the latest client to call. A bit wierd to only keep the last one, 
        /// but this was quick to provide some functionality for demo purposes.
        /// </summary>
        public static IChatClient LastChatClient { get; private set; }

        public ChatServer()
        {            
        }

        private void OnMessage(MessageEventArgs e)
        {
            if (GotMessage != null)
            {
                GotMessage(this, e);
            }
        }

        public void Send(string senderName, string message)
        {
            Trace.WriteLine(string.Format("Sender {0}: {1}", senderName, message));
            OnMessage(new MessageEventArgs(senderName, message));

            LastChatClient = OperationContext.Current.GetCallbackChannel<IChatClient>();
        }
    }
}
