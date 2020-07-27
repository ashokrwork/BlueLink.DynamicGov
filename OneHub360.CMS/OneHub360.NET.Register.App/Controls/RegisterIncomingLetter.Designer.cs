namespace OneHub360.Register.App.Controls
{
    partial class RegisterIncomingLetter
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterIncomingLetter));
            this.datePickerOutgoingDate = new MetroFramework.Controls.MetroDateTime();
            this.listExternalOrganizations = new System.Windows.Forms.ComboBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.cbConfidential = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBrief = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.txtOutgoingNumber = new System.Windows.Forms.TextBox();
            this.tileRegisterIncomingLetter = new MetroFramework.Controls.MetroTile();
            this.SuspendLayout();
            // 
            // datePickerOutgoingDate
            // 
            resources.ApplyResources(this.datePickerOutgoingDate, "datePickerOutgoingDate");
            this.datePickerOutgoingDate.Name = "datePickerOutgoingDate";
            // 
            // listExternalOrganizations
            // 
            resources.ApplyResources(this.listExternalOrganizations, "listExternalOrganizations");
            this.listExternalOrganizations.DisplayMember = "Title";
            this.listExternalOrganizations.FormattingEnabled = true;
            this.listExternalOrganizations.Name = "listExternalOrganizations";
            this.listExternalOrganizations.ValueMember = "Id";
            // 
            // btnRegister
            // 
            resources.ApplyResources(this.btnRegister, "btnRegister");
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // cbConfidential
            // 
            resources.ApplyResources(this.cbConfidential, "cbConfidential");
            this.cbConfidential.Name = "cbConfidential";
            this.cbConfidential.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtBrief
            // 
            resources.ApplyResources(this.txtBrief, "txtBrief");
            this.txtBrief.Name = "txtBrief";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtSubject
            // 
            resources.ApplyResources(this.txtSubject, "txtSubject");
            this.txtSubject.Name = "txtSubject";
            // 
            // txtOutgoingNumber
            // 
            resources.ApplyResources(this.txtOutgoingNumber, "txtOutgoingNumber");
            this.txtOutgoingNumber.Name = "txtOutgoingNumber";
            // 
            // tileRegisterIncomingLetter
            // 
            resources.ApplyResources(this.tileRegisterIncomingLetter, "tileRegisterIncomingLetter");
            this.tileRegisterIncomingLetter.ActiveControl = null;
            this.tileRegisterIncomingLetter.Name = "tileRegisterIncomingLetter";
            this.tileRegisterIncomingLetter.PaintTileCount = false;
            this.tileRegisterIncomingLetter.Style = MetroFramework.MetroColorStyle.Black;
            this.tileRegisterIncomingLetter.TileImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tileRegisterIncomingLetter.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.tileRegisterIncomingLetter.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.tileRegisterIncomingLetter.UseSelectable = true;
            this.tileRegisterIncomingLetter.UseTileImage = true;
            // 
            // RegisterIncomingLetter
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tileRegisterIncomingLetter);
            this.Controls.Add(this.datePickerOutgoingDate);
            this.Controls.Add(this.listExternalOrganizations);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.cbConfidential);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBrief);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.txtOutgoingNumber);
            this.Name = "RegisterIncomingLetter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroDateTime datePickerOutgoingDate;
        private System.Windows.Forms.ComboBox listExternalOrganizations;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.CheckBox cbConfidential;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBrief;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.TextBox txtOutgoingNumber;
        private MetroFramework.Controls.MetroTile tileRegisterIncomingLetter;
    }
}
