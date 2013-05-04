using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DiscoveryAnnouncementChat;

namespace DiscoveryChat
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var isService = (e.Args.Length > 0 && e.Args[0].ToLowerInvariant().Equals("service"));
            
            new ChatWindow(isService).Show();
            
        }
    }
}
