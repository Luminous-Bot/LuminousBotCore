using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot.Modules.Handlers
{
    [DiscordHandler]
    public class UserCreatorHandler
    {
        public DiscordShardedClient client;
        public UserCreatorHandler(DiscordShardedClient c)
        {
            client = c;
            client.MessageReceived += CheckUser;
        }

        private async Task CheckUser(SocketMessage arg)
        {
            if (arg.Channel.GetType() != typeof(SocketGuildChannel))
                return;
            var channel = arg.Channel as SocketGuildChannel;
            var guild = channel.Guild;
            string q = $"{{ user(id: \"{arg.Author.Id}\"){{ Id }} }}";
            var usr = await StateHandler.Postgql<Users>(q);
            if(usr == null)
            {
                //create user
                string user = $"{{\"operationName\":\"createUser\",\"variables\":{{\"data\":{{\"Id\":\"{arg.Id}\",\"CurrentUsername\":\"{arg.ToString()}\",\"BarColor\":\"00ff00\",\"BackgroundColor\":\"282828\",\"Donator\":false,\"BackgroundUrl\":null}}}},\"query\":\"mutation createUser($data: CreateUserInput!) {{ createUser(data: $data) {{ Id }} }}\"}}";
                await StateHandler.Postgql(user);
                //create guildmember

            }
        }
        
    }
}
