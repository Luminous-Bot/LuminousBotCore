using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Public_Bot
{
    class ConfigLoader
    {
        public static string DataDirectoryPath = $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Data";

        private static string ConfigPath = $"{DataDirectoryPath}{Path.DirectorySeparatorChar}Config.json";
        public static string Token { get; set; }
        public static string StateUrl { get; set; }
        public static void LoadConfig()
        {
            if (!Directory.Exists(DataDirectoryPath))
                Directory.CreateDirectory(DataDirectoryPath);
            if (!File.Exists(ConfigPath))
            {
                File.Create(ConfigPath).Close();
                throw new Exception("Config didnt exist, we created it and stopped executing");
            }
            string configContent = File.ReadAllText(ConfigPath);
            if (configContent == "")
                throw new Exception("Content of config was null");
            Dictionary<string, object> Config = JsonConvert.DeserializeObject<Dictionary<string, object>>(configContent);

            Token = Config["Token"].ToString();
            StateUrl = Config["StateUrl"].ToString();
        }
    }
}
