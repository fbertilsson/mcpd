using System.Windows;
using System.Windows.Input;

namespace NorthwindPocoClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private MainWindowViewModel m_Model;

        public MainWindow(MainWindowViewModel model)
        {
            m_Model = model;
            DataContext = model;
            InitializeComponent();
        }

        private void MenuItemDetachTest(object sender, RoutedEventArgs e)
        {
            m_Model.OnAddDigitToPhoneNumberTestingDetatch();
        }

        private void OnSave(object sender, ExecutedRoutedEventArgs e)
        {
            m_Model.Save();
        }

        private void OnCanSave(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = m_Model.SelectedCustomer != null;
        }

        private void MenuItemTruncateLastPhoneDigitClick(object sender, RoutedEventArgs e)
        {
            m_Model.OnTruncateLastPhoneDigit();
        }
    }
}
