using System;
using System.Linq;
using HistoricEntitiesCodeFirst;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace WpfViewer
{
    public class HistoricEventsViewModel : NotificationObject
    {
        public Action SaveDelegate { get; set; }

        public IQueryable<HistoricEvent> HistoricEvents
        {
            get { return m_HistoricEvents; }
        }

        public DelegateCommand CloseCommand { get; private set; }
        
        public DelegateCommand NewCommand { get; private set; }

        public DelegateCommand SaveCommand { get; private set; }

        private HistoricEvent m_CurrentHistoricEvent;


        private IQueryable<HistoricEvent> m_HistoricEvents;

        private Repository m_Repository;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="historicEvents"></param>
        /// <param name="repository"></param>
        public HistoricEventsViewModel(IQueryable<HistoricEvent> historicEvents, Repository repository)
        {
            m_HistoricEvents = historicEvents;
            historicEvents.ToList();
            m_Repository = repository;

            CloseCommand = new DelegateCommand(OnClose);
            NewCommand = new DelegateCommand(OnNew);
            SaveCommand = new DelegateCommand(OnSave);
        }

        public HistoricEvent CurrentHistoricEvent
        {
            get { return m_CurrentHistoricEvent; }
            set
            {
                if (value == m_CurrentHistoricEvent) return;
                m_CurrentHistoricEvent = value;
                RaisePropertyChanged(() => CurrentHistoricEvent);
            }
        }

        private void OnClose()
        {
            throw new NotImplementedException();
        }

        private void OnNew()
        {
            m_CurrentHistoricEvent = new HistoricEvent {Name = "New"};
            m_Repository.Add(m_CurrentHistoricEvent);
            RaisePropertyChanged(() => CurrentHistoricEvent);
        }

        private void OnSave()
        {
            SaveDelegate();
        }
    }
}