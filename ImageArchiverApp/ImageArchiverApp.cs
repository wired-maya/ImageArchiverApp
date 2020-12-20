using System;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Threading;
using ImageArchiverApp.Downloaders;
using Newtonsoft.Json;
using System.Linq;

//TODO:
// - make generic downloader that downloads all images on web page (have messagebox that shows up and warns you about the jank-ness of jank)
// - maybe make options group box a scrollable thing to fit more, might not need to use more columns
//      ⤷ maybe have options area have sub group boxes since it can now scroll
//          ⤷ section for compression that chooses what format, what to compress, when to compress maybe even?
//          ⤷ maybe remove options group box altogether, depending on how it ends up looking
//      ⤷ add textboxes to pixiv to use your own pixiv username and password, instead of using the provided one
//      ⤷ oragnize based on generic options > downloader specifix options > etc
//          ⤷ could have tabs with this as well?
//      ⤷ reset settings to default button in each individual core's settings
// - auto update
//      ⤷ build everything into one file to help with updates
//      ⤷ have popup dialogue with option to never ask again, save it to settings if yes
//          ⤷ have button to check for updates, use same popup dialogue but w/o option
//      ⤷ update settings by reading them, assigning values that are in both defaults and read settings to a new settings object, fill in anything new with default values
//      ⤷ maybe add changelog?
// - fix all the downloaders ffs
// - add support for love live music scraper and yiff.party
// - FILENAME IDEA: have a tag system like mp3tag, some of the same variables, arrow select for the variables, etc
//      ⤷ have what you chose in settings, have set of options (eg. %TITLE% or something) in defaults.cs
//      ⤷ textbox INSIDE options groupbox (maybe) with preview
//      ⤷ have option on EVERYTHING to preserve original file name (as if you downloaded stuff from the actual website)
//          ⤷ have it override any other settings that have to do with filename, with warning in option's text
// - add option to save all the stuff to compressed file (eg .zip)
//      ⤷ compress has drop-down with options to compress individual galleries or whole download
//      ⤷ try being able to compress directly (maybe by making archive > add to it) or if you can't just compress after download
// - add support for danbooru
// - add support for twitter and reddit
// - add support for more things
// - add support for links instead of just ids
//      ⤷ eg pixiv can download ugoira, image sets, and entire profile depending on link sent
// - add icons, and even a fancy program icon (plus progress thingies)
//      ⤷ close to task view option (the little arrow thingy in the bottom right, icon shows when stuff is finished downloading)

namespace ImageArchiverApp
{
    public partial class MainWindow : Form
    {
        CancellationTokenSource cts;
        BaseDownloader currentDownloader;

        public MainWindow()
        {
            InitializeComponent();
            ReadSettingsFromFile();

            FilePath = AppSettings["FilePath"];

            string[] downloaderList = DownloaderList.Keys.ToArray();
            Array.Sort(downloaderList);

            PlatformComboBox.Items.AddRange(downloaderList);
            PlatformComboBox.SelectedIndex = 0;

            LibraryTextProgressBar.Maximum = 0;
            LibraryTextProgressBar.Step = 1;

            ImageTextProgressBar.Maximum = 0;
            ImageTextProgressBar.Step = 1;
        }

        public Dictionary<string, dynamic> DefaultSettings {
            get => new Dictionary<string, dynamic>()
            {
                { "FilePath", Environment.GetFolderPath(Environment.SpecialFolder.Desktop) }
            }; 
        }

        public Dictionary<string, dynamic> AppSettings { get; set; }

        private Dictionary<string, string> DownloaderList
        {
            get => new Dictionary<string, string>()
            {
                { "Nhentai", "NhentaiDownloader" },
                { "Pixiv", "PixivDownloader" },
                { "Booru", "BooruDownloader" }
            };
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
                AppSettings["FilePath"] = value;
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
                FolderBrowseButton.Enabled = !value;
                PlatformComboBox.Enabled = !value;
            }
        }

        public void SetImageTextProgressBar(int max)
        {
            ImageTextProgressBar.Maximum = max;
            ImageTextProgressBar.Value = 0;
            ImageTextProgressBar.Step = 1;
        }

        public void SetLibraryTextProgressBar(int max)
        {
            LibraryTextProgressBar.Maximum = max;
            LibraryTextProgressBar.Value = 0;
            LibraryTextProgressBar.Step = 1;
        }

        public void ImageTextProgressBarPerformStep()
        {
            ImageTextProgressBar.PerformStep();
        }

        public void LibraryTextProgressBarPerformStep()
        {
            LibraryTextProgressBar.PerformStep();
        }

