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
        private DoubleIDEntityCache<GuildMember> GuildMembers = new DoubleIDEntityCache<GuildMember>();

        public int Count { get => GuildMembers.Count; }
        /// <summary>
        /// Adds a guildmember to the cache
        /// </summary>
        /// <param name="member">The guild member to add</param>
        public void AddGuildMember(GuildMember member)
            => GuildMembers.Add(member);
        /// <summary>
        /// Creates a <seealso cref="GuildMember"/> and a <seealso cref="User"/> if there isn't one already 
        /// </summary>
        /// <param name="UserID">The users ID</param>
        /// <returns>The newly created <seealso cref="GuildMember"/></returns>
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
        /// <summary>
        /// Checks if a guild member exists in the cache or the db
        /// </summary>
        /// <param name="UserID">the users id to check</param>
        /// <returns>If a guild member exists</returns>
        public bool GuildMemberExists(ulong UserID)
            => GuildMemberExists(UserID, this.CurrentGuild.Id);
        /// <summary>
        /// Checks if a guild member exists in the cache or the db
        /// </summary>
        /// <param name="UserID">The Users ID to check</param>
        /// <param name="GuildId">The Guilds ID</param>
        /// <returns>If a guild member exists</returns>
        public bool GuildMemberExists(ulong UserID, ulong GuildId)
        {
            if (GuildMembers.Any(x => x.Id == UserID && x.GuildID == GuildId))
                return true;
            else return GuildMember.Exists(UserID, GuildId);
        }
        /// <summary>
        /// Gets a guild member
        /// </summary>
        /// <param name="UserId">The Users ID</param>
        /// <returns>The fetched guild member</returns>
        public GuildMember GetGuildMember(ulong UserId)
            => GetGuildMember(UserId, this.CurrentGuild.Id);
        /// <summary>
        /// Gets a guild member
        /// </summary>
        /// <param name="UserID">The Users ID</param>
        /// <param name="GuildId">The Guilds ID</param>
        /// <returns>The fetched guild member</returns>
        public GuildMember GetGuildMember(ulong UserID, ulong GuildId)
        {
            if (GuildMembers.Any(x => x.Id == UserID && x.GuildID == GuildId))
                return GuildMembers[UserID, GuildId];
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
