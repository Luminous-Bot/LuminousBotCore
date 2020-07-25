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
        [GraphQLObj]
        [GraphQLName("LevelMembers"), JsonProperty("LevelMembers")]
        public List<LevelUser> CurrentUsers { get; set; } = new List<LevelUser>();


        public static GuildLeaderboards Get(ulong id)
            => GuildLevels.Any(x => x.GuildID == id) ? GuildLevels.Find(x => x.GuildID == id) : null;
        public void Save()
            => StateService.Mutate<GuildLeaderboards>(GraphQLParser.GenerateGQLMutation<GuildLeaderboards>("createOrUpdateGuildLeaderboard", true, this, "GuildLevelSettings", "GuildLevelSettingsInput!", new KeyValuePair<string, object>("GuildID", this.GuildID)));
        public async Task SaveAsync()
            => await StateService.MutateAsync<GuildLeaderboards>(GraphQLParser.GenerateGQLMutation<GuildLeaderboards>("createOrUpdateGuildLeaderboard", true, this, "GuildLevelSettings", "GuildLevelSettingsInput!", new KeyValuePair<string, object>("GuildID", this.GuildID)));

        public static GuildLeaderboards Fetch(ulong id)
            => StateService.Query<GuildLeaderboards>(GraphQLParser.GenerateGQLQuery<GuildLeaderboards>("guildLeaderboard", new KeyValuePair<string, object>("GuildID", id)));
        public GuildLeaderboards() { }
        public GuildLeaderboards(IGuild g)
        {
            this.GuildID = g.Id;
            if (g.AFKChannelId.HasValue)
                this.Settings.BlacklistedChannels.Add(g.AFKChannelId.Value);
            this.Settings.LevelUpChan = g.SystemChannelId.HasValue ? g.SystemChannelId.Value : g.DefaultChannelId;
            Save();
        }
    }
}
