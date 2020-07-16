using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Public_Bot
{
    public class GuildLevelSettings
    {
        //FIX THIS: Look at parser recurse
        [GraphQLObj, GraphQLSObj]
        public List<RankRole> RankRoles { get; set; } = new List<RankRole>();
        [GraphQLProp, GraphQLSVar]
        public double LevelMultiplier { get; set; } = 1.10409;
        [GraphQLProp, GraphQLSVar]
        public uint MaxLevel { get; set; } = 100;
        [GraphQLProp, GraphQLSVar]
        public uint DefaultBaseLevelXp { get; set; } = 30;
        [GraphQLProp, GraphQLSVar]
        public double XpPerMessage { get; set; } = 1;
        [GraphQLProp, GraphQLSVar]
        public double XpPerVCMinute { get; set; } = 5;
        [GraphQLProp, GraphQLSVar]
        public ulong LevelUpChan { get; set; }
        //fix type to gen json arr
        [GraphQLProp, GraphQLSVar]
        public List<ulong> BlacklistedChannels { get; set; } = new List<ulong>();
        public static GuildLevelSettings Get(ulong id)
            => LevelHandler.GuildLevels.Any(x => x.GuildID == id) 
               ? LevelHandler.GuildLevels.Find(x => x.GuildID == id).Settings 
               : null;
    }
}
