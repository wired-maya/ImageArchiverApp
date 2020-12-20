using System;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace ImageArchiverApp.Downloaders
{
    abstract class BaseDownloader
    {
        protected readonly MainWindow form;

        public abstract Dictionary<string, dynamic> DefaultSettings { get; }
        public abstract Dictionary<string, SingleOption> SettingsStructure { get; }
        public abstract string Name { get; }
        public abstract string TextBoxWatermark { get; }
        public Dictionary<string, dynamic> DownloaderSettings { get; set; }

        public BaseDownloader(MainWindow form)
        {
            this.form = form;
        }

        public class SingleOption
        {
            public readonly string ControlType;
            public readonly string Title;

            public SingleOption(string controlType, string title)
            {
                ControlType = controlType;
                Title = title;
            }
        }

        protected static async Task<string> GetAsync(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }

        protected static string RemoveInvalidCharacters(string path)
        {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            foreach (char c in invalid)
            {
                path = path.Replace(c.ToString(), "");
            }

            return path;
        }

        protected static IEnumerable<List<T>> SplitList<T>(List<T> subList, int nSize = 30)
        {
            for (int i = 0; i < subList.Count; i += nSize)
            {
                yield return subList.GetRange(i, Math.Min(nSize, subList.Count - i));
            }
        }

        public void SaveCurrentSettings()
        {
            try
            {
                var settings = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, dynamic>>>(File.ReadAllText(@"settings.json"));

                settings[Name] = DownloaderSettings;

                File.WriteAllText(@"settings.json", JsonConvert.SerializeObject(settings, Formatting.Indented));
            }
            catch
            {
                Dictionary<string, Dictionary<string, dynamic>> settings = new Dictionary<string, Dictionary<string, dynamic>>()
                {
                    { Name, DownloaderSettings }
                };

                File.WriteAllText(@"settings.json", JsonConvert.SerializeObject(settings, Formatting.Indented));
            }
        }

        public void ReadSettingsFromFile()
        {
            try
            {
                var settings = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, dynamic>>>(File.ReadAllText(@"settings.json"));

                DownloaderSettings = settings[Name];
            }
            catch
            {
                DownloaderSettings = DefaultSettings;

                SaveCurrentSettings();
            }
        }

        protected virtual async Task DownloadFileAsync(string uri, string filePath, string fileName, bool canOverwrite, CancellationToken ct)
        {
            filePath += @"\" + RemoveInvalidCharacters(fileName);

            if (File.Exists(filePath) && !canOverwrite)
            {
                await Task.Run(() =>
                {
                    Console.WriteLine($"File {filePath.Substring(filePath.LastIndexOf("/") + 1)} already exists");
                });

                form.ImageTextProgressBarPerformStep();

                return;
            }

            HttpClient client = new HttpClient();

            var response = await client.GetAsync(uri, ct);

            using (var fs = new FileStream(filePath, FileMode.CreateNew))
            {
                await response.Content.CopyToAsync(fs);
            }

            form.ImageTextProgressBarPerformStep();
        }

        public virtual async Task DownloadAsync(string input, CancellationToken ct)
        {
            string[] IDs = input.Split(' ');

            form.SetLibraryTextProgressBar(IDs.Length);

            foreach (string ID in IDs)
            {
                await DownloadGalleryAsync(ID, ct);

                form.LibraryTextProgressBarPerformStep();
            }
        }

        protected abstract Task DownloadGalleryAsync(string id, CancellationToken ct);
    }
}
