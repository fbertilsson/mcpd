using System;
using System.ServiceModel;
using System.Threading;
using System.Linq;

namespace CallbackService
{
    [ServiceContract]
    public interface ITalkCallback
    {
        //[OperationContract(IsOneWay = true)] // N.B. If IsOneWay is used, the ServiceBehavior does not need to be ConcurrencyModel.Reentrant!
        [OperationContract]
        void TalkBack(string response);
    }

    [ServiceContract(CallbackContract = typeof(ITalkCallback))]
    public interface ITalk
    {
        //[OperationContract(IsOneWay = true)]
        [OperationContract]
        string Talk(string words);
    }

    
    //[ServiceBehavior(ConcurrencyMode=ConcurrencyMode.Single)]
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)] // N.B. Unless Reentrant, the TalkBack methods needs to be IsOneWay!
    public class TalkService : ITalk
    {
        public string Talk(string words)
        {
            Console.WriteLine("Talk got '{0}'", words);
            var callbackChannel = OperationContext.Current.GetCallbackChannel<ITalkCallback>();

            callbackChannel.TalkBack("The same to you!");                   // Callback no. 1
            new Thread(() => DoCallback(words, callbackChannel)).Start();   // Callback no. 2
            return "ok";
        }

        private static void DoCallback(string words, ITalkCallback callback)
        {
            try
            {
                Thread.Sleep(1000 * words.Length);
                var response = string.Format("'{0}'", words);
                callback.TalkBack(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    /// <summary>
    /// Experimenting with callbacks.
    /// - Tried to make a deadlock with a callback within the call execution without Reentrant, but failed to provoke it. Why?
    /// - 
    /// </summary>
    class Program : ITalkCallback
    {
        static void Main(string[] args)
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

        private static void StartClient()
        {
            var factory = new DuplexChannelFactory<ITalk>(new Program(), "talk1");
            //ChannelFactory won't work when using callbacks:  var factory = new ChannelFactory<ITalk>("talk1");
            var proxy = factory.CreateChannel();

            Console.Write("Enter what to talk: ");
            string words = Console.ReadLine();
            Console.WriteLine("Talking: '{0}'", words);
            proxy.Talk(words);
            Console.WriteLine("Done.");
            for (var i = 0; i < 8; i++)
            {
                Console.Write(i + 1);
                Thread.Sleep(500);
            }
            factory.Abort();
        }

        private static void StartServer()
        {
            Console.Write("Starting CallbackService...");

            var host = new ServiceHost(typeof (TalkService));
            host.Open();

            Console.WriteLine("\nPress ENTER to exit...");
            Console.ReadLine();
            host.Close();
        }

        public void TalkBack(string response)
        {
            Console.WriteLine(" TalkBack got '{0}'. ", response);
        }
    }
}
