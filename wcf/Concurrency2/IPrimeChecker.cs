using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Concurrency2
{
    [ServiceContract]
    interface IPrimeChecker
    {
        [OperationContract]
        bool IsPrime(long number);
    }
}
