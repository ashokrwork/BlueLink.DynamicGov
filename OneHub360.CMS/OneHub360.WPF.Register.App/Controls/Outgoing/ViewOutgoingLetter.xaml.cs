using MoonPdfLib.MuPdf;
using OneHub360.CMS.DAL;
using System;
using System.Collections.Generic;
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

namespace OneHub360.WPF.Register.App.Controls.Outgoing
{
    /// <summary>
    /// Interaction logic for ViewOutgoingLetter.xaml
    /// </summary>
    public partial class ViewOutgoingLetter : UserControl
    {
        public ViewOutgoingLetter()
        {
            InitializeComponent();
        }

        private OutgoingLetterView OutgoingLetter;

        public Guid OutgoingLetterId { get; set; }

        public ViewOutgoingLetter(Guid outgoingLetterId)
        {
            InitializeComponent();
            OutgoingLetterId = outgoingLetterId;
            LoadLetter();
            LoadAttachements();
        }

        private async void LoadAttachements()
        {
            using (var api = new APIClient())
            {
                listLetterAttachements.ItemsSource = await api.GetOutgoingLetterAttachements(OutgoingLetterId);
            }
        }
            private async void LoadLetter()
        {
            using (var api = new APIClient())
            {
                OutgoingLetter = await api.GetOutgoingLetter(OutgoingLetterId);

                ShowDocument(OutgoingLetter.MainDocumentViewUrl.Split(new string[] { "file=" }, StringSplitOptions.None)[1]);

                toExternalOrganization.OrgUnitId = Guid.Parse(OutgoingLetter.To);
                fromUser.UserId = Guid.Parse(OutgoingLetter.From);
                lblOutgoingDate.Content = OutgoingLetter.OutgoingDate.Value.ToString("yyyy-MM-dd");
                lblOutgoingNumber.Content = OutgoingLetter.OutgoingNumber;

                lblPageNumber.Content = string.Format("{0}/{1}", moonPdfPanel.GetCurrentPageNumber(), moonPdfPanel.TotalPages);

            }
        }

        private async void ShowDocument(string documentUrl)
        {
            using (var api = new APIClient())
            {
                var source = new MemorySource(await api.ReadFile(documentUrl));
                moonPdfPanel.Open(source);

                lblPageNumber.Content = string.Format("{0}/{1}", moonPdfPanel.GetCurrentPageNumber(), moonPdfPanel.TotalPages);
            }
        }

        private async void btnHandToMessanger_Click(object sender, RoutedEventArgs e)
        {
            using (var api = new APIClient())
            {
                await api.ExportManually(OutgoingLetterId);
            }
                ((MainWindow)Window.GetWindow(this)).ShowInformation("تم تصدير الكتاب بنجاح");
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var api = new APIClient())
            {
                await api.ExportG2G(OutgoingLetterId);
            }
                ((MainWindow)Window.GetWindow(this)).ShowInformation("تم تصدير الكتاب بنجاح");
        }

        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            moonPdfPanel.GotoPreviousPage();
            lblPageNumber.Content = string.Format("{0}/{1}", moonPdfPanel.GetCurrentPageNumber(), moonPdfPanel.TotalPages);
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            moonPdfPanel.GotoNextPage();
            lblPageNumber.Content = string.Format("{0}/{1}", moonPdfPanel.GetCurrentPageNumber(), moonPdfPanel.TotalPages);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Window.GetWindow(this)).ShowControl(new OutgoingLetters());
        }

        private void btnMainDocumentView_Click(object sender, RoutedEventArgs e)
        {
            ShowDocument(OutgoingLetter.MainDocumentViewUrl.Split(new string[] { "file=" }, StringSplitOptions.None)[1]);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var documentViewUrl = ((Button)e.Source).CommandParameter.ToString();
            ShowDocument(documentViewUrl.Split(new string[] { "file=" }, StringSplitOptions.None)[1]);
        }
    }
}
