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
    public class Oldest : CommandModuleBase
    {
        [DiscordCommand("oldest", commandHelp = "`(PREFIX)oldest`\n`(PREFIX)oldest <number_of_users>`", description = "Finds the x users eldest on Discord")]
        [Alt("eldest")]
        public async Task El(params string[] argz)
        {
            var test = 10;
            if (int.TryParse(argz.FirstOrDefault(), out int retest))
            {
                test = retest;
            }
            if (test >= Context.Guild.MemberCount)
            {
                await Context.Channel.SendMessageAsync("You dont even have so many users :rofl:");
                return;
            }
            var yus = Context.Guild.Users;
            string cty = "```";
            var tenYoungestUsers = yus.ToList();
            tenYoungestUsers.RemoveAll(x => x.IsBot);
            try
            {
                tenYoungestUsers.Sort((prev, next) => 1 / DateTimeOffset.Compare(prev.CreatedAt, next.CreatedAt));
            }
            catch
            {
                tenYoungestUsers.Sort((prev, next) => 0);
            }
            //tenYoungestUsers.Reverse();
            var current = tenYoungestUsers.GetRange(0, test);
            current.ForEach(x => cty += (x.Username + '\t' + $"{x.CreatedAt.Month}/{x.CreatedAt.Day}/{x.CreatedAt.Year}" + '\n'));
            cty += "```";
            var mmbed = new EmbedBuilder
            {
                Title = "Eldest Users!",
                Description = cty,
                Color = Blurple
            }.WithCurrentTimestamp().Build();
            await Context.Channel.SendMessageAsync("", false, mmbed);
        }
    }
}