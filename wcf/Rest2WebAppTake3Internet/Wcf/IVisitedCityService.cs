using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Rest2WebAppTake3Internet.Wcf
{
    [ServiceContract]
    public interface IVisitedCityService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/version")]
        string Version();

        [OperationContract]
        [WebGet(UriTemplate = "/SmartCompleteCountry?startsWith={prefix}")]
        List<string> SmartCompleteCountry(string prefix);
    }
}
