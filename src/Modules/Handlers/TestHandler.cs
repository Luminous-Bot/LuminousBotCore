using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot.Modules.Handlers
{
    [DiscordHandler]
    class TestHandler
    {
        public DiscordShardedClient client;
        public TestHandler(DiscordShardedClient c)
        {
            client = c;
            //Console.WriteLine("Handler loaded!");
        }
    }
}
