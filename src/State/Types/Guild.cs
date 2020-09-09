using Discord;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using static Public_Bot.StateService;

namespace Public_Bot
{
    public class Guild : IEntityID
    {
        [GraphQLProp]
        public ulong Id { get; set; }
        [GraphQLProp]
        public string Name { get; set; }
        //[GraphQLObj]
        //public List<GuildMember> GuildMembers { get; set; } = new List<GuildMember>();
        public GuildMemberCache GuildMembers { get; set; }
        [GraphQLObj]
        public GuildLeaderboards Leaderboard { get; set; }

        public static bool Exists(ulong id)
            => StateService.Exists<ExistNullBase>("{\"operationName\":null,\"variables\":{},\"query\":\"{ guild(id: \\\"" + id + "\\\") { Id }}\"}");
        public static Guild Fetch(ulong id)
            => StateService.Query<Guild>(GraphQLParser.GenerateGQLQuery<Guild>("guild", ("id", id)));
        public Guild() 
        {
            GuildMembers = new GuildMemberCache(this);
        }
        public Guild(IGuild g)
        {
            this.Id = g.Id;
            this.Name = g.Name;
            StateService.Mutate<Guild>(GraphQLParser.GenerateGQLMutation<Guild>("createGuild", false, this, "", "", ("Name", this.Name), ("Id", this.Id)));
            this.Leaderboard = new GuildLeaderboards(g);
            GuildMembers = new GuildMemberCache(this);
        }
    }
}
