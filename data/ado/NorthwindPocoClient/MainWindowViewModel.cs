using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using NorthwindPocoClient.PrismStuff;
using NorthwindPocoEntities;

namespace NorthwindPocoClient
{
    class MainWindowViewModel : NotificationObject
    {
        private readonly NorthwindPocoContext m_Context;

        public IEnumerable<Customer> Customers {
            get { return m_Context.Customers; }
        }

        public IEnumerable<Order> CustomerOrders
        {
            get
            {
                IEnumerable<Order> result = null;
                if (SelectedCustomer != null)
                {
                    result = from o in m_Context.Orders
                        where o.CustomerID.Equals(SelectedCustomer.CustomerID)
                        select o;
                    //result = m_Context.Orders.Where(o => o.CustomerID == SelectedCustomer.CustomerID);
                }
                return result;
            }
        }

        public IEnumerable<Order> Orders
        {
            get
            {
                return m_Context.Orders;
            }
        }

        private Customer m_SelectedCustomer;

        public Customer SelectedCustomer
        {
            get { return m_SelectedCustomer; }
            set
            {
                if (value == m_SelectedCustomer)return;
                m_SelectedCustomer = value;
                RaisePropertyChanged(() => SelectedCustomer);
                RaisePropertyChanged(() => CustomerOrders);
            }
        }

        public MainWindowViewModel(NorthwindPocoContext context)
        {
            m_Context = context;
        }

    }
}
