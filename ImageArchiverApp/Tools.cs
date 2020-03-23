using System;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Net.Http;

//TODO:
// - think of more things to do

namespace ImageArchiverApp
{
    class Tools
    {
        private readonly MainWindow form;
        public Tools(MainWindow form)
        {
            this.form = form;
        }

        public async Task DownloadFileAsync(string uri, string filePath, Dictionary<string, Boolean> options, CancellationToken ct, string leadingString = "")
        {
            string fileName = leadingString + uri.Substring(uri.LastIndexOf("/") + 1, uri.Length - (uri.LastIndexOf("/") + 1));
            filePath = Path.Combine(filePath, RemoveInvalidCharacters(fileName));
            if (File.Exists(filePath))
            {
                if (!options["Overwrite"])
                {
                    await Task.Run(() => { Console.WriteLine($"File {fileName} already exists"); });
                    form.ImageTextProgressBarPerformStep();
                    return;
                }
            }
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(uri, ct);
            using (var fs = new FileStream(filePath, FileMode.CreateNew))
            {
                await response.Content.CopyToAsync(fs);
            }
            form.ImageTextProgressBarPerformStep();
        }

        public static string RemoveInvalidCharacters(string path)
        {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            foreach (char c in invalid)
            {
                path = path.Replace(c.ToString(), "");
            }
            return path;
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> subList, int nSize = 30)
        {
            for (int i = 0; i < subList.Count; i += nSize)
            {
                yield return subList.GetRange(i, Math.Min(nSize, subList.Count - i));
            }
        }

        public static async Task<string> GetAsync(string uri)
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
    }
}
