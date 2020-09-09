using Discord.WebSocket;
using Newtonsoft.Json;
using Public_Bot.State.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot
{
    public class GuildMember : IDoubleEntityID
    {
        [GraphQLProp, GraphQLSVar]
        public ulong GuildID { get; set; }
        [GraphQLProp, GraphQLSVar, GraphQLName("UserID"), JsonProperty("UserID")]
        public ulong Id { get; set; }
        public string Username
            => User.Username;
        [GraphQLProp]
        public bool IsInServer { get; set; }
        [GraphQLProp]
        public Nullable<DateTime> DateLeft { get; set; }

        public async Task<GuildMember> UpdateIsInServer(bool isInServer)
        {
            var q = GraphQLParser.GenerateGQLMutation<GuildMember>("updateGuildMemberServerStatus", false, null, "", "",
                ("DateLeft", isInServer ? null : DateTime.UtcNow.ToString("o")),
                ("IsInServer", isInServer), 
                ("GuildID", this.GuildID),
                ("UserID", this.Id));
            return await StateService.MutateAsync<GuildMember>(q);
        }

        [GraphQLObj]
        public List<Infraction> Infractions { get; set; } = new List<Infraction>();
        [GraphQLObj]
        public User User { get; set; }
        public static GuildMember Fetch(ulong id, ulong GuildID)
            => StateService.Query<GuildMember>(GraphQLParser.GenerateGQLQuery<GuildMember>("guildMember", ("UserID", id), ("GuildID", GuildID)));
        public static bool Exists(ulong Id, ulong GuildID)
            => StateService.Exists<GuildMember>(GraphQLParser.GenerateGQLQuery<GuildMember>("guildMember", ("UserID", Id), ("GuildID", GuildID)));
        public GuildMember() { }
        public GuildMember(SocketGuildUser user)
        {
            this.GuildID = user.Guild.Id;
            this.Id = user.Id;
            if (!UserCache.UserExists(user.Id))
                UserCache.CreateUser(user);
            var mt = StateService.Mutate<GuildMember>(GraphQLParser.GenerateGQLMutation<GuildMember>("createGuildMember", true, this, "data", "CreateGuildMemberInput!"), true);
            this.User = UserCache.GetUser(this.Id);

            //add to cache 
            GuildCache.AddGuildMember(this);
        }
        public GuildMember(ulong UserId, ulong GuildId)
        {
            this.GuildID = GuildId;
            this.Id = UserId;
            if (!UserCache.UserExists(UserId))
                UserCache.CreateUser(UserId);
            var mt = StateService.Mutate<GuildMember>(GraphQLParser.GenerateGQLMutation<GuildMember>("createGuildMember", true, this, "data", "CreateGuildMemberInput!"), true);
            this.User = UserCache.GetUser(this.Id);
        }
    }
}
