using System;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using PixeeSharp;
using PixeeSharp.Models;
using System.Collections.Generic;
using ImageArchiverApp.Downloaders;

//TODO:
// - also does not work
// - also find a more wholesome example number
// - fix pixiv login timer!!!!!!!!
// - change filename in pixivdownloader to only use filepath instead of filepath and filename
// - get sort by franchise working by using the tags
// - figure out how to download the weird gif things (ugoria)
// - add ignore r-18 and r-18g tagged image options

namespace ImageArchiverApp
{
    class PixivDownloader : BaseDownloader
    {
        private readonly PixeeSharpAppApi client = new PixeeSharpAppApi(null, null, null, 0);

        public PixivDownloader(MainWindow form) : base(form) { }

        public override Dictionary<string, dynamic> DefaultSettings
        {
            get => new Dictionary<string, dynamic>() {
                { "FileAsTitle", false },
                { "Overwrite", false }
            };
        }

        public override Dictionary<string, SingleOption> SettingsStructure
        {
            get => new Dictionary<string, SingleOption>()
            {
                { "FileAsTitle", new SingleOption("CheckBox", "Name files after work title") },
                { "Overwrite", new SingleOption("CheckBox", "Overwrite existing files") }
            };
        }

        public override string Name { get => "Pixiv"; }
        public override string TextBoxWatermark { get => "Input artist's pixiv ids, seperated by spaces. Ex: 26690900"; }

        protected override async Task DownloadGalleryAsync(string id, CancellationToken ct)
        {
            PixivResult<PixivIllustration> illusts = await client.GetUserIllustrations(id);
            PixivResult<PixivIllustration> mangas = await client.GetUserIllustrations(id, "manga");
            PixivUserProfile profile = await client.GetUserDetail(id);
            List<Task> tasks = new List<Task>();
            IEnumerable<List<Task>> splitTasks;

            await illusts.SearchAll();
            await mangas.SearchAll();

            form.LibraryDisplayMode = CustomWinControls.ProgressBarDisplayMode.TextAndCurrProgress;
            form.LibraryCustomText = profile.User.Name;

            foreach (PixivIllustration illust in illusts.Result)
            {
                if (illust.MetaSinglePage.Url != null) tasks.Add(DownloadFileAsync(illust.MetaSinglePage.Url, profile, illust, ct));
                else
                {
                    for (int i = 0; i < illust.MetaPages.Count; i++)
                    {
                        tasks.Add(DownloadFileAsync(illust.MetaPages[i].Original, profile, illust, ct, i, true));
                    }
                }
            }

            foreach (PixivIllustration manga in mangas.Result)
            {
                if (manga.MetaSinglePage.Url != null) tasks.Add(DownloadFileAsync(manga.MetaSinglePage.Url, profile, manga, ct));
                else
                {
                    for (int i = 0; i < manga.MetaPages.Count; i++)
                    {
                        tasks.Add(DownloadFileAsync(manga.MetaPages[i].Original, profile, manga, ct, i, true));
                    }
                }
            }

            form.SetImageTextProgressBar(tasks.Count);

            splitTasks = SplitList(tasks);

            foreach (List<Task> TaskList in splitTasks)
            {
                if (!ct.IsCancellationRequested) await Task.WhenAny(Task.WhenAll(TaskList.ToArray()), Task.Delay(Timeout.Infinite, ct));
                
                ct.ThrowIfCancellationRequested();
            }
        }

        private async Task DownloadFileAsync(string uri, PixivUserProfile profile, PixivIllustration illust, CancellationToken ct, int i = 0, bool multiplePages = false)
        {
            ct.ThrowIfCancellationRequested();

            // - for testing tags as filename
            string test = "";
            //foreach (Tag tag in illust.Tags)
            //{
            //    test += tag.Name + ", ";
            //}
            //test = test.Substring(0, test.Length - 2);

            string path = Path.Combine(form.FilePath, RemoveInvalidCharacters(/*form.Settings["PixivOptions"]["SortByArtist"].IsTrue ?*/ profile.User.Name /*: form.Settings["PixivOptions"]["SortByFranchise"].IsTrue ? "test" : ""*/));
            string fileName = DownloaderSettings["FilesAsTitle"] ? illust.Title + (multiplePages ? (i + 1).ToString() : "") + uri.Substring(uri.LastIndexOf(".")) : test + (multiplePages ? (i + 1).ToString() : "") + uri.Substring(uri.LastIndexOf("."));
            string filePath = Path.Combine(path, RemoveInvalidCharacters(fileName));

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            if (File.Exists(filePath))
            {
                if (DownloaderSettings["Overwrite"])
                {
                    await Task.Run(() => { Console.WriteLine($"File {fileName} already exists"); });
                    form.ImageTextProgressBarPerformStep();
                    return;
                }
            }
            
            Stream imgStream = await illust.GetImage(PixeeSharp.Enums.ImageSize.Original, i);

            if (ct.IsCancellationRequested)
            {
                imgStream.Close();
                ct.ThrowIfCancellationRequested();
            }

            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                await imgStream.CopyToAsync(fs);
            }

            imgStream.Close();

            form.ImageTextProgressBarPerformStep();
        }

        public override async Task DownloadAsync(string input, CancellationToken ct)
        {
            string[] IDs = input.Split(' ');

            form.SetLibraryTextProgressBar(IDs.Length);

            await client.Login("user_wmxv8884", "Rkd4BeQD4Ynr76u");

            foreach (string ID in IDs)
            {
                await DownloadGalleryAsync(ID, ct);

                form.LibraryTextProgressBarPerformStep();
            }
        }
    }
}