        private void ShowErrorDialogue(dynamic err, string errMessage)
        {
            Console.WriteLine(err);

            DialogResult result = MessageBox.Show(errMessage, "Error", MessageBoxButtons.OK);
            if (result == DialogResult.OK) IsDownloading = false;
        }

        private async void DownloadButton_Click(object sender, EventArgs e)
        {

            try
            {
                if (!IsDownloading)
                {
                    cts = new CancellationTokenSource();

                    IsDownloading = true;
                    LibraryTextProgressBar.Value = 0;

                    if (currentDownloader == null) throw new Exception("No website selected");

                    await currentDownloader.DownloadAsync(IdWaterMarkTextBox.Text, cts.Token);
                }
                else if (cts != null)
                {
                    cts.Cancel();
                }
            }
            catch (OperationCanceledException)
            {
                LibraryDisplayMode = CustomWinControls.ProgressBarDisplayMode.CurrProgress;

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

        private void FolderBrowseButton_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath)) FilePath = fbd.SelectedPath;
            }
        }

        private void PathTextbox_TextChanged(object sender, EventArgs e)
        {
            DownloadButton.Enabled = PathTextbox.Text != "";
        }

        private void PlatformComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentDownloader != null) currentDownloader.SaveCurrentSettings();

            if (PlatformComboBox.SelectedItem.ToString() == "Choose Platform")
            {
                currentDownloader = null;
                IdWaterMarkTextBox.WaterMarkText = "To begin, choose a platform from the dropdown below";

                OptionsFlowLayoutPanel.Controls.Clear();
            }
            else
            {
                Type downloaderType = Type.GetType("ImageArchiverApp." + DownloaderList[PlatformComboBox.SelectedItem.ToString()]);
                currentDownloader = (BaseDownloader)Activator.CreateInstance(downloaderType, this);

                IdWaterMarkTextBox.WaterMarkText = currentDownloader.TextBoxWatermark;

                currentDownloader.ReadSettingsFromFile();
                BuildOptions(currentDownloader);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (currentDownloader != null)  currentDownloader.SaveCurrentSettings();
            SaveCurrentSettings();

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            if (IsDownloading)
            {
                switch (MessageBox.Show(this, "Downloads are still in progress,\nare you sure you want to close?", "Closing", MessageBoxButtons.YesNo))
                {
                    case DialogResult.No:

                        e.Cancel = true;

                        break;
                    default:

                        if (cts != null) cts.Cancel();

                        break;
                }
            }
        }

        private void BuildOptions(BaseDownloader downloader)
        {
            OptionsFlowLayoutPanel.Controls.Clear();

            foreach (KeyValuePair<string, BaseDownloader.SingleOption> setting in downloader.SettingsStructure)
            {
                switch (setting.Value.ControlType)
                {
                    case "CheckBox":
                        {
                            CheckBox checkBox = new CheckBox
                            {
                                Text = setting.Value.Title,
                                AutoSize = true,
                                Name = setting.Key,
                                Checked = downloader.DownloaderSettings[setting.Key]
                            };

                            checkBox.CheckedChanged += CheckedChanged;

                            OptionsFlowLayoutPanel.Controls.Add(checkBox);
                            
                            break;
                        }

                    case "RadioButton":
                        {
                            RadioButton radioButton = new RadioButton
                            {
                                Text = setting.Value.Title,
                                AutoSize = true,
                                Name = setting.Key,
                                Checked = downloader.DownloaderSettings[setting.Key]
                            };

                            radioButton.CheckedChanged += CheckedChanged;
                            
                            OptionsFlowLayoutPanel.Controls.Add(radioButton);
                            
                            break;
                        }
                }
            }

        }

        private void CheckedChanged(dynamic sender, EventArgs e)
        {
            currentDownloader.DownloaderSettings[sender.Name] = sender.Checked;
        }

        public void SaveCurrentSettings()
        {
            try
            {
                var settings = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, dynamic>>>(File.ReadAllText(@"settings.json"));

                settings["App"] = AppSettings;

                File.WriteAllText(@"settings.json", JsonConvert.SerializeObject(settings, Formatting.Indented));
            }
            catch
            {
                Dictionary<string, Dictionary<string, dynamic>> settings = new Dictionary<string, Dictionary<string, dynamic>>()
                {
                    { Name, AppSettings }
                };

                File.WriteAllText(@"settings.json", JsonConvert.SerializeObject(settings, Formatting.Indented));
            }
        }

        public void ReadSettingsFromFile()
        {
            try
            {
                var settings = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, dynamic>>>(File.ReadAllText(@"settings.json"));

                AppSettings = settings["App"];
            }
            catch
            {
                AppSettings = DefaultSettings;

                SaveCurrentSettings();
            }
        }
    }
}
