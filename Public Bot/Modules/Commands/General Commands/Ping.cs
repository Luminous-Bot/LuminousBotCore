using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Color = Discord.Color;
namespace Public_Bot.Modules.Commands.General_Commands
{
    [DiscordCommandClass("👨🏼‍💻 General 👨🏼‍💻", "General bot commands for everyone!")]
    public class Ping : CommandModuleBase
    {
        [DiscordCommand("ping", description = "Gets the bot's ping to Discord", commandHelp = "Usage `(PREFIX)ping`")]
        public async Task ping()
        {
            var msg = await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
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
                              $"```Gateway:     {this.Context.Client.Latency}ms\n" +
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