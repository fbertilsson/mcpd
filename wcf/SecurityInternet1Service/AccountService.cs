using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace SecurityInternet1Service
{
    class AccountService : IAccount
    {
        private double m_Balance = 0d;

        public double Deposit(long accountNumber, double amount)
        {
            Console.WriteLine("Deposit {0}, {1}", accountNumber, amount);
            m_Balance += amount;
            return m_Balance;
        }

        public double Withdraw(long accountNumber, double amount)
        {
            Console.WriteLine("Withdraw {0}, {1}", accountNumber, amount);
            m_Balance -= amount;
            return m_Balance;
        }

        public double GetBalance(long accountNumber)
        {
            Console.WriteLine("GetBalance {0}, {1}", accountNumber, m_Balance);
            return m_Balance;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "nofreber\\AccountManager")]
        public void Terminate(long accountNumber)
        {
            Console.WriteLine("Terminated account {0}", accountNumber);
        }
    }
}
