using System.Configuration;
using System.Threading;
using NorthwindPocoEntities;

namespace NorthwindPocoClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            var c = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = c; // but did not get swedish date format :-(

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
