using System.Collections.Generic;
using System.Linq;
using HistoricEntitiesCodeFirst;
using NSubstitute;
using NUnit.Framework;
using WpfViewer.TagsTreeView;

namespace WpfViewerTest
{
    [TestFixture]
    public class TagsTreeViewModelTest
    {
        private TagsTreeViewModel m_Model;
        private IRepository m_Repository;
        private List<Tag> m_Tags;

        [SetUp]
        public void SetUp()
        {
            m_Repository = Substitute.For<IRepository>();
            m_Tags = new List<Tag>
            {
                new Tag { Name = "Alpha" },
                new Tag { Name = "Bravo" },
            };

            m_Model = new TagsTreeViewModel(m_Tags.AsQueryable(), m_Repository, null);
        }

        [Test]
        public void Remove_WhenFirstRoot_RemovesIt()
        {
            // Arrange
            var firstRoot = m_Model.Roots.First();

            // Act
            m_Model.DeleteChild(firstRoot);

            // Assert
            Assert.AreEqual(1, m_Model.Roots.Count);
            Assert.AreEqual("Bravo", m_Model.Roots.First().Name);
        }

        [Test]
        public void Remove_WhenSecondRoot_RemovesIt()
        {
            // Arrange
            var secondRoot = m_Model.Roots[1];

            // Act
            m_Model.DeleteChild(secondRoot);

            // Assert
            Assert.AreEqual(1, m_Model.Roots.Count);
            Assert.AreEqual("Alpha", m_Model.Roots.First().Name);
        }


    }
}
