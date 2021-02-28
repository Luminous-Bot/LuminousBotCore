using Discord;
using Discord.WebSocket;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static Public_Bot.Modules.Handlers.LevelHandler;
using Color = Discord.Color;
namespace Public_Bot.Modules.Commands.Level_Commands
{
    [DiscordCommandClass("🧪 Levels 🧪", "Add ranks and leaderboards for your server with Levels!")]
    public class Givexp : CommandModuleBase
    {
        [DiscordCommand("givexp", RequiredPermission = true, description = "Gives a user XP", commandHelp = "Usage - `(PREFIX)givexp <user> <amount>")]
        public async Task gxp(params string[] args)
        {
            if (args.Length == 0)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Who? and how much?",
                    Description = "You didn't provide any arguments!",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            var user = await GetUser(args[0]);
            if (user == null)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "That user is invalid",
                    Description = $"The user \"{args[0]}\" is invalid!",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            if (args.Length == 1)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "How much XP?",
                    Description = $"How much XP do you want to give {user.Mention}? use `{GuildSettings.Prefix}givexp <@user> <amount>`",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            if (args.Length == 2)
            {
                if (uint.TryParse(args[1], out var res))
                {
                    var gl = GuildLeaderboards.Get(Context.Guild.Id);
                    LevelUser lusr = null;
                    if (gl.CurrentUsers.LevelUserExists(user.Id))
                    {
                        lusr = gl.CurrentUsers.GetLevelUser(user.Id);
                        lusr.CurrentXP += res;
                        //The below line will ensure he reaches maxlevel possible if given enough xp
                        LevelUpUser(lusr);
                    }
                    else
                    {
                        lusr = new LevelUser(user);
                        lusr.CurrentXP += res;
                        gl.CurrentUsers.AddLevelUser(lusr);
                        //As he's already been added he can now be levelled up.
                        LevelUpUser(lusr);
                    }
                    lusr.Save();
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Success!",
                        Description = $"{lusr.Username}'s XP is now set to {res}!",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                }
                else
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Invalid Value",
                        Description = $"Please provide a __positive whole__ number!",
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build());
                    //gl.SaveCurrent();
                    return;
                }
            }
        }
    }
}