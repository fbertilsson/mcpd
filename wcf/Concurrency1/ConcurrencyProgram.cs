using System;
using System.Linq;
using System.ServiceModel;
using System.Threading;

namespace Concurrency1
{
    /// <summary>
    /// Exploring ConcurrencyMode. The interface ICalculator has two different 
    /// implementations CalcSingle and CalcMulti.
    /// 
    /// I have only managed to get concurrent calls in the IsOneWay method ClearMemory(). Why?
    /// </summary>
    class ConcurrencyProgram
    {
        static void Main(string[] args)
        {
            bool doStartClient = false;
            if (args.Length > 0)
            {
                doStartClient = args.Contains("client");
            }
            if (doStartClient)
            {
                StartClient();
            }
            else
            {
                StartServer();
            }

        }

        private static void StartServer()
        {
            Console.Write("Starting server... ");

            var host = new ServiceHost(typeof (CalcSingle));
            host.Open();

            var hostMulti = new ServiceHost(typeof (CalcMulti));
            hostMulti.Open();
            
            Console.WriteLine("Done.");

            Console.WriteLine("Press ENTER to exit... ");
            Console.ReadLine();
            host.Abort();
            hostMulti.Abort();
        }

        private static void StartClient()
        {
            var doMultiTreaded = false;
            if (doMultiTreaded)
            {
                var factory = new ChannelFactory<ICalculator>("MultiMode");
                InvokeMultithreaded(factory.CreateChannel());
            }
            else
            {
                var factory = new ChannelFactory<ICalculator>("SingleMode");
                InvokeMultithreaded(factory.CreateChannel());
            }
        }


        private static void InvokeMultithreaded(ICalculator calculator)
        {
            //var s1 = new CalcMulti();
            //var s2 = new CalcMulti();
            //Console.WriteLine("Hash codes:  s1: {0}, s2: {1}", s1.GetHashCode(), s2.GetHashCode());

            var thread1 = new Thread(() => Console.WriteLine("{0} + {1} = {2}", 1, 2, calculator.Add(1, 2)));
            var thread2 = new Thread(() => Console.WriteLine("{0} + {1} = {2}", 3, 4, calculator.Add(3, 4)));
            thread1.Name = "t1";
            thread2.Name = "t2";

            thread1.Start();
            thread2.Start();
            Thread.Sleep(2000);

            thread1.Join();
            thread2.Join();

            Console.WriteLine("Clearing memory, trying to get concurrent calls:");
            calculator.ClearMemory();
            calculator.ClearMemory();
            calculator.ClearMemory();

            Console.WriteLine("Done");
        }
    }
}
