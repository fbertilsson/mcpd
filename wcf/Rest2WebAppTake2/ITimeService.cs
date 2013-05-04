using System.ServiceModel;
using System.ServiceModel.Web;

namespace Rest2WebAppTake2
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITimeService" in both code and config file together.
    [ServiceContract]
    public interface ITimeService
    {
        [WebGet(UriTemplate = "/version")]
        [OperationContract]
        string Version();

        [WebGet(UriTemplate = "/now")]
        [OperationContract]
        string Now();
    }
}
