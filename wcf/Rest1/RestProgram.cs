using System;
using System.Linq;
using System.ServiceModel.Web;

namespace Rest1
{
    class RestProgram
    {
        static void Main(string[] args)
        {
            new RestProgram().Run(args);
        }

        private void Run(string[] args)
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

        private void StartClient()
        {
            throw new NotImplementedException();
        }

        private void StartServer()
        {
            //var host = new ServiceHost(typeof(MusicLibraryService));
            const string baseUri = "http://localhost:8080/musiclib";
            var host = new WebServiceHost(typeof (MusicLibraryService), new Uri(baseUri));
            host.Open();
            Console.Write("Press ENTER to exit...");
            Console.ReadLine();
        }
    }
}
