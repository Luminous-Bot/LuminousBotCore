using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot.Modules.Handlers
{
    [DiscordHandler]
    public class UserHandler
    {
        public DiscordShardedClient client;
        public UserHandler(DiscordShardedClient c)
        {
            client = c;
        }
    }
}
