using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot.Modules.Commands
{
    [DiscordCommandClass("⚙️ Settings ⚙️", "Change how this bot works in your server!")]
    class SettingsCommands : CommandModuleBase
    {
        [DiscordCommand("modules",
            commandHelp = "`(PREFIX)modules <enable/disable/list> <modulename>`",
            description = "Enables or disables a module",
            RequiredPermission = true
            )]
        public async Task Modules(params string[] args)
        {
            await Module(args);
        }
        [DiscordCommand("module",
            commandHelp = "`(PREFIX)module <enable/disable/list> <modulename>`",
            description = "Enables or disables a module",
            RequiredPermission = true
            )]
        public async Task Module(params string[] args)
        {
            if(args.Length == 0)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "What do you want to do?",
                    Description = $"Please use the format `{GuildSettings.Prefix}modules <enable/disable/list> <modulename>",
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
                        Description = $"Please use the format `{GuildSettings.Prefix}modules <enable/disable/list> <modulename>",
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
                    if(GuildSettings.ModulesSettings.Keys.Any(x => x.ToLower().Contains("general")))
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = $"Why?",
                            Description = $"You cant disable the General Commands module :/",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if (GuildSettings.ModulesSettings.Keys.Any(x => x.ToLower().Contains("setting")))
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
        [DiscordCommand("setmutedrole", RequiredPermission = true)]
        public async Task setmrole(string t)
        {
            var role = GetRole(t);
            if(role == null)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Invalid Role!",
                    Description = $"The role \"{t}\" is invalid!",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            GuildSettings.MutedRoleID = role.Id;
            GuildSettings.SaveGuildSettings();
            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
            {
                Title = "The muted role has been set!",
                Color = Color.Green,
                Description = $"The role {role.Mention} will be given when someone does {GuildSettings.Prefix}mute"
            }.WithCurrentTimestamp().Build());
        }
        [DiscordCommand("createmutedrole", RequiredPermission = true)]
        public async Task cmr(params string[] args)
        {
            if (args.Length == 0)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "**Are you sure?**",
                    Description = $"This will create a role and modify each text channel to make that role unable to text. this will take some time.\n\n**if you have a muted role already** please add it by typing `{GuildSettings.Prefix}setmutedrole <@role>`\n\nOtherwise **Type `{GuildSettings.Prefix}createmutedrole confirm` to continue**",
                    Color = Color.Blue,
                    Timestamp = DateTime.Now
                }.Build());
                return;
            }
            else if (args[0] == "confirm")
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "**We're on it!**",
                    Description = $"We are setting up the Muted Role, depending on how many channels you have this will take some time",
                    Color = Color.Blue,
                    Timestamp = DateTime.Now
                }.Build());
                MuteHandler.SetupMutedRole(Context.Guild, Context.Channel.Id, Context.User.Id);
            }
        }
        [DiscordCommand("permissions", RequiredPermission = true, commandHelp = "`(PREFIX)permissions", description = "Lists all roles with elevated permissions for this bot")]
        public async Task permission()
        {
            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
            {
                Title = "Permissions!",
                Description = $"here are all the roles with permissions\n\n<@&{string.Join(">\n<@&", GuildSettings.PermissionRoles)}>\n\nTo add one use `{GuildSettings.Prefix}addpermission <@role>`\nTo remove one use `{GuildSettings.Prefix}removepermission <@role>`",
                Color = Color.Green,
            }.WithCurrentTimestamp().Build());
        }
        [DiscordCommand("addpermission", RequiredPermission = true, commandHelp = "`(PREFIX)addpermission <@role>`", description = "Adds a role to the permission list")]
        public async Task addmodrole(string r)
        {
            var role = GetRole(r);
            try
            {
                CommandHandler.CurrentGuildSettings.Find(x => x.GuildID == Context.Guild.Id).AddRole(role);
            }
            catch
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "That role is already added!",
                    Description = "The roles is already in the permission list!",
                    Color = Color.Green,
                }.WithCurrentTimestamp().Build());
                return;
            }
            GuildSettings.SaveGuildSettings();
            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
            {
                Title = "Added and Saved!",
                Description = $"Added {role.Mention} to the Permissions list. here are all the roles with permissions\n\n<@&{string.Join(">\n<@&", GuildSettings.PermissionRoles)}>\n\nTo remove one use `{GuildSettings.Prefix}removepermission <@role>`",
                Color = Color.Green,
            }.WithCurrentTimestamp().Build());
        }
        [DiscordCommand("removepermission", RequiredPermission = true, commandHelp = "`(PREFIX)removepermission <@role>`", description = "Removes a role from the permission list")]
        public async Task removemodrole(string r)
        {
            var role = GetRole(r);
            try
            {
                CommandHandler.CurrentGuildSettings.Find(x => x.GuildID == Context.Guild.Id).RemoveRole(role);
            }
            catch
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "That role isn't added!",
                    Description = "The roles is not in the permission list!",
                    Color = Color.Green,
                }.WithCurrentTimestamp().Build());
                return;
            }
            GuildSettings.SaveGuildSettings();
            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
            {
                Title = "Removed and Saved!",
                Description = $"Removed {role.Mention} to the Permissions list. here are all the roles with permissions\n\n<@&{string.Join(">\n<@&", GuildSettings.PermissionRoles)}>\n\nTo add one use `{GuildSettings.Prefix}addpermission <@role>`",
                Color = Color.Green,
            }.WithCurrentTimestamp().Build());
        }
        //[DiscordCommand("leveling", RequiredPermission = true)]
        //public async Task leveling(string param)
        //{
        //    bool res = true;
        //    switch (param.ToLower())
        //    {
        //        case "disable":
        //            res = false;
        //            break;
        //        case "off":
        //            res = false;
        //            break;
        //        case "false":
        //            res = false;
        //            break;
        //        case "enable":
        //            res = true;
        //            break;
        //        case "on":
        //            res = true;
        //            break;
        //        case "true":
        //            res = true;
        //            break;
        //        default:
        //            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
        //            {
        //                Title = "Sorry I don't understand",
        //                Description = "please use one of these to turn on or off the leveling system",
        //                Fields = new List<EmbedFieldBuilder>()
        //                {
        //                    new EmbedFieldBuilder()
        //                    {
        //                        Name = "**__Turn on leveling__**",
        //                        Value = "```enable\non\ntrue```",
        //                        IsInline = true
        //                    },
        //                    new EmbedFieldBuilder()
        //                    {
        //                        Name = "**__Turn off leveling__**",
        //                        Value = "```disable\noff\nfalse```",
        //                        IsInline = true
        //                    }
        //                },
        //                Color = Color.Orange
        //            }.WithCurrentTimestamp().Build());
        //            return;
        //    }
        //    var inx = CommandHandler.CurrentGuildSettings.IndexOf(GuildSettings);
        //    CommandHandler.CurrentGuildSettings[inx].Leveling = res;
        //    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
        //    {
        //        Title = "Success!",
        //        Description = $"Leveling is now {(res == true ? "Enabled" : "Disabled")}!",
        //        Color = Color.Green
        //    }.WithCurrentTimestamp().Build());
        //}
        //[DiscordCommand("rolecard")]
        //public async Task rolecard(params string[] args)
        //{
        //    if(args.Length == 0)
        //    {
        //        //TODO:
        //    }
        //}
        [DiscordCommand("prefix", RequiredPermission = true, description = "Changes the prefix of the Bot", commandHelp = "Usage - `(PREFIX)prefix <prefix>`")]
        public async Task prefix(string prefix)
        {
            GuildSettings.Prefix = prefix;
            GuildSettings.SaveGuildSettings();
            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
            {
                Title = "Success!",
                Description  = $"The prefix is now `{prefix}`!",
                Color = Color.Green
            }.WithCurrentTimestamp().Build());
        }
        [DiscordCommand("logs", RequiredPermission = true, commandHelp = "Usage - `(PREFIX)logs channel <channel>`, `(PREFIX)logs on/off`", description = "Set a channel to log to and enable or disable logging to a channel.")]
        public async Task logs(params string[] args)
        {
            if(args.Length == 0)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "You didn't provide any arguments!",
                    Description = $"Please see `{GuildSettings.Prefix}help logs` for how to use this command",
                    Color = Color.Red
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
