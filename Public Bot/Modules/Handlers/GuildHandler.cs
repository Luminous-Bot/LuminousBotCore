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
        public static List<Guild> CurrentGuilds = new List<Guild>();
        public GuildHandler(DiscordShardedClient c)
        {
            client = c;
            //string q = GraphQLParser.GenerateGQLQuery<Guild>("guilds");
            CurrentGuilds = new List<Guild>();
            client.JoinedGuild += AddGuild;
            client.UserJoined += NewUser;
            //client.ShardConnected += Init;
        }
        
        //bool isInitCompt = false;
        //private async Task Init(DiscordSocketClient arg)
        //{
        //    if (!isInitCompt)
        //    {
        //        //await CheckGuilds();
        //        CheckGuilds().GetAwaiter().GetResult();
        //        isInitCompt = true;
        //    }
        //}
        public static GuildMember GetOrCreateGuildMember(ulong MemberID, ulong GuildID)
        {
            if (!GuildMemberExists(MemberID, GuildID))
            {
                if (!GuildMember.Exists(MemberID, GuildID))
                    return CreateGuildMember(MemberID, GuildID);
                else
                {
                    var gm = GuildMember.Fetch(MemberID, GuildID);
                    AddGuildMember(gm);
                    return gm;
                }    
            }
            else
                return CurrentGuilds.Find(x => x.Id == GuildID).GuildMembers.Find(x => x.UserID == MemberID);
        }
        public static void AddGuildMember(GuildMember gm)
        {
            if(CurrentGuilds.Any(x => x.Id == gm.GuildID))
                CurrentGuilds.Find(x => x.Id == gm.GuildID).GuildMembers.Add(gm);
            else if(Guild.Exists(gm.GuildID))
            {
                var g = Guild.Fetch(gm.GuildID);
                if (!g.GuildMembers.Any(x => x.UserID == gm.UserID))
                    g.GuildMembers.Add(gm);
                CurrentGuilds.Add(g);
            }
        }
        public static bool GuildMemberExists(ulong MemberId, ulong GuildID)
        {
            if (!CurrentGuilds.Any(x => x.Id == GuildID))
                if (Guild.Exists(GuildID))
                {
                    var g = Guild.Fetch(GuildID);
                    CurrentGuilds.Add(g);
                    return g.GuildMembers.Any(x => x.UserID == MemberId);
                }
                else
                    return GuildMember.Exists(MemberId, GuildID);
            else
            {
                var gld = CurrentGuilds.Find(x => x.Id == GuildID);
                return gld.GuildMembers.Any(x => x.UserID == MemberId);
            }
        }
        public static GuildMember CreateGuildMember(ulong Idm, ulong GuildId)
        {
            var gld = client.GetGuild(GuildId);
            if (gld == null)
                return null;
            var usr = gld.GetUser(Idm);
            if (usr == null)
                return null;
            var gm = new GuildMember(usr);
            if (CurrentGuilds.Any(x => x.Id == GuildId))
                CurrentGuilds.Find(x => x.Id == GuildId).GuildMembers.Add(gm);
            else
            {
                if(Guild.Exists(GuildId))
                {
                    var g = Guild.Fetch(GuildId);
                    if (!g.GuildMembers.Any(x => x.UserID == Idm))
                        g.GuildMembers.Add(gm);
                }
            }
            return gm;
        }
        public static GuildMember GetGuildMember(ulong MemberId, ulong GuildID)
        {
            if(CurrentGuilds.Any(x => x.Id == GuildID))
            {
                var guild = CurrentGuilds.Find(x => x.Id == GuildID);
                if (guild.GuildMembers.Any(x => x.UserID == MemberId))
                    return guild.GuildMembers.Find(x => x.UserID == MemberId);
                else
                {
                    var m = GuildMember.Fetch(MemberId, GuildID);
                    guild.GuildMembers.Add(m);
                    return m;
                }
            }
            else
            {
                if (Guild.Exists(GuildID))
                {
                    var g = Guild.Fetch(GuildID);
                    if (g.GuildMembers.Any(x => x.UserID == MemberId))
                        return g.GuildMembers.Find(x => x.UserID == MemberId);
                    else
                        return null;
                }
                else
                    return null;
            }
        }
        private async Task NewUser(SocketGuildUser arg)
        {
            var guild = CurrentGuilds.Any(x => x.Id == arg.Guild.Id) ? CurrentGuilds.Find(x => x.Id == arg.Guild.Id) : Guild.Fetch(arg.Guild.Id);
            if (StateService.Exists<User>(GraphQLParser.GenerateGQLQuery<User>("user", new KeyValuePair<string, object>("id", arg.Id))))
            {
                //check if guildmember exists
                if (!StateService.Exists<GuildMember>(GraphQLParser.GenerateGQLQuery<GuildMember>("guildMember", new KeyValuePair<string, object>("UserID", arg.Id), new KeyValuePair<string, object>("GuildID", arg.Guild.Id))))
                {
                    var gm = new GuildMember(arg);
                    // (!gm.IsInServer)
                    guild.GuildMembers.Add(gm);
                }
                else
                {
                    var gm = GuildMember.Fetch(arg.Id, arg.Guild.Id);
                    //gm = await gm.UpdateIsInServer(true);
                    guild.GuildMembers.Add(gm);
                }
            }
            else
            {
                //create user
                var user = new User(arg);
                //create guildmember
                var gm = new GuildMember(arg) { User = user};
                guild.GuildMembers.Add(gm);
            }
        }

        //public async Task CheckGuilds()
        //{
        //    foreach(var guild in client.Guilds)
        //    {
        //        if(!CurrentGuilds.Any(x => x.Id == guild.Id))
        //        {
        //            await AddGuild(guild);
        //        }
        //    }
        //}

        private async Task AddGuild(SocketGuild arg)
        {
            Guild g;
            if (Guild.Exists(arg.Id))
                g = Guild.Fetch(arg.Id);
            else
                g = new Guild(arg);

            CurrentGuilds.Add(g);
            LevelHandler.GuildLevels.Add(g.Leaderboard);
        }
        public static void Addguild(SocketGuild arg)
        {
            var g = new Guild(arg);
            CurrentGuilds.Add(g);
            LevelHandler.GuildLevels.Add(g.Leaderboard);
        }

        public static Guild GetGuild(ulong id)
        {
            if (CurrentGuilds.Any(x => x.Id == id))
                return CurrentGuilds.Find(x => x.Id == id);
            else
            {
                var g = Guild.Fetch(id);
                if (g == null)
                {
                    Logger.Write("Holy shit they tried a command when there guild didn't exist god help us!", Logger.Severity.Critical);
                    return null;
                }
                else
                    return g;
            }
        }
    }
}
