using System;
using System.ServiceModel;
using System.Transactions;

namespace Msmq1
{
    class Service : IService
    {
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void PrintSquareRoot(int value)
        {
            var root = Math.Sqrt(value);
            Console.WriteLine("Square root of {0} is {1}.", value, root);
            if (root - Math.Truncate(root) > 1e-5)
            {
                //throw new Exception("Trying to roll back with exception. Yes, now the message is moved to a folder 'retry'");
                Transaction.Current.Rollback();      // Yes, this also works fine. Probably faster.
                Console.WriteLine("- Rolled back");
            }
        }
    }
}
