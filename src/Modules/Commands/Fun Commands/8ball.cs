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
        private static string[] Answers = new string[]
        {
            "It is certain.",
            "It is decidedly so.",
            "Without a doubt.",
            "Yes – definitely.",
            "You may rely on it.",
            "As I see it, yes.",
            "Most likely.",
            "Outlook good.",
            "Yes.",
            "Signs point to yes.",
            "Reply hazy, try again.",
            "Ask again later.",
            "Better not tell you now.",
            "Cannot predict now.",
            "Concentrate and ask again.",
            "Don't count on it.",
            "My reply is no.",
            "My sources say no.",
            "Outlook not so good.",
            "Very doubtful.",
        };


        [DiscordCommand("8ball", commandHelp = "`(PREFIX)8ball`", description = "Ask the all knowing 8-ball")]
        public async Task eightball(params string[] args)
        {
            if(args.Length == 0)
            {
                // Error

                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "What sorry?",
                    Description = "The magic 8 ball can't give an answer to nothing..",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }

            string question = string.Join(" ", args);

            int seed = 0;

            foreach (char c in question)
                seed += c;

            var indx = new Random(seed).Next(Answers.Length);

            var av = Context.User.GetAvatarUrl();
            if (av == null)
                av = Context.User.GetDefaultAvatarUrl();

            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
            {
                Author = new EmbedAuthorBuilder()
                {
                    IconUrl = av,
                    Name = Context.User.Username,
                },
                Description = $"\"{question}\"\n\n**{Answers[indx]}**",
                Color = Blurple,
            }.WithCurrentTimestamp().Build());
        }
    }
}