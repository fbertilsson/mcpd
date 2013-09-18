using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LinqToSql1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly LinqToSql1DataContext m_Context;

        public MainWindow()
        {
            InitializeComponent();

            m_Context = new LinqToSql1DataContext();
            var topics = from t in m_Context.Topics
                         select t;
            TopicsGrid.ItemsSource = topics;
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            m_Context.SubmitChanges();
        }

        private void OnRevert(object sender, RoutedEventArgs e)
        {
            m_Context.Refresh(RefreshMode.OverwriteCurrentValues);
            //TopicsGrid.
        }
    }
}
