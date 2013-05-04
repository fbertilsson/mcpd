using System;
using System.ServiceModel;
using System.Threading;

namespace Concurrency1
{
    [ServiceBehavior(
        ConcurrencyMode = ConcurrencyMode.Multiple, 
        InstanceContextMode = InstanceContextMode.PerSession)]
    internal class CalcMulti : ICalculator
    {
        private static int nConcurrent = 0;

        public int Add(int a, int b)
        {
            nConcurrent++;
            Console.WriteLine("Calculating {0} + {1}, nConcurrent: {2}, Thread: {3}, my hash: {4}", a, b, nConcurrent, Thread.CurrentThread.ManagedThreadId, this.GetHashCode());
            Thread.Sleep(900);
            if (nConcurrent > 1) Console.WriteLine("====== Bingo! nConcurrent: {0}", nConcurrent);
            var sum = a + b;
            Console.WriteLine("   {0} + {1} = {2}", a, b, sum);
            nConcurrent--;
            return sum;
        }

        public void ClearMemory()
        {
            nConcurrent++;
            Console.WriteLine("Clearing memory..., Thread: {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(500);
            if (nConcurrent > 1) Console.WriteLine("====== Bingo! nConcurrent: {0}", nConcurrent);
            Console.WriteLine("    Done. Thread: {0}", Thread.CurrentThread.ManagedThreadId);
            nConcurrent--;
        }
    }
}