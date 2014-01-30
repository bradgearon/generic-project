using System;
using System.Configuration;

namespace GenericProject
{
    public static class Settings
    {
        private static string GetAppSettingValue(string key)
        {
            return ConfigurationManager.AppSettings[key] ?? String.Empty;
        }
    }
}