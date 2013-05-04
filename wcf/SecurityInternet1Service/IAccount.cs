using System.ServiceModel;

namespace SecurityInternet1Service
{
    [ServiceContract]
    public interface IAccount
    {
        [OperationContract]
        double Deposit(long accountNumber, double amount);

        [OperationContract]
        double Withdraw(long accountNumber, double amount);

        [OperationContract]
        double GetBalance(long accountNumber);

        [OperationContract]
        void Terminate(long accountNumber);
    }
}
