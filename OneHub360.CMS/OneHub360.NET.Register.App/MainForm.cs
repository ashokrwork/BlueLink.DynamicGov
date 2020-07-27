//using MetroFramework.Forms;
using OneHub360.Register.App;
using OneHub360.Register.App.Controls;
using System;
using System.Windows.Forms;

namespace OneHub360.NET.Register.App
{
    public partial class MainForm : Form
    {
       
        public MainForm()
        {
            InitializeComponent();

            // Initialize Chiled Forms
            //IncomingList = new LettersList(ListMode.IncomingLetters);
            //this.InitializChildeForm(IncomingList);

            //OutgoingList = new LettersList(ListMode.OutgoingLetters);
            //this.InitializChildeForm(OutgoingList);

            //IncomingArchiveList = new LettersList(ListMode.ArchivedIncoming);
            //this.InitializChildeForm(IncomingArchiveList);

            //OutgoingArchiveList = new LettersList(ListMode.ArchivedIncoming);
            //OutgoingArchiveList.MdiParent = this;
        }

        public void UpdateStatusBar(string message)
        {
            tsPendingTasks.Text = string.Format("عدد الكتب المسجلة ولم ترسل للخادم : {0}", message);
            stsMain.Update();
        }

        private void ntfRegister_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Set the Default Ribbon Tab
            rbnMain.ActiveTab = tabIncoming;
            
            // Initialize the Default Form
            //IncomingList.Activate();
        }

        private void rbnMain_ActiveTabChanged(object sender, EventArgs e)
        {
            RibbonTab senderTab = ((Ribbon)sender).ActiveTab;
            if (senderTab == tabIncoming)
            {
                //IncomingList.Activate();
            }
            else if(senderTab == tabOutgoing)
            {
                //OutgoingList.Activate();
            }
            else if(senderTab == tabArchive)
            {
                //IncomingArchiveList.Activate();
            }
        }
        #region Private MainForm Methods
        private void InitializChildeForm(Form form)
        {
            form.MdiParent = this;
            form.Menu = null;
            form.ControlBox = false;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.ShowIcon = false;
            form.Dock = DockStyle.Fill;
            form.Show();

            form.FormBorderStyle = FormBorderStyle.None;
        }
        #endregion

        #region Ribbon Handlers
        private void btnRegister_Click(object sender, EventArgs e)
        {
            var registerIncomingLetter = new OneHub360.Register.App.Controls.RegisterIncomingLetter();
            ShowControl(registerIncomingLetter);
        }

        public void ShowControl(Control control)
        {
            control.Dock = DockStyle.Fill;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(control);
        }

        #endregion

        private void ddlViews_DropDownItemClicked(object sender, RibbonItemEventArgs e)
        {
            var selectedView = ddlViews.SelectedItem.Text;
            
            switch(selectedView)
            {
                case "الكتب المسجلة":
                    ShowControl(new RegiteredLetters());
                    break;
            }
        }
    }
}
