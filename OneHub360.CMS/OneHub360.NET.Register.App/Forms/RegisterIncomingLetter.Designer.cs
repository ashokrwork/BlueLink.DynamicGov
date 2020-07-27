namespace OneHub360.Register.App
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterIncomingLetter));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOutgoingNumber = new System.Windows.Forms.TextBox();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.cbConfidential = new System.Windows.Forms.CheckBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.listExternalOrganizations = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.datePickerOutgoingDate = new MetroFramework.Controls.MetroDateTime();
            this.txtBrief = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtOutgoingNumber
            // 
            resources.ApplyResources(this.txtOutgoingNumber, "txtOutgoingNumber");
            this.txtOutgoingNumber.Name = "txtOutgoingNumber";
            // 
            // txtSubject
            // 
            resources.ApplyResources(this.txtSubject, "txtSubject");
            this.txtSubject.Name = "txtSubject";
            // 
            // cbConfidential
            // 
            resources.ApplyResources(this.cbConfidential, "cbConfidential");
            this.cbConfidential.Name = "cbConfidential";
            this.cbConfidential.UseVisualStyleBackColor = true;
            // 
            // btnRegister
            // 
            resources.ApplyResources(this.btnRegister, "btnRegister");
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // listExternalOrganizations
            // 
            this.listExternalOrganizations.DisplayMember = "Title";
            this.listExternalOrganizations.FormattingEnabled = true;
            resources.ApplyResources(this.listExternalOrganizations, "listExternalOrganizations");
            this.listExternalOrganizations.Name = "listExternalOrganizations";
            this.listExternalOrganizations.ValueMember = "Id";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // datePickerOutgoingDate
            // 
            resources.ApplyResources(this.datePickerOutgoingDate, "datePickerOutgoingDate");
            this.datePickerOutgoingDate.Name = "datePickerOutgoingDate";
            // 
            // txtBrief
            // 
            resources.ApplyResources(this.txtBrief, "txtBrief");
            this.txtBrief.Name = "txtBrief";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // RegisterIncomingLetter
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegisterIncomingLetter";
            this.Resizable = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOutgoingNumber;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.CheckBox cbConfidential;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.ComboBox listExternalOrganizations;
        private System.Windows.Forms.Label label4;
        private MetroFramework.Controls.MetroDateTime datePickerOutgoingDate;
        private System.Windows.Forms.TextBox txtBrief;
        private System.Windows.Forms.Label label5;
    }
}