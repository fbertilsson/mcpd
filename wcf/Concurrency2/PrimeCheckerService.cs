using System.ServiceModel;
using System.Threading;

namespace Concurrency2
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)] // Try different ConcurrencyMode and see if you get your answers fast.
    class PrimeCheckerService : IPrimeChecker
    {
        public bool IsPrime(long number)
        {
            Thread.Sleep(1000);
            return number > 3;
        }
    }
}
