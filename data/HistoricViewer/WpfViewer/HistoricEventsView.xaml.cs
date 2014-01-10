namespace WpfViewer
{
    /// <summary>
    /// Interaction logic for HistoricEventsView.xaml
    /// </summary>
    public partial class HistoricEventsView
    {
        public HistoricEventsView(HistoricEventsViewModel historicEventsModel)
        {
            InitializeComponent();

            DataContext = historicEventsModel;
        }
    }
}
