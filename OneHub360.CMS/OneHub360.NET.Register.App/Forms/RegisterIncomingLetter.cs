using MetroFramework.Forms;
using OneHub360.CMS.DAL;

using System;

namespace OneHub360.Register.App
{
    public partial class RegisterIncomingLetter : MetroForm
    {
        
        public RegisterIncomingLetter()
        {
            InitializeComponent();
            var externalOrganizations = new CMSApiClient().GetExternalOrganizations();
            listExternalOrganizations.DataSource = externalOrganizations;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var incomingLetter = new IncomingLetter
            {
                CreatedBy = "e005080a-3430-4c78-a57b-f296d1af4dcc",
                CreationDate = DateTime.Now,
                Confidential = false,
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
