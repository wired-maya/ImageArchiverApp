using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ImageArchiverApp.Downloaders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

//TODO:
// - allow tag blacklists, maybe with !<tag>
// - format for downloads is: anime wallpaper, cute = downloads anime wallpapers, and artowrks tagged cute
// - handle timeouts with too many things downloading at once (and handle the memory spikes): https://stackoverflow.com/questions/43251728/list-of-tasks-starts-them-synchronously-i-would-like-them-to-start-all-at-once
// - make it download the webm instead of zip
// - just one have booru option, let settings input custom url and booru type
// - change hintbox based on if pool or not
// - include/exclude blacklisted tags option
// - add support for pools
// - add support for options
// - save tags for each image in txt (option)
// - add support for more danbooru boorus and gelbooru boorus: https://gelbooru.com/index.php?page=wiki&s=view&id=18780
// - add possible support for custom gelboorus/danboorus

namespace ImageArchiverApp
{
    class BooruDownloader : BaseDownloader
    {
        public BooruDownloader(MainWindow form) : base(form) { }

        public override Dictionary<string, dynamic> DefaultSettings
        {
            get => new Dictionary<string, dynamic>() {
                { "Overwrite", false },
                { "FilesAsTags", false },
                { "SortByArtist", true },
                { "SortByFranchise", false },
                { "SortByTag", false }
            };
        }

        public override Dictionary<string, SingleOption> SettingsStructure
        {
            get => new Dictionary<string, SingleOption>()
            {
                { "Overwrite", new SingleOption("CheckBox", "Overwrite existing files") },
                { "FilesAsTags", new SingleOption("CheckBox", "Name files after tags") },
                { "SortByArtist", new SingleOption("RadioButton", "Sort by artist") },
                { "SortByFranchise", new SingleOption("RadioButton", "Sort by franchise") },
                { "SortByTag", new SingleOption("RadioButton", "Sort by tag entered") }
            };
        }

        public override string Name { get => "Booru"; }
        public override string TextBoxWatermark { get => ""; }

        protected override async Task DownloadGalleryAsync(string tags, CancellationToken ct)
        {
            string tagString = string.Join("%20", tags);
            string combinedTags = string.Join(", ", tags);
            string path = Path.Combine(form.FilePath, RemoveInvalidCharacters(combinedTags));
            int pageNum = 1;
            dynamic json = JsonConvert.DeserializeObject(await GetAsync($"https://danbooru.donmai.us/posts.json?page={pageNum}&limit=200&tags={tagString}"));
            List<Task> tasks = new List<Task>();
            IEnumerable<List<Task>> splitTasks = SplitList(tasks);

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            while (true)
            {
                pageNum++;

                dynamic tempJson = JsonConvert.DeserializeObject(await GetAsync($"https://danbooru.donmai.us/posts.json?page={pageNum}&limit=200&tags={tagString}"));

                if (tempJson.Count > 0)
                {
                    json.Merge(tempJson, new JsonMergeSettings
                    {
                        MergeArrayHandling = MergeArrayHandling.Concat
                    });
                }
                else break;
            }

            if (json.Count == 0) throw new Exception("Nobody here but us chickens!");

            foreach (dynamic post in json)
            {
                if (post.file_url != null) tasks.Add(DownloadFileAsync(
                    post.file_url.ToString(),
                    path + @"\" + post.file_url.ToString().Substring(post.file_url.ToString().LastIndexOf("/") + 1),
                    DownloaderSettings["Overwrite"],
                    ct
                    ));
            }

            form.LibraryDisplayMode = CustomWinControls.ProgressBarDisplayMode.TextAndCurrProgress;
            form.LibraryCustomText = combinedTags;
            form.SetImageTextProgressBar(tasks.Count);

            foreach (List<Task> TaskList in splitTasks)
            {
                await Task.WhenAll(TaskList.ToArray());
            }
        }
    }
}
