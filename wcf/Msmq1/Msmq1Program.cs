using System;
using System.Configuration;
using System.Linq;
using System.Messaging;
using System.ServiceModel;

namespace Msmq1
{
    class Msmq1Program
    {
        private string m_QueueName;

        static void Main(string[] args)
        {
            new Msmq1Program().Run(args);
        }

        private void Run(string[] args)
        {
            m_QueueName = ConfigurationManager.AppSettings["queueName"];
            if (! MessageQueue.Exists(m_QueueName))
            {
                MessageQueue.Create(m_QueueName, true);
            }

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
            var factory = new ChannelFactory<IService>("myClient");
            var proxy = factory.CreateChannel();
            string numberString;
            do
            {
                Console.Write("Enter integer value to send to square root calculator: ");
                numberString = Console.ReadLine();
                int number;
                if (int.TryParse(numberString, out number))
                {
                    proxy.PrintSquareRoot(number);
                }
            } while (!String.IsNullOrEmpty(numberString));

            factory.Close();
        }

        private void StartServer()
        {
            var host = new ServiceHost(typeof (Service));
            host.Open();
            Console.Write("Press ENTER to exit...");
            Console.ReadLine();
            host.Close();            
        }

    }
}
