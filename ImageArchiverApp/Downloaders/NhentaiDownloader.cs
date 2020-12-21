using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

//TODO:
// - find a more wholesome example number
// - has that interesting wrinkle where it doesn't work
// - allow downloading of tags/artists w/ tag blacklists
// - save tags for doujin in txt (option)

namespace ImageArchiverApp.Downloaders
{
    class NhentaiDownloader : BaseDownloader
    {
        public NhentaiDownloader(MainWindow form) : base(form) { }

        public override Dictionary<string, dynamic> DefaultSettings
        {
            get => new Dictionary<string, dynamic>()
            {
                { "PrettyNames", true },
                { "IncludeTitleInFilename", false },
                { "Overwrite", false },
            };
        }

        public override Dictionary<string, SingleOption> SettingsStructure
        {
            get => new Dictionary<string, SingleOption>()
            {
                { "PrettyNames", new SingleOption("CheckBox", "Use pretty doujin names (File paths could be too long without this on!)") },
                { "IncludeTitleInFilename", new SingleOption("CheckBox", "Include title in filename") },
                { "Overwrite", new SingleOption("CheckBox", "Overwrite existing files") }
            };
        }

        public override string Name { get => "Nhentai";  }
        public override string TextBoxWatermark { get => "Input those magic numbers, seperated by spaces. Ex: 177013"; }

        protected override async Task DownloadGalleryAsync(string id, CancellationToken ct)
        {
            dynamic json = JsonConvert.DeserializeObject(await GetAsync($"https://nhentai.net/api/gallery/{id}"));
            string title = DownloaderSettings["PrettyNames"] ? json.title.pretty.ToString() : json.title.english.ToString();
            string path = Path.Combine(form.FilePath, title);
            List<Task> tasks = new List<Task>();
            IEnumerable<List<Task>> splitTasks;

            form.LibraryDisplayMode = CustomWinControls.ProgressBarDisplayMode.TextAndCurrProgress;
            form.LibraryCustomText = json.title.english.ToString();

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            for (int i = 0; i < json.images.pages.Count; i++)
            {
                string fileType = json.images.pages[i].t.ToString() == "p" ? "png" : "jpg";

                // add include title in filename option back in
                tasks.Add(DownloadFileAsync(
                    $"https://i.nhentai.net/galleries/{json.media_id}/{i + 1}.{fileType}",
                    path,
                    $@"\{i + 1}.{fileType}",
                    DownloaderSettings["Overwrite"],
                    ct
                    ));
            }

            form.SetImageTextProgressBar(tasks.Count);

            splitTasks = SplitList(tasks);

            foreach (List<Task> TaskList in splitTasks)
            {
                await Task.WhenAll(TaskList.ToArray());
            }
        }
    }
}
