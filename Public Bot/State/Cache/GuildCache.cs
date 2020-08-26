using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Public_Bot
{
    public class GuildCache
    {
        private static SingleIDEntityCache<Guild> Guilds = new SingleIDEntityCache<Guild>();

        public static int Count { get => Guilds.Count; }
        /// <summary>
        /// Adds a guild member to the cache
        /// </summary>
        /// <param name="gm">The GuildMember to add</param>
        public static void AddGuildMember(GuildMember gm)
        {
            var g = GetGuild(gm.GuildID);
            if(g != null)
                g.GuildMembers.AddGuildMember(gm);
        }
        /// <summary>
        /// Checks if a guild exists in the cache or Db
        /// </summary>
        /// <param name="Id">The Guild ID</param>
        /// <returns>Boolean representing if the guild exists</returns>
        public static bool GuildExists(ulong Id)
        {
            if (Guilds.Any(x => x.Id == Id))
                return true;
            else
                return Guild.Exists(Id);
        }
        public static void AddGuild(Guild g)
            => Guilds.Add(g);
        /// <summary>
        /// Gets a guild
        /// </summary>
        /// <param name="Id">The Guilds ID</param>
        /// <returns>The fetched Guild</returns>
        public static Guild GetGuild(ulong Id)
        {
            if (Guilds.Any(x => x.Id == Id))
                return Guilds[Id];
            else
            {
                var g = Guild.Fetch(Id);
                Guilds.Add(g);
                return g;
            }    
        }
    }
}
