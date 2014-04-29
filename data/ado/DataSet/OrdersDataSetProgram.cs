using System;
using System.Configuration;
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
            Console.WriteLine("q: quit, f: fill, a: add order line, p: print, s: save");
            Console.Write("Type ENTER to exit: ");
            var input = Console.ReadLine();
            var doLoop = ! string.IsNullOrEmpty(input) && ! input.Trim().ToLower().StartsWith("q");
            if (doLoop)
            {
                if (input.StartsWith("f"))
                {
                    Fill(conn);
                }
                else if (input.StartsWith("a"))
                {
                    AddOrderLine();
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

        private void Save()
        {
            throw new NotImplementedException();
        }

        private void Print()
        {
            foreach (var order in m_DataSet.Orders)
            {
                Console.WriteLine("Order #{0}, {1}", 
                    order[m_DataSet.Orders.IdColumn], 
                    order[m_DataSet.Orders.CommentColumn]);
            }
        }

        private void AddOrderLine()
        {
            Console.Write("Adding order line. Enter order comment: ");
            var orderComment = Console.ReadLine();
            var row = m_DataSet.Orders.NewOrdersRow();
            row[m_DataSet.Orders.CommentColumn] = orderComment;
            m_DataSet.Orders.AddOrdersRow(row);
        }

        private void Fill(DbConnection conn)
        {
            m_OrdersAdapter = new OrdersTableAdapter();
            m_OrdersAdapter.Fill(m_DataSet.Orders);

            m_LinesAdapter = new OrderLinesTableAdapter();
            m_LinesAdapter.Fill(m_DataSet.OrderLines);

            m_AdapterManager = new TableAdapterManager { Connection = conn };
            var x = m_AdapterManager.UpdateAll(m_DataSet);
        }
    }
}
