using System;
using System.Threading;

//using Microsoft.Practices.Prism.Commands;

namespace Client1
{
    public class MainWindowViewModel
    {
        //public DelegateCommand SendCommand { get; private set; }

        private bool m_IsConnected;
        public bool IsConnected
        {
            get { return m_IsConnected; }
            set { m_IsConnected = value; }
        }


        public MainWindowViewModel()
        {
            //SendCommand = new DelegateCommand(OnSend, CanSend);
        }

        private bool CanSend()
        {
            return true;
        }

        private void OnSend()      
        {
            try
            {
                var thread = new Thread(Test);
                thread.Start();            
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void Test()
        {
            //var proxy = InProcFactory.CreateInstance<DataService, IDataService>();
            //var x = proxy.GetData();
            //proxy.SetData(null);
        }
    }

}
