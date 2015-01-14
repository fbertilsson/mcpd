using System.Windows;
using System.Windows.Input;

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
            MessageBox.Show("Add...");
            Model.AddTag("New tag");
        }
    }
}
