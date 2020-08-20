using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Public_Bot
{
    public class GuildCache
    {
        private static ConcurrentBag<Guild> Guilds = new ConcurrentBag<Guild>();

        public static void AddGuildMember(GuildMember gm)
        {
            if(GuildExists(gm.GuildID))
            {
                var g = GetGuild(gm.GuildID);
                g.GuildMembers.AddGuildMember(gm);
            }
        }
        public static bool GuildExists(ulong Id)
        {
            if (Guilds.Any(x => x.Id == Id))
                return true;
            else
                return Guild.Exists(Id);
        }
        public static Guild GetGuild(ulong Id)
        {
            if (Guilds.Any(x => x.Id == Id))
                return Guilds.First(x => x.Id == Id);
            else
            {
                var g = Guild.Fetch(Id);
                Guilds.Add(g);
                return g;
            }    
        }
    }
}
