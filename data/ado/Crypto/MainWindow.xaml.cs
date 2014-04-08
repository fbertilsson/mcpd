using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Crypto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly DSACryptoServiceProvider m_Provider;
        private readonly DSAParameters m_PublicKeyInfo;
        private readonly DSAParameters m_PrivateKeyInfo;


        public MainWindow()
        {
            InitializeComponent();
            m_Provider = new DSACryptoServiceProvider();
            m_PublicKeyInfo = m_Provider.ExportParameters(false);
            m_PrivateKeyInfo = m_Provider.ExportParameters(true);
        }

        private void OnHashClicked(object sender, RoutedEventArgs e)
        {
            var data = GetDataToSign();
            using (var subProvider = new DSACryptoServiceProvider())
            {
                subProvider.ImportParameters(m_PrivateKeyInfo);
                var formatter = new DSASignatureFormatter(subProvider);
                formatter.SetHashAlgorithm("SHA1");
                formatter.SetKey(m_Provider);
                var hash = formatter.CreateSignature(data);
                HashTextBox.Text = Convert.ToBase64String(hash);
            }
        }

        private byte[] GetDataToSign()
        {
            var data = Encoding.UTF8.GetBytes(TextToHashTextBox.Text);
            return data;
        }

        private void OnVerifyHashClicked(object sender, RoutedEventArgs e)
        {
            var signature = Convert.FromBase64String(HashTextBox.Text);
            using (var subProvider = new DSACryptoServiceProvider())
            {
                subProvider.ImportParameters(m_PublicKeyInfo);
                var deformatter = new DSASignatureDeformatter(subProvider);
                deformatter.SetHashAlgorithm("SHA1");
                var isVerified = deformatter.VerifySignature(GetDataToSign(), signature);
                IsVerifiedCheckBox.IsChecked = isVerified;
            }
        }
    }
}
