using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Public_Bot
{
    public class LevelMemberCache
    {
        private GuildLeaderboards GuildLeaderboards { get; set; }
        private DoubleIDEntityCache<LevelUser> LevelUsers = new DoubleIDEntityCache<LevelUser>();
        public LevelMemberCache(GuildLeaderboards guildLeaderboards)
        {
            this.GuildLeaderboards = guildLeaderboards;
        }
        public int Count { get => LevelUsers.Count; }
        public void AddLevelUser(LevelUser u)
            => LevelUsers.Add(u);

        public bool LevelUserExists(ulong UserId)
            => LevelUserExists(UserId, GuildLeaderboards.GuildID);
        public bool LevelUserExists(ulong UserId, ulong GuildId)
        {
            if (LevelUsers.Any(x => x.Id == UserId && x.GuildID == GuildId))
                return true;
            else
                return LevelUser.Exists(GuildId, UserId);
        }

        public LevelUser GetLevelUser(ulong UserId)
            => GetLevelUser(UserId, GuildLeaderboards.GuildID);
        public LevelUser GetLevelUser(ulong UserId, ulong GuildId)
        {
            if (LevelUsers.Any(x => x.Id == UserId && x.GuildID == GuildId))
                return LevelUsers[UserId, GuildId];
            else
            {
                if (LevelUser.Exists(GuildId, UserId))
                {
                    var lu = LevelUser.Fetch(GuildId, UserId);
                    LevelUsers.Add(lu);
                    return lu;
                }
                else
                    return null;
            }
        }
    }
}
