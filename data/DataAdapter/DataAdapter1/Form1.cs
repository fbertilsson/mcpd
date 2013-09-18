using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using DataAdapter1.VehicleDataSetTableAdapters;

namespace DataAdapter1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            try
            {
                CheckConnectivity();

                dataGridView1.Invalidate();
                
                //vehicleDataSet.Load();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private static void CheckConnectivity()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DataAdapter1.Properties.Settings.McpdAspNet1ConnectionString"];
            using (var connection = new SqlConnection(connectionString.ConnectionString))
            {
                connection.Open();
                var v = connection.ServerVersion;
                var c = connection.CreateCommand();
                c.CommandText = "select count(*) from vehicle";
                var count = c.ExecuteScalar();
                connection.Close();
            }
        }

        private void createDistributedTx_Click(object sender, EventArgs e)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DataAdapter1.Properties.Settings.McpdAspNet1ConnectionString"];
            using (var tx = new TransactionScope())
            {
                var c1 = new SqlConnection(connectionString.ConnectionString);
                c1.Open();
                var c = c1.CreateCommand();
                c.CommandText = "select count(*) from vehicle";
                var count = c.ExecuteScalar();

                var c2 = new SqlConnection(connectionString.ConnectionString);
                c2.Open();
                c = c2.CreateCommand();
                c.CommandText = "select count(*) from vehicle";
                count = c.ExecuteScalar();

                
                
                c2.Close(); // Here, check the active tx in Component Services.
                c1.Close();
                
            }
        }
    }
}
