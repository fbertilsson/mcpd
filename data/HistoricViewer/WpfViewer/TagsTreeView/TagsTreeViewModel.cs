using System.Collections.ObjectModel;
using System.Linq;
using HistoricEntitiesCodeFirst;
using Microsoft.Practices.ServiceLocation;

namespace WpfViewer.TagsTreeView
{
    public class TagsTreeViewModel
    {
        private readonly ObservableCollection<TagViewModel> m_Roots;
        private readonly Repository m_Repository;

        public TagsTreeViewModel(IQueryable<Tag> tags, Repository repository, IServiceLocator serviceLocator)
        {
            // TODO remove
            //var a1 = new Tag { Name = "a1" };
            //var alpha = new TagViewModel(new Tag { Name = "Alpha", Children = new List<Tag> { a1 }});
            //var beta = new TagViewModel(new Tag { Name = "Beta"});
            //m_Roots = new ReadOnlyCollection<TagViewModel>(new[]
            //{
            //    alpha, 
            //    beta,
            //});

            m_Repository = repository;
            m_Roots = new ObservableCollection<TagViewModel>(
                (from tag in tags 
                 where tag.Parent == null
                 select new TagViewModel { Tag = tag} ).ToList());
        }

        public ObservableCollection<TagViewModel> Roots
        {
            get { return m_Roots; }
        }

        public void AddTag(string name)
        {
            var newTag = new Tag {Name = name};
            m_Repository.Add(newTag);
            m_Roots.Add(new TagViewModel(newTag) { IsEditing = true });
        }
    }
}
