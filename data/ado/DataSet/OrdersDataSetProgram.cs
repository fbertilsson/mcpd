using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using DataSet.OrdersDataSetTableAdapters;

namespace DataSet
{
    class OrdersDataSetProgram
    {
        private OrdersDataSet m_DataSet;
        private OrdersTableAdapter m_OrdersAdapter;
        private OrderLinesTableAdapter m_LinesAdapter;
        private TableAdapterManager m_AdapterManager;

        public void Go()
        {
            m_DataSet = new OrdersDataSet();

            var connectionString = ConfigurationManager.ConnectionStrings["DataSet.Properties.Settings.TypedDataSetConnectionString"];
            using (var conn = new SqlConnection(connectionString.ConnectionString))
            {
                conn.Open();

                bool doLoop;
                do
                {
                    doLoop = ReadEvalPrint(conn);

                } while (doLoop);
            }

        }

        private bool ReadEvalPrint(DbConnection conn)
        {
            Console.WriteLine("q: quit, f: fill, a: add order, al: add line, p: print, s: save");
            Console.Write("Type ENTER to exit: ");
            var input = Console.ReadLine();
            var doLoop = ! string.IsNullOrEmpty(input) && ! input.Trim().ToLower().StartsWith("q");
            if (doLoop)
            {
                if (input.StartsWith("f"))
                {
                    Fill(conn);
                }
                else if (input.StartsWith("al"))
                {
                    AddOrderLine();
                }
                else if (input.StartsWith("a"))
                {
                    AddOrder();
                }
                else if (input.StartsWith("p"))
                {
                    Print();
                }
                else if (input.StartsWith("s"))
                {
                    Save();
                }
            }

            return doLoop;
        }

        private void Fill(IDbConnection conn)
        {
            m_OrdersAdapter = new OrdersTableAdapter();
            m_OrdersAdapter.Fill(m_DataSet.Orders);

            m_LinesAdapter = new OrderLinesTableAdapter();
            m_LinesAdapter.Fill(m_DataSet.OrderLines);

            m_AdapterManager = new TableAdapterManager
            {
                Connection = conn,
                OrdersTableAdapter = m_OrdersAdapter,
                OrderLinesTableAdapter = m_LinesAdapter
            };
        }

        private void Save()
        {
            var x = m_AdapterManager.UpdateAll(m_DataSet);
            Console.WriteLine(x);
        }

        private void Print()
        {
            foreach (var order in m_DataSet.Orders)
            {
                Console.WriteLine("  Order #{0}, {1}", 
                    order[m_DataSet.Orders.IdColumn], 
                    order[m_DataSet.Orders.CommentColumn]);

                foreach (var line in order.GetOrderLinesRows())
                {
                    Console.WriteLine("    {0,-17} {1,10:C0} {2,5:f0}",
                        line[m_DataSet.OrderLines.ProductColumn],
                        line[m_DataSet.OrderLines.PriceColumn],
                        line[m_DataSet.OrderLines.QuantityColumn]);
                }
            }
        }

        private void AddOrder()
        {
            Console.Write("Adding order. Enter order comment: ");
            var orderComment = Console.ReadLine();
            var row = m_DataSet.Orders.NewOrdersRow();
            row[m_DataSet.Orders.CommentColumn] = orderComment;
            m_DataSet.Orders.AddOrdersRow(row);
        }

        private void AddOrderLine()
        {
            Console.Write("Adding order line. Which order id? ");
            var orderIdString = Console.ReadLine();
            int orderId;
            if (! int.TryParse(orderIdString, out orderId)) return;
            var row = m_DataSet.OrderLines.NewOrderLinesRow();
            row[m_DataSet.OrderLines.OrderIdColumn] = orderId;

            Console.Write("   Product? ");
            var product = Console.ReadLine();
            Console.Write("   Price? ");
            var price = Console.ReadLine();
            Console.Write("   Quantity? ");
            var quantity = Console.ReadLine();

            row[m_DataSet.OrderLines.ProductColumn] = product;
            row[m_DataSet.OrderLines.PriceColumn] = price;
            row[m_DataSet.OrderLines.QuantityColumn] = quantity;

            m_DataSet.OrderLines.AddOrderLinesRow(row);
        }

    }
}
