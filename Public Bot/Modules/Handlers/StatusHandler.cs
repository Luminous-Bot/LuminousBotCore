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
        public static string[] status = new string[]
            {
                $"Active in (GC) unique servers!",
                $"Serving (UC) Users in (GC) Guilds",
                $"Executing commands..",
                $"Running the batch file",
                $"Handling exceptions",
                $"Talking to (UC) different users!",
                $"Opening a new socket to the gateway",
                $"Searching for clide",
                $"Crying in binary",
                $"Admiring Amaribot",
                $"Watching Coding train",
                $"Searching for the singularity",
                $"Chilling with kazoo kid",
                $"Compiling the bee movie script",
                $"Fixing bugs",
                $"Programming something",
                $"Checking your messages for commands",
                $"Upgraded!",
                $"Somebody once told me.....",
                $"Xua hua piao piao (<3 -belle)",
                $"Watching memes",
                $"Writing my own compiler",
                $"Sneezing in braille",
                $"Understanding stupidity",
                $"Plotting the destruction of humanity",
                $"Tiktok is a social malware",
                $"GraphQL for da boi's",
                $"Star be lookin kinda thicc",
                $"⚡Powered by caffeine",
                $"Postman is your friend",
                $"Rickrollin' someone",
                $"Some get him some milk!",
                $"Databases are sick!",
                $"Beep boop?"
            };
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
            
            await client.SetGameAsync($"[0.5.1 | https://luminousbot.com] - {status[new Random().Next(0, status.Length)].Replace("(UC)", alusr.ToString()).Replace("(GC)", client.Guilds.Count.ToString())}", null, ActivityType.Playing);
        }
    }
}
