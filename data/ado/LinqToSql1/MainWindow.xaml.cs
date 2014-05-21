using System.Data.Linq;
using System.Linq;
using System.Windows;

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
        }

        private void OnMarkForDeletionOnSave(object sender, RoutedEventArgs e)
        {
            var topic = m_Context.Topics.First();
            m_Context.Topics.DeleteOnSubmit(topic);
            MessageBox.Show("Marked for deletion. Note the popup in the extension point when you save.");
        }
    }
}
