using System;
using System.Data.Services.Client;
using System.Net;
using System.Text;
using System.Windows;
using WpfClient.NorthwindServiceReference;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly NorthwindEntities m_Db = new NorthwindEntities(new Uri("http://localhost:59557/NorthwindDataService.svc"));

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnGetCustomersClick(object sender, RoutedEventArgs e)
        {
            // Wrap the db.Customers in an observable collection, so that changes are saved
            dg.ItemsSource = new DataServiceCollection<Customer>(m_Db.Customers);
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            m_Db.SaveChanges();
            MessageBox.Show("Saved");
        }

        private void OnGetCustomerWithWebClient(object sender, RoutedEventArgs e)
        {
            var builder = new StringBuilder();
            var uri = m_Db.BaseUri + "/Customers('ALFKI')";

            builder.Append(" ------------- Default -------------\n");
            using (var webClient = new WebClient())
            {
                var result = webClient.DownloadString(uri);
                builder.Append(result);
            }

            builder.Append(Environment.NewLine);
            builder.Append(" ------------- JSON    -------------\n");
            using (var webClient = new WebClient())
            {
                webClient.Headers["Accept"] = "application/json";
                var result = webClient.DownloadString(uri);
                builder.Append(result);
            }

            MessageBox.Show(builder.ToString(), "Results");
        }
    }
}
