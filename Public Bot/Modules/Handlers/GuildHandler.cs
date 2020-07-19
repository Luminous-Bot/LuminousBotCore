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
            client.JoinedGuild += AddGuild;
            client.UserJoined += NewUser;
            client.ShardConnected += Init;
            //load guilds
            string q = GraphQLParser.GenerateGQLQuery<Guild>("guilds");
            CurrentGuilds = StateService.Query<List<Guild>>(q);
        }
        bool isInitCompt = false;
        private async Task Init(DiscordSocketClient arg)
        {
            if (!isInitCompt)
            {
                await CheckGuilds();
                isInitCompt = true;
            }
        }
        public static GuildMember GetOrCreateGuildMember(ulong MemberID, ulong GuildID)
        {
            if (!GuildMemberExists(MemberID, GuildID))
            {
                if (!GuildMember.Exists(MemberID, GuildID))
                    return CreateGuildMember(MemberID, GuildID);
                else
                    return GuildMember.Fetch(MemberID, GuildID);
            }
            else
                return CurrentGuilds.Find(x => x.Id == GuildID).GuildMembers.Find(x => x.UserID == MemberID);
        }
        public static bool GuildMemberExists(ulong MemberId, ulong GuildID)
        {
            if (!CurrentGuilds.Any(x => x.Id == GuildID))
                return false;
            var gld = CurrentGuilds.Find(x => x.Id == GuildID);
            return gld.GuildMembers.Any(x => x.UserID == MemberId);
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
                Addguild(gld);
            return gm;
        }
        public static GuildMember GetGuildMember(ulong MemberId, ulong GuildID)
        {
            if(CurrentGuilds.Any(x => x.Id == GuildID))
            {
                var guild = CurrentGuilds.Find(x => x.Id == GuildID);
                if (guild.GuildMembers.Any(x => x.UserID == MemberId))
                    return guild.GuildMembers.Find(x => x.UserID == MemberId);
                return GuildMember.Fetch(MemberId, GuildID);
            }
            else
                return GuildMember.Fetch(MemberId, GuildID);
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

        public async Task CheckGuilds()
        {
            foreach(var guild in client.Guilds)
            {
                if(!CurrentGuilds.Any(x => x.Id == guild.Id))
                {
                    await AddGuild(guild);
                }
            }
        }

        private async Task AddGuild(SocketGuild arg)
        {
            var g = new Guild(arg);
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
                return null;
        }
    }
}
