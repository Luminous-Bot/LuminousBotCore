using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Public_Bot
{
    [DiscordHandler]
    public class GuildSettingsHelper
    {
        public static DiscordShardedClient client { get; set; }
        public GuildSettingsHelper(DiscordShardedClient c)
        {
            client = c;
            if (!Directory.Exists(GuildSettingsFolder))
                Directory.CreateDirectory(GuildSettingsFolder);
        }
        public static List<GuildSettings> LoadedGuildSettings { get; set; } = new List<GuildSettings>();
        public static string GuildSettingsFolder = $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}GuildSettings";
        
        public static GuildSettings GetGuildSettings(ulong GuildID)
        {
            if (LoadedGuildSettings.Any(x => x.GuildID == GuildID))
                return LoadedGuildSettings.Find(x => x.GuildID == GuildID);
            else
            {
                string fp = GuildSettingsFolder + $"{Path.DirectorySeparatorChar}{GuildID}.gs";
                if (File.Exists(fp))
                {
                    var gs = JsonConvert.DeserializeObject<GuildSettings>(File.ReadAllText(fp));
                    if (gs == null)
                        Logger.Write($"GS Null: {GuildID}", Logger.Severity.Critical);
                    FixGs(gs);
                    LoadedGuildSettings.Add(gs);
                    return gs;
                }
                else
                {
                    var gs = new GuildSettings(client.GetGuild(GuildID));
                    LoadedGuildSettings.Add(gs);
                    SaveGuildSettings(gs);
                    return gs;
                }
            }
        }
        public static GuildSettings FixGs(GuildSettings gs)
        {
            if (gs == null)
                return null;
            if(gs.WelcomeCard != null)
                if (gs.WelcomeCard.BackgroundUrl != null)
                    if (gs.WelcomeCard.BackgroundUrl == "https://image.freepik.com/free-vector/luminous-stadium-light-effect_23-2148366134.jpg")
                    {
                        gs.WelcomeCard.BackgroundUrl = null;
                        SaveGuildSettings(gs);
                    }
            return gs;
        }
        public static void SaveGuildSettings(GuildSettings gs)
        {
            Logger.Write($"Saving {gs.GuildID}'s settings");
            string json = JsonConvert.SerializeObject(gs);
            File.WriteAllText(GuildSettingsFolder + $"{Path.DirectorySeparatorChar}{gs.GuildID}.gs", json);
            Logger.Write($"Saved {gs.GuildID}");
        }
    }
}
