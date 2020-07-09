using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot.Modules.Handlers
{
    [DiscordHandler]
    class StatusHandler
    {
        public DiscordShardedClient client;
        public StatusHandler(DiscordShardedClient c)
        {
            client = c;
            c.ShardLatencyUpdated += C_ShardLatencyUpdated;
            c.SetStatusAsync(Discord.UserStatus.Online).GetAwaiter().GetResult();
            C_ShardLatencyUpdated(0, 0, null);
        }

        private async Task C_ShardLatencyUpdated(int arg1, int arg2, DiscordSocketClient arg3)
        {
            int alusr = 0;
            foreach (var g in client.Guilds)
                alusr += g.MemberCount;
            Logger.Write($"Count of {alusr}");
            string[] status = new string[]
            {
                $"Active in {client.Guilds.Count} unique servers!",
                $"Serving {alusr} Users in {client.Guilds.Count} Guilds",
                $"Executing commands..",
                $"Running the batch file",
                $"Handling exceptions",
                $"Talking to {alusr} different users!",
                $"Opening a new socket to the gateway",
                $"Searching for clide",
                $"Crying in binary",
                $"Admiring Amaribot",
                $"Watching Coding train",
                $"Serching for the singularity",
                $"Chilling with kazoo kid",
                $"Compiling the bee movie script",
                $"Fixing bugs",
                $"Programming somthing",
                $"Checking your messages for commands",
                $"Upgraded!",
                $"Some body once told me!",
                $"Xua hua piao piao (<3 -belle)",
                $"Watching memes",
                $"Writing my own compiler",
                $"Sneezing in brail",
                $"Understanding stupitity",
                $"Plotting the destruction of humanity",
                $"Tiktok is a social malware"
            };
            await client.SetGameAsync($"[0.2.9] - {status[new Random().Next(0, status.Length -1)]}", null, ActivityType.Playing);
        }
    }
}
