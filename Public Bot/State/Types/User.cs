using Discord;
using Discord.WebSocket;
using Public_Bot.State.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot
{
    public class User
    {
        [GraphQLSVar, GraphQLProp]
        public ulong Id { get; set; }
        [GraphQLSVar, GraphQLProp]
        public string BarColor { get; set; } = "00ff00";
        [GraphQLSVar, GraphQLProp]
        public string BackgroundColor { get; set; } = "262626";
        [GraphQLSVar, GraphQLProp]
        public string BackgroundUrl { get; set; }
        [GraphQLSVar, GraphQLProp]
        public bool Donator { get; set; } = false;
        [GraphQLSVar, GraphQLProp]
        public bool MentionOnLevelup { get; set; } = true;
        [GraphQLObj]
        public List<NameRecord> Usernames { get; set; }
        [GraphQLSVar, GraphQLProp]
        public string Username { get; set; }
        public User() { }
        public User(IUser user) 
        {
            this.Id = user.Id;
            this.Username = GraphQLParser.CleanUserContent(user.Username + "#" + user.DiscriminatorValue);
            StateService.Mutate<User>(GraphQLParser.GenerateGQLMutation<User>("createUser", true, this, "data", "CreateUserInput!"));
        }
        public static bool UserExists(ulong Id)
            => StateService.Query<bool>("{\"operationName\":null,\"variables\":{},\"query\":\"{ userExists(id: \\\"" + Id + "\\\") } \"}", true);
        public static User Fetch(ulong Id)
            => StateService.Query<User>(GraphQLParser.GenerateGQLQuery<User>("user", ("id", Id)));
    }
}
