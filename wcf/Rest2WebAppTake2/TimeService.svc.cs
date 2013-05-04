using System;
using System.ServiceModel.Activation;

namespace Rest2WebAppTake2
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TimeService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TimeService.svc or TimeService.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TimeService : ITimeService
    {
        public string Version()
        {
            return "2.2";
        }

        public string Now()
        {
            return DateTime.Now.ToShortTimeString();
        }
    }
}
