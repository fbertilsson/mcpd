using System.Collections.Generic;
using NorthwindPocoEntities;

namespace NorthwindPocoClient
{
    class MainWindowViewModel
    {
        private readonly NorthwindPocoContext m_Context;

        public IEnumerable<Customer> Customers {
            get { return m_Context.Customers; }
        }

        public IEnumerable<Order> Orders
        {
            get { return m_Context.Orders; }
        }

        public MainWindowViewModel(NorthwindPocoContext context)
        {
            m_Context = context;
        }

    }
}
