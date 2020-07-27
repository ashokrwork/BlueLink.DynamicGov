using System;
using System.Configuration;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Printing;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;

namespace OneHub360.CMS.Web.Print
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        private string correspondenceId;

        public Main()
        {
            InitializeComponent();
            this.ShowsNavigationUI = false;
        }


       
        private PrintQueue GetPrinter()
        {
            PrintDialog dialog = new PrintDialog();

            dialog.SelectedPagesEnabled = false;
            dialog.UserPageRangeEnabled = false;

            PrintQueue printQueue = null;

            if (dialog.ShowDialog().Value)
            {
                var capabilities = dialog.PrintQueue.GetPrintCapabilities();

                if (capabilities.InputBinCapability.Count >= 1)
                {
                    printQueue = dialog.PrintQueue;
                }
            }

            return printQueue;
        }

        private async Task<ZipArchive> GetFilesToPrint(string correspondenceID)
        {


            var getFilesUrl = ConfigurationManager.AppSettings["APIBaseUrl"] + "cms/correspondence/download/" + correspondenceId;

            var tempDirectory = string.Format("{0}{1}", System.IO.Path.GetTempPath(), Guid.NewGuid());

            var webClient = new HttpClient();

            var filesZipArray = await webClient.GetByteArrayAsync(getFilesUrl);

            return new ZipArchive(new MemoryStream(filesZipArray));
        }
        string extractPath = @"c:\temp\unzip";
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var nameValueTable = HttpUtility.ParseQueryString(ApplicationDeployment.CurrentDeployment.ActivationUri.Query);

            correspondenceId = nameValueTable[0];
            //MessageBox.Show("correspondenceId: " + correspondenceId);
            //correspondenceId = "8f04889b-16b4-4725-80f7-a98631c86707";
            //var printQueue = GetPrinter();

            //if (printQueue == null)
            //{
            //    MessageBox.Show("من فضلك قم بإختيار طابعة مناسبة");
            //    return;
            //}
            Directory.CreateDirectory(extractPath);
            var filesZip = await GetFilesToPrint(correspondenceId);
            foreach (var file in filesZip.Entries)
            {
                try
                {

                    //var printJob = printQueue.AddJob();
                    //Stream printJobStream = printJob.JobStream;
                    //file.Open().CopyTo(printJobStream);
                    var str = DateTime.Now.ToOADate() + file.FullName;
                file.ExtractToFile(Path.Combine(extractPath, str),true);
                  //  MessageBox.Show("Extracted" + Path.Combine(extractPath, file.FullName));
                //printJobStream.Close();
                printfile(Path.Combine(extractPath, str));

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            

        }
        void printfile(string FileName)
        {
            ProcessStartInfo info = new ProcessStartInfo(FileName.Trim());
            info.Verb = "Print";
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(info);
        }
    }
}
