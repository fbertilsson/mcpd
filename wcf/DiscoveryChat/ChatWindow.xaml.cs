using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Windows;
using System.Windows.Input;

namespace DiscoveryChat
{
    /// <summary>
    /// An oversimplified chat client/server that uses WCF static discovery. 
    /// The server only talks to the latest client.
    /// 
    /// Key WCF points:
    /// * Using DuplexChannelFactory
    /// * Discovery enpoints are added using the "kind=udpDiscoveryEndpoint" feature. Very nice.
    /// 
    /// Enhancements that can be made:
    /// * Use discovery announcements to enable dynamic discovery
    /// </summary>
    public partial class ChatWindow
    {
        public string NickName { get; set; }
        private ServiceHost m_Host;
        private IChatServer m_Server;
        private bool IsService { get; set; }

        public ChatWindow(bool isService = false)
        {
            InitializeComponent();
            DataContext = this;
            IsService = isService;

            if (IsService)
            {
                NickName = "Sigurd";
                StartServer();
            }
            else
            {
                NickName = "Ceasar";
                StartClient();
            }
            tbNickName.Content = NickName;
        }

        private void StartServer()
        {
            ChatServer.GotMessage += OnGotMessage;
            var server = new ChatServer();
            m_Server = server;
            m_Host = new ServiceHost(typeof(ChatServer));
            m_Host.Open();
            if (m_Host.State != CommunicationState.Opened)
            {
                Trace.WriteLine(string.Format("Host not opened. State: {0}", m_Host.State));
            } else
            {
                Trace.WriteLine("Host opened.");
            }
        }

        private void OnGotMessage(object sender, MessageEventArgs e)
        {
            Dispatcher.InvokeAsync(() => AddToConversation(e.SenderName, e.Message));
        }

        private void StartClient()
        {
            ChatClient.GotMessage += OnGotMessage;
            var discoveryClient = new DiscoveryClient();
            var response = discoveryClient.Find(new FindCriteria(typeof (IChatServer)));
            if (response.Endpoints.Count > 0)
            {
                var client = new ChatClient();
                var address = response.Endpoints[0].Address;
                var factory = new DuplexChannelFactory<IChatServer>(client, new NetTcpBinding(), address);
                m_Server = factory.CreateChannel();
                Trace.WriteLine("Client opened.");
            } 
            else
            {
                Trace.WriteLine("No server found.");
            }
        }


        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (m_Host != null)
            {
                if (m_Host.State == CommunicationState.Faulted)
                {
                    m_Host.Abort();
                }
                else
                {
                    m_Host.Abort();
                    // Commenting out Close because it hung. Abort works fine.
                    //m_Host.Close();
                }
            }

            base.OnClosing(e);
        }

        private void MessageKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
            }
        }

        private void SendMessage()
        {
            if (IsService)
            {
                ChatServer.LastChatClient.Send(NickName, Message.Text);
            }
            else
            {
                m_Server.Send(NickName, Message.Text);   
            }

            AddToConversation(NickName, Message.Text);
            Message.Text = string.Empty;
            Message.Focus();
        }

        private void AddToConversation(string sender, string text)
        {
            Conversation.Text += string.Format("{0}: {1}\n", sender, text);
        }

        private void SendButtonClick(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }
    }
}
