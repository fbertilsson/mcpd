using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using HistoricEntitiesCodeFirst;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace WpfViewer.TagsTreeView
{
    public class TagsTreeViewModel : NotificationObject
    {
        public DelegateCommand SaveCommand { get; private set; }
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

            foreach (var tagViewModel in m_Roots)
            {
                tagViewModel.Repository = m_Repository;
            }

            SaveCommand = new DelegateCommand(OnSave);
        }

        private void OnSave()
        {
            try
            {
                m_Repository.SaveChanges();
            }
            catch (Exception e)
            {
                var x = e; // TODO FB
            }
        }

        public ObservableCollection<TagViewModel> Roots
        {
            get { return m_Roots; }
        }

        public void AddTag(string name)
        {
            var newTag = new Tag {Name = name};
            m_Repository.Add(newTag);
            m_Roots.Add(new TagViewModel(newTag)
            {
                IsReadOnly = false,
                IsSelected = true,
                Repository = m_Repository,
            });
        }
    }
}
