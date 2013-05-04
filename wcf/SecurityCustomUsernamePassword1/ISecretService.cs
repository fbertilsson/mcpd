using System.ServiceModel;

namespace SecurityCustomUsernamePassword1
{
    [ServiceContract]
    internal interface ISecretService
    {
        [OperationContract]
        string GetSecret(int number);
    }
}