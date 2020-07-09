using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot.State.Types
{
    public class GuildLevelSettings : StateEntity<GuildLevelSettings>
    {
        public Dictionary<uint, string> RankRoles { get; set; }
        public double LevelMultiplier { get; set; }
        public uint MaxLevel { get; set; }
        public uint DefaultBaseLevelXp { get; set; }
        public double XpPerMessage { get; set; }
        public double XpPerVCMinute { get; set; }
        public string LevelUpChan { get; set; }
        public List<string> BlacklistedChannels { get; set; }
    }
}
