using System.ServiceModel.Channels;
using System.ServiceModel;

namespace GenericMessageHandling
{
    /// <summary>
    /// A simple service with a generic message interface.
    /// Look how the namespace complicates things.
    /// </summary>
    [ServiceContract(Namespace = "http://f.se")]
    public interface IDataService
    {
        [OperationContract]
        Message GetData();

        [OperationContract]
        void SetData(Message m);
    }
}
