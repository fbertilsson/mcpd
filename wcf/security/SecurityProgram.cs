using System;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;

namespace security
{
    class SecurityProgram
    {
        static void Main(string[] args)
        {
            new SecurityProgram().Run(args);
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
            string roleName;
            bool isInRole;
            do
            {
                Console.Write("Enter role name: ");
                roleName = Console.ReadLine();
                var p = WindowsPrincipal.Current;
                var id = WindowsIdentity.GetCurrent(); // new WindowsIdentity("AFAB\\nofreber");
                p = new WindowsPrincipal(id);
                isInRole = p.IsInRole(roleName);
                Console.WriteLine("   Is '{0}' in role {1}:   {2}", id.Name, roleName, isInRole);
            } while (!string.IsNullOrEmpty(roleName));


            var factory = new ChannelFactory<IMySecureService>("s1");
            var proxy = factory.CreateChannel();
            proxy.OnlyForAfabFredrik();
            var isFredrik = proxy.ReturnYesIfFredrik();
            Console.WriteLine("isFredrik: {0}", isFredrik);
            factory.Close();

            //factory = new ChannelFactory<IMySecureService>("s1");
            //var id = WindowsIdentity.GetAnonymous();
            //factory.Credentials.Windows.ClientCredential.Domain = string.Empty;
            //factory.Credentials.Windows.ClientCredential.UserName = id.Name;
            //factory.Credentials.Windows.ClientCredential.Password = string.Empty;
            //proxy = factory.CreateChannel();
            //isFredrik = proxy.ReturnYesIfFredrik();
            //Console.WriteLine("isFredrik: {0}", isFredrik);
            //factory.Close();

            Console.WriteLine("Calling OnlyForAdmins");
            factory = new ChannelFactory<IMySecureService>("s1");
            proxy = factory.CreateChannel();
            proxy.OnlyForAdmins();
            factory.Close();

            Console.WriteLine("Calling OnlyForUsers");
            factory = new ChannelFactory<IMySecureService>("s1");
            proxy = factory.CreateChannel();
            proxy.OnlyForUsers();
            factory.Close();
        }

        private void StartServer()
        {
            //var host = new ServiceHost(typeof(MusicLibraryService));
            //var baseUri = "net.tcp://localhost:8080/security";
            var host = new ServiceHost(typeof(MySecureService));
            host.Open();
            Console.Write("Press ENTER to exit...");
            Console.ReadLine();
        }

    }
}
