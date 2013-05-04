using System.Threading;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using ServiceModelEx;

namespace Concurrency2
{
    class MainWindowViewModel : NotificationObject
    {
        public DelegateCommand CheckPrimeCommand { get; private set; }

        private IPrimeChecker m_PrimeCheckerService;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindowViewModel()
        {
            CheckPrimeCommand = new DelegateCommand(OnCheckPrime, CanCheckPrime);
            ThreadPool.QueueUserWorkItem(x => m_PrimeCheckerService = InProcFactory.CreateInstance<PrimeCheckerService, IPrimeChecker>());
        }

        private bool CanCheckPrime()
        {
            return PrimeCandidate != 0;
        }

        private void OnCheckPrime()
        {
            var candidate = PrimeCandidate;
            ThreadPool.QueueUserWorkItem(x => CheckPrime(candidate));
        }

        private void CheckPrime(long candidate)
        {
            var isPrime = m_PrimeCheckerService.IsPrime(candidate);
            Output += string.Format("{0} : {1}\n", candidate, isPrime);
        }

        private long m_PrimeCandidate;

        public long PrimeCandidate
        {
            get { return m_PrimeCandidate; }
            set
            {
                m_PrimeCandidate = value;
                CheckPrimeCommand.RaiseCanExecuteChanged();
            }
        }


        private string m_Output;
        public string Output
        {
            get { return m_Output; }
            private set
            {
                m_Output = value;
                RaisePropertyChanged(() => Output);
            }
        }

        private bool m_IsPrime;

        public bool IsPrime
        {
            get { return m_IsPrime; }
            set
            {
                m_IsPrime = value;
                RaisePropertyChanged(() => IsPrime);
            }
        }
    }
}
