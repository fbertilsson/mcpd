using System.ServiceModel;

namespace Msmq1
{
    [ServiceContract(Namespace = "http://fb.se/sqrt")]
    interface IService
    {
        [OperationContract(IsOneWay = true)]
        void PrintSquareRoot(int value);
    }
}
