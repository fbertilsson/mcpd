using System;
using System.Collections.Generic;
using System.Threading;
using DataContractClient.ServRef;

// This project is used to explore how WCF treats data contracts. 
// I use svcutil on the service and observe the generated code.
//
// In addition I create a client to explore deriving from ClientBase.
//
namespace DataContractClient
{
    class Program
    {
        private AutoResetEvent m_Waiter;
        private MyClient m_Client;
        private Contact[] Customers { get; set; }

        static void Main(string[] args)
        {
            new Program().Run();
        }

        private void Run()
        {
            //
            // Testing the hand-coded client
            //
            m_Client = new MyClient("BasicHttpBinding_IMyService");

            // Synchronous access:
            Customers = m_Client.GetCustomers();
            Print(Customers);

            // Async
            Customers = null;
            m_Waiter = new AutoResetEvent(false);
            var result = m_Client.BeginGetCustomers(GetCustomersCallback, new object());
            Thread.Sleep(1);
            if (!result.IsCompleted)
            {
                Console.Write('.');
            }
            m_Waiter.WaitOne();
            Print(Customers);
            m_Client.Close();
            
        }

        private void Print(IEnumerable<Contact> contacts)
        {
            foreach (var customer in contacts)
            {
                Console.WriteLine(customer.Name);
            }
        }

        private void GetCustomersCallback(IAsyncResult ar)
        {
            Customers = m_Client.EndGetCustomers(ar);
            m_Waiter.Set();
        }
    }
}
