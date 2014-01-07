using System;
using System.Linq;
using HistoricEntitiesCodeFirst;

namespace WpfViewer
{
    public class HistoricEventsViewModel
    {
        public Action SaveDelegate { get; set; }

        public HistoricEventsViewModel(IQueryable<HistoricEvent> historicEvents, Repository repository)
        {
            
        }

    }
}