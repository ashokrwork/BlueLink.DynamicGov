using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Deployment.Application;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace OneHub360.App.WPF.Signature
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();


        }

        public string UserId { get; set; }

        X509Certificate2Collection Certificates { get; set; }

        byte[] SignatureImage { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                signatureImage.Source = new BitmapImage(new Uri(op.FileName));

                SignatureImage = File.ReadAllBytes(op.FileName);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);

            store.Open(OpenFlags.ReadOnly);

            Certificates = new X509Certificate2Collection();

            foreach (X509Certificate2 certificate in store.Certificates)
            {

                if (certificate.HasPrivateKey)
                    Certificates.Add(certificate);
            }

            foreach (var certificate in Certificates)
            {
                listCertificates.Items.Add(certificate.Subject);
            }

            store.Close();

            var nameValueTable = HttpUtility.ParseQueryString(ApplicationDeployment.CurrentDeployment.ActivationUri.Query);

            UserId = nameValueTable[0];
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //MessageBox.Show(UserId);



                HttpClient apiClient = new HttpClient();
                apiClient.BaseAddress = new Uri(ConfigurationSettings.AppSettings["host"].ToString());

                JObject postData = new JObject();

                if (listCertificates.SelectedIndex > 0)
                {
                    var selectedCertificate = Certificates[listCertificates.SelectedIndex];

                    postData.Add("certificateRawData", Convert.ToBase64String(selectedCertificate.RawData));
                    postData.Add("certificatePrivateKey", selectedCertificate.PrivateKey.ToXmlString(true));
                }
                else
                {
                    postData.Add("certificateRawData", "");
                    postData.Add("certificatePrivateKey", "");
                }
                if (SignatureImage != null)
                    postData.Add("signatureImage", Convert.ToBase64String(SignatureImage));
                else
                    postData.Add("signatureImage", "");

                postData.Add("title", txtTitle.Text);
                postData.Add("userId", UserId);

                StringContent content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");

                var response = apiClient.PostAsync("api/user/signature", content);
                response.Wait();

                if (!response.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("تم تسجيل التوقيع الإلكتروني بنجاح");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
