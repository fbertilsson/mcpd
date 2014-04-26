using Microsoft.Practices.Unity.Utility;

namespace WpfViewer.Resolve
{
    /// <summary>
    /// Interaction logic for ResolveView.xaml
    /// </summary>
    public partial class ResolveView : IResolveView
    {
        private ResolveViewModel m_Model;

        public ResolveView()
        {
            InitializeComponent();
        }

        public ResolveView(ResolveViewModel model)
        {
            Model = model;
            InitializeComponent();

            Use(model);
        }

        public ResolveViewModel Model
        {
            get { return m_Model; }
            set
            {
                DataContext = value;
                m_Model = value;
                Use(m_Model);
            }
        }

        public new bool? ShowDialog()
        {
            return base.ShowDialog();
        }

        private void Use(ResolveViewModel model)
        {
            var props = model.CurrentValues.PropertyNames;
            foreach (var prop in props)
            {
                System.Console.WriteLine("property: {0}: {1}", prop, model.CurrentValues[prop]);
                DgCurrent.Items.Add(new Pair<string, string>(prop, model.CurrentValues[prop].ToString()));
            }
        }
    }
}
