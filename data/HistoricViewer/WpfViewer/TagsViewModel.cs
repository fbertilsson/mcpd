using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Windows.Data;
using HistoricEntitiesCodeFirst;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace WpfViewer
{
    public class TagsViewModel : NotificationObject
    {
        protected bool IsModified
        {
            get { return m_IsModified; }
            set
            {
                if (value == m_IsModified) return;
                m_IsModified = value;
                RaisePropertyChanged(() => IsModified);
            }
        }

        public ObservableCollection<Tag> ObservableTags { get; private set; }
        public IEnumerable<Tag> ParentTags
        {
            get
            {
                return SelectedTag == null ? 
                    null : 
                    ObservableTags.Where(t => !t.Equals(SelectedTag) 
                        && ((t.Parent == null) || (t.Parent != SelectedTag)) ); // TODO filter out parents
            }
        }

        public DelegateCommand CloseCommand { get; private set; }
        public DelegateCommand SaveCommand { get; private set; }

        public Tag SelectedTag
        {
            get { return m_SelectedTag; }
            set
            {
                if (Equals(value, m_SelectedTag)) return;
                m_SelectedTag = value;
                RaisePropertyChanged(() => SelectedTag);
                RaisePropertyChanged(() => ParentTags);
            }
        }

        private IQueryable<Tag> m_Tags;
        private DataTable m_DataTable;
        private bool m_IsModified;
        private Tag m_SelectedTag;


        public TagsViewModel(IQueryable<Tag> tags, Repository repository)
        {
            //Tags = tags;
            var dummy = tags.ToList();
            ObservableTags = new ObservableCollection<Tag>(dummy);
            ObservableTags.CollectionChanged += (sender, args) =>
                {
                    IsModified = true;
                    SaveCommand.RaiseCanExecuteChanged();

                    switch (args.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                            foreach (var newItem in args.NewItems)
                            {
                                var tag = newItem as Tag;
                                if (tag != null)
                                {
                                    repository.Add(tag);
                                }
                            }
                            break;
                            // If I add the tag here, but the user cancels, I must remove it. 
                            // Better to use the data table and add/remove items when user saves?

                        case NotifyCollectionChangedAction.Remove:
                            foreach (var item in args.OldItems)
                            {
                                var tag = item as Tag;
                                if (tag != null)
                                {
                                    repository.Remove(tag);
                                }

                            }
                            break;
                    }

                };

            CloseCommand = new DelegateCommand(OnCloseCommand);
            SaveCommand = new DelegateCommand(OnSaveCommand, CanSaveCommand);
        }

        public Action CloseDelegate { get; set; }

        private void OnCloseCommand()
        {
            if (CloseDelegate != null)
            {
                CloseDelegate();
            }
        }

        public Action SaveDelegate { get; set; }

        private void OnSaveCommand()
        {
            try
            {
                View.SetWaitCursor();
                SaveDelegate();
                IsModified = false;
            }
            finally
            {
                View.SetNormalCursor();
            }
        }

        private bool CanSaveCommand()
        {
            return IsModified;
        }


        public IQueryable<Tag> Tags
        {
            get { return m_Tags; }
            private set
            {
                m_Tags = value;
                m_DataTable = new DataTable();
                //m_DataTable.Load(IDataReader);
                var dataSet = new DataSet();
                dataSet.Tables.Add(m_DataTable);
                //TagCollection = new CollectionViewSource
                //    {
                //        Source = m_Tags.ToList()
                //    };
            }
        }

        public CollectionViewSource TagCollection { get; private set; }
        
        public ITagsView View { get; set; }
    }
}
