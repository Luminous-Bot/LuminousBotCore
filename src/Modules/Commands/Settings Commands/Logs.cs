using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
namespace Public_Bot.Modules.Commands.Settings_Commands
{

    [DiscordCommandClass("⚙️ Settings ⚙️", "Change how this bot works in your server!")]
    public class Logs : CommandModuleBase
    {
        [GuildPermissions(GuildPermission.ViewAuditLog)]
        [DiscordCommand("logs", RequiredPermission = true, commandHelp = "Usage - `(PREFIX)logs channel <channel>`, `(PREFIX)logs on/off`", description = "Set a channel to log to and enable or disable logging to a channel.")]
        public async Task logs(params string[] args)
        {
            if(args.Length == 0)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Logs",
                    Description = $"Here are the current log settings:\n\nEnabled?: {(GuildSettings.Logging ? "✅" : "❌")}\nChannel?: {(GuildSettings.LogChannel == 0 ? "❌" : $"<#{GuildSettings.LogChannel}>")}",
                    Color = Blurple
                }.WithCurrentTimestamp().Build());
                return;
            }
            if(args[0].ToLower() == "on")
            {
                GuildSettings.Logging = true;
                GuildSettings.SaveGuildSettings();
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Success!",
                    Description = $"Logging is now Enabled!",
                    Color = Color.Green
                }.WithCurrentTimestamp().Build());
                return;
            }
            if (args[0].ToLower() == "off")
            {
                GuildSettings.Logging = false;
                GuildSettings.SaveGuildSettings();
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Success!",
                    Description = $"Logging is now Disabled!",
                    Color = Color.Green
                }.WithCurrentTimestamp().Build());
                return;
            }
            if (args[0].ToLower() == "channel")
            {
                if(args.Length == 2)
                {
                    var chan = GetChannel(args[1]);
                    if(chan == null)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "That channel is invalid",
                            Description = $"The channel \"{args[1]}\" is invalid",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if(chan.GetType() != typeof(SocketTextChannel))
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "That channel isn't a Text Channel",
                            Description = $"Please provide a text channel",
                            Color = Color.Red,
                            Footer = new EmbedFooterBuilder()
                            {
                                Text = "Owo secrete footer text"
                            }
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    GuildSettings.LogChannel = chan.Id;
                    GuildSettings.Logging = true;
                    GuildSettings.SaveGuildSettings();
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Success!",
                        Description = $"The channel <#{chan.Id}> is now the log channel! We also turned on logging for you, if you wish to turn logging off you can do so by running `{GuildSettings.Prefix}logs off`",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    return;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "You didn't provide the correct arguments!",
                        Description = $"Please see `{GuildSettings.Prefix}help logs` for how to use this command",
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build());
                    return;
                }
            }
        }
    }
}