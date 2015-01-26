using System.Linq;

namespace HistoricEntitiesCodeFirst
{
    public interface IRepository
    {
        IQueryable<HistoricEvent> HistoricEvents { //get { return m_Context.HistoricEvents; }
            get; }

        IQueryable<Tag> Tags { get; }
        IQueryable<TimeRef> TimeRefs { get; }
        int SaveChanges();
        Tag Add(Tag tag);
        void Remove(Tag tag);
        void Add(HistoricEvent historicEvent);
        void Add(TimeRefSpan timeRefSpan);
    }
}
