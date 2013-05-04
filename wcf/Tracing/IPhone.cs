using System.ServiceModel;

namespace Tracing
{
    [ServiceContract]
    interface IPhone
    {
        [OperationContract]
        string Call(int number);
    }
}
