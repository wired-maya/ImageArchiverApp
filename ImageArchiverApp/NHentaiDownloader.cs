using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

//TODO:
// - save tags for doujin in txt (option)
// - think of more things to do

namespace ImageArchiverApp
{
    class NhentaiDownloader
    {
        private readonly MainWindow form;
        private readonly Tools tools;
        public NhentaiDownloader(MainWindow form)
        {
            this.form = form;
            tools = new Tools(form);
        }

        public async Task NhentaiGalleryDownloadAsync(string id, CancellationToken ct)
        {
            dynamic json = JsonConvert.DeserializeObject(await Tools.GetAsync($"https://nhentai.net/api/gallery/{id}"));
            string title = Tools.RemoveInvalidCharacters(form.NhentaiOptions["PrettyNames"] ? json.title.pretty.ToString() : json.title.english.ToString());
            string path = Path.Combine(form.FilePath, title);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            form.LibraryDisplayMode = CustomWinControls.ProgressBarDisplayMode.TextAndCurrProgress;
            form.LibraryCustomText = json.title.english.ToString();
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < json.images.pages.Count; i++)
            {
                string fileType = json.images.pages[i].t.ToString() == "p" ? "png" : "jpg";
                tasks.Add(tools.DownloadFileAsync($"https://i.nhentai.net/galleries/{json.media_id}/{i + 1}.{fileType}", path, form.NhentaiOptions, ct, form.NhentaiOptions["IncludeTitleInFilename"] ? $"{title} " : ""));
            }
            form.SetImageTextProgressBar(tasks.Count);
            IEnumerable<List<Task>> splitTasks = Tools.SplitList(tasks);
            foreach (List<Task> TaskList in splitTasks)
            {
                await Task.WhenAll(TaskList.ToArray());
            }
        }
    }
}
