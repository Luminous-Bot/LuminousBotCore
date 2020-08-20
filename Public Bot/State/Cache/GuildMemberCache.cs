using Discord.WebSocket;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Public_Bot
{
    //[DiscordHandler]
    public class GuildMemberCache
    {
        public Guild CurrentGuild { get; private set; }
        public GuildMemberCache(Guild guild)
        {
            CurrentGuild = guild;
        }
        private ConcurrentBag<GuildMember> GuildMembers = new ConcurrentBag<GuildMember>();

        public void AddGuildMember(GuildMember member)
            => GuildMembers.Add(member);
        public GuildMember CreateGuildMember(ulong UserId)
            => CreateGuildMember(UserId, this.CurrentGuild.Id);
        /// <summary>
        /// Creates a <seealso cref="GuildMember"/> and a <seealso cref="User"/> if there isn't one already 
        /// </summary>
        /// <param name="UserID">The users ID</param>
        /// <param name="GuildId">The guilds ID</param>
        /// <returns>The newly created <seealso cref="GuildMember"/></returns>
        public GuildMember CreateGuildMember(ulong UserID, ulong GuildId)
        {
            if(!UserCache.UserExists(UserID))
                UserCache.CreateUser(UserID);
            var gm = new GuildMember(UserID, GuildId);
            GuildMembers.Add(gm);
            return gm;
        }
        public bool GuildMemberExists(ulong UserID)
            => GuildMemberExists(UserID, this.CurrentGuild.Id);
        public bool GuildMemberExists(ulong UserID, ulong GuildId)
        {
            if (GuildMembers.Any(x => x.UserID == UserID && x.GuildID == GuildId))
                return true;
            else return GuildMember.Exists(UserID, GuildId);
        }
        public GuildMember GetGuildMember(ulong UserId)
            => GetGuildMember(UserId, this.CurrentGuild.Id);
        public GuildMember GetGuildMember(ulong UserID, ulong GuildId)
        {
            if (GuildMembers.Any(x => x.UserID == UserID && x.GuildID == GuildId))
                return GuildMembers.First(x => x.UserID == UserID && x.GuildID == GuildId);
            else
            {
                if (GuildMember.Exists(UserID, GuildId))
                {
                    var gm = GuildMember.Fetch(UserID, GuildId);
                    GuildMembers.Add(gm);
                    return gm;
                }    
                else
                    return null;
            }
        }
    }
}
