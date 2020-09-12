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
    public class Module : CommandModuleBase
    {
        [Alt("modules")]
        [DiscordCommand("module",
            commandHelp = "`(PREFIX)module <enable/disable/list> <modulename>`",
            description = "Enables or disables a module",
            RequiredPermission = true
            )]
        public async Task module(params string[] args)
        {
            if(args.Length == 0)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "What do you want to do?",
                    Description = $"Please use the format `{GuildSettings.Prefix}modules <enable/disable/list> <modulename>`",
                    Color = Color.Orange
                }.WithCurrentTimestamp().Build());
                return;
            }
            if(args.Length == 1)
            {
                if (args[0].ToLower() == "list")
                {
                    var mods = CustomCommandService.Modules;
                    EmbedBuilder b = new EmbedBuilder()
                    {
                        Title = "**Modules**",
                        Color = Color.Blue,
                        Timestamp = DateTime.Now,
                        Description = "Here are the current modules"
                    };
                    int splt = 3;
                    foreach (var item in mods)
                    {
                        b.Description += $"\n\n**{item.Key}**\n{item.Value}\nEnabled? {GuildSettings.ModulesSettings[item.Key]}";
                    }
                    await Context.Channel.SendMessageAsync("", false, b.Build());
                    return;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "What do you want to do?",
                        Description = $"Please use the format `{GuildSettings.Prefix}modules <enable/disable/list> <modulename>`",
                        Color = Color.Orange
                    }.WithCurrentTimestamp().Build());
                    return;
                }
            }
            if(args.Length >= 2)
            {
                var moduleName = string.Join(' ', args.Skip(1)).ToLower();
                if (!GuildSettings.ModulesSettings.Keys.Any(x => x.ToLower().Contains(moduleName)))
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = $"What..?",
                        Description = $"We couldn't find the module \"{moduleName}\", do `{GuildSettings.Prefix}modules list` to view all the modules",
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build());
                    return;
                }
                var setting = GuildSettings.ModulesSettings.First(x => x.Key.ToLower().Contains(moduleName));

                if (setting.Key.ToLower().Contains("mod commands"))
                {
                    if(!Context.Guild.CurrentUser.GuildPermissions.BanMembers || !Context.Guild.CurrentUser.GuildPermissions.KickMembers || !Context.Guild.CurrentUser.GuildPermissions.ManageRoles || !Context.Guild.CurrentUser.GuildPermissions.ManageMessages)
                    {
                        var prms = Context.Guild.CurrentUser.GuildPermissions;
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = $"The bot needs more permission",
                            Description = $"Please make sure the bot has these permissions!",
                            Fields = new List<EmbedFieldBuilder>()
                            {
                                new EmbedFieldBuilder()
                                {
                                    Name = "Required Permissions",
                                    Value = $"```Ban Members:     {(prms.BanMembers ? "✅" : "❌")}\n" +
                                            $"Kick Members:    {(prms.KickMembers ? "✅" : "❌")}\n" +
                                            $"Manage Roles:    {(prms.ManageRoles ? "✅" : "❌")}\n" +
                                            $"Manage Messages: {(prms.ManageMessages ? "✅" : "❌")}\n```"
                                }
                            },
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                }
                if (setting.Key.ToLower().Contains("levels"))
                {
                    if (!Context.Guild.CurrentUser.GuildPermissions.ManageRoles)
                    {
                        var prms = Context.Guild.CurrentUser.GuildPermissions;
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = $"The Bot needs more Permissions",
                            Description = $"Please make sure the bot has these permissions!",
                            Fields = new List<EmbedFieldBuilder>()
                            {
                                new EmbedFieldBuilder()
                                {
                                    Name = "Required Permissions",
                                    Value = //$"Ban Members:     {(prms.BanMembers ? "✅" : "❌")}\n" +
                                            //$"Kick Members:    {(prms.KickMembers ? "✅" : "❌")}\n" +
                                            $"```Manage Roles: {(prms.ManageRoles ? "✅" : "❌")}\n```"
                                            //$"Manage Messages: {(prms.ManageMessages ? "✅" : "❌")}\n"
                                }
                            },
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                }

                if (args[0].ToLower() == "enable")
                {
                    if(GuildSettings.ModulesSettings.Keys.Any(x => x.ToLower().Contains(moduleName)))
                    {
                        if(!setting.Value)
                        {
                            GuildSettings.ModulesSettings[setting.Key] = true;
                            GuildSettings.SaveGuildSettings();
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = $"Success!",
                                Description = $"{setting.Key} is now Enabled",
                                Color = Color.Green
                            }.WithCurrentTimestamp().Build());

                            return;
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = $"{setting.Key} is already Enabled",
                                Description = $"{setting.Key} is already Enabled, if you want to see the current modules do `{GuildSettings.Prefix}modules list`",
                                Color = Color.Orange
                            }.WithCurrentTimestamp().Build());
                            return;
                        }
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = $"What..?",
                            Description = $"We couldn't find the module \"{moduleName}\", do `{GuildSettings.Prefix}modules list` to view all the modules",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                }
                if (args[0].ToLower() == "disable")
                {
                    if (setting.Key == "👨🏼‍💻 General 👨🏼‍💻")
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = $"Why?",
                            Description = $"You cant disable the General Commands module :/",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if (setting.Key == "⚙️ Settings ⚙️")
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = $"Why?",
                            Description = $"You cant disable the Settings Commands module :/",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if (GuildSettings.ModulesSettings.Keys.Any(x => x.ToLower().Contains(moduleName)))
                    {
                        if (setting.Value)
                        {
                            GuildSettings.ModulesSettings[setting.Key] = false;
                            GuildSettings.SaveGuildSettings();
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = $"Success!",
                                Description = $"{setting.Key} is now Disabled",
                                Color = Color.Green
                            }.WithCurrentTimestamp().Build());

                            return;
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = $"{setting.Key} is already Disabled",
                                Description = $"{setting.Key} is already Disabled, if you want to see the current modules do `{GuildSettings.Prefix}modules list`",
                                Color = Color.Orange
                            }.WithCurrentTimestamp().Build());
                            return;
                        }
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = $"What..?",
                            Description = $"We couldn't find the module \"{moduleName}\", do `{GuildSettings.Prefix}modules list` to view all the modules",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                }
            }
        }
    }
}