using System;
using System.Windows.Forms;
using System.Threading;
using OneHub360.CMS.DAL;

namespace OneHub360.Register.App.Controls
{
    public partial class RegisterIncomingLetter : UserControl
    {
        public RegisterIncomingLetter()
        {
            InitializeComponent();
            var thread = new Thread(InitControl);
            thread.Start();
        }

        private void InitControl()
        {
            listExternalOrganizations.Text = "جاري التحميل";
            btnRegister.Enabled = false;
            var externalOrganizations = new CMSApiClient().GetExternalOrganizations();
            listExternalOrganizations.DataSource = externalOrganizations;
            btnRegister.Enabled = true;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var incomingLetter = new IncomingLetter
            {
                CreatedBy = "e005080a-3430-4c78-a57b-f296d1af4dcc",
                CreationDate = DateTime.Now,
                Confidential = cbConfidential.Checked,
                Brief = txtBrief.Text,
                IncomingDate = DateTime.Now,
                From = listExternalOrganizations.SelectedValue.ToString(),
                OutgoingNumber = txtOutgoingNumber.Text,
                OutgoingDate = datePickerOutgoingDate.Value,
                RegisteredBy = "e005080a-3430-4c78-a57b-f296d1af4dcc",
                RegisteringDate = DateTime.Now,
                Status = IncomingLetterStatus.Registered,
                Subject = txtSubject.Text
            };

            using (var cmsWebApiClient = new CMSApiClient())
            {
                cmsWebApiClient.RegisterIncomingLetter(incomingLetter);
            }
        }
    }
}
