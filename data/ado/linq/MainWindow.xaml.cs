using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace linq
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IFibonacciView
    {
        private readonly MainWindowViewModel m_Model;

        public MainWindow()
        {
            InitializeComponent();
            m_Model = new MainWindowViewModel(this);
            DataContext = m_Model;
        }

        public void SetWaitPointer()
        {
            Cursor = Cursors.Wait;
        }

        public void SetNormalPointer()
        {
            Cursor = Cursors.Arrow;
        }

        public void InvokeAsync(Action a)
        {
            Dispatcher.InvokeAsync(a, DispatcherPriority.Background);
        }



    }
}
