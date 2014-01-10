using System;
using System.Linq;

namespace HistoricEntitiesCodeFirst
{
    /// <summary>
    /// A thin wrapper around the Entity Framework entity context class.
    /// </summary>
    public class Repository : IDisposable
    {

        private readonly HistoricEventContext m_Context;

        public Repository()
        {
            m_Context = new HistoricEventContext();
        }

        public Repository(string connectionString)
        {
            m_Context = new HistoricEventContext(connectionString);
        }

        public IQueryable<HistoricEvent> HistoricEvents
        {
            get { return m_Context.HistoricEvents; }
        }

        public IQueryable<Tag> Tags
        {
            get { return m_Context.Tags; }        
        } 

        //public IQueryable<TimeRef> TimeRefs
        //{
        //    get { return m_Context.TimeRefs; }
        //}


        public int SaveChanges()
        {
            return m_Context.SaveChanges();
        }


        public Tag Add(Tag tag)
        {
            return m_Context.Tags.Add(tag);
        }

        public void Remove(Tag tag)
        {
            m_Context.Tags.Remove(tag);
        }


        public void Add(HistoricEvent historicEvent)
        {
            m_Context.HistoricEvents.Add(historicEvent);
        }

        #region IDisposable

        private bool m_IsDisposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            //Log.DebugFormat("Enter, isDisposing: {0}", isDisposing);
            lock (this)
            {
                if (!m_IsDisposed)
                {
                    m_IsDisposed = true;
                    if (isDisposing)
                    {
                        // Cleanup managed objects by calling their Dispose(), make threads exit, unregister events etc:
                        m_Context.Dispose();
                    }
                }

                // Cleanup native objects (if any):
                // None yet.
            }

            //Log.Debug("Exit");
        }

        #endregion IDisposable

    }
}
