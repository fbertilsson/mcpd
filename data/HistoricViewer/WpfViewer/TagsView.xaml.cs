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
        public TagsView(TagsViewModel model)
        {
            DataContext = model;
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
    }
}
