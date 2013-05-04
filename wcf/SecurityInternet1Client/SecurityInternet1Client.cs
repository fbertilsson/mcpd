using System;
using System.ServiceModel;
using SecurityInternet1Service;

namespace SecurityInternet1Client
{
    class SecurityInternet1ClientProgram
    {
        static void Main(string[] args)
        {
            var factory = new ChannelFactory<IAccount>("AccountService");
            factory.Credentials.UserName.UserName = "nofreber\\LocalUserA";
            factory.Credentials.UserName.Password = "157";
            //factory.Credentials.UserName.UserName = "AFAB\\nofreber";
            //factory.Credentials.UserName.Password = "you guess";
            var proxy = factory.CreateChannel();

            bool done;
            do
            {
                done = ReadEval(proxy);
            } while (!done);

            factory.Close();
        }

        private static bool ReadEval(IAccount proxy)
        {
            Console.Write(@"Accountant: How can I help you?
  E) Exit
  D) Deposit
  W) Withdraw
  T) Terminate
  > ");
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) return true;
            input = input.ToUpperInvariant();

            long accountNumber = 0L;
            double amount = 1d;
            double balance = 0;
            switch (input[0])
            {
                case 'E':
                    return true;
                case 'D':
                    balance = proxy.Deposit(accountNumber, amount);
                    break;
                case 'W':
                    balance = proxy.Withdraw(accountNumber, amount);
                    break;
                case 'T':
                    proxy.Terminate(accountNumber);
                    break;
                default:
                    balance = proxy.GetBalance(accountNumber);
                    break;
            }
            Console.WriteLine("  Balance: {0}", balance);
            return false;
        }
    }
}
