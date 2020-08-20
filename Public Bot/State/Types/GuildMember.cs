using Discord.WebSocket;
using Public_Bot.State.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot
{
    public class GuildMember
    {
        [GraphQLProp, GraphQLSVar]
        public ulong GuildID { get; set; }
        [GraphQLProp, GraphQLSVar]
        public ulong UserID { get; set; }
        public string Username
            => User.Username;
        [GraphQLProp]
        public bool IsInServer { get; set; }
        [GraphQLProp]
        public Nullable<DateTime> DateLeft { get; set; }

        public async Task<GuildMember> UpdateIsInServer(bool isInServer)
        {
            var q = GraphQLParser.GenerateGQLMutation<GuildMember>("updateGuildMemberServerStatus", false, null, "", "",
                new KeyValuePair<string, object>("DateLeft", isInServer ? null : DateTime.UtcNow.ToString("o")),
                new KeyValuePair<string, object>("IsInServer", isInServer), 
                new KeyValuePair<string, object>("GuildID", this.GuildID),
                new KeyValuePair<string, object>("UserID", this.UserID));
            return await StateService.MutateAsync<GuildMember>(q);
        }

        [GraphQLObj]
        public List<Infraction> Infractions { get; set; } = new List<Infraction>();
        [GraphQLObj]
        public User User { get; set; }
        public static GuildMember Fetch(ulong id, ulong GuildID)
            => StateService.Query<GuildMember>(GraphQLParser.GenerateGQLQuery<GuildMember>("guildMember", new KeyValuePair<string, object>("UserID", id), new KeyValuePair<string, object>("GuildID", GuildID)));
        public static bool Exists(ulong Id, ulong GuildID)
            => StateService.Exists<GuildMember>(GraphQLParser.GenerateGQLQuery<GuildMember>("guildMember", new KeyValuePair<string, object>("UserID", Id), new KeyValuePair<string, object>("GuildID", GuildID)));
        public GuildMember() { }
        public GuildMember(SocketGuildUser user)
        {
            this.GuildID = user.Guild.Id;
            this.UserID = user.Id;
            if (!UserHandler.Users.Any(x => x.Id == this.UserID))
                if (!User.UserExists(UserID))
                    UserCache.CreateUser(UserID);
            var mt = StateService.Mutate<GuildMember>(GraphQLParser.GenerateGQLMutation<GuildMember>("createGuildMember", true, this, "data", "CreateGuildMemberInput!"));
            this.User = mt.User;

            //add to cache 
            GuildCache.AddGuildMember(this);
        }
        public GuildMember(ulong UserId, ulong GuildId)
        {
            this.GuildID = GuildId;
            this.UserID = UserId;
            if (!UserHandler.Users.Any(x => x.Id == this.UserID))
                if (!User.UserExists(UserID))
                    UserCache.CreateUser(UserID);
            var mt = StateService.Mutate<GuildMember>(GraphQLParser.GenerateGQLMutation<GuildMember>("createGuildMember", true, this, "data", "CreateGuildMemberInput!"));
            this.User = mt.User;

            //add to cache 
            GuildCache.AddGuildMember(this);
        }
    }
}
