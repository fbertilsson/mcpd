using System.ServiceModel;

namespace Concurrency1
{
    [ServiceContract]
    public interface ICalculator
    {
        [OperationContract]
        int Add(int a, int b);

        [OperationContract(IsOneWay = true)]
        void ClearMemory();
    }
}
