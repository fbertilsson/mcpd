using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace Rest2WebApp.Wcf
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class VisitedCityService : IVisitedCityService
    {
        private static readonly List<string> AllCountries = new List<string> {"Oman", "Norge", "Sudan", "Sverige"};

        public string Version()
        {
            return "2.0";
        }

        public string Foo()
        {
            return "Foo";
        }

        public string Fie(string fum)
        {
            return string.Format("Fie, fum: {0}", fum);
        }

        public List<string> SmartCompleteCountry(string prefix)
        {
            var toReturn = AllCountries.FindAll(country => country.StartsWith(prefix));
            var resultAsString = toReturn.Aggregate(string.Empty, (agg, element) => string.Format("{0}, {1}", agg, element));
            Console.WriteLine("SmartCompleteCountry prefix: '{0}', return: {1}", prefix, resultAsString);
            return toReturn;
        }

        public List<string> SmartCompleteCountry2(string prefix)
        {
            return SmartCompleteCountry(prefix);
        }
    }
}