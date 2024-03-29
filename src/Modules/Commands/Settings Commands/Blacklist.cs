﻿using System;
using System.Collections.Generic;
using System.Linq;
using Discord;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Public_Bot.Modules.Commands.Settings_Commands
{
    // TODO: Give Command Class a Module
    [DiscordCommandClass("⚙️ Settings ⚙️", "Change how this bot works in your server!")]
    public class Blacklist : CommandModuleBase
    {
        [DiscordCommand("blacklist",
            // TODO: Change Required permission if need be
            RequiredPermission = true,
            // TODO: Fill out description
            description = "Blacklist channels so commands cant be used in them",
            // TODO: Change help message if need be
            commandHelp = "```\n" +
                          "(PREFIX)blacklist\n" +
                          "(PREFIX)blacklist list" +
                          "(PREFIX)blacklist add <#channel>" +
                          "(PREFIX)blacklist remove <#channel>" +
                          "```"
        )]
        public async Task Blacklist_Command(params string[] args)
        {
            if(args.Length == 0)
            {
                // List the guilds blacklisted channels
                
                if (GuildSettings.BlacklistedChannels.Any())
                {
                    var chans = Context.Guild.TextChannels.Where(x => GuildSettings.BlacklistedChannels.Contains(x.Id));

                    await Context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    {
                        Title = "Channel Blacklist",
                        Description = $"Here are the channels that are blacklist from commands:\n" +
                                      $"```#{string.Join("\n#", chans)}\n```" +
                                      $"You can add a channel where commands can't be use with `{GuildSettings.Prefix}blacklist add <#channel>`\n" +
                                      $"You can remove a blacklisted channel with `{GuildSettings.Prefix}blacklist remove <#channel>`.",
                        Color = Blurple
                    }.WithCurrentTimestamp().Build());
                }
                else
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Channel Blacklist",
                        Description = $"You dont have any channels blacklisted from commands!\n" +
                                      $"You can add a channel where commands can't be use with `{GuildSettings.Prefix}blacklist add <#channel>`\n" +
                                      $"You can remove a blacklisted channel with `{GuildSettings.Prefix}blacklist remove <#channel>`.",
                        Color = Blurple
                    }.WithCurrentTimestamp().Build());
                }

                return;
            }

            switch (args[0].ToLower())
            {
                case "list":
                    // List the guilds blacklisted channels

                    if (GuildSettings.BlacklistedChannels.Any())
                    {
                        var chans = Context.Guild.TextChannels.Where(x => GuildSettings.BlacklistedChannels.Contains(x.Id));

                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Channel Blacklist",
                            Description = $"Here are the channels that are blacklist from commands:\n" +
                                          $"```#{string.Join("\n#", chans)}\n```" +
                                          $"You can add a channel where commands can't be use with `{GuildSettings.Prefix}blacklist add <#channel>`\n" +
                                          $"You can remove a blacklisted channel with `{GuildSettings.Prefix}blacklist remove <#channel>`.",
                            Color = Blurple
                        }.WithCurrentTimestamp().Build());
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Channel Blacklist",
                            Description = $"You dont have any channels blacklisted from commands!\n" +
                                          $"You can add a channel where commands can't be use with `{GuildSettings.Prefix}blacklist add <#channel>`\n" +
                                          $"You can remove a blacklisted channel with `{GuildSettings.Prefix}blacklist remove <#channel>`.",
                            Color = Blurple
                        }.WithCurrentTimestamp().Build());
                    }

                    return;

                case "add":
                    // Add a channel to the blacklist

                    if(args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "What Channel?",
                            Description = "Please provide a channel you want to blacklist!",
                            Color = Color.Orange
                        }.WithCurrentTimestamp().Build());
                        return;
                    }

                    List<SocketGuildChannel> Added = new List<SocketGuildChannel>();
                    List<string> Failed = new List<string>();

                    foreach(var arg in args.Skip(1))
                    {
                        var chan = GetChannel(arg);

                        if (chan == null)
                        {
                            Failed.Add(arg);
                            continue;
                        }

                        if (chan.GetType() == typeof(SocketVoiceChannel))
                        {
                            Failed.Add(arg);
                            continue;
                        }

                        GuildSettings.BlacklistedChannels.Add(chan.Id);
                        GuildSettings.SaveGuildSettings();

                        Added.Add(chan);
                    }

                    var addedFields = new EmbedFieldBuilder()
                    {
                        Name = "Added",
                        Value = "```\n"
                    };

                    foreach (var item in Added)
                        addedFields.Value += $"#{item.Name}\n";
                    addedFields.Value += "```";

                    var failedFields = new EmbedFieldBuilder()
                    {
                        Name = "Failed",
                        Value = "```\n"
                    };

                    foreach (var item in Failed)
                        failedFields.Value += $"{item}\n";

                    failedFields.Value += "```";

                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = Added.Any() ? "Success!" : "Failed",
                        Color = Added.Any() ? Color.Green : Color.Red,
                        Fields = new List<EmbedFieldBuilder>()
                        {
                            Added.Any() ? addedFields : null,
                            Failed.Any() ? failedFields : null
                        },
                        
                    }.WithCurrentTimestamp().Build());

                    return;

                case "remove":
                    // Remove a channel from the blacklist

                    if (args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "What Channel?",
                            Description = "Please provide a channel you want to remove from blacklist!",
                            Color = Color.Orange
                        }.WithCurrentTimestamp().Build());
                        return;
                    }

                    var channel = GetChannel(args[1]);

                    if (channel == null)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Invalid Channel!",
                            Description = "The channel you provided was Invalid!",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }

                    if (!GuildSettings.BlacklistedChannels.Contains(channel.Id))
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Channel isn't blacklisted!",
                            Description = $"`{channel.Name}` isn't in the blacklist! We cant remove something that's not there!",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                    }

                    GuildSettings.BlacklistedChannels.Remove(channel.Id);
                    GuildSettings.SaveGuildSettings();

                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Success",
                        Description = $"Removed <#{channel.Id}> from the blacklist!",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());

                    return;
            }
        }
    }
}
