using System;
using System.Linq;
using System.ServiceModel;

namespace MessageLogging1
{
    class MessageLoggingProgram
    {
        static void Main(string[] args)
        {
            new MessageLoggingProgram().Run(args);
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

        private void StartClient()
        {
            Console.Write("Number to call: ");
            var numberString = Console.ReadLine();
            if (numberString == null) return;
            int number;
            int.TryParse(numberString, out number);
            var factory = new ChannelFactory<IPhone>("nr1");
            var phone = factory.CreateChannel();
            phone.Call(number);
            factory.Close();
        }

        private void StartServer()
        {
            var host = new ServiceHost(typeof(Phone));
            host.Open();
            Console.Write("Press ENTER to exit...");
            Console.ReadLine();
        }
    }
}
