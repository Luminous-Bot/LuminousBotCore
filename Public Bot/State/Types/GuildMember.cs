using Discord.WebSocket;
using Public_Bot.State.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot
{
    public class GuildMember
    {
        [GraphQLProp, GraphQLSVar]
        public ulong GuildID { get; set; }
        [GraphQLObj]
        public List<NameRecord> Nicknames { get; set; } = new List<NameRecord>();
        [GraphQLProp, GraphQLSVar]
        public ulong UserID { get; set; }
        [GraphQLSVar]
        public string CurrentNickname { get; set; }
        [GraphQLObj]
        public List<Infraction> Infractions { get; set; } = new List<Infraction>();
        [GraphQLObj]
        public User User { get; set; }
        public static GuildMember Fetch(ulong id, ulong GuildID)
            => StateService.Query<GuildMember>(GraphQLParser.GenerateGQLQuery<GuildMember>("guildMember", new KeyValuePair<string, object>("UserID", id), new KeyValuePair<string, object>("GuildID", GuildID)));
        
        public GuildMember() { }
        public GuildMember(SocketGuildUser user)
        {
            this.GuildID = user.Id;
            this.UserID = user.Id;
            this.User = UserHandler.GetUser(user.Id);
            this.CurrentNickname = user.Nickname;
            StateService.Mutate<GuildMember>(GraphQLParser.GenerateGQLMutation<GuildMember>("createGuildMember", true, this, "CreateGuildMemberInput"));
        }
    }
}
