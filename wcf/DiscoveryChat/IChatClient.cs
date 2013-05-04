using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DiscoveryChat
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
