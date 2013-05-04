using System.ServiceModel.Channels;
using System.ServiceModel;

namespace Host1
{
    
    [ServiceContract(Namespace = "http://f.se") ]
    public interface IDataService
    {
        [OperationContract]
        Message GetData();

        [OperationContract]
        Message GetFault();

        [OperationContract]
        void SetData(Message m);
    }
}
