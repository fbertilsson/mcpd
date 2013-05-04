namespace DiscoveryAnnouncementChat
{
    class MessageEventArgs
    {
        public MessageEventArgs(string senderName, string message)
        {
            SenderName = senderName;
            Message = message;
        }
        public string SenderName { get; private set; }
        public string Message { get; private set; }
    }
}