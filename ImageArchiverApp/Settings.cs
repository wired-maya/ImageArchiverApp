using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

//TODO:
// - make the options getter in imagearchiverapp.cs return everything as one dictionary, to then simplify this (once there are more downloaders maybe?)
// - nameof() is a thing
// - think of more things to do

namespace ImageArchiverApp
{
    class Settings
    {
        private readonly MainWindow form;
        public Settings(MainWindow form)
        {
            this.form = form;
        }

        public void SaveCurrent()
        {
            Dictionary<string, Dictionary<string, bool>> options = new Dictionary<string, Dictionary<string, bool>>();
            options.Add("NhentaiOptions", form.NhentaiOptions);
            options.Add("PixivOptions", form.PixivOptions);
            File.WriteAllText(@"settings.json", JsonConvert.SerializeObject(options, Formatting.Indented));
        }

        public void ReadFromFile()
        {
            try
            {
                var settings = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, bool>>>(File.ReadAllText(@"settings.json"));
                foreach (var options in settings)
                {
                    if (options.Key == "NhentaiOptions")
                    {
                        form.NhentaiOptions = options.Value;
                    }
                    else if (options.Key == "PixivOptions")
                    {
                        form.PixivOptions = options.Value;
                    }
                }
            }
            catch
            {
                form.NhentaiOptions = new Dictionary<string, bool>
                {
                    {"PrettyNames", true },
                    {"IncludeTitleInFilename", false },
                    {"Overwrite", false },
                };
                form.PixivOptions = new Dictionary<string, bool>
                {
                    {"SortByArtist", true },
                    {"SortByFranchise", false },
                    {"FilesAsTitle", false },
                    {"Overwrite", false },
                };
                SaveCurrent();
            }
        }
    }
}
