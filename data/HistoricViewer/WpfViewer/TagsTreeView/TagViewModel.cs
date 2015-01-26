using System.Collections.ObjectModel;
using System.Linq;
using HistoricEntitiesCodeFirst;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace WpfViewer.TagsTreeView
{
    public class TagViewModel : NotificationObject
    {
        public DelegateCommand AddChildCommand { get; private set; }

        private readonly ObservableCollection<TagViewModel> m_Children;
        private bool m_IsSelected;
        private bool m_IsExpanded;
        private readonly TagViewModel m_Parent;
        private Tag m_Tag;
        private bool m_IsReadOnly;
        

        public TagViewModel()
        {
            m_Children = new ObservableCollection<TagViewModel>();
            AddChildCommand = new DelegateCommand(OnAddChild);
        }


        public TagViewModel(Tag tag) : this(tag, null, null) { }

        public TagViewModel(Tag tag, TagViewModel parent, IRepository repository)
            : this()
        {
            m_Tag = tag;
            m_Parent = parent;
            Repository = repository;
            if (tag.Children.Any())
            {
                m_Children.AddRange(
                    (from child in tag.Children
                     select new TagViewModel(child, this, repository)).ToList());
            }
        }

        public IRepository Repository { get; set; }

        public ObservableCollection<TagViewModel> Children
        {
            get { return m_Children; }
        }

        public string Name
        {
            get { return m_Tag.Name; }
            set
            {
                if (value.Equals(m_Tag.Name))
                    return;
                m_Tag.Name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public int Id
        {
            get { return m_Tag.Id; }
        }

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is expanded.
        /// </summary>
        public bool IsExpanded
        {
            get { return m_IsExpanded; }
            set
            {
                if (value != m_IsExpanded)
                {
                    m_IsExpanded = value;
                    RaisePropertyChanged(() => IsExpanded);
                }

                // Expand all the way up to the root.
                if (m_IsExpanded && m_Parent != null)
                    m_Parent.IsExpanded = true;
            }
        }

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return m_IsSelected; }
            set
            {
                if (value != m_IsSelected)
                {
                    m_IsSelected = value;
                    RaisePropertyChanged(() => IsSelected);
                    m_IsReadOnly = !value;
                }
            }
        }

        public bool IsReadOnly
        {
            get { return m_IsReadOnly; }
            set
            {
                if (value.Equals(m_IsReadOnly)) return;
                m_IsReadOnly = value;
                RaisePropertyChanged(() => IsReadOnly);
            }
        }


        private void OnAddChild()
        {
            var newTag = new Tag
            {
                Name = "New child",
                Parent = m_Tag
            };
            Repository.Add(newTag);
            // 2015-01-26 FB: Causes a duplicate child:
            // m_Tag.Children.Add(newTag); 
            m_Children.Add(new TagViewModel(newTag, this, Repository)
            {
                IsSelected = true,
            });
            IsExpanded = true;
            IsSelected = false;
        }
        
        public Tag Tag
        {
            get { return m_Tag; }
            set { m_Tag = value; }
        }
    }
}