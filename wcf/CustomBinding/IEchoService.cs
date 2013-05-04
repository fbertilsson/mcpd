using System.ServiceModel;

namespace CustomBinding
{
    [ServiceContract]
    interface IEchoService
    {
        [OperationContract]
        string Echo(string text);
    }
}
