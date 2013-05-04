using System.ServiceModel;
using System.ServiceModel.Web;

namespace Rest2WebAppTake2
{
    [ServiceContract]
    public interface ITeaService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/teatime")]
        string TeaTime();

        [OperationContract]
        [WebGet(UriTemplate = "bedtime")]
        string BedTime();

        [OperationContract]
        [WebGet(UriTemplate = "sugar({lumps})")]
        string GetSugar(string lumps);
    }
}
