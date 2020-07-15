using Discord;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot
{
    public class Guild
    {
        [GraphQLProp]
        public ulong ID { get; set; }
        [GraphQLProp]
        public string Name { get; set; }
        [GraphQLObj]
        public List<GuildMember> GuildMembers { get; set; } = new List<GuildMember>();
        [GraphQLObj]
        public GuildLeaderboards Leaderboard { get; set; }

        public static Guild Fetch(ulong id)
            => StateService.Query<Guild>(GraphQLParser.GenerateGQLQuery<Guild>("guild", new KeyValuePair<string, object>("id", id)));
        public Guild() { }
        public Guild(IGuild g)
        {
            this.ID = g.Id;
            this.Name = g.Name;
            StateService.Mutate<Guild>(GraphQLParser.GenerateGQLMutation<Guild>("createGuild", false, this, "", new KeyValuePair<string, object>("Name", this.Name), new KeyValuePair<string, object>("Id", this.ID)));
            this.Leaderboard = new GuildLeaderboards(g);
        }
    }
}
