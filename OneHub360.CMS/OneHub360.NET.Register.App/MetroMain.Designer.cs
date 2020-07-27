namespace OneHub360.Register.App
{
    partial class MetroMain
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
            this.metroPanelMain = new MetroFramework.Controls.MetroPanel();
            this.metroTileRegisteredIncomingList = new MetroFramework.Controls.MetroTile();
            this.metroTileIndexedIncomingList = new MetroFramework.Controls.MetroTile();
            this.metroPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroPanelMain
            // 
            this.metroPanelMain.Controls.Add(this.metroTileIndexedIncomingList);
            this.metroPanelMain.Controls.Add(this.metroTileRegisteredIncomingList);
            this.metroPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanelMain.HorizontalScrollbarBarColor = true;
            this.metroPanelMain.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanelMain.HorizontalScrollbarSize = 10;
            this.metroPanelMain.Location = new System.Drawing.Point(0, 0);
            this.metroPanelMain.Name = "metroPanelMain";
            this.metroPanelMain.Size = new System.Drawing.Size(1008, 729);
            this.metroPanelMain.TabIndex = 0;
            this.metroPanelMain.VerticalScrollbarBarColor = true;
            this.metroPanelMain.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanelMain.VerticalScrollbarSize = 10;
            // 
            // metroTileRegisteredIncomingList
            // 
            this.metroTileRegisteredIncomingList.ActiveControl = null;
            this.metroTileRegisteredIncomingList.Location = new System.Drawing.Point(24, 21);
            this.metroTileRegisteredIncomingList.Name = "metroTileRegisteredIncomingList";
            this.metroTileRegisteredIncomingList.Size = new System.Drawing.Size(205, 114);
            this.metroTileRegisteredIncomingList.TabIndex = 2;
            this.metroTileRegisteredIncomingList.Text = "الكتب المسجلة";
            this.metroTileRegisteredIncomingList.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.metroTileRegisteredIncomingList.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTileRegisteredIncomingList.TileCount = 5;
            this.metroTileRegisteredIncomingList.TileImage = global::OneHub360.Register.App.Properties.Resources.incoming_mail_1_64x64;
            this.metroTileRegisteredIncomingList.TileImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.metroTileRegisteredIncomingList.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.metroTileRegisteredIncomingList.UseSelectable = true;
            this.metroTileRegisteredIncomingList.UseTileImage = true;
            // 
            // metroTileIndexedIncomingList
            // 
            this.metroTileIndexedIncomingList.ActiveControl = null;
            this.metroTileIndexedIncomingList.Location = new System.Drawing.Point(255, 21);
            this.metroTileIndexedIncomingList.Name = "metroTileIndexedIncomingList";
            this.metroTileIndexedIncomingList.Size = new System.Drawing.Size(205, 114);
            this.metroTileIndexedIncomingList.TabIndex = 3;
            this.metroTileIndexedIncomingList.Text = "الكتب المفهرسة";
            this.metroTileIndexedIncomingList.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.metroTileIndexedIncomingList.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTileIndexedIncomingList.TileCount = 3000;
            this.metroTileIndexedIncomingList.TileImage = global::OneHub360.Register.App.Properties.Resources.incoming_mail_1_64x64;
            this.metroTileIndexedIncomingList.TileImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.metroTileIndexedIncomingList.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.metroTileIndexedIncomingList.UseSelectable = true;
            this.metroTileIndexedIncomingList.UseTileImage = true;
            // 
            // MetroMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.metroPanelMain);
            this.Name = "MetroMain";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "نظام السجل العام (OneHub360)";
            this.metroPanelMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel metroPanelMain;
        private MetroFramework.Controls.MetroTile metroTileRegisteredIncomingList;
        private MetroFramework.Controls.MetroTile metroTileIndexedIncomingList;
    }
}