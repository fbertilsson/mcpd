using System;
using System.Linq;
using System.ServiceModel;
using System.Threading;

namespace wshttp1
{
    public class Constants
    {
        public const string Namespace = "http://helenaofredrik.se/mcpd/wshttp1/Greet"; 
    }

    [ServiceContract(Namespace = Constants.Namespace)]
    internal interface IGreet
    {
        [OperationContract, FaultContract(typeof(FoulLanguageException))]
        string Say(string something);

        [OperationContract]
        string SaySlowly(string something);
    }

    [ServiceBehavior(Namespace = Constants.Namespace)]
    class GreetService : IGreet
    {
        public string Say(string something)
        {
            if (something.StartsWith("f")) // Oh, an f-word
            {
                var exception = new FoulLanguageException(string.Format("You said the f-word '{0}'", something));
                throw new FaultException<FoulLanguageException>(exception);
            }
            return "Bonsoir, monsieur.";
        }
        public string SaySlowly(string something)
        {
            Thread.Sleep(5000);
            return "Bonsoir, monsieur, nez pas trez rapide.";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args.Contains("client")) StartClient();
                else StartServer();
            }
            else
            {
                StartServer();
            }
        }

        private static void StartServer()
        {
            Console.Write("Starting WsHTTP server... ");

            var host = new ServiceHost(typeof (GreetService));
            host.Open();

            Console.WriteLine("Done.");
            WaitForEnterKey();
        }

        private static void WaitForEnterKey()
        {
            Console.Write("Press ENTER to exit...");
            Console.ReadLine();
        }

        private static void StartClient()
        {
            var factory = new ChannelFactory<IGreet>("client1");
            var proxy = factory.CreateChannel();
            Console.Write("Enter something: ");
            var something = Console.ReadLine();
            Console.Write("Saying {0}. ", something);
            try
            {
                var response = proxy.Say(something);
                Console.WriteLine("Response: {0}", response);
            }
            catch (FaultException<FoulLanguageException> e)
            {
                Console.WriteLine(e);
            }
            WaitForEnterKey();
        }
    }

}
