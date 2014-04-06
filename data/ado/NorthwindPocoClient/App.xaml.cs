using System.Configuration;
using System.Windows;
using NorthwindPocoEntities;

namespace NorthwindPocoClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["NorthwindPocoEntities"].ConnectionString;
            var context = new NorthwindPocoContext(connectionString);
            var model = new MainWindowViewModel(context);
            var view = new MainWindow
            {
                DataContext = model
            };
            view.Show();
        }
    }
}
