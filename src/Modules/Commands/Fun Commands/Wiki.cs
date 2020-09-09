using Discord;
using Discord.WebSocket;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Linq;
using Discord.Commands;
using WikiDotNet;
namespace Public_Bot.Modules.Commands.Fun_Commands
{
    [DiscordCommandClass("🤽🏼 Fun 🤽🏼", "Commands to use to spice up your server.")]
    public class Wiki : CommandModuleBase
    {
        [DiscordCommand("wiki", commandHelp = "(PREFIX)wiki <search>", description = "Searches wikipedia!")]
        public async Task SearchWikipedia(params string[] arg1)
        {
            if(arg1.Length == 0)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder() 
                {
                    Title = "No Arguments",
                    Description = $"What wiki do you want to search for? Please provide some arguments: `{GuildSettings.Prefix}wiki <wiki>`",
                    Color = Color.Orange
                }.Build());
                return;
            }

            var args = String.Join(' ', arg1);

            EmbedBuilder b = new EmbedBuilder()
                .WithFooter("Not what you wanted? Try searching without spaces.")
                .WithColor(Blurple);
            

            StringBuilder sb = new StringBuilder();

            WikiSearchResponse response = WikiSearcher.Search(args, new WikiSearchSettings
            {
                ResultLimit = 1
            });

            if (response.Query.SearchResults == null)
            {
                b.WithDescription("No results could be found :(");
                await Context.Message.Channel.SendMessageAsync("", false, b.Build());
                return;
            }
            //Extra protection I guess
            if (response.Query.SearchResults.Length == 0)
            {
                b.WithDescription("No results could be found :(");
                await Context.Message.Channel.SendMessageAsync("", false, b.Build());
                return;
            }
            foreach (WikiSearchResult result in response.Query.SearchResults)
            {
                string link =
                    $"{result.Preview}...\n\n";

                string title = $"{result.Title}";

                b.WithTitle(title);

                //There is a character limit of 2048, so lets make sure we don't hit that
                if (sb.Length >= 2048) continue;

                if (sb.Length + link.Length >= 2048) continue;

                sb.Append(link);

                b.WithUrl(result.ConstantUrl("en"));

            }

            b.WithDescription(sb.ToString());
            b.WithCurrentTimestamp();

            await Context.Channel.SendMessageAsync("", false, b.Build());
        }
    }
}