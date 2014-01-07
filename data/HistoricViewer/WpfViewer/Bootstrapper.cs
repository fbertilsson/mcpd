using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;

namespace WpfViewer
{
    internal class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            PrintDataDirectoryPath();
            //var connectionString = ConfigurationManager.ConnectionStrings["localHistoric"];
            var connectionString = ConfigurationManager.ConnectionStrings["nofreberHistoric"];

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<HistoricEntitiesCodeFirst.HistoricEventContext>());

            //var model = Container.TryResolve<ShellViewModel>();
            var model = Container.Resolve<ShellViewModel>(new ResolverOverride[]
                {
                    new ParameterOverride("connectionString", connectionString.ToString()),
                });
            //var model = Container.Resolve<ShellViewModel>() as ShellViewModel;
            var mainWindow = new Shell(model);
            return mainWindow;
        }

        private static string PrintDataDirectoryPath()
        {
            var debugDataDirectoryPath = (string) AppDomain.CurrentDomain.GetData("DataDirectory");
            if (string.IsNullOrEmpty(debugDataDirectoryPath))
            {
                debugDataDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
            }
            Debug.WriteLine("DataDirectory: {0}", debugDataDirectoryPath);
            return debugDataDirectoryPath;
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (Shell)Shell;
            Application.Current.MainWindow.Show();
        }
    }
}
