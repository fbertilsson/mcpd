using System.Threading;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Prism.Commands;
using ServiceModelEx;

namespace Tracing
{
    class MainWindowViewModel : NotificationObject
    {
        public int Number { get; set; }

        public string CallResult
        {
            get { return m_CallResult; }
            set
            {
                if (value == m_CallResult) return;
                m_CallResult = value;
                RaisePropertyChanged(() => CallResult);
            }
        }

        public DelegateCommand CallCommand { get; private set; }

        private IPhone m_PhoneService;
        private string m_CallResult;

        public MainWindowViewModel()
        {
            CallCommand = new DelegateCommand(OnCallCommand);

            ThreadPool.QueueUserWorkItem(InitializeService);
        }

        private void InitializeService(object state)
        {
            m_PhoneService = InProcFactory.CreateInstance<PhoneService, IPhone>();
        }

        private void OnCallCommand()
        {
            CallResult = m_PhoneService.Call(Number);
        }
    }
}
