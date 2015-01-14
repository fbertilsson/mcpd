using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HistoricEntitiesCodeFirst;
using Microsoft.Practices.Prism.ViewModel;

namespace WpfViewer.TagsTreeView
{
    public class TagViewModel : NotificationObject
    {
        private readonly IReadOnlyCollection<TagViewModel> m_Children;
        private bool m_IsSelected;
        private bool m_IsExpanded;
        private readonly TagViewModel m_Parent;
        private Tag m_Tag;
        private bool m_IsEditing;

        public TagViewModel()
        {            
        }

        public TagViewModel(Tag tag) : this(tag, null) { }

        public TagViewModel(Tag tag, TagViewModel parent)
        {
            m_Tag = tag;
            m_Parent = parent;
            if (tag.Children == null)
            {
                m_Children = new List<TagViewModel>();
            }
            else
            {
                m_Children = new ReadOnlyCollection<TagViewModel>(
                    (from child in tag.Children
                        select new TagViewModel(child, this)).ToList());
            }
        }

        public IEnumerable<TagViewModel> Children
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
                }
            }
        }

        public bool IsEditing
        {
            get { return m_IsEditing; }
            set
            {
                if (value.Equals(m_IsEditing)) return;
                m_IsEditing = value;
                RaisePropertyChanged(() => IsEditing);
            }
        }

        public Tag Tag
        {
            get { return m_Tag; }
            set { m_Tag = value; }
        }
    }
}