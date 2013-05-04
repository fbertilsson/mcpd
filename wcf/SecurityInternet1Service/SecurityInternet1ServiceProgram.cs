using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SecurityInternet1Service
{
    class SecurityInternet1ServiceProgram
    {
        static void Main(string[] args)
        {
            Console.Write("Opening host... ");
            var host = new ServiceHost(typeof (AccountService));
            host.Open();

            Console.WriteLine("Done.");
            WaitForEnterKey();
        }

        private static void WaitForEnterKey()
        {
            Console.Write("Press ENTER to exit...");
            Console.ReadLine();
        }

    }
}
