using Discord;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Public_Bot.Modules.Handlers.LevelHandler;

namespace Public_Bot
{
    public class GuildLeaderboards
    {
        [GraphQLProp]
        public ulong GuildID { get; set; }
        [GraphQLObj, GraphQLSObj]
        public GuildLevelSettings Settings { get; set; } = new GuildLevelSettings();
        //[GraphQLObj]
        //[GraphQLName("LevelMembers"), JsonProperty("LevelMembers")]
        //public List<LevelUser> CurrentUsers { get; set; } = new List<LevelUser>();
        public LevelMemberCache CurrentUsers { get; set; }

        public static GuildLeaderboards Get(ulong id)
        {
            var g = GuildCache.GetGuild(id);
            if (g == null)
                return null;
            return g.Leaderboard;
        }
        public void Save()
            => StateService.Mutate<GuildLeaderboards>(GraphQLParser.GenerateGQLMutation<GuildLeaderboards>("createOrUpdateGuildLeaderboard", true, this, "GuildLevelSettings", "GuildLevelSettingsInput!", ("GuildID", this.GuildID)));
        public async Task SaveAsync()
            => await StateService.MutateAsync<GuildLeaderboards>(GraphQLParser.GenerateGQLMutation<GuildLeaderboards>("createOrUpdateGuildLeaderboard", true, this, "GuildLevelSettings", "GuildLevelSettingsInput!", ("GuildID", this.GuildID)));

        public static GuildLeaderboards Fetch(ulong id)
            => StateService.Query<GuildLeaderboards>(GraphQLParser.GenerateGQLQuery<GuildLeaderboards>("guildLeaderboard", ("GuildID", id))).Value;
        public List<LevelUser> GetTop(int count)
            => StateService.Query<List<LevelUser>>(GraphQLParser.GenerateGQLQuery<List<LevelUser>>("topLevelMembers", ("Count", count), ("GuildID", $"{this.GuildID}"))).Value;
        public GuildLeaderboards() 
        {
            CurrentUsers = new LevelMemberCache(this);
        }
        public GuildLeaderboards(IGuild g)
        {
            
            this.GuildID = g.Id;
            if (g.AFKChannelId.HasValue)
                this.Settings.BlacklistedChannels.Add(g.AFKChannelId.Value);
            this.Settings.LevelUpChan = g.SystemChannelId.HasValue ? g.SystemChannelId.Value : g.DefaultChannelId;
            Save();
            CurrentUsers = new LevelMemberCache(this);
        }
    }
}
