using System;
using System.Windows.Forms;
using System.IO;
using System.Net;
using PixeeSharp;
using System.Collections.Generic;
using System.Threading;

//TODO:
// - organize everything
// - auto update
// - have the settings file draw settings pane instead of relying on designer
// - add support for love live music scraper and yiff.party
// - FILENAME IDEA: have a tag system like mp3tag, some of the same variables, arrow select for the variables, etc
// - add support for danbooru
// - add support for twitter and reddit
// - add support for more things
// - add support for links instead of just ids
// - add icons
// - think of more options for each downloader in the "options box"
// - think of more things to do

namespace ImageArchiverApp
{
    public partial class MainWindow : Form
    {
        Settings settings;
        CancellationTokenSource cts;
        PixeeSharpAppApi client;

        public MainWindow()
        {
            InitializeComponent();

            PlatformComboBox.SelectedIndex = 0;
            PathTextbox.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            LibraryTextProgressBar.Maximum = 0;
            ImageTextProgressBar.Maximum = 0;

            settings = new Settings(this);
            settings.ReadFromFile();
        }

        public string FilePath
        {
            get
            {
                return PathTextbox.Text;
            }
            set
            {
                PathTextbox.Text = value;
            }
        }

        public CustomWinControls.ProgressBarDisplayMode LibraryDisplayMode
        {
            get
            {
                return LibraryTextProgressBar.VisualMode;
            }
            set
            {
                LibraryTextProgressBar.VisualMode = value;
            }
        }

        public string LibraryCustomText
        {
            get
            {
                return LibraryTextProgressBar.CustomText;
            }
            set
            {
                LibraryTextProgressBar.CustomText = value;
            }
        }

        public Dictionary<string, Dictionary<string, SingleOption>> Settings { get; set; }

        public bool IsDownloading
        {
            get
            {
                return !PathTextbox.Enabled;
            }
            set
            {
                if (value) DownloadButton.Text = "Cancel";
                else DownloadButton.Text = "Download";
                PathTextbox.Enabled = !value;
                IdWaterMarkTextBox.Enabled = !value;
                OptionsGroupBox.Enabled = !value;
                FileBrowseButton.Enabled = !value;
                PlatformComboBox.Enabled = !value;
            }
        }

        public void SetImageTextProgressBar(int max)
        {
            ImageTextProgressBar.Maximum = max;
            ImageTextProgressBar.Value = 0;
            ImageTextProgressBar.Step = 1;
        }

        public void ImageTextProgressBarPerformStep()
        {
            ImageTextProgressBar.PerformStep();
        }

        private void ShowErrorDialogue(dynamic err, string errMessage)
        {
            Console.WriteLine(err);
            DialogResult result = MessageBox.Show(errMessage, "Error", MessageBoxButtons.OK);
            if (result == DialogResult.OK) DownloadButton.Enabled = true;
        }

