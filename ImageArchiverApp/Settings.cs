using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace ImageArchiverApp
{
    public class Settings
    {
        private readonly MainWindow form;
        public Settings(MainWindow form)
        {
            this.form = form;
        }

        public void SaveCurrent()
        {
            File.WriteAllText(@"settings.json", JsonConvert.SerializeObject(form.Settings, Formatting.Indented));
        }

        public void ReadFromFile()
        {
            try
            {
                var settings = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, SingleOption>>>(File.ReadAllText(@"settings.json"));
                form.Settings = settings;
            }
            catch
            {
                form.Settings = Default.Settings;
                SaveCurrent();
            }
        }
    }
    public class SingleOption
    {
        public readonly string ControlType;
        public bool IsTrue;
        public readonly string Title;

        public SingleOption(string controlType, bool isTrue, string title)
        {
            ControlType = controlType;
            IsTrue = isTrue;
            Title = title;
        }
    }
}
