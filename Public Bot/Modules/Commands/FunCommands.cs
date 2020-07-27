using Discord;
using Discord.WebSocket;
using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Public_Bot.Modules.Commands
{

    [DiscordCommandClass("🤽🏼 Fun 🤽🏼", "Commands to use to spice up your server.")]
    public class FunCommands : CommandModuleBase
    {
        // Variables to use later on
        int rot = 0;
        private readonly Random _random = new Random();

        [DiscordCommand("meme", commandHelp = "`(PREFIX)meme`", description = "Grabs a random meme from reddit")]
        public async Task ImageGen()
        {

            HttpClient client = new HttpClient();
            var request = await client.GetAsync("https://www.reddit.com/r/dankmemes.json");
            string response = await request.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Public_Bot.Modules.Handlers.RedditHandler>(response);
            Regex r = new Regex(@"https:\/\/i.redd.it\/(.*?)\.");
            var childs = data.Data.Children.Where(x => r.IsMatch(x.Data.Url.ToString())).ToList();
            Random rnd = new Random();
            var post = childs[rnd.Next(0, childs.Count())];

            EmbedBuilder b = new EmbedBuilder()
            {
                Title = "r/dankmemes",
                ImageUrl = post.Data.Url.ToString(),
                Footer = new EmbedFooterBuilder()
                {
                    Text = "u/" + post.Data.Author
                }
            };

            b.WithCurrentTimestamp();
            b.WithColor(Blurple);

            await Context.Channel.SendMessageAsync("", false, b.Build());

        }

        [DiscordCommand("coinflip", commandHelp = "`(PREFIX)coinflip`", description = "Flips a coin")]

        public async Task CoinFlip()
        {
            bool chancheck = Context.Guild.GetTextChannel(Context.Channel.Id).IsNsfw;
            var value = _random.Next(0, 2);
            EmbedBuilder coin = new EmbedBuilder()
                .WithColor(Blurple)
                .WithTitle("Coin Flip!");

            switch (value)
            {
                case 1:

                    coin.WithDescription("Heads!");

                    if (chancheck)
                    {
                        coin.WithImageUrl("https://cdn.hapsy.net/ee8f064c-e010-4699-8ccd-f68a30823da7");
                        break;
                    }

                    coin.WithImageUrl("https://realflipacoin.net/media/assets/coin-mid-0.png?a");
                    break;
                case 0:
                    coin.WithDescription("Tails!");
                    if (chancheck)
                    {
                        coin.WithImageUrl("https://cdn.hapsy.net/99420dee-1c24-41cd-a7af-b13c7a6f9528");
                        break;
                    }
                    coin.WithImageUrl("https://realflipacoin.net/media/assets/coin-mid-1.png?a");
                    break;
            }

            await Context.Channel.SendMessageAsync("", false, coin.Build());
        }
    }
}
