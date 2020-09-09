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
    public class _8ball : CommandModuleBase
    {
        [DiscordCommand("8ball", commandHelp = "`(PREFIX)8ball`", description = "Ask the all knowing 8-ball")]
        public async Task eightball(params string[] args)
        {
            var sb = new StringBuilder();

            var embed = new EmbedBuilder();

            var replies = new List<string>();

            //Possible Replies
            replies.Add("Yes");
            replies.Add("No");
            replies.Add("Lol idk");
            replies.Add("69% Chance");

            embed.WithColor(Blurple);
            embed.WithTitle("8-Ball");

            sb.AppendLine($"{Context.User.Username}");
            sb.AppendLine();

            if (args == null)
            {
                sb.AppendLine("Sorry, can't answer air");
            }
            else
            {
                var ans = replies[new Random().Next(replies.Count - 1)];

                sb.AppendLine($"Your question is **{string.Join(' ', args)}**");
                sb.AppendLine();
                sb.AppendLine($"The answer is **{ans}**");
            }

            embed.Description = sb.ToString();

            await Context.Channel.SendMessageAsync("", false, embed.Build());

        }
    }
}