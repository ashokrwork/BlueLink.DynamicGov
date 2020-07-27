using MoonPdfLib.MuPdf;
using OneHub360.WPF.Register.App.Scan;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace OneHub360.WPF.Register.App.Controls.Incoming
{
    /// <summary>
    /// Interaction logic for IndexIncomingLetter.xaml
    /// </summary>
    public partial class IndexIncomingLetter : UserControl
    {
        public IndexIncomingLetter()
        {
            InitializeComponent();
            
            moonPdfPanel.OpenFile(Environment.CurrentDirectory + "/temp/test.pdf");
            lblPageNumber.Content = string.Format("{0}/{1}", moonPdfPanel.GetCurrentPageNumber(), moonPdfPanel.TotalPages);
            

        }

        private void btnScanMainFile_Click(object sender, RoutedEventArgs e)
        {
            var scanner = new ScannerService();
            BitmapSource ScannedImage;
            try
            {
                ImageFile file = scanner.Scan();

                if (file != null)
                {
                    var converter = new ScannerImageConverter();

                    ScannedImage = converter.ConvertScannedImage(file);
                }
                else
                {
                    ScannedImage = null;
                }

            }
            catch (ScannerException ex)
            {
                // yeah, I know. Showing UI from the VM. Shoot me now.
                MessageBox.Show(ex.Message, "Unable to Scan Image");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            moonPdfPanel.GotoNextPage();
            lblPageNumber.Content = string.Format("{0}/{1}", moonPdfPanel.GetCurrentPageNumber(), moonPdfPanel.TotalPages);
        }

        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            moonPdfPanel.GotoPreviousPage();
            lblPageNumber.Content = string.Format("{0}/{1}", moonPdfPanel.GetCurrentPageNumber(), moonPdfPanel.TotalPages);
        }

        private void moonPdfPanel_PageRowDisplayChanged(object sender, EventArgs e)
        {
            lblPageNumber.Content = string.Format("{0}/{1}", moonPdfPanel.GetCurrentPageNumber(), moonPdfPanel.TotalPages);

        }
    }
}
