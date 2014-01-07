using System.Windows.Input;

namespace WpfViewer
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : IShell
    {
        public Shell(ShellViewModel model)
        {
            DataContext = model;
            model.View = this;
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
