using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Public_Bot.Modules.Handlers.LevelHandler;

namespace Public_Bot
{
    public class GuildLeaderboards
    {
        public ulong GuildID { get; set; }
        public GuildLevelSettings Settings { get; set; } = new GuildLevelSettings();
        public List<LevelUser> CurrentUsers { get; set; } = new List<LevelUser>();
        public static GuildLeaderboards Get(ulong id)
            => GuildLevels.Any(x => x.GuildID == id) ? GuildLevels.Find(x => x.GuildID == id) : null;

        public static void Save() => SaveLevels();
        public void SaveCurrent() => SaveLevels();

        public GuildLeaderboards() { }
        public GuildLeaderboards(IGuild g)
        {
            this.GuildID = g.Id;
            if (g.AFKChannelId.HasValue)
                this.Settings.BlacklistedChannels.Add(g.AFKChannelId.Value);
            this.Settings.LevelUpChan = g.DefaultChannelId;
            GuildLevels.Add(this);
            SaveLevels();
        }

    }
}
