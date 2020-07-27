//using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OneHub360.CMS.DAL;
using OneHub360.NET.Register.App;

namespace OneHub360.Register.App
{
    public partial class LettersList : Form
    {
        public LettersList(ListMode mode)
        {
            InitializeComponent();
            InitializeListForm(mode);
            
        }
       
        private void InitializeListForm(ListMode mode)
        {
            switch (mode)
            {
                case ListMode.IncomingLetters:
                    this.Text = "قائمة الكتب الواردة";
                    dgvMainList.DataSource = this.GetIncomingLetters();
                    dgvMainList.Columns.Clear();
                    dgvMainList.AutoGenerateColumns = false;
                    txtFrom.DataPropertyName = "From";
                    dgvMainList.Columns.Add(txtFrom);

                    txtLetterNumber.DataPropertyName = "OutgoingNumber";
                    dgvMainList.Columns.Add(txtLetterNumber);

                    txtRegisterationDate.DataPropertyName = "RegisteringDate";
                    dgvMainList.Columns.Add(txtRegisterationDate);

                    txtDate.DataPropertyName = "CreationDate";
                    dgvMainList.Columns.Add(txtDate);

                    lnkSubject.DataPropertyName = "Subject";
                    dgvMainList.Columns.Add(lnkSubject);

                    txtStatus.DataPropertyName = "Status";
                    dgvMainList.Columns.Add(txtStatus);

                    break;
                case ListMode.OutgoingLetters:
                    this.Text = "قائمة الكتب الصادرة";
                    break;
                case ListMode.ArchivedIncoming:
                    this.Text = "قائمة الكتب الواردة المحفوظة";
                    break;
                case ListMode.ArchivedOutgoing:
                    this.Text = "قائمة الكتب الصادرة المحفوظة";
                    break;
                default:
                    this.Text = "";
                    break;
            }
        }

        private IList<IncomingLetter> GetIncomingLetters()
        {
            throw new NotImplementedException();

        }
    }
}
