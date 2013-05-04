using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Rest2WebApp.Wcf
{
    [ServiceContract]
    public interface IVisitedCityService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/version", ResponseFormat = WebMessageFormat.Json)]
        string Version();

        [OperationContract]
        [WebGet(UriTemplate = "/foo", ResponseFormat = WebMessageFormat.Json)]
        string Foo();

        [OperationContract]
        [WebGet(UriTemplate = "/fie/{fum}", ResponseFormat = WebMessageFormat.Json)]
        string Fie(string fum);

        [OperationContract]
        [WebGet(UriTemplate = "/SmartCompleteCountry/{prefix}", ResponseFormat = WebMessageFormat.Json)]
        List<string> SmartCompleteCountry(string prefix);

        [OperationContract]
        [WebGet(UriTemplate = "/SmartCompleteCountry2?p={prefix}", ResponseFormat = WebMessageFormat.Json)]
        List<string> SmartCompleteCountry2(string prefix);
    }
}
