using Discord.WebSocket;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot
{
    [DiscordHandler]
    public class GuildHandler
    {
        public static DiscordShardedClient client;
        //public static List<Guild> CurrentGuilds = new List<Guild>();
        public GuildHandler(DiscordShardedClient c)
        {
            client = c;
            client.JoinedGuild += AddGuild;
            client.UserJoined += NewUser;
            client.GuildAvailable += Client_GuildAvailable; ;
            client.ShardConnected += Client_ShardReady;
        }

        private Task Client_GuildAvailable(SocketGuild arg)
        {
            Task.Run(() => HandleGuildDownloadAsync(arg));
            return Task.CompletedTask;
        }

        private async Task Client_ShardReady(DiscordSocketClient arg)
        {
            Logger.Write("Starting Init download..");

            foreach (var guild in arg.Guilds)
            {
                Task.Run(() => HandleGuildDownloadAsync(guild));
            }
        }

        private async Task HandleGuildDownloadAsync(SocketGuild arg)
        {
            // We need to download the guilds members for checking by name/nickname
            Logger.Write($"Starting download of {arg.Name} Members..");
            await arg.DownloadUsersAsync();
            Logger.Write($"Finished download of {arg.Name}");
        }


        #region old
        //public static GuildMember GetOrCreateGuildMember(ulong MemberID, ulong GuildID)
        //{
        //    if (!GuildMemberExists(MemberID, GuildID))
        //    {
        //        if (!GuildMember.Exists(MemberID, GuildID))
        //            return CreateGuildMember(MemberID, GuildID);
        //        else
        //            return GuildMember.Fetch(MemberID, GuildID);
        //    }
        //    else
        //        return CurrentGuilds.Find(x => x.Id == GuildID).GuildMembers.Find(x => x.UserID == MemberID);
        //}
        //public static bool GuildMemberExists(ulong MemberId, ulong GuildID)
        //{
        //    if (!CurrentGuilds.Any(x => x.Id == GuildID))
        //        return false;
        //    var gld = CurrentGuilds.Find(x => x.Id == GuildID);
        //    return gld.GuildMembers.Any(x => x.UserID == MemberId);
        //}
        //public static GuildMember CreateGuildMember(ulong Idm, ulong GuildId)
        //{
        //    var gld = client.GetGuild(GuildId);
        //    if (gld == null)
        //        return null;
        //    var usr = gld.GetUser(Idm);
        //    if (usr == null)
        //        return null;
        //    var gm = new GuildMember(usr);
        //    if (CurrentGuilds.Any(x => x.Id == GuildId))
        //        CurrentGuilds.Find(x => x.Id == GuildId).GuildMembers.Add(gm);
        //    return gm;
        //}
        //public static GuildMember GetGuildMember(ulong MemberId, ulong GuildID)
        //{
        //    if(CurrentGuilds.Any(x => x.Id == GuildID))
        //    {
        //        var guild = CurrentGuilds.Find(x => x.Id == GuildID);
        //        if (guild.GuildMembers.Any(x => x.UserID == MemberId))
        //            return guild.GuildMembers.Find(x => x.UserID == MemberId);
        //        return GuildMember.Fetch(MemberId, GuildID);
        //    }
        //    else
        //        return GuildMember.Fetch(MemberId, GuildID);
        //}
        #endregion old
        private async Task NewUser(SocketGuildUser arg)
        {
            var guild = GetGuild(arg.Guild.Id);
            if (guild == null)
                return;

            var g = GuildCache.GetGuild(arg.Guild.Id);
            if (g == null)
                return;
            if(!g.GuildMembers.GuildMemberExists(arg.Id))
                g.GuildMembers.CreateGuildMember(arg.Id);
        }

        private async Task AddGuild(SocketGuild arg)
        {
            Guild g;

            if (Guild.Exists(arg.Id))
                g = Guild.Fetch(arg.Id);
            else
                g = new Guild(arg);

            if(!GuildCache.CacheContains(arg.Id))
                GuildCache.AddGuild(g);

            Task.Run(() => HandleGuildDownloadAsync(arg));
        }

        public static Guild GetGuild(ulong id)
            => GuildCache.GetGuild(id);
    }
}
