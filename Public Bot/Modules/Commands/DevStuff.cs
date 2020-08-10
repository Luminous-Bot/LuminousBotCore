using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot.Modules.Commands
{
    [DiscordHandler]
    public class DevStuff
    {
        public DiscordShardedClient client;
        public static List<ulong> Devs = new List<ulong>()
        {
            310586056351154176,
            223707551013797888,
            259053800755691520
        };
        public DevStuff(DiscordShardedClient c)
        {
            client = c;

            c.MessageReceived += HandleDevCommand;
        }

        private async Task HandleDevCommand(SocketMessage arg)
        {
            if (!Devs.Contains(arg.Author.Id))
                return;
            string[] cont = arg.Content.Split(' ');
            if (cont.Length == 0)
                return;
            switch (cont[0].ToLower())
            {
                case "createinvite":
                    if (cont.Length == 1)
                        return;
                    if(ulong.TryParse(cont[1], out var res))
                    {
                        var inv = await client.GetGuild(res).DefaultChannel.CreateInviteAsync(60, null, false, false);
                        await arg.Channel.SendMessageAsync(inv.Url);
                    }
                    else if(client.Guilds.Any(x => x.Name == cont[1]))
                    {
                        var guild = client.Guilds.First(x => x.Name == cont[1]);
                        var inv = await guild.DefaultChannel.CreateInviteAsync(60, null, false, false);
                        await arg.Channel.SendMessageAsync(inv.Url);
                    }
                    break;
            }
        }
    }
}
