using System.Collections.Generic;
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
        private Tag m_TagA;
        private Tag m_TagB;

        [SetUp]
        public void SetUp()
        {
            m_TagA = new Tag { Name = "A", Id = 1 };
            m_TagB = new Tag { Name = "B", Id = 2 };
            m_Tag = new Tag();
            var tagList = new List<Tag> {m_TagA, m_TagB};

            m_Repository = Substitute.For<IRepository>();
            m_Repository.Tags.Returns(tagList.AsQueryable());

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

        [Test]
        public void DeleteChild_WhenNoChildren_ChildrenIsEmpty()
        {
            // Arrange
            // Act
            m_Model.DeleteChild(new TagViewModel());

            // Assert
            Assert.IsEmpty(m_Model.Children);
        }

        [Test]
        public void DeleteChild_WhenNotAChild_ChildrenIsUnchanged()
        {
            // Arrange
            var child = m_Model.AddChild(new Tag());

            // Act
            m_Model.DeleteChild(new TagViewModel());

            // Assert
            Assert.AreEqual(1, m_Model.Children.Count);
            Assert.AreSame(child, m_Model.Children.First());
        }

        [Test]
        public void DeleteChild_WhenFirstChildOfTwo_IsRemovedFromChildren()
        {
            // Arrange
            var childA = m_Model.AddChild(m_TagA);
            var childB = m_Model.AddChild(m_TagB);

            // Act
            m_Model.DeleteChild(childA);

            // Assert
            Assert.AreEqual(1, m_Model.Children.Count);
            Assert.AreSame(childB, m_Model.Children.First());
        }

        [Test]
        public void DeleteChild_WhenSecondChildOfTwo_IsRemovedFromChildren()
        {
            // Arrange
            
            var childA = m_Model.AddChild(m_TagA);
            var childB = m_Model.AddChild(m_TagB);

            // Act
            m_Model.DeleteChild(childB);

            // Assert
            Assert.AreEqual(1, m_Model.Children.Count);
            Assert.AreSame(childA, m_Model.Children.First());
        }

        [Test]
        public void DeleteCommand_WhenRoot_CallsRepoDotDelete()
        {
            // Arrange
            // Act
            m_Model.DeleteCommand.Execute();

            // Assert
            m_Repository.Received(1).Remove(m_Tag);
        }

        [Test]
        public void DeleteCommand_WhenHasChildren_CallsRepoDotDeleteOnChildren()
        {
            // Arrange
            var childA = new Tag { Name = "A", Parent = m_Tag };
            var childB = new Tag { Name = "B", Parent = m_Tag };
            m_Model.AddChild(childA);
            m_Model.AddChild(childB);

            // Act
            m_Model.DeleteCommand.Execute();

            // Assert
            m_Repository.Received(1).Remove(childA);
            m_Repository.Received(1).Remove(childB);
        }

        [Test]
        public void DeleteCommand_WhenHasChildren_RemovesThemFromChildren()
        {
            // Arrange
            var childA = new Tag { Name = "A", Parent = m_Tag };
            var childB = new Tag { Name = "B", Parent = m_Tag };
            m_Model.AddChild(childA);
            m_Model.AddChild(childB);

            // Act
            m_Model.DeleteCommand.Execute();

            // Assert
            Assert.IsEmpty(m_Model.Children);
        }

        [Test]
        public void DeleteCommand_WhenHasParent_RemovesItFromParentsChildren()
        {
            // Arrange
            var childA = new Tag { Name = "A", Parent = m_Tag };
            var childB = new Tag { Name = "B", Parent = m_Tag };
            var viewModelA = m_Model.AddChild(childA);
            var viewModelB = m_Model.AddChild(childB);

            // Act
            viewModelA.DeleteCommand.Execute();

            // Assert
            Assert.Contains(viewModelB, m_Model.Children);
            Assert.AreEqual(1, m_Model.Children.Count);
        }

        [Test]
        public void SetIsExpanded_WhenChildrenAreLoaded_DoesNotLoadAgain()
        {
            // Arrange
            var parent = new Tag { Name = "Root" };
            m_TagA = new Tag { Name = "A", Id = 1, Parent = parent };
            m_TagB = new Tag { Name = "B", Id = 2, Parent = parent };
            var b1 = new Tag { Name = "B1", Id = 21, Parent = m_TagB };
            parent.Children.Add(m_TagA);
            parent.Children.Add(m_TagB);
            m_TagB.Children.Add(b1);
            var tagList = new List<Tag> { parent, m_TagA, m_TagB, b1 };
            m_Repository.Tags.Returns(tagList.AsQueryable());
            m_Model = new TagViewModel(parent, null, m_Repository);

            var modelB = m_Model.Children.First(t => t.Name == "B");
            m_Model.IsExpanded = true;
            m_Model.IsExpanded = false;
            m_Repository.ClearReceivedCalls();

            // Act
            m_Model.IsExpanded = true;

            // Assert
            Assert.AreEqual(1, modelB.Children.Count);
            var dummy = m_Repository.DidNotReceive().Tags;
        }
    }
}
