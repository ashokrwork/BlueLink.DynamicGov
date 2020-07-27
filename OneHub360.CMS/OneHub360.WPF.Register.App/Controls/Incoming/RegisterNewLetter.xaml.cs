using OneHub360.CMS.DAL;
using System;
using System.Windows;
using System.Windows.Controls;

namespace OneHub360.WPF.Register.App.Controls.Incoming
{
    /// <summary>
    /// Interaction logic for RegisterNewLetter.xaml
    /// </summary>
    public partial class RegisterNewLetter : UserControl
    {
        public RegisterNewLetter()
        {
            InitializeComponent();

            
                listExternalOrganizations.ItemsSource = ((App)Application.Current).ExternalOrganizationSource;
            
        }

        private void btnRegisterLetter_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var incomingLetter = new IncomingLetter
            {
                CreatedBy = "e005080a-3430-4c78-a57b-f296d1af4dcc",
                CreationDate = DateTime.Now,
                Confidential = chkConfidential.IsChecked,
                IncomingDate = DateTime.Now,
                From = listExternalOrganizations.SelectedValue.ToString(),
                OutgoingNumber = txtOutgoingNumber.Text,
                OutgoingDate = dateOutgoingDate.SelectedDate,
                RegisteredBy = "e005080a-3430-4c78-a57b-f296d1af4dcc",
                RegisteringDate = DateTime.Now,
                Status = IncomingLetterStatus.Registered,
                Subject = txtSubject.Text
            };

            using (var client = new APIClient())
            {
                client.RegisterIncomingLetter(incomingLetter);
            }

            ((MainWindow)Window.GetWindow(this)).ShowInformation("تم تسجيل الكتاب بنجاح");
        }
    }
}
