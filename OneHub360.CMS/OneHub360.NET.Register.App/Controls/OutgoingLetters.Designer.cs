namespace OneHub360.Register.App.Controls
{
    partial class OutgoingLetters
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tileRegisteredLetters = new MetroFramework.Controls.MetroTile();
            this.metroGridRegisteredLetters = new MetroFramework.Controls.MetroGrid();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.From = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IncomingNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IncomingDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OutgoingNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OutgoingDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegisteringDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnIndexing = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.metroGridRegisteredLetters)).BeginInit();
            this.SuspendLayout();
            // 
            // tileRegisteredLetters
            // 
            this.tileRegisteredLetters.ActiveControl = null;
            this.tileRegisteredLetters.Dock = System.Windows.Forms.DockStyle.Top;
            this.tileRegisteredLetters.Enabled = false;
            this.tileRegisteredLetters.Location = new System.Drawing.Point(0, 0);
            this.tileRegisteredLetters.Name = "tileRegisteredLetters";
            this.tileRegisteredLetters.PaintTileCount = false;
            this.tileRegisteredLetters.Size = new System.Drawing.Size(1008, 62);
            this.tileRegisteredLetters.Style = MetroFramework.MetroColorStyle.Black;
            this.tileRegisteredLetters.TabIndex = 24;
            this.tileRegisteredLetters.Text = "قائمة الكتب الصادرة";
            this.tileRegisteredLetters.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tileRegisteredLetters.TileImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tileRegisteredLetters.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.tileRegisteredLetters.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.tileRegisteredLetters.UseSelectable = true;
            this.tileRegisteredLetters.UseTileImage = true;
            // 
            // metroGridRegisteredLetters
            // 
            this.metroGridRegisteredLetters.AllowUserToAddRows = false;
            this.metroGridRegisteredLetters.AllowUserToDeleteRows = false;
            this.metroGridRegisteredLetters.AllowUserToResizeRows = false;
            this.metroGridRegisteredLetters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.metroGridRegisteredLetters.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroGridRegisteredLetters.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.metroGridRegisteredLetters.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.metroGridRegisteredLetters.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGridRegisteredLetters.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.metroGridRegisteredLetters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.metroGridRegisteredLetters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.From,
            this.Subject,
            this.IncomingNumber,
            this.IncomingDate,
            this.OutgoingNumber,
            this.OutgoingDate,
            this.RegisteringDate,
            this.btnIndexing});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.metroGridRegisteredLetters.DefaultCellStyle = dataGridViewCellStyle2;
            this.metroGridRegisteredLetters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroGridRegisteredLetters.EnableHeadersVisualStyles = false;
            this.metroGridRegisteredLetters.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.metroGridRegisteredLetters.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroGridRegisteredLetters.Location = new System.Drawing.Point(0, 62);
            this.metroGridRegisteredLetters.MultiSelect = false;
            this.metroGridRegisteredLetters.Name = "metroGridRegisteredLetters";
            this.metroGridRegisteredLetters.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.metroGridRegisteredLetters.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGridRegisteredLetters.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.metroGridRegisteredLetters.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.metroGridRegisteredLetters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.metroGridRegisteredLetters.ShowEditingIcon = false;
            this.metroGridRegisteredLetters.Size = new System.Drawing.Size(1008, 478);
            this.metroGridRegisteredLetters.TabIndex = 25;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // From
            // 
            this.From.DataPropertyName = "Organization";
            this.From.HeaderText = "وارد من";
            this.From.Name = "From";
            this.From.ReadOnly = true;
            // 
            // Subject
            // 
            this.Subject.DataPropertyName = "Subject";
            this.Subject.HeaderText = "الموضوع";
            this.Subject.Name = "Subject";
            this.Subject.ReadOnly = true;
            // 
            // IncomingNumber
            // 
            this.IncomingNumber.DataPropertyName = "IncomingNumber";
            this.IncomingNumber.HeaderText = "رقم الوارد";
            this.IncomingNumber.Name = "IncomingNumber";
            this.IncomingNumber.ReadOnly = true;
            // 
            // IncomingDate
            // 
            this.IncomingDate.DataPropertyName = "IncomingDate";
            this.IncomingDate.HeaderText = "تاريخ الوارد";
            this.IncomingDate.Name = "IncomingDate";
            this.IncomingDate.ReadOnly = true;
            // 
            // OutgoingNumber
            // 
            this.OutgoingNumber.DataPropertyName = "OutgoingNumber";
            this.OutgoingNumber.HeaderText = "رقم الصادر";
            this.OutgoingNumber.Name = "OutgoingNumber";
            this.OutgoingNumber.ReadOnly = true;
            // 
            // OutgoingDate
            // 
            this.OutgoingDate.DataPropertyName = "OutgoingDate";
            this.OutgoingDate.HeaderText = "تاريخ الصادر";
            this.OutgoingDate.Name = "OutgoingDate";
            this.OutgoingDate.ReadOnly = true;
            // 
            // RegisteringDate
            // 
            this.RegisteringDate.DataPropertyName = "RegisteringDate";
            this.RegisteringDate.HeaderText = "تاريخ التسجيل";
            this.RegisteringDate.Name = "RegisteringDate";
            this.RegisteringDate.ReadOnly = true;
            // 
            // btnIndexing
            // 
            this.btnIndexing.DataPropertyName = "Id";
            this.btnIndexing.HeaderText = "فهرسة";
            this.btnIndexing.Name = "btnIndexing";
            this.btnIndexing.ReadOnly = true;
            this.btnIndexing.Text = "فهرسة";
            this.btnIndexing.UseColumnTextForButtonValue = true;
            // 
            // OutgoingLetters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.metroGridRegisteredLetters);
            this.Controls.Add(this.tileRegisteredLetters);
            this.Name = "OutgoingLetters";
            this.Size = new System.Drawing.Size(1008, 540);
            ((System.ComponentModel.ISupportInitialize)(this.metroGridRegisteredLetters)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTile tileRegisteredLetters;
        private MetroFramework.Controls.MetroGrid metroGridRegisteredLetters;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn From;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subject;
        private System.Windows.Forms.DataGridViewTextBoxColumn IncomingNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn IncomingDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn OutgoingNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn OutgoingDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn RegisteringDate;
        private System.Windows.Forms.DataGridViewButtonColumn btnIndexing;
    }
}
