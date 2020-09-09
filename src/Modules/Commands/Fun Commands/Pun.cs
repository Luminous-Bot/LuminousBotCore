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
    public class Pun : CommandModuleBase
    {
        static string[] puns = File.ReadAllLines($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}puns.txt");
        [DiscordCommand("pun", commandHelp = "(PREFIX)pun", description = "Sends a random pun from our collection!")]
        public async Task pun()
        {
            try
            {
                Random rand = new Random();
                int index = rand.Next(puns.Length);

                EmbedBuilder b = new EmbedBuilder()
                    .WithTitle($"{puns[index]}")
                    .WithColor(Blurple);

                await Context.Channel.SendMessageAsync("", false, b.Build());
            } catch
            {
                EmbedBuilder e = new EmbedBuilder()
                {
                    Title = "puns.txt doesn't exist, please contact quin#3017"
                };
                await Context.Channel.SendMessageAsync("", false, e.Build());
            }
        }
    }
}