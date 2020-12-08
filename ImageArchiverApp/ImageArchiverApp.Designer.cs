namespace ImageArchiverApp
{
    partial class MainWindow
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
            this.DownloadButton = new System.Windows.Forms.Button();
            this.FileBrowseButton = new System.Windows.Forms.Button();
            this.PathTextbox = new System.Windows.Forms.TextBox();
            this.PlatformComboBox = new System.Windows.Forms.ComboBox();
            this.IdWaterMarkTextBox = new CustomWinControls.WaterMarkTextBox();
            this.ImageTextProgressBar = new CustomWinControls.TextProgressBar();
            this.LibraryTextProgressBar = new CustomWinControls.TextProgressBar();
            this.PixivLoginTimer = new System.Windows.Forms.Timer(this.components);
            this.OptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.OptionsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.OptionsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // DownloadButton
            // 
            this.DownloadButton.Location = new System.Drawing.Point(11, 352);
            this.DownloadButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(779, 30);
            this.DownloadButton.TabIndex = 0;
            this.DownloadButton.Text = "Download";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // FileBrowseButton
            // 
            this.FileBrowseButton.Location = new System.Drawing.Point(669, 322);
            this.FileBrowseButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FileBrowseButton.Name = "FileBrowseButton";
            this.FileBrowseButton.Size = new System.Drawing.Size(120, 23);
            this.FileBrowseButton.TabIndex = 1;
            this.FileBrowseButton.Text = "Browse";
            this.FileBrowseButton.UseVisualStyleBackColor = true;
            this.FileBrowseButton.Click += new System.EventHandler(this.FileBrowseButton_Click);
            // 
            // PathTextbox
            // 
            this.PathTextbox.Location = new System.Drawing.Point(12, 324);
            this.PathTextbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PathTextbox.Name = "PathTextbox";
            this.PathTextbox.Size = new System.Drawing.Size(651, 22);
            this.PathTextbox.TabIndex = 2;
            this.PathTextbox.Text = "C:\\";
            this.PathTextbox.TextChanged += new System.EventHandler(this.PathTextbox_TextChanged);
            // 
            // PlatformComboBox
            // 
            this.PlatformComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PlatformComboBox.Items.AddRange(new object[] {
            "Choose Platform",
            "Booru",
            "Nhentai",
            "Pixiv"});
            this.PlatformComboBox.Location = new System.Drawing.Point(11, 46);
            this.PlatformComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PlatformComboBox.Name = "PlatformComboBox";
            this.PlatformComboBox.Size = new System.Drawing.Size(776, 24);
            this.PlatformComboBox.TabIndex = 11;
            this.PlatformComboBox.SelectedIndexChanged += new System.EventHandler(this.PlatformComboBox_SelectedIndexChanged);
            // 
            // IdWaterMarkTextBox
            // 
            this.IdWaterMarkTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.IdWaterMarkTextBox.Location = new System.Drawing.Point(11, 15);
            this.IdWaterMarkTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.IdWaterMarkTextBox.Name = "IdWaterMarkTextBox";
            this.IdWaterMarkTextBox.Size = new System.Drawing.Size(777, 23);
            this.IdWaterMarkTextBox.TabIndex = 1;
            this.IdWaterMarkTextBox.WaterMarkColor = System.Drawing.Color.Gray;
            this.IdWaterMarkTextBox.WaterMarkText = "Select Platform Below";
            // 
            // ImageTextProgressBar
            // 
            this.ImageTextProgressBar.CustomText = "";
            this.ImageTextProgressBar.Location = new System.Drawing.Point(11, 416);
            this.ImageTextProgressBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ImageTextProgressBar.Name = "ImageTextProgressBar";
            this.ImageTextProgressBar.ProgressColor = System.Drawing.Color.Lime;
            this.ImageTextProgressBar.Size = new System.Drawing.Size(779, 23);
            this.ImageTextProgressBar.TabIndex = 14;
            this.ImageTextProgressBar.TextColor = System.Drawing.Color.Black;
            this.ImageTextProgressBar.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImageTextProgressBar.VisualMode = CustomWinControls.ProgressBarDisplayMode.CurrProgress;
            // 
            // LibraryTextProgressBar
            // 
            this.LibraryTextProgressBar.CustomText = "";
            this.LibraryTextProgressBar.Location = new System.Drawing.Point(11, 386);
            this.LibraryTextProgressBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LibraryTextProgressBar.Name = "LibraryTextProgressBar";
            this.LibraryTextProgressBar.ProgressColor = System.Drawing.Color.Lime;
            this.LibraryTextProgressBar.Size = new System.Drawing.Size(779, 23);
            this.LibraryTextProgressBar.TabIndex = 13;
            this.LibraryTextProgressBar.TextColor = System.Drawing.Color.Black;
            this.LibraryTextProgressBar.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LibraryTextProgressBar.VisualMode = CustomWinControls.ProgressBarDisplayMode.CurrProgress;
            // 
            // PixivLoginTimer
            // 
            this.PixivLoginTimer.Interval = 2700000;
            this.PixivLoginTimer.Tick += new System.EventHandler(this.PixivLoginTimer_Tick);
            // 
            // OptionsGroupBox
            // 
            this.OptionsGroupBox.Controls.Add(this.OptionsFlowLayoutPanel);
            this.OptionsGroupBox.Location = new System.Drawing.Point(12, 78);
            this.OptionsGroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.OptionsGroupBox.Name = "OptionsGroupBox";
            this.OptionsGroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.OptionsGroupBox.Size = new System.Drawing.Size(777, 239);
            this.OptionsGroupBox.TabIndex = 13;
            this.OptionsGroupBox.TabStop = false;
            this.OptionsGroupBox.Text = "Options";
            // 
            // OptionsFlowLayoutPanel
            // 
            this.OptionsFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.OptionsFlowLayoutPanel.Location = new System.Drawing.Point(7, 22);
            this.OptionsFlowLayoutPanel.Name = "OptionsFlowLayoutPanel";
            this.OptionsFlowLayoutPanel.Size = new System.Drawing.Size(763, 210);
            this.OptionsFlowLayoutPanel.TabIndex = 0;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.IdWaterMarkTextBox);
            this.Controls.Add(this.ImageTextProgressBar);
            this.Controls.Add(this.LibraryTextProgressBar);
            this.Controls.Add(this.PlatformComboBox);
            this.Controls.Add(this.PathTextbox);
            this.Controls.Add(this.FileBrowseButton);
            this.Controls.Add(this.DownloadButton);
            this.Controls.Add(this.OptionsGroupBox);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Image Archiver Tool";
            this.OptionsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.Button FileBrowseButton;
        private System.Windows.Forms.ComboBox PlatformComboBox;
        private CustomWinControls.TextProgressBar LibraryTextProgressBar;
        private CustomWinControls.TextProgressBar ImageTextProgressBar;
        private CustomWinControls.WaterMarkTextBox IdWaterMarkTextBox;
        internal System.Windows.Forms.TextBox PathTextbox;
        private System.Windows.Forms.Timer PixivLoginTimer;
        private System.Windows.Forms.GroupBox OptionsGroupBox;
        private System.Windows.Forms.FlowLayoutPanel OptionsFlowLayoutPanel;
    }
}

