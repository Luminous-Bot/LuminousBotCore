using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Timers;

namespace Public_Bot.Modules.Handlers
{
    [DiscordHandler]
    class MemberCountHandler
    {
        public DiscordShardedClient client;
        public static ulong TotalCount;
        internal static string Token = ConfigLoader.Token;
        public MemberCountHandler(DiscordShardedClient c)
        {
            //client = c;
            //Timer usercountTimer = new Timer()
            //{
            //    Interval = 60000,
            //    AutoReset = true,
            //};
            //usercountTimer.Elapsed += UsercountTimer_Elapsed;
            //usercountTimer.Start();
        }
        public class userCount
        {
            public ulong approximate_member_count { get; set; }
        }
        private async void UsercountTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //ulong final = 0;
            //HttpClient c = new HttpClient();
            //foreach(var guild in client.Guilds)
            //{
            //    //make url
            //    string url = $"https://discord.com/api/guilds/{guild.Id}?with_counts=true";
            //    c.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", $"Bot {Token}");
            //    var res = await c.GetAsync(url);
            //    string json = await res.Content.ReadAsStringAsync();
            //    userCount count = JsonConvert.DeserializeObject<userCount>(json);
            //    final += count.approximate_member_count;
            //}
            
            //Logger.Write($"Found {final} members in {client.Guilds.Count} guilds");
            //TotalCount = final;
        }
    }
}
