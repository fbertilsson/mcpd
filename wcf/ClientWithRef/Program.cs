using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientWithRef
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Connecting...");

            var client = new ServiceReference1.GreetClient();
            client.Open();
            Console.Write("Saying 'Hej.'... ");
            var response = client.Say("Hej.");
            Console.WriteLine(response);

            Console.Write("Saying async slowly 'Hej.'");
            var task = client.SaySlowlyAsync("Hej.");
            while (! task.IsCompleted && ! task.IsFaulted)
            {
                Console.Write('.');
                Thread.Sleep(1000);
            }
            if (task.IsCompleted)
            {
                Console.WriteLine(task.Result);
            } else
            {
                Console.WriteLine("Faulted");
            }

            client.Close();
        }
    }
}
