using System.ServiceModel.Activation;

namespace Rest2WebAppTake2
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TeaService : ITeaService
    {
        public string TeaTime()
        {
            return "at 5 p.m. of course";
        }

        public string BedTime()
        {
            return "at eleven";
        }

        public string GetSugar(string lumps)
        {
            var nLumps = int.Parse(lumps);
            string response;
            if (nLumps == 2)
            {
                response = "Just right!";
            }
            else if (nLumps < 2)
            {
                response = "Too little.";
            }
            else
            {
                response = "Too much.";
            }
            return response;
        }
    }
}