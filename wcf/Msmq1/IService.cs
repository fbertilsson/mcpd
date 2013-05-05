using System.ServiceModel;

namespace Msmq1
{
    [ServiceContract]
    interface IService
    {
        [OperationContract(IsOneWay = true)]
        void PrintSquareRoot(int value);
    }
}
