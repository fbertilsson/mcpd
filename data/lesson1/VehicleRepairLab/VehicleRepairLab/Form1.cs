using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace VehicleRepairLab
{
    public partial class Form1 : Form
    {
        private DataSet m_Ds;

        private readonly string m_XsdFile =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VehicleRepairs.xsd");
        private readonly string m_XmlFile =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VehicleRepairs.xml");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1Load(object sender, EventArgs e)
        {
            PopulateDataSet();

            dgVehicles.DataSource = m_Ds;
            dgVehicles.DataMember = "Vehicles";

            dgRepairs.DataSource = m_Ds;
            dgRepairs.DataMember = "Vehicles.vehicles_repairs";
        }

        private void PopulateDataSet()
        {
            if (File.Exists(m_XsdFile))
            {
                m_Ds = new DataSet();
                m_Ds.ReadXmlSchema(m_XsdFile);
            }
            else
            {
                CreateSchema();
            }

            if (File.Exists(m_XmlFile))
            {
                m_Ds.ReadXml(m_XmlFile, XmlReadMode.IgnoreSchema);
            }
        }

        private void CreateSchema()
        {
            m_Ds = new DataSet("VehiclesRepairs");

            var vehicles = m_Ds.Tables.Add("Vehicles");
            vehicles.Columns.Add("VIN", typeof (string));
            vehicles.Columns.Add("Make", typeof (string));
            vehicles.Columns.Add("Model", typeof (string));
            vehicles.Columns.Add("Year", typeof (int));
            vehicles.PrimaryKey = new DataColumn[] {vehicles.Columns["VIN"]};

            var repairs = m_Ds.Tables.Add("Repairs");
            var pk = repairs.Columns.Add("ID", typeof (int));
            pk.AutoIncrement = true;
            pk.AutoIncrementSeed = -1;
            pk.AutoIncrementStep = -1;
            repairs.Columns.Add("VIN", typeof (string));
            repairs.Columns.Add("Description", typeof (string));
            repairs.Columns.Add("Cost", typeof (decimal));
            repairs.PrimaryKey = new DataColumn[] { repairs.Columns["ID"] };

            m_Ds.Relations.Add("vehicles_repairs", vehicles.Columns["VIN"], repairs.Columns["VIN"]);

            m_Ds.WriteXmlSchema(m_XsdFile);
        }

        private void Form1FormClosing(object sender, FormClosingEventArgs e)
        {
            m_Ds.WriteXml(m_XmlFile, XmlWriteMode.DiffGram);
        }

    }
}
