using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfViewer
{
    /// <summary>
    /// Interaction logic for TagView.xaml
    /// </summary>
    public partial class TagsView : ITagsView
    {
        public TagsViewModel Model { get; private set; }

        public TagsView(TagsViewModel model)
        {
            DataContext = model;
            Model = model;
            InitializeComponent();
        }

        public void SetWaitCursor()
        {
            Cursor = Cursors.Wait;
        }

        public void SetNormalCursor()
        {
            Cursor = Cursors.Arrow;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Model.IsModified = true;
        }

        private void TagsGrid_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            IsSelectionChangeOngoing = false;
        }

        public bool IsSelectionChangeOngoing { get; set; }


        private void TagsGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void UIElement_OnTextInput(object sender, TextCompositionEventArgs e)
        {
            // Never called?
        }
    }
}