        private async void DownloadButton_Click(object sender, EventArgs e)
        {

            try
            {
                if (!IsDownloading)
                {
                    IsDownloading = true;
                    cts = new CancellationTokenSource();
                    LibraryTextProgressBar.Step = 1;
                    LibraryTextProgressBar.Value = 0;
                    string[] IDs = IdWaterMarkTextBox.Text.Split(' ');
                    LibraryTextProgressBar.Maximum = IDs.Length;
                    if (PlatformComboBox.SelectedItem.ToString() == "Danbooru")
                    {
                        LibraryTextProgressBar.Maximum = 1;
                        BooruDownloader downloader = new BooruDownloader(this);
                        await downloader.DanbooruTagDownloadAsync(IDs, cts.Token);
                        LibraryTextProgressBar.PerformStep();
                    }
                    else if (PlatformComboBox.SelectedItem.ToString() == "NHentai")
                    {
                        NhentaiDownloader downloader = new NhentaiDownloader(this);
                        foreach (string ID in IDs)
                        {
                            await downloader.NhentaiGalleryDownloadAsync(ID, cts.Token);
                            LibraryTextProgressBar.PerformStep();
                        }
                    }
                    else if (PlatformComboBox.SelectedItem.ToString() == "Pixiv")
                    {
                        client = new PixeeSharpAppApi(null, null, null, 0);
                        PixivDownloader downloader = new PixivDownloader(this);
                        await client.Login("user_wmxv8884", "Rkd4BeQD4Ynr76u");
                        PixivLoginTimer.Start();
                        foreach (string ID in IDs)
                        {
                            await downloader.PixivGalleryDownloadAsync(ID, client, cts.Token);
                            LibraryTextProgressBar.PerformStep();
                        }
                        PixivLoginTimer.Stop();
                    }
                    else throw new Exception("No website selected");
                }
                else if (cts != null)
                {
                    cts.Cancel();
                }
            }
            catch (OperationCanceledException)
            {
                LibraryDisplayMode = CustomWinControls.ProgressBarDisplayMode.CurrProgress;
                LibraryTextProgressBar.Step = 1;
                LibraryTextProgressBar.Value = 0;
                LibraryTextProgressBar.Maximum = 0;
                SetImageTextProgressBar(0);
            }
            catch (UnauthorizedAccessException err)
            {
                ShowErrorDialogue(err, "Access To Specified Directory Denied");
            }
            catch (DirectoryNotFoundException err)
            {
                ShowErrorDialogue(err, "Specified Directory Not Found");
            }
            catch (UriFormatException err)
            {
                ShowErrorDialogue(err, "Invalid URL/library ID");
            }
            catch (WebException err)
            {
                ShowErrorDialogue(err, "Web Error\nEither the site you are trying to acces is blocked, one of the IDs you inputted are wrong, or you have no internet connection");
            }
            catch (Exception err)
            {
                ShowErrorDialogue(err, err.Message);
            }
            finally
            {
                IsDownloading = false;
            }
        }

        private void FileBrowseButton_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath)) PathTextbox.Text = fbd.SelectedPath;
            }
        }

        private void PathTextbox_TextChanged(object sender, EventArgs e)
        {
            DownloadButton.Enabled = PathTextbox.Text != "";
        }

        private void PlatformComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (PlatformComboBox.SelectedItem.ToString())
            {
                case "Nhentai":
                    IdWaterMarkTextBox.WaterMarkText = "Input those magic numbers, seperated by spaces. Ex: 177013";
                    BuildOptions("NhentaiOptions");
                    break;
                case "Pixiv":
                    IdWaterMarkTextBox.WaterMarkText = "Input artist's pixiv ids, seperated by spaces. Ex: 26690900";
                    BuildOptions("PixivOptions");
                    break;
                case "Booru":
                    IdWaterMarkTextBox.WaterMarkText = "";
                    BuildOptions("BooruOptions");
                    break;
                case "Choose Platform":
                    IdWaterMarkTextBox.WaterMarkText = "To begin, choose a platform from the dropdown below";
                    OptionsFlowLayoutPanel.Controls.Clear();
                    break;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            settings.SaveCurrent();
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (IsDownloading)
            {
                switch (MessageBox.Show(this, "Downloads are still in progress,\nare you sure you want to close?", "Closing", MessageBoxButtons.YesNo))
                {
                    case DialogResult.No:
                        e.Cancel = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void PixivLoginTimer_Tick(object sender, EventArgs e)
        {
            client.Auth(client.RefreshToken);
        }

        private void BuildOptions(string downloader)
        {
            OptionsFlowLayoutPanel.Controls.Clear();

            foreach (KeyValuePair<string, SingleOption> setting in Settings[downloader])
            {
                if (setting.Value.ControlType == "CheckBox")
                {
                    CheckBox checkBox = new CheckBox
                    {
                        Text = setting.Value.Title,
                        Checked = setting.Value.IsTrue,
                        AutoSize = true,
                        Name = setting.Key
                    };
                    checkBox.CheckedChanged += CheckedChanged;
                    OptionsFlowLayoutPanel.Controls.Add(checkBox);
                }
                else if (setting.Value.ControlType == "RadioButton")
                {
                    RadioButton radioButton = new RadioButton
                    {
                        Text = setting.Value.Title,
                        Checked = setting.Value.IsTrue,
                        AutoSize = true,
                        Name = setting.Key
                    };
                    radioButton.CheckedChanged += CheckedChanged;
                    OptionsFlowLayoutPanel.Controls.Add(radioButton);
                }
            }

        }

        void CheckedChanged(dynamic sender, EventArgs e)
        {
            Settings[PlatformComboBox.SelectedItem.ToString() + "Options"][sender.Name].IsTrue = sender.Checked;
        }
    }
}
