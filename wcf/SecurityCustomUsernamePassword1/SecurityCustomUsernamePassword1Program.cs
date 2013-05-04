using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace SecurityCustomUsernamePassword1
{
    class SecurityCustomUsernamePassword1Program
    {
        static void Main(string[] args)
        {
            new SecurityCustomUsernamePassword1Program().Run(args);
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
                WaitForEnterKey();
            }
            else
            {
                StartServer();
            }
        }

        private void StartClient()
        {
            var factory = new ChannelFactory<ISecretService>("client1");
            factory.Credentials.UserName.UserName = "Bond";
            factory.Credentials.UserName.Password = "James Bond";
            var proxy = factory.CreateChannel();
            var secret = proxy.GetSecret(0);
            Console.WriteLine("Secret: {0}", secret);
            factory.Close();
        }


        private void StartServer()
        {
            var host = new ServiceHost(typeof (SecretService));
            host.Open();
            WaitForEnterKey();
            host.Close();
        }

        private static void WaitForEnterKey()
        {
            Console.Write("Press ENTER to exit...");
            Console.ReadLine();
        }
    }
}
