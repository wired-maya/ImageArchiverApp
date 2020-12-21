using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ImageArchiverApp.Downloaders
{
    class GenericDownloader : BaseDownloader
    {
        public GenericDownloader(MainWindow form) : base(form) { }

        public override Dictionary<string, dynamic> DefaultSettings
        {
            get => new Dictionary<string, dynamic>()
            {
                { "Overwrite", false },
                { "NumFileNames", false },
                { "OriginalFileNames", true }
            };
        }

        public override Dictionary<string, SingleOption> SettingsStructure
        {
            get => new Dictionary<string, SingleOption>()
            {
                { "Overwrite", new SingleOption("CheckBox", "Overwrite existing files") },
                { "NumFileNames", new SingleOption("RadioButton", "Numbered file names") },
                { "OriginalFileNames", new SingleOption("RadioButton", "Preserve original filenames") }
            };
        }

        public override string Name { get => "Generic Downloader"; }
        public override string TextBoxWatermark { get => "Downloads all images found in a webpage. Paste links separated by spaces"; }

        protected override async Task DownloadGalleryAsync(string id, CancellationToken ct)
        {
            
        }
    }
}
