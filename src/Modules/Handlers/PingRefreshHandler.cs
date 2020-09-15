using System;
using System.Collections.Generic;
using System.Linq;
using Discord;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Rest;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;

namespace Public_Bot.Modules.Handlers
{
    [DiscordHandler]
    public class PingRefreshHandler
    {
        private DiscordShardedClient _client;

        public PingRefreshHandler(DiscordShardedClient client)
        {
            this._client = client;

            this._client.ReactionAdded += CheckRefresh;
        }

        private async Task CheckRefresh(Cacheable<IUserMessage, ulong> arg1, ISocketMessageChannel arg2, SocketReaction arg3)
        {
            var chan = (SocketTextChannel)arg2;

            if (chan == null)
                return;

            if (arg3.Emote.Name != "🔄")
                return;

            var usr = chan.GetUser(arg3.UserId);
            if (usr.IsBot)
                return;

            var msg = (RestUserMessage)await chan.GetRestMessage(arg1.Id);

            if (msg == null)
                return;

            if (msg.Author.Id != _client.CurrentUser.Id)
                return;

            if (msg.Embeds.Count == 0)
                return;

            var embed = msg.Embeds.First();

            if (embed.Title != "Discord Ping and Status")
                return;

            await msg.RemoveReactionAsync(arg3.Emote, usr);

            await msg.ModifyAsync(x => x.Embed = new EmbedBuilder()
            {
                Title = "Discord Ping and Status",
                Color = Color.Green,
                Description = $"You can view Discord's status page [here](https://status.discord.com/)\n" +
                              $"```\nGateway:     Fetching...\n" +
                              $"Api Latest:  Fetching...\n" +
                              $"Api Average: Fetching...```",
                Footer = new EmbedFooterBuilder()
                {
                    Text = "Last Updated: Fetching..."
                }
            }.Build());

            HttpClient c = new HttpClient();
            var resp = await c.GetAsync("https://discord.statuspage.io/metrics-display/ztt4777v23lf/day.json");
            var cont = await resp.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<PingGenerator.PingData.DiscordApiPing>(cont);
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var tm = epoch.AddSeconds(data.Metrics.First().Data.Last().Timestamp);
            var gfp = await PingGenerator.Generate(data);
            gfp.Save($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}Ping.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            await msg.ModifyAsync(x => x.Embed = new EmbedBuilder()
            {
                Title = "Discord Ping and Status",
                Color = Color.Green,
                Description = $"You can view Discord's status page [here](https://status.discord.com/)" +
                              $"```Gateway:     {_client.Latency}ms\n" +
                              $"Api Latest:  {data.Summary.Last}ms\n" +
                              $"Api Average: {data.Summary.Mean}ms```",
                Timestamp = tm,
                Footer = new EmbedFooterBuilder()
                {
                    Text = "Last Updated: "
                },
                Author = new EmbedAuthorBuilder()
                {
                    Name = "Powered by Hapsy",
                    IconUrl = "https://cdn.discordapp.com/avatars/223707551013797888/a_9f25c6c6374f4b57c5c9fb45baa5a8e8.png?size=256"
                },
                ImageUrl = PingGenerator.GetImageLink($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}Ping.jpg").GetAwaiter().GetResult()
            }.Build());
        }
    }
}
