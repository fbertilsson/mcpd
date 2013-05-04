using System;
using System.ServiceModel;

namespace Host1
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(DataService));
            host.Open();

            Console.WriteLine("Host opened. Press ENTER to exit.");
            Console.ReadLine();
        }
    }
}
