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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.DownloadButton = new System.Windows.Forms.Button();
            this.FileBrowseButton = new System.Windows.Forms.Button();
            this.PathTextbox = new System.Windows.Forms.TextBox();
            this.PlatformComboBox = new System.Windows.Forms.ComboBox();
            this.NhentaiOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.NhentaiPrettyNamesCheckBox = new System.Windows.Forms.CheckBox();
            this.NhentaiIncludeTitleInFilenameCheckBox = new System.Windows.Forms.CheckBox();
            this.NhentaiOverwriteCheckBox = new System.Windows.Forms.CheckBox();
            this.IdWaterMarkTextBox = new CustomWinControls.WaterMarkTextBox();
            this.ImageTextProgressBar = new CustomWinControls.TextProgressBar();
            this.LibraryTextProgressBar = new CustomWinControls.TextProgressBar();
            this.PixivOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.PixivSortByArtistRadioButton = new System.Windows.Forms.RadioButton();
            this.PixivSortByFranchiseRadioButton = new System.Windows.Forms.RadioButton();
            this.PixivFilesAsTitleCheckBox = new System.Windows.Forms.CheckBox();
            this.PixivOverwriteCheckbox = new System.Windows.Forms.CheckBox();
            this.PixivLoginTimer = new System.Windows.Forms.Timer(this.components);
            this.BooruOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.BooruSortByArtistRadioButton = new System.Windows.Forms.RadioButton();
            this.BooruSortByFranchiseRadioButton = new System.Windows.Forms.RadioButton();
            this.BooruNameAfterTagsCheckbox = new System.Windows.Forms.CheckBox();
            this.BooruOverwriteCheckbox = new System.Windows.Forms.CheckBox();
            this.NhentaiOptionsGroupBox.SuspendLayout();
            this.PixivOptionsGroupBox.SuspendLayout();
            this.BooruOptionsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // DownloadButton
            // 
            this.DownloadButton.Location = new System.Drawing.Point(8, 286);
            this.DownloadButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(584, 24);
            this.DownloadButton.TabIndex = 0;
            this.DownloadButton.Text = "Download";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // FileBrowseButton
            // 
            this.FileBrowseButton.Location = new System.Drawing.Point(502, 262);
            this.FileBrowseButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.FileBrowseButton.Name = "FileBrowseButton";
            this.FileBrowseButton.Size = new System.Drawing.Size(90, 19);
            this.FileBrowseButton.TabIndex = 1;
            this.FileBrowseButton.Text = "Browse";
            this.FileBrowseButton.UseVisualStyleBackColor = true;
            this.FileBrowseButton.Click += new System.EventHandler(this.FileBrowseButton_Click);
            // 
            // PathTextbox
            // 
            this.PathTextbox.Location = new System.Drawing.Point(9, 263);
            this.PathTextbox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PathTextbox.Name = "PathTextbox";
            this.PathTextbox.Size = new System.Drawing.Size(489, 20);
            this.PathTextbox.TabIndex = 2;
            this.PathTextbox.Text = "C:\\";
            this.PathTextbox.TextChanged += new System.EventHandler(this.PathTextbox_TextChanged);
            // 
            // PlatformComboBox
            // 
            this.PlatformComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PlatformComboBox.Items.AddRange(new object[] {
            "Choose Platform",
            "Danbooru",
            "NHentai",
            "Pixiv"});
            this.PlatformComboBox.Location = new System.Drawing.Point(8, 37);
            this.PlatformComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PlatformComboBox.Name = "PlatformComboBox";
            this.PlatformComboBox.Size = new System.Drawing.Size(583, 21);
            this.PlatformComboBox.TabIndex = 11;
            this.PlatformComboBox.SelectedIndexChanged += new System.EventHandler(this.PlatformComboBox_SelectedIndexChanged);
            // 
            // NhentaiOptionsGroupBox
            // 
            this.NhentaiOptionsGroupBox.Controls.Add(this.NhentaiIncludeTitleInFilenameCheckBox);
            this.NhentaiOptionsGroupBox.Controls.Add(this.NhentaiOverwriteCheckBox);
            this.NhentaiOptionsGroupBox.Controls.Add(this.NhentaiPrettyNamesCheckBox);
            this.NhentaiOptionsGroupBox.Location = new System.Drawing.Point(9, 63);
            this.NhentaiOptionsGroupBox.Name = "NhentaiOptionsGroupBox";
            this.NhentaiOptionsGroupBox.Size = new System.Drawing.Size(583, 194);
            this.NhentaiOptionsGroupBox.TabIndex = 12;
            this.NhentaiOptionsGroupBox.TabStop = false;
            this.NhentaiOptionsGroupBox.Text = "NHentai Options";
            this.NhentaiOptionsGroupBox.Visible = false;
            // 
            // NhentaiPrettyNamesCheckBox
            // 
            this.NhentaiPrettyNamesCheckBox.AutoSize = true;
            this.NhentaiPrettyNamesCheckBox.Location = new System.Drawing.Point(6, 65);
            this.NhentaiPrettyNamesCheckBox.Name = "NhentaiPrettyNamesCheckBox";
            this.NhentaiPrettyNamesCheckBox.Size = new System.Drawing.Size(367, 17);
            this.NhentaiPrettyNamesCheckBox.TabIndex = 2;
            this.NhentaiPrettyNamesCheckBox.Text = "Use \"Pretty\" Doujin Names (File paths could be too long without this on!)";
            this.NhentaiPrettyNamesCheckBox.UseVisualStyleBackColor = true;
            // 
            // NhentaiIncludeTitleInFilenameCheckBox
            // 
            this.NhentaiIncludeTitleInFilenameCheckBox.AutoSize = true;
            this.NhentaiIncludeTitleInFilenameCheckBox.Location = new System.Drawing.Point(6, 42);
            this.NhentaiIncludeTitleInFilenameCheckBox.Name = "NhentaiIncludeTitleInFilenameCheckBox";
            this.NhentaiIncludeTitleInFilenameCheckBox.Size = new System.Drawing.Size(140, 17);
            this.NhentaiIncludeTitleInFilenameCheckBox.TabIndex = 1;
            this.NhentaiIncludeTitleInFilenameCheckBox.Text = "Include Title in Filename";
            this.NhentaiIncludeTitleInFilenameCheckBox.UseVisualStyleBackColor = true;
            // 
            // NhentaiOverwriteCheckBox
            // 
            this.NhentaiOverwriteCheckBox.AutoSize = true;
            this.NhentaiOverwriteCheckBox.Location = new System.Drawing.Point(6, 19);
            this.NhentaiOverwriteCheckBox.Name = "NhentaiOverwriteCheckBox";
            this.NhentaiOverwriteCheckBox.Size = new System.Drawing.Size(95, 17);
            this.NhentaiOverwriteCheckBox.TabIndex = 0;
            this.NhentaiOverwriteCheckBox.Text = "Overwrite Files";
            this.NhentaiOverwriteCheckBox.UseVisualStyleBackColor = true;
            // 
            // IdWaterMarkTextBox
            // 
            this.IdWaterMarkTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.IdWaterMarkTextBox.Location = new System.Drawing.Point(8, 12);
            this.IdWaterMarkTextBox.Name = "IdWaterMarkTextBox";
            this.IdWaterMarkTextBox.Size = new System.Drawing.Size(584, 20);
            this.IdWaterMarkTextBox.TabIndex = 1;
            this.IdWaterMarkTextBox.WaterMarkColor = System.Drawing.Color.Gray;
            this.IdWaterMarkTextBox.WaterMarkText = "Select Platform Below";
            // 
            // ImageTextProgressBar
            // 
            this.ImageTextProgressBar.CustomText = "";
            this.ImageTextProgressBar.Location = new System.Drawing.Point(8, 338);
            this.ImageTextProgressBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ImageTextProgressBar.Name = "ImageTextProgressBar";
            this.ImageTextProgressBar.ProgressColor = System.Drawing.Color.Lime;
            this.ImageTextProgressBar.Size = new System.Drawing.Size(584, 19);
            this.ImageTextProgressBar.TabIndex = 14;
            this.ImageTextProgressBar.TextColor = System.Drawing.Color.Black;
            this.ImageTextProgressBar.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImageTextProgressBar.VisualMode = CustomWinControls.ProgressBarDisplayMode.CurrProgress;
            // 
            // LibraryTextProgressBar
            // 
            this.LibraryTextProgressBar.CustomText = "";
            this.LibraryTextProgressBar.Location = new System.Drawing.Point(8, 314);
            this.LibraryTextProgressBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.LibraryTextProgressBar.Name = "LibraryTextProgressBar";
            this.LibraryTextProgressBar.ProgressColor = System.Drawing.Color.Lime;
            this.LibraryTextProgressBar.Size = new System.Drawing.Size(584, 19);
            this.LibraryTextProgressBar.TabIndex = 13;
            this.LibraryTextProgressBar.TextColor = System.Drawing.Color.Black;
            this.LibraryTextProgressBar.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LibraryTextProgressBar.VisualMode = CustomWinControls.ProgressBarDisplayMode.CurrProgress;
            // 
            // PixivOptionsGroupBox
            // 
            this.PixivOptionsGroupBox.Controls.Add(this.PixivSortByArtistRadioButton);
            this.PixivOptionsGroupBox.Controls.Add(this.PixivSortByFranchiseRadioButton);
            this.PixivOptionsGroupBox.Controls.Add(this.PixivFilesAsTitleCheckBox);
            this.PixivOptionsGroupBox.Controls.Add(this.PixivOverwriteCheckbox);
            this.PixivOptionsGroupBox.Location = new System.Drawing.Point(9, 63);
            this.PixivOptionsGroupBox.Name = "PixivOptionsGroupBox";
            this.PixivOptionsGroupBox.Size = new System.Drawing.Size(583, 194);
            this.PixivOptionsGroupBox.TabIndex = 13;
            this.PixivOptionsGroupBox.TabStop = false;
            this.PixivOptionsGroupBox.Text = "Pixiv Options";
            this.PixivOptionsGroupBox.Visible = false;
            // 
            // PixivSortByArtistRadioButton
            // 
            this.PixivSortByArtistRadioButton.AutoSize = true;
            this.PixivSortByArtistRadioButton.Location = new System.Drawing.Point(6, 65);
            this.PixivSortByArtistRadioButton.Name = "PixivSortByArtistRadioButton";
            this.PixivSortByArtistRadioButton.Size = new System.Drawing.Size(85, 17);
            this.PixivSortByArtistRadioButton.TabIndex = 5;
            this.PixivSortByArtistRadioButton.TabStop = true;
            this.PixivSortByArtistRadioButton.Text = "Sort By Artist";
            this.PixivSortByArtistRadioButton.UseVisualStyleBackColor = true;
            // 
            // PixivSortByFranchiseRadioButton
            // 
            this.PixivSortByFranchiseRadioButton.AutoSize = true;
            this.PixivSortByFranchiseRadioButton.Location = new System.Drawing.Point(6, 88);
            this.PixivSortByFranchiseRadioButton.Name = "PixivSortByFranchiseRadioButton";
            this.PixivSortByFranchiseRadioButton.Size = new System.Drawing.Size(108, 17);
            this.PixivSortByFranchiseRadioButton.TabIndex = 4;
            this.PixivSortByFranchiseRadioButton.TabStop = true;
            this.PixivSortByFranchiseRadioButton.Text = "Sort By Franchise";
            this.PixivSortByFranchiseRadioButton.UseVisualStyleBackColor = true;
            // 
            // PixivFilesAsTitleCheckBox
            // 
            this.PixivFilesAsTitleCheckBox.AutoSize = true;
            this.PixivFilesAsTitleCheckBox.Location = new System.Drawing.Point(6, 42);
            this.PixivFilesAsTitleCheckBox.Name = "PixivFilesAsTitleCheckBox";
            this.PixivFilesAsTitleCheckBox.Size = new System.Drawing.Size(155, 17);
            this.PixivFilesAsTitleCheckBox.TabIndex = 1;
            this.PixivFilesAsTitleCheckBox.Text = "Name Files After Work Title";
            this.PixivFilesAsTitleCheckBox.UseVisualStyleBackColor = true;
            // 
            // PixivOverwriteCheckbox
            // 
            this.PixivOverwriteCheckbox.AutoSize = true;
            this.PixivOverwriteCheckbox.Location = new System.Drawing.Point(6, 19);
            this.PixivOverwriteCheckbox.Name = "PixivOverwriteCheckbox";
            this.PixivOverwriteCheckbox.Size = new System.Drawing.Size(95, 17);
            this.PixivOverwriteCheckbox.TabIndex = 0;
            this.PixivOverwriteCheckbox.Text = "Overwrite Files";
            this.PixivOverwriteCheckbox.UseVisualStyleBackColor = true;
            // 
            // PixivLoginTimer
            // 
            this.PixivLoginTimer.Interval = 2700000;
            this.PixivLoginTimer.Tick += new System.EventHandler(this.PixivLoginTimer_Tick);
            // 
            // BooruOptionsGroupBox
            // 
            this.BooruOptionsGroupBox.Controls.Add(this.BooruSortByArtistRadioButton);
            this.BooruOptionsGroupBox.Controls.Add(this.BooruSortByFranchiseRadioButton);
            this.BooruOptionsGroupBox.Controls.Add(this.BooruNameAfterTagsCheckbox);
            this.BooruOptionsGroupBox.Controls.Add(this.BooruOverwriteCheckbox);
            this.BooruOptionsGroupBox.Location = new System.Drawing.Point(9, 63);
            this.BooruOptionsGroupBox.Name = "BooruOptionsGroupBox";
            this.BooruOptionsGroupBox.Size = new System.Drawing.Size(583, 194);
            this.BooruOptionsGroupBox.TabIndex = 14;
            this.BooruOptionsGroupBox.TabStop = false;
            this.BooruOptionsGroupBox.Text = "Booru Options";
            this.BooruOptionsGroupBox.Visible = false;
            // 
            // BooruSortByArtistRadioButton
            // 
            this.BooruSortByArtistRadioButton.AutoSize = true;
            this.BooruSortByArtistRadioButton.Location = new System.Drawing.Point(6, 65);
            this.BooruSortByArtistRadioButton.Name = "BooruSortByArtistRadioButton";
            this.BooruSortByArtistRadioButton.Size = new System.Drawing.Size(85, 17);
            this.BooruSortByArtistRadioButton.TabIndex = 5;
            this.BooruSortByArtistRadioButton.TabStop = true;
            this.BooruSortByArtistRadioButton.Text = "Sort By Artist";
            this.BooruSortByArtistRadioButton.UseVisualStyleBackColor = true;
            // 
            // BooruSortByFranchiseRadioButton
            // 
            this.BooruSortByFranchiseRadioButton.AutoSize = true;
            this.BooruSortByFranchiseRadioButton.Location = new System.Drawing.Point(6, 88);
            this.BooruSortByFranchiseRadioButton.Name = "BooruSortByFranchiseRadioButton";
            this.BooruSortByFranchiseRadioButton.Size = new System.Drawing.Size(108, 17);
            this.BooruSortByFranchiseRadioButton.TabIndex = 4;
            this.BooruSortByFranchiseRadioButton.TabStop = true;
            this.BooruSortByFranchiseRadioButton.Text = "Sort By Franchise";
            this.BooruSortByFranchiseRadioButton.UseVisualStyleBackColor = true;
            // 
            // BooruNameAfterTagsCheckbox
            // 
            this.BooruNameAfterTagsCheckbox.AutoSize = true;
            this.BooruNameAfterTagsCheckbox.Location = new System.Drawing.Point(6, 42);
            this.BooruNameAfterTagsCheckbox.Name = "BooruNameAfterTagsCheckbox";
            this.BooruNameAfterTagsCheckbox.Size = new System.Drawing.Size(130, 17);
            this.BooruNameAfterTagsCheckbox.TabIndex = 1;
            this.BooruNameAfterTagsCheckbox.Text = "Name Files After Tags";
            this.BooruNameAfterTagsCheckbox.UseVisualStyleBackColor = true;
            // 
            // BooruOverwriteCheckbox
            // 
            this.BooruOverwriteCheckbox.AutoSize = true;
            this.BooruOverwriteCheckbox.Location = new System.Drawing.Point(6, 19);
            this.BooruOverwriteCheckbox.Name = "BooruOverwriteCheckbox";
            this.BooruOverwriteCheckbox.Size = new System.Drawing.Size(95, 17);
            this.BooruOverwriteCheckbox.TabIndex = 0;
            this.BooruOverwriteCheckbox.Text = "Overwrite Files";
            this.BooruOverwriteCheckbox.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.IdWaterMarkTextBox);
            this.Controls.Add(this.ImageTextProgressBar);
            this.Controls.Add(this.LibraryTextProgressBar);
            this.Controls.Add(this.PlatformComboBox);
            this.Controls.Add(this.PathTextbox);
            this.Controls.Add(this.FileBrowseButton);
            this.Controls.Add(this.DownloadButton);
            this.Controls.Add(this.BooruOptionsGroupBox);
            this.Controls.Add(this.PixivOptionsGroupBox);
            this.Controls.Add(this.NhentaiOptionsGroupBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Image Archiver Tool";
            this.NhentaiOptionsGroupBox.ResumeLayout(false);
            this.NhentaiOptionsGroupBox.PerformLayout();
            this.PixivOptionsGroupBox.ResumeLayout(false);
            this.PixivOptionsGroupBox.PerformLayout();
            this.BooruOptionsGroupBox.ResumeLayout(false);
            this.BooruOptionsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.Button FileBrowseButton;
        private System.Windows.Forms.ComboBox PlatformComboBox;
        private System.Windows.Forms.GroupBox NhentaiOptionsGroupBox;
        private System.Windows.Forms.CheckBox NhentaiOverwriteCheckBox;
        private CustomWinControls.TextProgressBar LibraryTextProgressBar;
        private CustomWinControls.TextProgressBar ImageTextProgressBar;
        private CustomWinControls.WaterMarkTextBox IdWaterMarkTextBox;
        internal System.Windows.Forms.TextBox PathTextbox;
        private System.Windows.Forms.CheckBox NhentaiIncludeTitleInFilenameCheckBox;
        private System.Windows.Forms.GroupBox PixivOptionsGroupBox;
        private System.Windows.Forms.CheckBox PixivFilesAsTitleCheckBox;
        private System.Windows.Forms.CheckBox PixivOverwriteCheckbox;
        private System.Windows.Forms.RadioButton PixivSortByArtistRadioButton;
        private System.Windows.Forms.RadioButton PixivSortByFranchiseRadioButton;
        private System.Windows.Forms.CheckBox NhentaiPrettyNamesCheckBox;
        private System.Windows.Forms.Timer PixivLoginTimer;
        private System.Windows.Forms.GroupBox BooruOptionsGroupBox;
        private System.Windows.Forms.RadioButton BooruSortByArtistRadioButton;
        private System.Windows.Forms.RadioButton BooruSortByFranchiseRadioButton;
        private System.Windows.Forms.CheckBox BooruNameAfterTagsCheckbox;
        private System.Windows.Forms.CheckBox BooruOverwriteCheckbox;
    }
}

