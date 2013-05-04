using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace Rest2WebAppTake3Internet.Wcf
{
    //[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class VisitedCityService : IVisitedCityService
    {
        private static List<string> m_AllCountries = new List<string> {"Norge", "Sverige"};

        public string Version()
        {
            return "1.0";
        }

        public List<string> SmartCompleteCountry(string prefix)
        {
            return m_AllCountries.FindAll(country => country.StartsWith(prefix));
        }
    }
}