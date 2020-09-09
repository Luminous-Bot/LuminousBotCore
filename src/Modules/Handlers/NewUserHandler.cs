using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot.Modules.Handlers
{
    [DiscordHandler]
    class NewUserHandler
    {
        public static DiscordShardedClient client;
        public NewUserHandler(DiscordShardedClient c)
        {
            client = c;
            client.UserJoined += Client_UserJoined;
        }

        private async Task Client_UserJoined(SocketGuildUser arg)
        {
            var gs = GuildSettings.Get(arg.Guild.Id);
            if(gs.NewMemberRole != 0)
            {
                var role = arg.Guild.GetRole(gs.NewMemberRole);
                if (role != null)
                    await arg.AddRoleAsync(role);
            }
        }
    }
}
