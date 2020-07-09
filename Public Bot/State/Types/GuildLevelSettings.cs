using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Public_Bot.Modules.Handlers.LevelHandler;

namespace Public_Bot
{
    public class GuildLevelSettings
    {
        public Dictionary<uint, ulong> RankRoles { get; set; } = new Dictionary<uint, ulong>();
        public double LevelMultiplier { get; set; } = 1.10409;
        public uint maxlevel { get; set; } = 100;
        public uint DefaultBaseLevelXp { get; set; } = 30;
        public double XpPerMessage { get; set; } = 1;
        public double XpPerVCMinute { get; set; } = 5;
        public ulong LevelUpChan { get; set; }
        public List<ulong> BlacklistedChannels { get; set; } = new List<ulong>();
        public static GuildLevelSettings Get(ulong id)
        {
            return GuildLevels.Any(x => x.GuildID == id) ? GuildLevels.Find(x => x.GuildID == id).Settings : null;
        }
    }
}
