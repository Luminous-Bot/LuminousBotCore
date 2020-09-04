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
    public class Givelevel : CommandModuleBase
    {
        [DiscordCommand("givelevel", RequiredPermission = true, commandHelp = "Usage `(PREFIX)givelevel <user> <ammount>`", description = "Gives a user Levels")]
        public async Task gl(params string[] args)
        {
            if (args.Length == 0)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Who? and how much?",
                    Description = "You didn't provied any arguments!",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            var user = GetUser(args[0]);
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
                    Title = "How many levels?",
                    Description = $"How many levels do you want to give {user.Mention}? use `{GuildSettings.Prefix}givelevel <@user> <ammount>`",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            if (args.Length == 2)
            {
                if (uint.TryParse(args[1], out var res))
                {
                    var gl = GuildLeaderboards.Get(Context.Guild.Id);
                    if (res > gl.Settings.MaxLevel)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Its over 9000!",
                            Description = $"Well it's not but its over your guilds max level. if you want to see the guilds level config please run `{GuildSettings.Prefix}levelsettings list`",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    LevelUser lusr = null;
                    if (gl.CurrentUsers.LevelUserExists(user.Id))
                    {
                        lusr = gl.CurrentUsers.GetLevelUser(user.Id);
                        if (res > lusr.CurrentLevel)
                        {
                            for (uint i = lusr.CurrentLevel; i != res; i++)
                            {
                                lusr.NextLevelXP *= gl.Settings.LevelMultiplier;
                            }
                        }
                        if (res < lusr.CurrentLevel)
                        {
                            for (uint i = lusr.CurrentLevel; i != res; i--)
                            {
                                lusr.NextLevelXP /= gl.Settings.LevelMultiplier;
                            }
                        }
                        lusr.CurrentLevel += res;
                        LevelUpUser(lusr);
                    }
                    else
                    {
                        lusr = new LevelUser(user);
                        if (res > lusr.CurrentLevel)
                        {
                            for (uint i = lusr.CurrentLevel; i != res; i++)
                            {
                                lusr.NextLevelXP *= gl.Settings.LevelMultiplier;
                            }
                        }
                        if (res < lusr.CurrentLevel)
                        {
                            for (uint i = lusr.CurrentLevel; i != res; i--)
                            {
                                lusr.NextLevelXP /= gl.Settings.LevelMultiplier;
                            }
                        }
                        lusr.CurrentLevel += res;
                        gl.CurrentUsers.AddLevelUser(lusr);
                    }
                    lusr.Save();
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Success!",
                        Description = $"The user {lusr.Username} level is now set to {res}!",
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