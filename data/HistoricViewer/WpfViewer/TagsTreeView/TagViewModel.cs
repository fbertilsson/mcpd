using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using HistoricEntitiesCodeFirst;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace WpfViewer.TagsTreeView
{
    public class TagViewModel : NotificationObject, ITagParent
    {
        public DelegateCommand AddChildCommand { get; private set; }
        public DelegateCommand DeleteCommand { get; private set; }

        private bool m_AreChildrenLoaded;
        private readonly ObservableCollection<TagViewModel> m_Children;
        private bool m_IsSelected;
        private bool m_IsExpanded;
        private readonly ITagParent m_Parent;
        private Tag m_Tag;
        private bool m_IsReadOnly;
        

        public TagViewModel()
        {
            m_Children = new ObservableCollection<TagViewModel>();
            AddChildCommand = new DelegateCommand(OnAddChild);
            DeleteCommand = new DelegateCommand(OnDeleteCommand);
        }


        public TagViewModel(Tag tag) : this(tag, null, null) { }

        public TagViewModel(Tag tag, ITagParent parent, IRepository repository)
            : this()
        {
            m_Tag = tag;
            m_Parent = parent;
            Repository = repository;
            if (tag.Children.Any())
            {
                m_Children.AddRange(
                    (from child in m_Tag.Children
                    select new TagViewModel(child, this, Repository)).ToList());
            }
        }

        private void EnsureGrandchildrenAdded()
        {
            foreach (var child in Children)
            {
                if (!child.Children.Any())
                {
                    child.AddChildren();
                }
            }
        }

        private void AddChildren()
        {
            if (m_AreChildrenLoaded)
            {
                return;
            }
            
            var children = Repository.Tags
                .Include(x => x.Children)
                .Where(t => (t.Parent != null) && (t.Parent.Id == m_Tag.Id));

            foreach (var child in children)
            {
                var childViewModel = new TagViewModel(child, this, Repository);
                m_Children.Add(childViewModel);
            }

            m_AreChildrenLoaded = true;
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
                    if (value)
                    {
                        EnsureGrandchildrenAdded();
                    }
                    m_IsExpanded = value;
                    RaisePropertyChanged(() => IsExpanded);
                }

                // Expand all the way up to the root.
                //if (m_IsExpanded && m_Parent != null)
                //    m_Parent.IsExpanded = true;
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
            AddChild(newTag);
            IsExpanded = true;
            IsSelected = false;
        }

        public TagViewModel AddChild(Tag newTag)
        {
            Repository.Add(newTag);
            // 2015-01-26 FB: Causes a duplicate child:
            // m_Tag.Children.Add(newTag); 
            var newTagViewModel = new TagViewModel(newTag, this, Repository)
            {
                IsSelected = true,
            };
            m_Children.Add(newTagViewModel);
            return newTagViewModel;
        }

        /// <summary>
        /// Deletes this instance and its children from the repository,
        /// as well as from the hierarchy.
        /// </summary>
        public void Delete()
        {
            if (m_Parent != null)
            {
                m_Parent.DeleteChild(this);
            }

            DeleteInternal();
        }

        private void DeleteInternal()
        {
            Repository.Remove(m_Tag);

            foreach (var child in m_Children)
            {
                child.DeleteInternal();
            }
            Children.Clear();
        }

        public Tag Tag
        {
            get { return m_Tag; }
            set { m_Tag = value; }
        }

        private void OnDeleteCommand()
        {
            Delete();
        }

        public void DeleteChild(TagViewModel child)
        {
            if (Children.Any())
            {
                Children.Remove(child);
            }
        }

        public override string ToString()
        {
            return string.Format("TagViewModel: {0}", Name);
        }
    }
}