namespace OneHub360.NET.Register.App
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.rbnMain = new System.Windows.Forms.Ribbon();
            this.ribbonButton8 = new System.Windows.Forms.RibbonButton();
            this.ribbonLabel1 = new System.Windows.Forms.RibbonLabel();
            this.tabIncoming = new System.Windows.Forms.RibbonTab();
            this.pnlRegister = new System.Windows.Forms.RibbonPanel();
            this.btnRegister = new System.Windows.Forms.RibbonButton();
            this.btnReceipt = new System.Windows.Forms.RibbonButton();
            this.pnlScanning = new System.Windows.Forms.RibbonPanel();
            this.btnScanLetter = new System.Windows.Forms.RibbonButton();
            this.btnScanAttachment = new System.Windows.Forms.RibbonButton();
            this.btnUploadLetter = new System.Windows.Forms.RibbonButton();
            this.btnUploadAttachment = new System.Windows.Forms.RibbonButton();
            this.pnlIndexing = new System.Windows.Forms.RibbonPanel();
            this.btnIndex = new System.Windows.Forms.RibbonButton();
            this.btnView = new System.Windows.Forms.RibbonButton();
            this.pnlActions = new System.Windows.Forms.RibbonPanel();
            this.btnArchive = new System.Windows.Forms.RibbonButton();
            this.btnPrint = new System.Windows.Forms.RibbonButton();
            this.btnHistory = new System.Windows.Forms.RibbonButton();
            this.btnRecall = new System.Windows.Forms.RibbonButton();
            this.btnSend = new System.Windows.Forms.RibbonButton();
            this.pnlViews = new System.Windows.Forms.RibbonPanel();
            this.ddlViews = new System.Windows.Forms.RibbonComboBox();
            this.btnAllItems = new System.Windows.Forms.RibbonButton();
            this.btnRegistered = new System.Windows.Forms.RibbonButton();
            this.tabOutgoing = new System.Windows.Forms.RibbonTab();
            this.pnlPrint = new System.Windows.Forms.RibbonPanel();
            this.btnPrintLetters = new System.Windows.Forms.RibbonButton();
            this.btnPrintAttachments = new System.Windows.Forms.RibbonButton();
            this.btnPrintActionsList = new System.Windows.Forms.RibbonButton();
            this.btnFullPrint = new System.Windows.Forms.RibbonButton();
            this.pnlSending = new System.Windows.Forms.RibbonPanel();
            this.btnDeliveryRejected = new System.Windows.Forms.RibbonButton();
            this.btnConfirmDelivery = new System.Windows.Forms.RibbonButton();
            this.btnSendOutgoing = new System.Windows.Forms.RibbonButton();
            this.pnlOutgoingActions = new System.Windows.Forms.RibbonPanel();
            this.btnArchiveOutgoing = new System.Windows.Forms.RibbonButton();
            this.ribbonButton3 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton4 = new System.Windows.Forms.RibbonButton();
            this.btnOutgoingAudit = new System.Windows.Forms.RibbonButton();
            this.btnReturnOutgoing = new System.Windows.Forms.RibbonButton();
            this.btnViewOutgoing = new System.Windows.Forms.RibbonButton();
            this.tabArchive = new System.Windows.Forms.RibbonTab();
            this.ntfRegister = new System.Windows.Forms.NotifyIcon(this.components);
            this.ctxItems = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itmRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.itmShowStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHidden = new System.Windows.Forms.MenuStrip();
            this.stsMain = new System.Windows.Forms.StatusStrip();
            this.tsPendingTasks = new System.Windows.Forms.ToolStripStatusLabel();
            this.ribbonButton1 = new System.Windows.Forms.RibbonButton();
            this.orbItemSettings = new System.Windows.Forms.RibbonOrbMenuItem();
            this.ribbonButton9 = new System.Windows.Forms.RibbonButton();
            this.panelMain = new System.Windows.Forms.Panel();
            this.ctxItems.SuspendLayout();
            this.stsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbnMain
            // 
            this.rbnMain.AllowDrop = true;
            resources.ApplyResources(this.rbnMain, "rbnMain");
            this.rbnMain.Minimized = false;
            this.rbnMain.Name = "rbnMain";
            // 
            // 
            // 
            this.rbnMain.OrbDropDown.BorderRoundness = 8;
            this.rbnMain.OrbDropDown.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("rbnMain.OrbDropDown.Dock")));
            this.rbnMain.OrbDropDown.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("rbnMain.OrbDropDown.ImeMode")));
            this.rbnMain.OrbDropDown.Location = ((System.Drawing.Point)(resources.GetObject("rbnMain.OrbDropDown.Location")));
            this.rbnMain.OrbDropDown.Name = "orbMain";
            this.rbnMain.OrbDropDown.OptionItems.Add(this.ribbonButton8);
            this.rbnMain.OrbDropDown.OptionItems.Add(this.ribbonLabel1);
            this.rbnMain.OrbDropDown.Size = ((System.Drawing.Size)(resources.GetObject("rbnMain.OrbDropDown.Size")));
            this.rbnMain.OrbDropDown.TabIndex = ((int)(resources.GetObject("rbnMain.OrbDropDown.TabIndex")));
            this.rbnMain.OrbDropDown.Text = resources.GetString("rbnMain.OrbDropDown.Text");
            this.rbnMain.OrbImage = null;
            this.rbnMain.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2013;
            this.rbnMain.OrbText = "السجل";
            this.rbnMain.RibbonTabFont = new System.Drawing.Font("Trebuchet MS", 9F);
            this.rbnMain.Tabs.Add(this.tabIncoming);
            this.rbnMain.Tabs.Add(this.tabOutgoing);
            this.rbnMain.Tabs.Add(this.tabArchive);
            this.rbnMain.TabsMargin = new System.Windows.Forms.Padding(12, 26, 20, 0);
            this.rbnMain.ThemeColor = System.Windows.Forms.RibbonTheme.Black;
            this.rbnMain.ActiveTabChanged += new System.EventHandler(this.rbnMain_ActiveTabChanged);
            // 
            // ribbonButton8
            // 
            this.ribbonButton8.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton8.Image")));
            this.ribbonButton8.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton8.SmallImage")));
            resources.ApplyResources(this.ribbonButton8, "ribbonButton8");
            // 
            // ribbonLabel1
            // 
            resources.ApplyResources(this.ribbonLabel1, "ribbonLabel1");
            // 
            // tabIncoming
            // 
            this.tabIncoming.Panels.Add(this.pnlRegister);
            this.tabIncoming.Panels.Add(this.pnlScanning);
            this.tabIncoming.Panels.Add(this.pnlIndexing);
            this.tabIncoming.Panels.Add(this.pnlActions);
            this.tabIncoming.Panels.Add(this.pnlViews);
            resources.ApplyResources(this.tabIncoming, "tabIncoming");
            // 
            // pnlRegister
            // 
            this.pnlRegister.Items.Add(this.btnRegister);
            this.pnlRegister.Items.Add(this.btnReceipt);
            resources.ApplyResources(this.pnlRegister, "pnlRegister");
            // 
            // btnRegister
            // 
            this.btnRegister.Image = ((System.Drawing.Image)(resources.GetObject("btnRegister.Image")));
            this.btnRegister.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnRegister.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnRegister.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnRegister.SmallImage")));
            resources.ApplyResources(this.btnRegister, "btnRegister");
            this.btnRegister.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Right;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnReceipt
            // 
            this.btnReceipt.Image = ((System.Drawing.Image)(resources.GetObject("btnReceipt.Image")));
            this.btnReceipt.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnReceipt.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnReceipt.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnReceipt.SmallImage")));
            resources.ApplyResources(this.btnReceipt, "btnReceipt");
            // 
            // pnlScanning
            // 
            this.pnlScanning.Items.Add(this.btnScanLetter);
            this.pnlScanning.Items.Add(this.btnScanAttachment);
            this.pnlScanning.Items.Add(this.btnUploadLetter);
            this.pnlScanning.Items.Add(this.btnUploadAttachment);
            resources.ApplyResources(this.pnlScanning, "pnlScanning");
            // 
            // btnScanLetter
            // 
            this.btnScanLetter.Image = ((System.Drawing.Image)(resources.GetObject("btnScanLetter.Image")));
            this.btnScanLetter.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.btnScanLetter.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnScanLetter.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnScanLetter.SmallImage")));
            resources.ApplyResources(this.btnScanLetter, "btnScanLetter");
            // 
            // btnScanAttachment
            // 
            this.btnScanAttachment.Image = ((System.Drawing.Image)(resources.GetObject("btnScanAttachment.Image")));
            this.btnScanAttachment.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnScanAttachment.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnScanAttachment.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnScanAttachment.SmallImage")));
            resources.ApplyResources(this.btnScanAttachment, "btnScanAttachment");
            // 
            // btnUploadLetter
            // 
            this.btnUploadLetter.Image = ((System.Drawing.Image)(resources.GetObject("btnUploadLetter.Image")));
            this.btnUploadLetter.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnUploadLetter.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnUploadLetter.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnUploadLetter.SmallImage")));
            resources.ApplyResources(this.btnUploadLetter, "btnUploadLetter");
            // 
            // btnUploadAttachment
            // 
            this.btnUploadAttachment.Image = ((System.Drawing.Image)(resources.GetObject("btnUploadAttachment.Image")));
            this.btnUploadAttachment.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnUploadAttachment.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnUploadAttachment.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnUploadAttachment.SmallImage")));
            resources.ApplyResources(this.btnUploadAttachment, "btnUploadAttachment");
            // 
            // pnlIndexing
            // 
            this.pnlIndexing.Items.Add(this.btnIndex);
            this.pnlIndexing.Items.Add(this.btnView);
            resources.ApplyResources(this.pnlIndexing, "pnlIndexing");
            // 
            // btnIndex
            // 
            this.btnIndex.Image = ((System.Drawing.Image)(resources.GetObject("btnIndex.Image")));
            this.btnIndex.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnIndex.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnIndex.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnIndex.SmallImage")));
            resources.ApplyResources(this.btnIndex, "btnIndex");
            this.btnIndex.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Right;
            // 
            // btnView
            // 
            this.btnView.Image = ((System.Drawing.Image)(resources.GetObject("btnView.Image")));
            this.btnView.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnView.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnView.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnView.SmallImage")));
            resources.ApplyResources(this.btnView, "btnView");
            this.btnView.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Right;
            // 
            // pnlActions
            // 
            this.pnlActions.Items.Add(this.btnArchive);
            this.pnlActions.Items.Add(this.btnPrint);
            this.pnlActions.Items.Add(this.btnHistory);
            this.pnlActions.Items.Add(this.btnRecall);
            this.pnlActions.Items.Add(this.btnSend);
            resources.ApplyResources(this.pnlActions, "pnlActions");
            // 
            // btnArchive
            // 
            this.btnArchive.Image = ((System.Drawing.Image)(resources.GetObject("btnArchive.Image")));
            this.btnArchive.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnArchive.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnArchive.SmallImage")));
            resources.ApplyResources(this.btnArchive, "btnArchive");
            this.btnArchive.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Right;
            // 
            // btnPrint
            // 
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnPrint.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnPrint.SmallImage")));
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Right;
            // 
            // btnHistory
            // 
            this.btnHistory.Image = ((System.Drawing.Image)(resources.GetObject("btnHistory.Image")));
            this.btnHistory.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnHistory.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnHistory.SmallImage")));
            resources.ApplyResources(this.btnHistory, "btnHistory");
            this.btnHistory.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Right;
            // 
            // btnRecall
            // 
            this.btnRecall.Image = ((System.Drawing.Image)(resources.GetObject("btnRecall.Image")));
            this.btnRecall.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnRecall.SmallImage")));
            resources.ApplyResources(this.btnRecall, "btnRecall");
            // 
            // btnSend
            // 
            this.btnSend.Image = ((System.Drawing.Image)(resources.GetObject("btnSend.Image")));
            this.btnSend.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnSend.SmallImage")));
            resources.ApplyResources(this.btnSend, "btnSend");
            // 
            // pnlViews
            // 
            this.pnlViews.Items.Add(this.ddlViews);
            resources.ApplyResources(this.pnlViews, "pnlViews");
            // 
            // ddlViews
            // 
            this.ddlViews.DropDownItems.Add(this.btnAllItems);
            this.ddlViews.DropDownItems.Add(this.btnRegistered);
            this.ddlViews.Image = ((System.Drawing.Image)(resources.GetObject("ddlViews.Image")));
            this.ddlViews.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.ddlViews.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            resources.ApplyResources(this.ddlViews, "ddlViews");
            this.ddlViews.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Right;
            this.ddlViews.TextBoxText = "";
            this.ddlViews.TextBoxWidth = 150;
            this.ddlViews.DropDownItemClicked += new System.Windows.Forms.RibbonComboBox.RibbonItemEventHandler(this.ddlViews_DropDownItemClicked);
            // 
            // btnAllItems
            // 
            this.btnAllItems.Image = ((System.Drawing.Image)(resources.GetObject("btnAllItems.Image")));
            this.btnAllItems.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnAllItems.SmallImage")));
            resources.ApplyResources(this.btnAllItems, "btnAllItems");
            // 
            // btnRegistered
            // 
            this.btnRegistered.Image = ((System.Drawing.Image)(resources.GetObject("btnRegistered.Image")));
            this.btnRegistered.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnRegistered.SmallImage")));
            resources.ApplyResources(this.btnRegistered, "btnRegistered");
            // 
            // tabOutgoing
            // 
            this.tabOutgoing.Panels.Add(this.pnlPrint);
            this.tabOutgoing.Panels.Add(this.pnlSending);
            this.tabOutgoing.Panels.Add(this.pnlOutgoingActions);
            resources.ApplyResources(this.tabOutgoing, "tabOutgoing");
            // 
            // pnlPrint
            // 
            this.pnlPrint.Items.Add(this.btnPrintLetters);
            this.pnlPrint.Items.Add(this.btnPrintAttachments);
            this.pnlPrint.Items.Add(this.btnPrintActionsList);
            this.pnlPrint.Items.Add(this.btnFullPrint);
            resources.ApplyResources(this.pnlPrint, "pnlPrint");
            // 
            // btnPrintLetters
            // 
            this.btnPrintLetters.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintLetters.Image")));
            this.btnPrintLetters.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnPrintLetters.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnPrintLetters.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnPrintLetters.SmallImage")));
            resources.ApplyResources(this.btnPrintLetters, "btnPrintLetters");
            this.btnPrintLetters.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Right;
            // 
            // btnPrintAttachments
            // 
            this.btnPrintAttachments.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintAttachments.Image")));
            this.btnPrintAttachments.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnPrintAttachments.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnPrintAttachments.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnPrintAttachments.SmallImage")));
            resources.ApplyResources(this.btnPrintAttachments, "btnPrintAttachments");
            this.btnPrintAttachments.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Right;
            // 
            // btnPrintActionsList
            // 
            this.btnPrintActionsList.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintActionsList.Image")));
            this.btnPrintActionsList.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnPrintActionsList.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnPrintActionsList.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnPrintActionsList.SmallImage")));
            resources.ApplyResources(this.btnPrintActionsList, "btnPrintActionsList");
            this.btnPrintActionsList.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Right;
            // 
            // btnFullPrint
            // 
            this.btnFullPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnFullPrint.Image")));
            this.btnFullPrint.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnFullPrint.SmallImage")));
            resources.ApplyResources(this.btnFullPrint, "btnFullPrint");
            this.btnFullPrint.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Right;
            // 
            // pnlSending
            // 
            this.pnlSending.Items.Add(this.btnDeliveryRejected);
            this.pnlSending.Items.Add(this.btnConfirmDelivery);
            this.pnlSending.Items.Add(this.btnSendOutgoing);
            resources.ApplyResources(this.pnlSending, "pnlSending");
            // 
            // btnDeliveryRejected
            // 
            this.btnDeliveryRejected.Image = ((System.Drawing.Image)(resources.GetObject("btnDeliveryRejected.Image")));
            this.btnDeliveryRejected.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnDeliveryRejected.SmallImage")));
            resources.ApplyResources(this.btnDeliveryRejected, "btnDeliveryRejected");
            this.btnDeliveryRejected.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Right;
            // 
            // btnConfirmDelivery
            // 
            this.btnConfirmDelivery.Image = ((System.Drawing.Image)(resources.GetObject("btnConfirmDelivery.Image")));
            this.btnConfirmDelivery.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnConfirmDelivery.SmallImage")));
            resources.ApplyResources(this.btnConfirmDelivery, "btnConfirmDelivery");
            this.btnConfirmDelivery.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Right;
            // 
            // btnSendOutgoing
            // 
            this.btnSendOutgoing.Image = ((System.Drawing.Image)(resources.GetObject("btnSendOutgoing.Image")));
            this.btnSendOutgoing.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnSendOutgoing.SmallImage")));
            resources.ApplyResources(this.btnSendOutgoing, "btnSendOutgoing");
            this.btnSendOutgoing.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Right;
            // 
            // pnlOutgoingActions
            // 
            this.pnlOutgoingActions.Items.Add(this.btnArchiveOutgoing);
            this.pnlOutgoingActions.Items.Add(this.btnOutgoingAudit);
            this.pnlOutgoingActions.Items.Add(this.btnReturnOutgoing);
            this.pnlOutgoingActions.Items.Add(this.btnViewOutgoing);
            resources.ApplyResources(this.pnlOutgoingActions, "pnlOutgoingActions");
            // 
            // btnArchiveOutgoing
            // 
            this.btnArchiveOutgoing.DropDownItems.Add(this.ribbonButton3);
            this.btnArchiveOutgoing.DropDownItems.Add(this.ribbonButton4);
            this.btnArchiveOutgoing.Image = ((System.Drawing.Image)(resources.GetObject("btnArchiveOutgoing.Image")));
            this.btnArchiveOutgoing.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnArchiveOutgoing.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnArchiveOutgoing.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnArchiveOutgoing.SmallImage")));
            resources.ApplyResources(this.btnArchiveOutgoing, "btnArchiveOutgoing");
            this.btnArchiveOutgoing.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Right;
            // 
            // ribbonButton3
            // 
            this.ribbonButton3.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton3.Image")));
            this.ribbonButton3.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton3.SmallImage")));
            resources.ApplyResources(this.ribbonButton3, "ribbonButton3");
            // 
            // ribbonButton4
            // 
            this.ribbonButton4.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton4.Image")));
            this.ribbonButton4.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton4.SmallImage")));
            resources.ApplyResources(this.ribbonButton4, "ribbonButton4");
            // 
            // btnOutgoingAudit
            // 
            this.btnOutgoingAudit.Image = ((System.Drawing.Image)(resources.GetObject("btnOutgoingAudit.Image")));
            this.btnOutgoingAudit.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnOutgoingAudit.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.btnOutgoingAudit.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnOutgoingAudit.SmallImage")));
            resources.ApplyResources(this.btnOutgoingAudit, "btnOutgoingAudit");
            // 
            // btnReturnOutgoing
            // 
            this.btnReturnOutgoing.Image = ((System.Drawing.Image)(resources.GetObject("btnReturnOutgoing.Image")));
            this.btnReturnOutgoing.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnReturnOutgoing.SmallImage")));
            resources.ApplyResources(this.btnReturnOutgoing, "btnReturnOutgoing");
            // 
            // btnViewOutgoing
            // 
            this.btnViewOutgoing.Image = ((System.Drawing.Image)(resources.GetObject("btnViewOutgoing.Image")));
            this.btnViewOutgoing.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnViewOutgoing.SmallImage")));
            resources.ApplyResources(this.btnViewOutgoing, "btnViewOutgoing");
            // 
            // tabArchive
            // 
            resources.ApplyResources(this.tabArchive, "tabArchive");
            // 
            // ntfRegister
            // 
            this.ntfRegister.ContextMenuStrip = this.ctxItems;
            resources.ApplyResources(this.ntfRegister, "ntfRegister");
            this.ntfRegister.DoubleClick += new System.EventHandler(this.ntfRegister_DoubleClick);
            // 
            // ctxItems
            // 
            this.ctxItems.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmRestore,
            this.itmShowStatus});
            this.ctxItems.Name = "ctxItems";
            resources.ApplyResources(this.ctxItems, "ctxItems");
            // 
            // itmRestore
            // 
            this.itmRestore.Name = "itmRestore";
            resources.ApplyResources(this.itmRestore, "itmRestore");
            // 
            // itmShowStatus
            // 
            this.itmShowStatus.Name = "itmShowStatus";
            resources.ApplyResources(this.itmShowStatus, "itmShowStatus");
            // 
            // mnuHidden
            // 
            resources.ApplyResources(this.mnuHidden, "mnuHidden");
            this.mnuHidden.Name = "mnuHidden";
            // 
            // stsMain
            // 
            this.stsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsPendingTasks});
            resources.ApplyResources(this.stsMain, "stsMain");
            this.stsMain.Name = "stsMain";
            this.stsMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // tsPendingTasks
            // 
            resources.ApplyResources(this.tsPendingTasks, "tsPendingTasks");
            this.tsPendingTasks.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.tsPendingTasks.Margin = new System.Windows.Forms.Padding(15, 3, 0, 2);
            this.tsPendingTasks.Name = "tsPendingTasks";
            // 
            // ribbonButton1
            // 
            this.ribbonButton1.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton1.Image")));
            this.ribbonButton1.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton1.SmallImage")));
            resources.ApplyResources(this.ribbonButton1, "ribbonButton1");
            // 
            // orbItemSettings
            // 
            this.orbItemSettings.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.orbItemSettings.Image = ((System.Drawing.Image)(resources.GetObject("orbItemSettings.Image")));
            this.orbItemSettings.SmallImage = ((System.Drawing.Image)(resources.GetObject("orbItemSettings.SmallImage")));
            // 
            // ribbonButton9
            // 
            this.ribbonButton9.Image = ((System.Drawing.Image)(resources.GetObject("ribbonButton9.Image")));
            this.ribbonButton9.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton9.SmallImage")));
            resources.ApplyResources(this.ribbonButton9, "ribbonButton9");
            // 
            // panelMain
            // 
            resources.ApplyResources(this.panelMain, "panelMain");
            this.panelMain.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panelMain.Name = "panelMain";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.stsMain);
            this.Controls.Add(this.rbnMain);
            this.Controls.Add(this.mnuHidden);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mnuHidden;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.ctxItems.ResumeLayout(false);
            this.stsMain.ResumeLayout(false);
            this.stsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Ribbon rbnMain;
        private System.Windows.Forms.RibbonTab tabIncoming;
        private System.Windows.Forms.RibbonTab tabOutgoing;
        private System.Windows.Forms.RibbonTab tabArchive;
        private System.Windows.Forms.RibbonPanel pnlRegister;
        private System.Windows.Forms.RibbonPanel pnlScanning;
        private System.Windows.Forms.RibbonPanel pnlIndexing;
        private System.Windows.Forms.RibbonButton btnRegister;
        private System.Windows.Forms.RibbonButton btnReceipt;
        private System.Windows.Forms.RibbonButton btnScanLetter;
        private System.Windows.Forms.RibbonButton btnScanAttachment;
        private System.Windows.Forms.RibbonButton btnUploadLetter;
        private System.Windows.Forms.RibbonButton btnUploadAttachment;
        private System.Windows.Forms.RibbonButton btnIndex;
        private System.Windows.Forms.RibbonButton btnView;
        private System.Windows.Forms.RibbonPanel pnlActions;
        private System.Windows.Forms.RibbonButton btnHistory;
        private System.Windows.Forms.RibbonButton btnPrint;
        private System.Windows.Forms.RibbonButton btnArchive;
        private System.Windows.Forms.RibbonButton btnRecall;
        private System.Windows.Forms.RibbonButton btnSend;
        private System.Windows.Forms.NotifyIcon ntfRegister;
        private System.Windows.Forms.ContextMenuStrip ctxItems;
        private System.Windows.Forms.ToolStripMenuItem itmRestore;
        private System.Windows.Forms.ToolStripMenuItem itmShowStatus;
        private System.Windows.Forms.RibbonButton ribbonButton1;
        private System.Windows.Forms.RibbonPanel pnlPrint;
        private System.Windows.Forms.RibbonPanel pnlSending;
        private System.Windows.Forms.RibbonPanel pnlOutgoingActions;
        private System.Windows.Forms.RibbonButton btnPrintLetters;
        private System.Windows.Forms.RibbonButton btnPrintAttachments;
        private System.Windows.Forms.RibbonButton btnPrintActionsList;
        private System.Windows.Forms.RibbonButton btnArchiveOutgoing;
        private System.Windows.Forms.RibbonButton btnFullPrint;
        private System.Windows.Forms.RibbonButton btnDeliveryRejected;
        private System.Windows.Forms.RibbonButton btnConfirmDelivery;
        private System.Windows.Forms.RibbonButton btnSendOutgoing;
        private System.Windows.Forms.RibbonButton ribbonButton3;
        private System.Windows.Forms.RibbonButton ribbonButton4;
        private System.Windows.Forms.RibbonButton btnOutgoingAudit;
        private System.Windows.Forms.RibbonButton btnReturnOutgoing;
        private System.Windows.Forms.RibbonButton btnViewOutgoing;
        private System.Windows.Forms.MenuStrip mnuHidden;
        private System.Windows.Forms.RibbonOrbMenuItem orbItemSettings;
        private System.Windows.Forms.RibbonButton ribbonButton8;
        private System.Windows.Forms.RibbonLabel ribbonLabel1;
        private System.Windows.Forms.RibbonButton ribbonButton9;
        private System.Windows.Forms.StatusStrip stsMain;
        private System.Windows.Forms.ToolStripStatusLabel tsPendingTasks;
        private System.Windows.Forms.RibbonPanel pnlViews;
        private System.Windows.Forms.RibbonComboBox ddlViews;
        private System.Windows.Forms.RibbonButton btnAllItems;
        private System.Windows.Forms.RibbonButton btnRegistered;
        public System.Windows.Forms.Panel panelMain;
    }
}