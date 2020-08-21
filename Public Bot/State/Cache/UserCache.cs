using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Public_Bot
{
    [DiscordHandler]
    public class UserCache
    {
        public static DiscordShardedClient client;
        public UserCache(DiscordShardedClient c)
        {
            client = c;
        }
        private static ConcurrentBag<User> Users = new ConcurrentBag<User>();

        /// <summary>
        /// Creates a User
        /// </summary>
        /// <param name="id">The users ID</param>
        /// <returns>The newly created user</returns>
        public static User CreateUser(ulong id)
        {
            var user = client.GetUser(id);
            if (user == null)
                return null;
            else
                return CreateUser(user);
        }
        /// <summary>
        /// Creates a User
        /// </summary>
        /// <param name="user">The user to create</param>
        /// <returns>The newly created user</returns>
        public static User CreateUser(IUser user)
        {
            var u = new User(user);
            Users.Add(u);
            return u;
        }
        public static bool UserExists(ulong UserId)
        {
            if (Users.Any(x => x.Id == UserId))
                return true;
            else
                return User.UserExists(UserId);
        }
        public static User GetUser(ulong UserId)
        {
            if (Users.Any(x => x.Id == UserId))
                return Users.First(x => x.Id == UserId);
            else if (User.UserExists(UserId))
            {
                var u = User.Fetch(UserId);
                Users.Add(u);
                return u;
            }    
            else
                return null;
        }
    }
}
