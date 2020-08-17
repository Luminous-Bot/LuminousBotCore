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
        public static DiscordShardedClient client;
        public static List<User> Users { get; set; }
        public UserHandler(DiscordShardedClient c)
        {
            client = c;

            Users = new List<User>();
        }
        public static User CreateUser(ulong Id)
        {
            var usr = client.GetUser(Id);
            var u = new User(usr);
            Users.Add(u);
            return u;
        }
        public static bool Exists(ulong id)
        {
            if (Users.Any(x => x.Id == id))
                return true;
            else
            {
                if (User.UserExists(id))
                {
                    var u = User.Fetch(id);
                    Users.Add(u);
                    return true;
                }
                else
                    return false;
            }
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
