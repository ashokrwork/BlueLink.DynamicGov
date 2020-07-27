namespace OneHub360.Register.App.Controls
{
    partial class IndexIncomingLetter
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
            this.tileIndexIncomingLetter = new MetroFramework.Controls.MetroTile();
            this.splitContainerIndexIncomingLetter = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerIndexIncomingLetter)).BeginInit();
            this.splitContainerIndexIncomingLetter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tileIndexIncomingLetter
            // 
            this.tileIndexIncomingLetter.ActiveControl = null;
            this.tileIndexIncomingLetter.Dock = System.Windows.Forms.DockStyle.Top;
            this.tileIndexIncomingLetter.Enabled = false;
            this.tileIndexIncomingLetter.Location = new System.Drawing.Point(0, 0);
            this.tileIndexIncomingLetter.Name = "tileIndexIncomingLetter";
            this.tileIndexIncomingLetter.PaintTileCount = false;
            this.tileIndexIncomingLetter.Size = new System.Drawing.Size(1008, 62);
            this.tileIndexIncomingLetter.Style = MetroFramework.MetroColorStyle.Black;
            this.tileIndexIncomingLetter.TabIndex = 24;
            this.tileIndexIncomingLetter.Text = "فهرسة كتاب";
            this.tileIndexIncomingLetter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tileIndexIncomingLetter.TileImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tileIndexIncomingLetter.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.tileIndexIncomingLetter.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.tileIndexIncomingLetter.UseSelectable = true;
            this.tileIndexIncomingLetter.UseTileImage = true;
            // 
            // splitContainerIndexIncomingLetter
            // 
            this.splitContainerIndexIncomingLetter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerIndexIncomingLetter.Location = new System.Drawing.Point(0, 62);
            this.splitContainerIndexIncomingLetter.Name = "splitContainerIndexIncomingLetter";
            this.splitContainerIndexIncomingLetter.Size = new System.Drawing.Size(1008, 478);
            this.splitContainerIndexIncomingLetter.SplitterDistance = 336;
            this.splitContainerIndexIncomingLetter.TabIndex = 25;
            // 
            // IndexIncomingLetter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerIndexIncomingLetter);
            this.Controls.Add(this.tileIndexIncomingLetter);
            this.Name = "IndexIncomingLetter";
            this.Size = new System.Drawing.Size(1008, 540);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerIndexIncomingLetter)).EndInit();
            this.splitContainerIndexIncomingLetter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTile tileIndexIncomingLetter;
        private System.Windows.Forms.SplitContainer splitContainerIndexIncomingLetter;
    }
}
