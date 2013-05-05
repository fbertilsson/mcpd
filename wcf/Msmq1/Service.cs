using System;
using System.ServiceModel;

namespace Msmq1
{
    class Service : IService
    {
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void PrintSquareRoot(int value)
        {
            Console.WriteLine("Square root of {0} is {1}.", value, Math.Sqrt(value));
        }
    }
}
