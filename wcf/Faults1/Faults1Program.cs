using System;
using System.Linq;
using System.ServiceModel;

namespace Faults1
{
    public class Constants
    {
        public const string Namespace = "http://helenaofredrik.se/mcpd/wshttp1/Greet";
    }

    [ServiceContract(Namespace = Constants.Namespace)]
    public interface IGreetNicely
    {
        [OperationContract]
        //[FaultContract(typeof(FoulLanguageException))]
        [FaultContract(typeof(FoulLanguageException2))]
        [FaultContract(typeof(DivideByZeroException))]
        string Say(string something);
    }

    [ServiceBehavior(Namespace = Constants.Namespace)]
    class GreetNicelyService : IGreetNicely
    {
        public string Say(string something)
        {
            if (something.StartsWith("f")) // Oh, an f-word
            {
                // Works: throw new FaultException<DivideByZeroException>(new DivideByZeroException("Ooops!"), "my reason");
                var message = string.Format("You said the f-word '{0}'", something);
                //var exception = new FoulLanguageException(message);
                //throw new FaultException<FoulLanguageException>(exception, "Foul language");
                var exception = new FoulLanguageException2(message);
                throw new FaultException<FoulLanguageException2>(exception);
            } 
            
            if (something.Equals("0"))
            {
                throw new FaultException<DivideByZeroException>(new DivideByZeroException("Ooops!"), "my reason");
            }

            return "Bonsoir, monsieur.";
        }
    }

    class Faults1Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args.Contains("client")) 
                    StartClient();
                else 
                    StartServer();
            }
            else
            {
                StartServer();
            }
        }

        private static void StartServer()
        {
            Console.Write("Starting server... ");

            var host = new ServiceHost(typeof(GreetNicelyService));
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
            var factory = new ChannelFactory<IGreetNicely>("client1");
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
                Console.WriteLine("  Detail: " + e.Detail);
            }
            catch (FaultException<FoulLanguageException2> e)
            {
                Console.WriteLine(e);
                Console.WriteLine("  Detail: " + e.Detail);
            }
            catch (FaultException<DivideByZeroException> e)
            {
                Console.WriteLine(e);
                Console.WriteLine("  Detail: " + e.Detail);
            }
            catch (Exception e)
            {
                Console.WriteLine("===> " + e);
            }
            WaitForEnterKey();
        }
    }

}
