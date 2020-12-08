using System.Collections.Generic;

namespace ImageArchiverApp
{
    class Default
    {
        public static readonly Dictionary<string, Dictionary<string, SingleOption>> Settings = new Dictionary<string, Dictionary<string, SingleOption>>() {
            {
            "NhentaiOptions",
            new Dictionary<string, SingleOption> () {
                { "PrettyNames",
                    new SingleOption("CheckBox", true, "Use pretty doujin names (File paths could be too long without this on!)")
                },
                { "IncludeTitleInFilename",
                    new SingleOption("CheckBox", false, "Include title in filename")
                },
                { "Overwrite",
                    new SingleOption("CheckBox", false, "Overwrite existing files")
                }
            }
            },
            {
                "PixivOptions",
                new Dictionary<string, SingleOption> () {
                    { "FilesAsTitle",
                        new SingleOption("CheckBox", false, "Name files after work title")
                    },
                    { "Overwrite",
                        new SingleOption("CheckBox", false, "Overwrite existing files")
                    }
                }
            },
            {
                "BooruOptions",
                new Dictionary<string, SingleOption> () {
                    { "Overwrite",
                        new SingleOption("CheckBox", false, "Overwrite existing files")
                    },
                    { "FilesAsTags",
                        new SingleOption("CheckBox", false, "Name files after tags")
                    },
                    { "SortByArtist",
                        new SingleOption("RadioButton", false, "Sort by artist")
                    },
                    { "SortByFranchise",
                        new SingleOption("RadioButton", false, "Sort by franchise")
                    },
                    { "SortByTag",
                        new SingleOption("RadioButton", true, "Sort by tag entered")
                    }
                }
            }
        };
    }
}