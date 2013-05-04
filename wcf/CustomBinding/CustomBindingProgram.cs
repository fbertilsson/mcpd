using System;
using System.Linq;
using System.ServiceModel;

namespace CustomBinding
{
    class CustomBindingProgram
    {
        static void Main(string[] args)
        {
            new CustomBindingProgram().Run(args);
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
                try
                {
                    StartClient();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                StartServer();
            }
        }

        private void StartServer()
        {
            var host = new ServiceHost(typeof (EchoService));
            host.Open();
            Console.Write("Press ENTER to exit...");
            Console.ReadLine();
        }

        private void StartClient()
        {
            var factory = new ChannelFactory<IEchoService>("Echo");
            var proxy = factory.CreateChannel();
            Console.Write("Enter something to echo: ");
            var text = Console.ReadLine();

            do
            {
                var result = proxy.Echo(text);
                Console.WriteLine("Echoed: {0}", result);
                Console.Write("Enter something to echo: ");
                text = Console.ReadLine();
            } while (!string.IsNullOrEmpty(text));
            
            factory.Close();

        }
    }
}
