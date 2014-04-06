using System.Data.Objects;

namespace NorthwindPocoEntities
{
    public class NorthwindPocoContext : ObjectContext
    {
        private ObjectSet<Customer> m_Customers;

        public ObjectSet<Customer> Customers
        {
            get { return m_Customers; }
        }

        private ObjectSet<Order> m_Orders;

        public ObjectSet<Order> Orders
        {
            get { return m_Orders; }
        }

        public NorthwindPocoContext(string connectionString) : base(connectionString)
        {
            m_Customers = CreateObjectSet<Customer>();
            m_Orders = CreateObjectSet<Order>();
        }

        //protected NorthwindPocoContext(string connectionString, string defaultContainerName) : base(connectionString, defaultContainerName)
        //{
        //}

        //protected NorthwindPocoContext(EntityConnection connection, string defaultContainerName) : base(connection, defaultContainerName)
        //{
        //}

        //public NorthwindPocoContext(EntityConnection connection) : base(connection)
        //{
        //}
    }

}
