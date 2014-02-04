using System.Globalization;
using System.Threading;
using HistoricEntitiesCodeFirst;
using Microsoft.Practices.Prism;

namespace WpfViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {

        public App()
        {
            new Bootstrapper().Run();
            //using (var repo = new Repository())
            //{                
                //repo.Add(new Tag {Name = "WW2"});
                //repo.SaveChanges();
            //}

            
        }
    }
}
