using System.Linq;
using System.Threading;
using HistoricEntitiesCodeFirst;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace WpfViewer
{
    public class ShellViewModel : NotificationObject
    {
        public DelegateCommand EditTagsCommand { get; private set; }
        public DelegateCommand EditHistoryCommand { get; private set; }

        public IRegionManager RegionManager { get; private set; }

        public IShell View { get; set; }

        private readonly Repository m_Repository;
        private IServiceLocator ServiceLocator { get; set; }


        public ShellViewModel(IRegionManager regionManager, string connectionString, IServiceLocator serviceLocator)
        {
            RegionManager = regionManager;
            EditTagsCommand = new DelegateCommand(OnEditTagsCommand);   
            EditHistoryCommand = new DelegateCommand(OnEditHistoryCommand);
            m_Repository = new Repository(connectionString);
            ServiceLocator = serviceLocator;
        }

        private void OnEditTagsCommand()
        {
            View.SetWaitCursor();
            try
            {
                var mainRegion = RegionManager.Regions["MainRegion"];
                foreach (var view in mainRegion.Views)
                {
                    if (view is TagsView)
                    {
                        mainRegion.Activate(view);
                        return;
                    }
                }
                var tags = m_Repository.Tags;
                var tagsModel = new TagsViewModel(tags, m_Repository, ServiceLocator)
                    {
                        SaveDelegate = () => m_Repository.SaveChanges(),
                    };
                var tagsView = new TagsView(tagsModel);
                tagsModel.View = tagsView;
                tagsModel.CloseDelegate = () => mainRegion.Deactivate(tagsView);

                mainRegion.Add(tagsView);
                mainRegion.Activate(tagsView);
            }
            finally
            {
                View.SetNormalCursor();
            }
        }


        private void OnEditHistoryCommand()
        {
            View.SetWaitCursor();
            try
            {
                var mainRegion = RegionManager.Regions["MainRegion"];
                foreach (var view in mainRegion.Views)
                {
                    if (view is HistoricEventsView)
                    {
                        mainRegion.Activate(view);
                        return;
                    } 
                }

                var historicEvents = m_Repository.HistoricEvents;
                var historicEventsModel = new HistoricEventsViewModel(historicEvents, m_Repository)
                    {
                        SaveDelegate = () => m_Repository.SaveChanges()
                    };
                var historicEventsView = new HistoricEventsView(historicEventsModel);
                
                historicEventsModel.CloseDelegate = () => mainRegion.Deactivate(historicEventsView);

                mainRegion.Add(historicEventsView);
                mainRegion.Activate(historicEventsView);
            } finally {
                View.SetNormalCursor();
            }
        }

    }
}
