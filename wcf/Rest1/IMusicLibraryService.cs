using System.ServiceModel;
using System.ServiceModel.Web;

namespace Rest1
{
    [ServiceContract]
    public interface IMusicLibraryService
    {
        [WebGet(UriTemplate = "/version")]
        [OperationContract]
        string Version();

        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{artist}/{album}/{track}")]
        [OperationContract]
        string[] GetComposers(string artist, string album, string track);
    }
}