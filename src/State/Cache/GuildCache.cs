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
        public static event EventHandler<Guild> ItemAdded;
        public static int TotalGuildMembers
            => _TotalGuildMembers();
        public static int TotalLevelMembers
           => _TotalLevelMembers();
        private static int _TotalGuildMembers()
        {
            int i = 0;

            foreach (var guild in Guilds.ToList())
                i += guild.GuildMembers.Count;

            return i;
        }
        private static int _TotalLevelMembers()
        {
            int i = 0;

            foreach (var guild in Guilds.ToList())
            {
                if (guild == null)
                    continue;
                if (guild.Leaderboard == null)
                    continue;
                if (guild.Leaderboard.CurrentUsers == null)
                    continue;

                i += guild.Leaderboard.CurrentUsers.Count;
            }

            return i;
        }
        public static bool CacheContains(ulong GuildId)
            => Guilds.Any(x => x.Id == GuildId);
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
        {
            Guilds.Add(g);
            ItemAdded?.Invoke(null, g);
        }
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
                ItemAdded?.Invoke(null, g);
                return g;
            }    
        }
    }
}
