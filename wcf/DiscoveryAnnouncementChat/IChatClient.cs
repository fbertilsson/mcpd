using System.ServiceModel;

namespace DiscoveryAnnouncementChat
{
    [ServiceContract(CallbackContract = typeof(IChatClient))]
    interface IChatServer
    {
        [OperationContract]
        void Send(string senderName, string message);
    }

    [ServiceContract]
    interface IChatClient
    {
        [OperationContract]
        void Send(string senderName, string message);
    }
}
