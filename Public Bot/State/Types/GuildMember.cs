using Discord.WebSocket;
using Public_Bot.State.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                    UserHandler.CreateUser(UserID);
            var mt = StateService.Mutate<GuildMember>(GraphQLParser.GenerateGQLMutation<GuildMember>("createGuildMember", true, this, "data", "CreateGuildMemberInput!"));
            this.User = mt.User;
        }
    }
}
