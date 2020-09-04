using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Public_Bot.Modules.Handlers.MuteHandler;
namespace Public_Bot.Modules.Commands.Mod_Commands
{
    [DiscordCommandClass("🔨 Mod Commands 🔨", "Make your staff team more efficient with this module, you can keep track of user infractions and keep your server in order!")]
    public class Slowmode : CommandModuleBase
    {
        [GuildPermissions(GuildPermission.ManageChannels)]
        [DiscordCommand("slowmode", RequiredPermission = true, commandHelp = "Usage: `(PREFIX)slowmode #general 10`, `(PREFIX)slowmode #general 1m`")]
        public async Task slowmode(params string[] args)
        {
            if (args.Length == 0)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "How much slowmode?",
                    Description = "Provide some arguments!",
                    Color = Color.Orange,
                }.WithCurrentTimestamp().Build());
                return;

            }
            if (uint.TryParse(args[0], out var t))
            {
                var chan = (SocketTextChannel)Context.Channel;
                try
                {
                    await chan.ModifyAsync(x => x.SlowModeInterval = (int)t);
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Success!",
                        Description = $"{Context.User.Mention} set the slowmode of <#{Context.Channel.Id}> to {t} Seconds!",
                        Color = Color.Green,
                    }.WithCurrentTimestamp().Build());
                    return;
                }
                catch (Exception ex)
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "That didn't work!",
                        Description = $"Here's why: {ex.Message}",
                        Color = Color.Red,
                    }.WithCurrentTimestamp().Build());
                    return;
                }
            }
            else
            {
                //check timespan
                var regex = new Regex(@"^(\d*)([a-z])$");
                if (regex.IsMatch(args[0].ToLower()))
                {
                    var match = regex.Match(args[0].ToLower());
                    TimeSpan timespan;
                    switch (match.Groups[2].Value)
                    {
                        case "m":
                            timespan = TimeSpan.FromMinutes(int.Parse(match.Groups[1].Value));
                            break;
                        case "h":
                            timespan = TimeSpan.FromHours(int.Parse(match.Groups[1].Value));
                            break;
                        case "s":
                            timespan = TimeSpan.FromSeconds(int.Parse(match.Groups[1].Value));
                            break;
                        default:
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Invalid format",
                                Description = $"The format of `{match.Groups[2].Value}` is invalid, please use either `m` `h` `s` (minutes, hours, seconds)",
                                Color = Color.Orange,
                            }.WithCurrentTimestamp().Build());
                            return;
                    }
                    var chan = (SocketTextChannel)Context.Channel;
                    try
                    {
                        await chan.ModifyAsync(x => x.SlowModeInterval = (int)timespan.TotalSeconds);
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Success!",
                            Description = $"{Context.User.Mention} set the slowmode of <#{Context.Channel.Id}> to {timespan.TotalSeconds} Seconds!",
                            Color = Color.Green,
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    catch (Exception ex)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "That didn't work!",
                            Description = $"Here's why: {ex.Message}",
                            Color = Color.Red,
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                }
                else
                {
                    var chan = (SocketTextChannel)Context.Channel;
                    if (chan.SlowModeInterval > 0)
                    {
                        await chan.ModifyAsync(x => x.SlowModeInterval = 0);
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Success!",
                            Description = $"{Context.User.Mention} turned off slowmode for <#{Context.Channel.Id}>",
                            Color = Color.Green,
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Invalid Parameters!",
                            Description = $"Please see `{GuildSettings.Prefix}help slowmode`",
                            Color = Color.Orange,
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                }
            }
        }
    }
}