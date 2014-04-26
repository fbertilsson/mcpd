using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace WpfViewer.Resolve
{
    public class ResolveViewModel : NotificationObject
    {
        public DelegateCommand UseCurrentValuesCommand { get; private set; }
        public DelegateCommand UseDatabaseValuesCommand{ get; private set; }
        public IResolveView View { get; set; }

        private readonly IEnumerable<DbEntityEntry> m_Entries;
        private readonly DbPropertyValues m_CurrentValues;
        private readonly DbPropertyValues m_DbValues;
        private readonly DbPropertyValues m_ResolvedValues;
        private bool m_IsCurrentDeleted;
        private bool m_IsDbDeleted;

        public ResolveViewModel(IEnumerable<DbEntityEntry> entries)
        {
            m_Entries = entries.ToList();
            var entry = m_Entries.First();
            var dbValues = entry.GetDatabaseValues();
            IsDbDeleted = dbValues == null;
            IsCurrentDeleted = entry.State == EntityState.Deleted;
            DbPropertyValues currentValues = IsCurrentDeleted ? null : entry.CurrentValues;
            var resolvedValues = dbValues == null ? null : dbValues.Clone();  // Conservatively, let the dbValues be the default resolved

            m_CurrentValues = currentValues;
            m_DbValues = dbValues;
            m_ResolvedValues = resolvedValues;

            ApplyCommand = new DelegateCommand(OnApplyCommand);
            UseCurrentValuesCommand = new DelegateCommand(OnUseCurrentValuesCommand);
            UseDatabaseValuesCommand = new DelegateCommand(OnUseDatabaseValuesCommand);
        }

        private void OnApplyCommand()
        {
            var entry = m_Entries.First();
            entry.OriginalValues.SetValues(m_DbValues);
            entry.CurrentValues.SetValues(m_ResolvedValues);
        }

        private void OnUseCurrentValuesCommand()
        {
            throw new System.NotImplementedException();
        }

        private void OnUseDatabaseValuesCommand()
        {
            m_Entries.Single().Reload();
            View.Close();
        }

        public DbPropertyValues CurrentValues
        {
            get { return m_CurrentValues; }
        }

        public DbPropertyValues DbValues
        {
            get { return m_DbValues; }
        }

        public DbPropertyValues ResolvedValues
        {
            get { return m_ResolvedValues; }
        }

        public DelegateCommand ApplyCommand { get; private set; }

        public bool IsCurrentDeleted
        {
            get { return m_IsCurrentDeleted; }
            set { m_IsCurrentDeleted = value; }
        }

        public bool IsDbDeleted
        {
            get { return m_IsDbDeleted; }
            set { m_IsDbDeleted = value; }
        }

    }
}