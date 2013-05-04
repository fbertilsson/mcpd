using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Windows;
using System.Windows.Input;
//using DiscoveryChat;

namespace DiscoveryAnnouncementChat
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
        private ServiceHost m_AnnouncementHost;
        //private AnnouncementService m_AnnouncementService;
        private bool IsService { get; set; }

        public ChatWindow(bool isService = false)
        {
            InitializeComponent();
            DataContext = this;
            IsService = isService;

            try
            {
                if (IsService)
                {
                    NickName = "Sigurd (server)";
                    StartServer();
                }
                else
                {

                    NickName = "Ceasar (client)";
                    StartClient();
                }
                tbNickName.Content = NickName;
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.ToString(), e.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private void StartServer()
        {
            ChatServer.GotMessage += OnGotMessage;
            var server = new ChatServer();
            m_Server = server;
            m_Host = new ServiceHost(typeof(ChatServer));            
            // Here, I wanted to dynamically set the scope to the nickname, but the Scope property of 
            // EndpointDiscoveryBehavior only has a getter :-(. Could use the Extensions property instead.
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
            bDisconnectedOverlay.Visibility = Visibility.Hidden;
            Dispatcher.InvokeAsync(() => AddToConversation(e.SenderName, e.Message));
        }

        private void StartClient()
        {
            ChatClient.GotMessage += OnGotMessage;

            // Start announcement service
            var announcementService = new AnnouncementService();
            announcementService.OnlineAnnouncementReceived += OnHello;
            announcementService.OfflineAnnouncementReceived += OnBye;
            m_AnnouncementHost = new ServiceHost(announcementService);
            m_AnnouncementHost.AddServiceEndpoint(new UdpAnnouncementEndpoint());
            m_AnnouncementHost.Open();

        }

        private void OnHello(object sender, AnnouncementEventArgs e)
        {
            if (!e.EndpointDiscoveryMetadata.ContractTypeNames.Any(x => x.Name.Equals("IChatServer"))) return;

            var client = new ChatClient();
            var address = e.EndpointDiscoveryMetadata.Address;
            var factory = new DuplexChannelFactory<IChatServer>(client, new NetTcpBinding(), address);
            m_Server = factory.CreateChannel();
            bDisconnectedOverlay.Visibility = Visibility.Hidden;
            Trace.WriteLine("Client opened.");
        }

        private void OnBye(object sender, AnnouncementEventArgs e)
        {
            bDisconnectedOverlay.Visibility = Visibility.Visible;
            m_Server = null;
        }


        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Close(m_Host);
            Close(m_AnnouncementHost);
            base.OnClosing(e);
        }

        private void Close(ICommunicationObject serviceHost)
        {
            if (serviceHost != null)
            {
                serviceHost.Abort();
            }            
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
