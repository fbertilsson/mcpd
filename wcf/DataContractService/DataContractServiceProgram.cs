using System;
using System.ServiceModel;

namespace DataContractService
{
    class DataContractServiceProgram
    {
        static void Main()
        {
            Console.Write("Opening host... ");
            var host = new ServiceHost(typeof(MyService));
            host.Open();
            Console.WriteLine("Done.");
            Console.Write("Press ENTER to EXIT");
            Console.ReadLine();
            host.Abort();
        }
    }
}
