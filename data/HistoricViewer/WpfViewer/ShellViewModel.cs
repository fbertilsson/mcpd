using System.Threading;
using HistoricEntitiesCodeFirst;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;

namespace WpfViewer
{
    public class ShellViewModel : NotificationObject
    {
        public DelegateCommand EditTagsCommand { get; private set; }
        public DelegateCommand EditHistoryCommand { get; private set; }

        public IRegionManager RegionManager { get; private set; }

        public IShell View { get; set; }

        private readonly Repository m_Repository;

        public ShellViewModel(IRegionManager regionManager, string connectionString)
        {
            RegionManager = regionManager;
            EditTagsCommand = new DelegateCommand(OnEditTagsCommand);   
            EditHistoryCommand = new DelegateCommand(OnEditHistoryCommand);
            m_Repository = new Repository(connectionString);
        }

        private void OnEditTagsCommand()
        {
            var tags = m_Repository.Tags;
            var tagsModel = new TagsViewModel(tags, m_Repository)
                {
                    SaveDelegate = () => m_Repository.SaveChanges()
                };
            var tagsView = new TagsView(tagsModel);
            RegionManager.Regions["MainRegion"].Add(tagsView);
        }


        private void OnEditHistoryCommand()
        {
            View.SetWaitCursor();
            try
            {
                var historicEvents = m_Repository.HistoricEvents;
                var historicEventsModel = new HistoricEventsViewModel(historicEvents, m_Repository)
                    {
                        SaveDelegate = () => m_Repository.SaveChanges()
                    };
                var historicEventsView = new HistoricEventsView(historicEventsModel);
                Thread.Sleep(2000);
                RegionManager.Regions["MainRegion"].Add(historicEventsView); // TODO Clear existing?
            } finally {
                View.SetNormalCursor();
            }
        }

    }
}
