using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot.Modules.Handlers
{
    [DiscordHandler]
    public class AutoModerationHandler
    {
        public static DiscordShardedClient client { get; set; }
        public AutoModerationHandler(DiscordShardedClient c)
        {
            client = c;
            client.MessageReceived += AutomodReceived;
        }

        public async Task AutomodReceived(SocketMessage arg)
        {
            if (arg == null) return;
            var context = new ShardedCommandContext(client, arg as SocketUserMessage);
            try
            {
                var x = GuildSettings.Get(context.Guild.Id).autoMod.AutoModeration(context);
                if (x == null) { return; }
                await x;
            }
            catch(NullReferenceException) { }
        }
    }
}
