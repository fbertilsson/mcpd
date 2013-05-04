using System;
using System.Threading;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using ServiceModelEx;

namespace GenericMessageHandling
{
    public class MainWindowViewModel : NotificationObject
    {
        public DelegateCommand OpenCommand { get; private set; }
        public DelegateCommand CloseCommand { get; private set; }
        public DelegateCommand SendCommand { get; private set; }

        private string m_Text;
        public string Text
        {
            get { return m_Text; }
            private set { 
                m_Text = value;
                RaisePropertyChanged(() => Text);
            }
        }


        private bool m_IsConnected;
        public bool IsConnected
        {
            get { return m_IsConnected; }
            set
            {
                m_IsConnected = value;
                OpenCommand.RaiseCanExecuteChanged();
                SendCommand.RaiseCanExecuteChanged();
                CloseCommand.RaiseCanExecuteChanged();
            }
        }

        private IDataService m_Proxy;


        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindowViewModel()
        {
            OpenCommand = new DelegateCommand(OnOpen, CanOpen);
            CloseCommand = new DelegateCommand(OnClose, CanClose);
            SendCommand = new DelegateCommand(OnSend, CanSend);
        }

        private bool CanOpen()
        {
            return !m_IsConnected;
        }

        private void OnOpen()
        {
            ThreadPool.QueueUserWorkItem(Connect); // The InProcFactory does not like to be invoked on the UI thread.
        }

        private void Connect(object state)
        {
            m_Proxy = InProcFactory.CreateInstance<DataService, IDataService>();
            IsConnected = true;
        }


        private bool CanSend()
        {
            return m_IsConnected;
        }

        private void OnSend()      
        {
            try
            {
                ThreadPool.QueueUserWorkItem(Send);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void Send(object obj)
        {
            var result = m_Proxy.GetData();
            Text = result.ToString();

            //m_Proxy.SetData(null);
        }

        private bool CanClose()
        {
            return m_IsConnected;
        }

        private void OnClose()
        {
            m_Proxy = null;
            IsConnected = false;
            Text = string.Empty;
        }

    }

}
