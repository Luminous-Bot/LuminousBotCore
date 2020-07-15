using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Public_Bot
{
    public class GuildLevelSettings
    {
        [GraphQLObj]
        public List<RankRole> RankRoles { get; set; } = new List<RankRole>();
        [GraphQLProp]
        public double LevelMultiplier { get; set; } = 1.10409;
        [GraphQLProp]
        public uint MaxLevel { get; set; } = 100;
        [GraphQLProp]
        public uint DefaultBaseLevelXp { get; set; } = 30;
        [GraphQLProp]
        public double XpPerMessage { get; set; } = 1;
        [GraphQLProp]
        public double XpPerVCMinute { get; set; } = 5;
        [GraphQLProp]
        public ulong LevelUpChan { get; set; }
        [GraphQLProp]
        public List<ulong> BlacklistedChannels { get; set; } = new List<ulong>();
        public static GuildLevelSettings Get(ulong id)
            => LevelHandler.GuildLevels.Any(x => x.GuildID == id) 
               ? LevelHandler.GuildLevels.Find(x => x.GuildID == id).Settings 
               : null;
    }
}
