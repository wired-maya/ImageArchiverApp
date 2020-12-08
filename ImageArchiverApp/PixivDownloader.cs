using System;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using PixeeSharp;
using PixeeSharp.Models;
using System.Collections.Generic;

//TODO:
// - get sort by franchise working by using the tags
// - figure out how to download the weird gif things (ugoria)
// - think of more things to do

namespace ImageArchiverApp
{
    class PixivDownloader
    {
        private readonly MainWindow form;
        public PixivDownloader(MainWindow form)
        {
            this.form = form;
        }

        public async Task PixivGalleryDownloadAsync(string id, PixeeSharpAppApi client, CancellationToken ct)
        {
            PixivResult<PixivIllustration> illusts = await client.GetUserIllustrations(id);
            await illusts.SearchAll();
            PixivResult<PixivIllustration> mangas = await client.GetUserIllustrations(id, "manga");
            await mangas.SearchAll();
            PixivUserProfile profile = await client.GetUserDetail(id);
            form.LibraryDisplayMode = CustomWinControls.ProgressBarDisplayMode.TextAndCurrProgress;
            form.LibraryCustomText = profile.User.Name;
            List<Task> tasks = new List<Task>();
            foreach (PixivIllustration illust in illusts.Result)
            {
                if (illust.MetaSinglePage.Url != null) tasks.Add(PixivImageDownload(illust.MetaSinglePage.Url, profile, illust, ct));
                else
                {
                    for (int i = 0; i < illust.MetaPages.Count; i++)
                    {
                        tasks.Add(PixivImageDownload(illust.MetaPages[i].Original, profile, illust, ct, i, true));
                    }
                }
            }
            foreach (PixivIllustration manga in mangas.Result)
            {
                if (manga.MetaSinglePage.Url != null) tasks.Add(PixivImageDownload(manga.MetaSinglePage.Url, profile, manga, ct));
                else
                {
                    for (int i = 0; i < manga.MetaPages.Count; i++)
                    {
                        tasks.Add(PixivImageDownload(manga.MetaPages[i].Original, profile, manga, ct, i, true));
                    }
                }
            }
            form.SetImageTextProgressBar(tasks.Count);
            IEnumerable<List<Task>> splitTasks = Tools.SplitList<Task>(tasks);
            foreach (List<Task> TaskList in splitTasks)
            {
                if (!ct.IsCancellationRequested) await Task.WhenAny(Task.WhenAll(TaskList.ToArray()), Task.Delay(Timeout.Infinite, ct));
                ct.ThrowIfCancellationRequested();
            }
        }

        private async Task PixivImageDownload(string uri, PixivUserProfile profile, PixivIllustration illust, CancellationToken ct, int i = 0, bool multiplePages = false)
        {
            ct.ThrowIfCancellationRequested();
            string path = Path.Combine(form.FilePath, Tools.RemoveInvalidCharacters(/*form.Settings["PixivOptions"]["SortByArtist"].IsTrue ?*/ profile.User.Name /*: form.Settings["PixivOptions"]["SortByFranchise"].IsTrue ? "test" : ""*/));
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            // uri.Substring(uri.LastIndexOf("/") + 1)
            string test = "";
            foreach (Tag tag in illust.Tags)
            {
                test += tag.Name + ", ";
            }
            test = test.Substring(0, test.Length - 2);
            string fileName = form.Settings["PixivOptions"]["FilesAsTitle"].IsTrue ? illust.Title + (multiplePages ? (i + 1).ToString() : "") + uri.Substring(uri.LastIndexOf(".")) : test + (multiplePages ? (i + 1).ToString() : "") + uri.Substring(uri.LastIndexOf("."));
            string filePath = Path.Combine(path, Tools.RemoveInvalidCharacters(fileName));
            if (File.Exists(filePath))
            {
                if (!form.Settings["PixivOptions"]["Overwrite"].IsTrue)
                {
                    await Task.Run(() => { Console.WriteLine($"File {fileName} already exists"); });
                    form.ImageTextProgressBarPerformStep();
                    return;
                }
            }
            ct.ThrowIfCancellationRequested();
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
    }
}
