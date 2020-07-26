using Discord;
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

        int rot = 0;

        [DiscordCommand("meme", commandHelp = "`(PREFIX)meme`", description = "Grabs a random meme from reddit")]
        public async Task ImageGen()
        {

            HttpClient client = new HttpClient();
            var request = await client.GetAsync("https://www.reddit.com/r/dankmemes.json/new");
            string response = await request.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Public_Bot.Modules.Handlers.RedditHandler>(response);
            Regex r = new Regex(@"https:\/\/i.redd.it\/(.*?)\.");
            var childs = data.Data.Children.Where(x => r.IsMatch(x.Data.Url.ToString()));
            Random rnd = new Random();
            int count = childs.Count();
            if (rot >= count - 1)
                rot = 0;
            var post = childs.ToArray()[0];
            rot++;

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
    }
}
