using Oqtane.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marks.Module.Wiki
{
    internal class SettingsViewModel
    {
        public static class Settings {
            public static string Topic => nameof(SettingsViewModel.Topic);
            public static string Category => nameof(SettingsViewModel.Topic);
        }

        public SettingsViewModel(ISettingService settingService, Dictionary<string, string> moduleSettings) {
            Topic = settingService.GetSetting(moduleSettings, Settings.Topic, Topic);
            Category = settingService.GetSetting(moduleSettings, Settings.Category, Category);
        }

        public string Topic { get; set; } = "";
        public string Category { get; set; } = "General";
    }
}


