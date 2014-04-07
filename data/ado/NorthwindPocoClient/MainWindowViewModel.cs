using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using NorthwindPocoClient.PrismStuff;
using NorthwindPocoEntities;

namespace NorthwindPocoClient
{
    public class MainWindowViewModel : NotificationObject
    {
        private readonly NorthwindPocoContext m_Context;

        public IEnumerable<Customer> Customers {
            get { return new ObservableCollection<Customer>(m_Context.Customers); }
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

        public void AddCustomer(Customer customer)
        {
            m_Context.Customers.AddObject(customer);
        }

        public void OnAddDigitToPhoneNumberTestingDetatch()
        {
            var customer = SelectedCustomer;
            m_Context.Customers.Detach(customer);

            var lastDigit = int.Parse(customer.Phone.Reverse().First().ToString());
            customer.Phone += (lastDigit + 1)%10;
            m_Context.Customers.Attach(customer);

            // Trying to make the entity marked as modified
            //m_Context.DetectChanges();                                                                 // Had no apparent effect
            //m_Context.AcceptAllChanges();                                                              // Had no apparent effect
            var entry = m_Context.ObjectStateManager.ChangeObjectState(customer, EntityState.Modified);  // Did the trick
            
            RaisePropertyChanged(() => Customers);
        }

        public void OnTruncateLastPhoneDigit()
        {
            var customer = SelectedCustomer;
            customer.Phone = customer.Phone.Substring(0, customer.Phone.Length - 1);
            RaisePropertyChanged(() => Customers);
        }

        public void Save()
        {
            m_Context.DetectChanges();             // Interesting that this needs to be called before save. Or maybe can get performance by setting state manually.
            var nDirty = m_Context.SaveChanges();
            RaisePropertyChanged(() => Customers);
        }
    }
}
