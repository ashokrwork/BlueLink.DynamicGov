using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Deployment.Application;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WIA;
using WIATest;

namespace OneHub360.CMS.Web.Scan
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            this.ShowsNavigationUI = false;
        }
        List<string> imagesURL=new List<string>();
        string FilePath = "c:\\temp\\PDFFiles\\";
        private void btnaddScan_Click(object sender, RoutedEventArgs e)
        { btnScan_Click(null, null); }
        private void btnfinish_Click(object sender, RoutedEventArgs e)
        {
            UploadImage();
        }

        private void btnScan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //get list of devices available
                List<string> devices = WIAScanner.GetDevices();

                //foreach (string device in devices)
                //{
                //    lbDevices.Items.Add(device);
                //}
                //check if device is not available
                if (devices.Count == 0)
                {
                    MessageBox.Show("You do not have any scan devices.");
                    //this.Close();
                }
                else
                {
                    Directory.CreateDirectory(FilePath);
                    // lbDevices.SelectedIndex = 0;
                    var images = WIAScanner.Scan(devices[0]);
                    foreach (System.Drawing.Image image in images)
                    {
                        //jpg_scan.Image = image;
                        //jpg_scan.Show();
                        //jpg_scan.SizeMode = jpgtureBoxSizeMode.StretchImage;
                        //save scanned image into specific folder
                        var stringURL = FilePath + DateTime.Now.ToString("yyyy-MM-dd HHmmss") + ".jpg";
                        imagesURL.Add(stringURL);
                        image.Save(stringURL, ImageFormat.Jpeg);
                    }
                    if (images.Count > 0)
                    {
                        PanelScanAgain.Visibility = Visibility.Visible;
                        btnScan.Visibility = Visibility.Collapsed;
                    }
                }
                //get images from scanner

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
       

        }
        string PDFURL;
        protected void createPDF()
        {
            Document doc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            PDFURL = FilePath + "Default.pdf";
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(PDFURL, FileMode.Create));
            if(!doc.IsOpen())
            doc.Open();
            try
            {
                for (int i = 0; i < imagesURL.Count; i++)
                {
                    //iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph("CPA0000"+(i+1).ToString());

                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imagesURL[i]);

                    if (jpg.Height > jpg.Width)
                    {
                        //Maximum height is 800 pixels.
                        float percentage = 0.0f;
                        percentage = 700 / jpg.Height;
                        jpg.ScalePercent(percentage * 100);
                    }
                    else
                    {
                        //Maximum width is 600 pixels.
                        float percentage = 0.0f;
                        percentage = 540 / jpg.Width;
                        jpg.ScalePercent(percentage * 100);
                    }

                    //Resize image depend upon your need
                    //jpg.ScaleToFit(140f, 120f);
                    //Give space before image
                    jpg.SpacingBefore = 10f;
                    //Give some space after the image
                    jpg.SpacingAfter = 1f;
                    jpg.Alignment = Element.ALIGN_MIDDLE;

                    //doc.Add(paragraph);
                    doc.Add(jpg);
                    doc.NewPage();
                }


            }
            catch (Exception ex)
            { }
            finally
            {
                doc.Close();

            }
        }
        private void UploadImage()
        {
            createPDF();
            var nameValueTable = HttpUtility.ParseQueryString(ApplicationDeployment.CurrentDeployment.ActivationUri.Query);
            string type = "0", Id = "";

            if (nameValueTable != null)
            {
                if (nameValueTable.Count > 0)
                    Id = nameValueTable[0];

                if (nameValueTable.Count > 1)
                    type = nameValueTable[1];
            }

            HttpClient ajpglient = new HttpClient();
            ajpglient.BaseAddress = new Uri(Properties.Settings.Default.ApiBaseUrl);
            byte[] bytes = System.IO.File.ReadAllBytes(PDFURL);

            var imageStream = new MemoryStream(bytes);

            var content = new MultipartFormDataContent();

            content.Add(new StreamContent(imageStream));
            if (type == "2")
            {
                content.Add(new StringContent("الوثيقة الرئيسية"));
                content.Add(new StringContent("file.pdf"));
                content.Add(new StringContent("الوثيقة الرئيسية.pdf"));
            }
            else
            {
                content.Add(new StringContent("مرفق"));
                content.Add(new StringContent("file.pdf"));
                content.Add(new StringContent("مرفق من الماسح الضوئي.pdf"));
            }

            string url = "";
            switch (type)
            {
                case "0":
                    url = string.Format(ConfigurationManager.AppSettings["APIBaseUrl"] + "cms/draftmemo/{0}/attachement/add", Id);
                    break;
                case "1":
                    url = string.Format(ConfigurationManager.AppSettings["APIBaseUrl"] + "cms/draftletter/{0}/attachement/add", Id);
                    break;
                case "2":
                    url = string.Format(ConfigurationManager.AppSettings["APIBaseUrl"] + "cms/incomingmemo/" + Id + "/main/add", Id);
                    break;
                default:
                    break;
            }
            //var memoId = "8f04889b-16b4-4725-80f7-a98631c86707";



            var response = ajpglient.PostAsync(url, content);
            response.Wait();

            //MessageBox.Show(response.Status.ToString());
            if (response.Result.ReasonPhrase == "OK")
            {
                List<string> imagesURL = new List<string>();
                PanelScanAgain.Visibility = Visibility.Collapsed;
                btnScan.Visibility = Visibility.Visible;
            }
        }
        //public static byte[] ImageToByte(System.Drawing.Image img)
        //{
        //    ImageConverter converter = new ImageConverter();
        //    return (byte[])converter.ConvertTo(img, typeof(byte[]));
        //}
        //public static byte[] ImageToByte2(System.Drawing.Image img)
        //{
        //    using (var stream = new MemoryStream())
        //    {
        //        img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
        //        return stream.ToArray();
        //    }
        //}
    }
}
