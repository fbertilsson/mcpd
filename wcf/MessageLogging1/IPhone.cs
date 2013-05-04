using System.ServiceModel;

namespace MessageLogging1
{
    [ServiceContract]
    interface IPhone
    {
        [OperationContract]
        bool Call(int number);
    }
}
