using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Public_Bot
{
    public class GuildLevelSettings
    {
        [GraphQLObj, GraphQLSObj]
        public List<RankRole> RankRoles { get; set; } = new List<RankRole>();
        [GraphQLProp, GraphQLSVar]
        public double LevelMultiplier { get; set; } = 1.12;
        [GraphQLProp, GraphQLSVar]
        public uint MaxLevel { get; set; } = 100;
        [GraphQLProp, GraphQLSVar]
        public uint DefaultBaseLevelXp { get; set; } = 30;
        [GraphQLProp, GraphQLSVar]
        public double XpPerMessage { get; set; } = 1;
        [GraphQLProp, GraphQLSVar]
        public double XpPerVCMinute { get; set; } = 2;
        [GraphQLProp, GraphQLSVar]
        public double XpPerVCStream { get; set; } = 3;
        [GraphQLProp, GraphQLSVar]
        public ulong LevelUpChan { get; set; }
        [GraphQLProp, GraphQLSVar]
        public List<ulong> BlacklistedChannels { get; set; } = new List<ulong>();
        public static GuildLevelSettings Get(ulong id)
        {
            var g = GuildCache.GetGuild(id);
            if (g == null)
                return null;
            if (g.Leaderboard == null)
                return null;
            return g.Leaderboard.Settings;
        }

    }
}
