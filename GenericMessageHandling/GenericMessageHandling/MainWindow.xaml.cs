using System;
using ServiceModelEx;

namespace GenericMessageHandling
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        //private readonly IDataService m_Proxy;

        public MainWindow()
        {
            DataContext = new MainWindowViewModel();
            InitializeComponent();
            //m_Proxy = InProcFactory.CreateInstance<DataService, IDataService>();
        }

        private void ButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(Go));
        }

        private void Go()
        {
            //var x = m_Proxy.GetData();
            //Console.WriteLine(x);
        }
    }
}
