using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Public_Bot
{
    [DiscordHandler]
    public class UserHandler
    {
        public DiscordShardedClient client;
        public static List<User> Users { get; set; }
        public UserHandler(DiscordShardedClient c)
        {
            client = c;

            Users = StateService.Query<List<User>>(GraphQLParser.GenerateGQLQuery<User>("users"));
        }
        public static User GetUser(ulong id)
        {
            if (Users.Any(x => x.Id == id))
                return Users.Find(x => x.Id == id);
            else
            {
                var u = User.Fetch(id);
                if (u != null)
                {
                    Users.Add(u);
                    return u;
                }
                else
                    return null;
            }    
        }
    }
}
