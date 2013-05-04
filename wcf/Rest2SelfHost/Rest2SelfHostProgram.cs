using System;
using System.ServiceModel.Web;

namespace Rest2SelfHost
{
    /// <summary>
    /// I could not get the ASP.NET app Rest2WebApp to host my REST service,
    /// so I wrote this self-host program.
    /// </summary>
    class Rest2SelfHostProgram
    {
        static void Main(string[] args)
        {
            Console.Write("Starting host... ");

            var baseAddress = new Uri("http://localhost:60600/vcs");
            var host = new WebServiceHost(typeof (Rest2WebApp.Wcf.VisitedCityService), baseAddress);
            host.Open();
            Console.WriteLine(" done, hosted at: ");
            Console.WriteLine(baseAddress);
            Console.Write("Press ENTER to exit");
            Console.ReadLine();
            host.Close();
        }
    }
}
