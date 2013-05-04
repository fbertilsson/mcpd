using System;
using System.ServiceModel;
using System.Threading;

namespace Concurrency1
{

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single)]
    class CalcSingle : ICalculator
    {
        public int Add(int a, int b)
        {
            Console.WriteLine("Calculating {0} + {1}...", a, b);
            Thread.Sleep(500);
            var sum = a + b;
            Console.WriteLine("    {0} + {1} = {2}", a, b, sum);
            return sum;
        }

        public void ClearMemory()
        {
            Console.WriteLine("Clearing memory..., Thread: {0}",  Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(500);
            Console.WriteLine("    Done. Thread: {0}",  Thread.CurrentThread.ManagedThreadId);
        }
    }
}
