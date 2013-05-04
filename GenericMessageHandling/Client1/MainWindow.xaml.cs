using System;
using System.Windows;
using System.ServiceModel;
using Host1;

namespace Client1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GetDataClick(object sender, RoutedEventArgs e)
        {
            var factory = new ChannelFactory<IDataService>("Host1.DataService");
            var proxy = factory.CreateChannel();
            var data = proxy.GetData();
            Dispatcher.Invoke(new Action(() => ResponseText.Text = data.ToString()));

            //proxy.SetData(data);
        }

        private void GetFaultClick(object sender, RoutedEventArgs dummy)
        {
            var factory = new ChannelFactory<IDataService>("Host1.DataService");
            var proxy = factory.CreateChannel();
            try
            {
                var data = proxy.GetFault();
                var isFault = data.IsFault;
                Dispatcher.Invoke(new Action(() => ResponseText.Text = string.Format("isFault: {0} \n {1}", isFault, data)));
            }
            catch (FaultException e)
            {
                var message = string.Format("Fault, Code: {0}, Reason: {1}\n{2}", e.Code, e.Reason, e);
                Dispatcher.Invoke(new Action(() => ResponseText.Text =message));
            }

            catch (Exception e)
            {
                Dispatcher.Invoke(new Action(() => ResponseText.Text = e.ToString()));
            }

        }
    }
}
