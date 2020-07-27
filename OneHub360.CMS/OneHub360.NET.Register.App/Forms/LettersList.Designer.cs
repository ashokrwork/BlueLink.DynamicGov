namespace OneHub360.Register.App
{
    partial class LettersList
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
            this.dgvMainList = new System.Windows.Forms.DataGridView();
            this.ctxIncomingMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.عرضToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.فهرسةToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.تصويرToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lnkSubject = new System.Windows.Forms.DataGridViewLinkColumn();
            this.txtFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtLetterNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtRegisterationDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMainList)).BeginInit();
            this.ctxIncomingMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMainList
            // 
            this.dgvMainList.AllowUserToDeleteRows = false;
            this.dgvMainList.AllowUserToOrderColumns = true;
            this.dgvMainList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMainList.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvMainList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMainList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lnkSubject,
            this.txtFrom,
            this.txtLetterNumber,
            this.txtRegisterationDate,
            this.txtDate,
            this.txtStatus});
            this.dgvMainList.ContextMenuStrip = this.ctxIncomingMenu;
            this.dgvMainList.Location = new System.Drawing.Point(12, 12);
            this.dgvMainList.Name = "dgvMainList";
            this.dgvMainList.ReadOnly = true;
            this.dgvMainList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMainList.Size = new System.Drawing.Size(785, 353);
            this.dgvMainList.TabIndex = 0;
            // 
            // ctxIncomingMenu
            // 
            this.ctxIncomingMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.عرضToolStripMenuItem,
            this.فهرسةToolStripMenuItem,
            this.تصويرToolStripMenuItem});
            this.ctxIncomingMenu.Name = "ctxIncomingMenu";
            this.ctxIncomingMenu.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ctxIncomingMenu.Size = new System.Drawing.Size(107, 70);
            // 
            // عرضToolStripMenuItem
            // 
            this.عرضToolStripMenuItem.Name = "عرضToolStripMenuItem";
            this.عرضToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.عرضToolStripMenuItem.Text = "عرض";
            // 
            // فهرسةToolStripMenuItem
            // 
            this.فهرسةToolStripMenuItem.Name = "فهرسةToolStripMenuItem";
            this.فهرسةToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.فهرسةToolStripMenuItem.Text = "فهرسة";
            // 
            // تصويرToolStripMenuItem
            // 
            this.تصويرToolStripMenuItem.Name = "تصويرToolStripMenuItem";
            this.تصويرToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.تصويرToolStripMenuItem.Text = "تصوير";
            // 
            // lnkSubject
            // 
            this.lnkSubject.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.lnkSubject.HeaderText = "الموضوع";
            this.lnkSubject.Name = "lnkSubject";
            this.lnkSubject.ReadOnly = true;
            this.lnkSubject.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // txtFrom
            // 
            this.txtFrom.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.txtFrom.HeaderText = "وارد من";
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.ReadOnly = true;
            // 
            // txtLetterNumber
            // 
            this.txtLetterNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.txtLetterNumber.HeaderText = "رقم الكتاب";
            this.txtLetterNumber.Name = "txtLetterNumber";
            this.txtLetterNumber.ReadOnly = true;
            // 
            // txtRegisterationDate
            // 
            this.txtRegisterationDate.FillWeight = 150F;
            this.txtRegisterationDate.HeaderText = "تاريخ التسجيل";
            this.txtRegisterationDate.Name = "txtRegisterationDate";
            this.txtRegisterationDate.ReadOnly = true;
            // 
            // txtDate
            // 
            this.txtDate.HeaderText = "التاريخ";
            this.txtDate.Name = "txtDate";
            this.txtDate.ReadOnly = true;
            // 
            // txtStatus
            // 
            this.txtStatus.HeaderText = "حالة الكتاب";
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            // 
            // LettersList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(809, 390);
            this.Controls.Add(this.dgvMainList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LettersList";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "قائمة البريد الوارد";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dgvMainList)).EndInit();
            this.ctxIncomingMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMainList;
        private System.Windows.Forms.ContextMenuStrip ctxIncomingMenu;
        private System.Windows.Forms.ToolStripMenuItem عرضToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem تصويرToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem فهرسةToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtRegisterationDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtLetterNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtFrom;
        private System.Windows.Forms.DataGridViewLinkColumn lnkSubject;
    }
}