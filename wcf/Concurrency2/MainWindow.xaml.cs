using System.Windows;

namespace Concurrency2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();

            Loaded += SetFocus;
        }

        private void SetFocus(object sender, RoutedEventArgs e)
        {
            PrimeCandidate.Focus();
        }

    }
}
