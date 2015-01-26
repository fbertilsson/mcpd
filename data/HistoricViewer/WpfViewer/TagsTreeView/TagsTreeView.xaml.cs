using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace WpfViewer.TagsTreeView
{
    /// <summary>
    /// Interaction logic for TagsTreeView.xaml
    /// </summary>
    public partial class TagsTreeView : ITagsView
    {
        public TagsTreeView(TagsTreeViewModel model)
        {
            InitializeComponent();

            Model = model;
            DataContext = model;
        }

        public TagsTreeViewModel Model { get; private set; }

        public void SetWaitCursor()
        {
            Cursor = Cursors.Wait;
        }

        public void SetNormalCursor()
        {
            Cursor = Cursors.Arrow;
        }

        private void OnMenuItemAdd(object sender, RoutedEventArgs e)
        {
            var contextMenuItem = (MenuItem)sender;
            var contextMenu = (ContextMenu) contextMenuItem.Parent;
            if (contextMenu.PlacementTarget.GetType() == typeof (TreeViewItem))
            {
                var originatingTreeViewItem = (TreeViewItem) contextMenu.PlacementTarget;
                //originatingTreeViewItem.
                var dummy = DateTime.Now;
            }
            Model.AddTag("New tag");
        }

        private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;
            if (e.Key.Equals(Key.F2))
            {
                textBox.IsReadOnly = false;
            }
            else if (e.Key.Equals(Key.Return) || e.Key.Equals(Key.Enter))
            {
                textBox.IsReadOnly = true;
            }
        }
    }
}
