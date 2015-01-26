using System.Linq;
using HistoricEntitiesCodeFirst;
using NSubstitute;
using NUnit.Framework;
using WpfViewer.TagsTreeView;

namespace WpfViewerTest
{
    [TestFixture]
    public class TagViewModelTest
    {
        private TagViewModel m_Model;
        private IRepository m_Repository;
        private Tag m_Tag;

        [SetUp]
        public void SetUp()
        {
            m_Repository = Substitute.For<IRepository>();
            m_Tag = new Tag();
            m_Model = new TagViewModel(m_Tag, null, m_Repository);
        }

        [Test]
        public void AddChildCommand_Always_AddsOneChildNamedNewChild()
        {
            // Arrange
            // Act
            m_Model.AddChildCommand.Execute();

            // Assert
            Assert.AreEqual(1, m_Model.Children.Count);
            Assert.AreEqual("New child", m_Model.Children.First().Name);
        }
        
        [Test]
        public void AddChildCommand_Always_AddsTagToRepository()
        {
            // Arrange
            // Act
            m_Model.AddChildCommand.Execute();

            // Assert
            m_Repository.Received(1).Add(Arg.Is<Tag>(t => t.Name == "New child"));
        }


    }
}
