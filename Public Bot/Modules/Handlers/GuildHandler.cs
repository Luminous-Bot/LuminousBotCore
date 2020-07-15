using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
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
            //load guilds
            CurrentGuilds = StateService.Query<List<Guild>>(GraphQLParser.GenerateGQLQuery<Guild>("guilds"));
            CheckGuilds().GetAwaiter().GetResult();
        }

        private async Task NewUser(SocketGuildUser arg)
        {
            var guild = CurrentGuilds.Any(x => x.ID == arg.Guild.Id) ? CurrentGuilds.Find(x => x.ID == arg.Guild.Id) : Guild.Fetch(arg.Guild.Id);
            if (StateService.Exists<User>(GraphQLParser.GenerateGQLQuery<User>("user", new KeyValuePair<string, object>("id", arg.Id))))
            {
                //check if guildmember exists
                if (!StateService.Exists<GuildMember>(GraphQLParser.GenerateGQLQuery<GuildMember>("guildMember", new KeyValuePair<string, object>("UserID", arg.Id), new KeyValuePair<string, object>("GuildID", arg.Guild.Id))))
                {
                    var gm = new GuildMember(arg) { User = user };
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
                if(!CurrentGuilds.Any(x => x.ID == guild.Id))
                {
                    await AddGuild(guild);
                }
            }
        }

        private async Task AddGuild(SocketGuild arg)
        {
            CurrentGuilds.Add(new Guild(arg));
        }

        public static Guild GetGuild(ulong id)
        {
            if (CurrentGuilds.Any(x => x.ID == id))
                return CurrentGuilds.Find(x => x.ID == id);
            else
                return null;
        }
    }
}
