using System.Diagnostics;

namespace DiscoveryChat
{
    class ChatClient : IChatClient
    {
        public delegate void GotMessageEventHandler(object sender, MessageEventArgs eventArgs);
        public static GotMessageEventHandler GotMessage;

        public void Send(string senderName, string message)
        {
            Trace.WriteLine(string.Format("Sender {0}: {1}", senderName, message));
            if (GotMessage != null)
            {
                GotMessage.Invoke(this, new MessageEventArgs(senderName, message));
            }
        }
    }
}
