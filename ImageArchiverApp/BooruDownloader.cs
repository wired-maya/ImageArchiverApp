using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

//TODO:
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
// - think of more things to do

namespace ImageArchiverApp
{
    class BooruDownloader
    {
        private readonly MainWindow form;
        private readonly Tools tools;
        public BooruDownloader(MainWindow form)
        {
            this.form = form;
            tools = new Tools(form);
        }

        public async Task DanbooruTagDownloadAsync(string[] tags, CancellationToken ct)
        {
            string tagString = string.Join("%20", tags);
            int pageNum = 1;
            string combinedTags = string.Join(", ", tags);
            string path = Path.Combine(form.FilePath, Tools.RemoveInvalidCharacters(combinedTags));
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            dynamic json = JsonConvert.DeserializeObject(await Tools.GetAsync($"https://danbooru.donmai.us/posts.json?page={pageNum}&limit=200&tags={tagString}"));
            while (true)
            {
                pageNum++;
                dynamic tempJson = JsonConvert.DeserializeObject(await Tools.GetAsync($"https://danbooru.donmai.us/posts.json?page={pageNum}&limit=200&tags={tagString}"));
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
            List<Task> tasks = new List<Task>();
            foreach (dynamic post in json)
            {
                if (post.file_url != null) tasks.Add(tools.DownloadFileAsync(post.file_url.ToString(), path, form.Settings["BooruOptions"], ct));
            }
            form.LibraryDisplayMode = CustomWinControls.ProgressBarDisplayMode.TextAndCurrProgress;
            form.LibraryCustomText = combinedTags;
            form.SetImageTextProgressBar(tasks.Count);
            IEnumerable<List<Task>> splitTasks = Tools.SplitList(tasks);
            foreach (List<Task> TaskList in splitTasks)
            {
                await Task.WhenAll(TaskList.ToArray());
            }
        }
    }
}
