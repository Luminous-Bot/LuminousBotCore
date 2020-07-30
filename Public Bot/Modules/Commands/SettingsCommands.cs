using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Public_Bot.Modules.Commands
{
    [DiscordCommandClass("⚙️ Settings ⚙️", "Change how this bot works in your server!")]
    class SettingsCommands : CommandModuleBase
    {
        [DiscordCommand("welcomer",
            commandHelp = "`(PREFIX)welcomer`\n" +
                          "`(PREFIX)welcomer channel <#channel>`\n" +
                          "`(PREFIX)welcomer message <your welcome message for new users>`\n" +
                          "`(PREFIX)welcomer <enable/on>`\n" +
                          "`(PREFIX)welcomer <disable/off>`\n" +
                          "`(PREFIX)welcomer dm <true/false>`\n" +
                          "`(PREFIX)welcomer mentions <true/false>`",
            description = "Welcomes new users into your guild!", RequiredPermission = true)]
        public async Task welcomer(params string[] args)
        {
            var ws = GuildSettings.WelcomeCard;
            var welcomechan = Context.Guild.GetTextChannel(ws.WelcomeChannel);
            if (args.Length == 0)
            {
                //var img = WelcomeHandler.GenerateWelcomeImage(Context.User as SocketGuildUser, Context.Guild, GuildSettings.WelcomeCard);
                //img.Save($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}WelcomeCard.png", System.Drawing.Imaging.ImageFormat.Png);
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Author = new EmbedAuthorBuilder()
                    {
                        IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                        Name = "Welcomer Settings"
                    },
                    Description = "Here's your current settings for welcoming new users:\n" +
                    $"**Enabled?**: {ws.isEnabled}\n" +
                    $"**Welcome Channel**: <#{ws.WelcomeChannel}>\n" +
                    $"**Welcome Message**:\n> {ws.WelcomeMessage}\n" +
                    $"**Background Image**: [Click me!]({ws.BackgroundUrl} \"ooo you found a easter egg! maybe theres more...?\")\n" +
                    $"**Mentions Users**: {ws.MentionsUsers}\n" +
                    $"**Sent in Dm's**: {ws.DMs}\n",
                    Color = Blurple,
                    Fields = new List<EmbedFieldBuilder>()
                    {
                        new EmbedFieldBuilder()
                        {
                            Name = "Compiled Welcome Message",
                            Value = $"{ws.GenerateWelcomeMessage(Context.User as SocketGuildUser, Context.Guild)}"
                        }
                    },
                    //ImageUrl = PingGenerator.GetImageLink($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}WelcomeCard.png").GetAwaiter().GetResult()
                }.WithCurrentTimestamp().Build()) ;
                return;
            }

            switch (args[0].ToLower())
            {
                case "channel":
                    if(args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder() 
                        {
                            Author = new EmbedAuthorBuilder()
                            {
                                IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                                Name = "Welcomer Settings"
                            },
                            Description = $"The current welcome channel is <#{ws.WelcomeChannel}> [Click here to jump to channel](https://discord.com/channels/{Context.Guild.Id}/{ws.WelcomeChannel})",
                            Color = Blurple
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    var chan = GetChannel(args[1]);
                    if(chan == null)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Author = new EmbedAuthorBuilder()
                            {
                                IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                                Name = "Error"
                            },
                            Description = $"The channel you provided is invalid!",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    ws.WelcomeChannel = chan.Id;
                    GuildSettings.SaveGuildSettings();
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Author = new EmbedAuthorBuilder()
                        {
                            IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                            Name = "Success!"
                        },
                        Description = $"The channel is now set to <#{ws.WelcomeChannel}>",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    break;
                case "message":
                    if (args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Author = new EmbedAuthorBuilder()
                            {
                                IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                                Name = "Welcomer Settings"
                            },
                            Description = $"You can specify variables, heres a list of them:\n" +
                            $"**{{user}}** - The user's full name. Example: quin#3017\n" +
                            $"**{{user.name}}** - The user's username. Example: quin\n" +
                            $"**{{guild.name}}** - The guilds name. Example: Swiss001 Official Discord Server" +
                            $"**{{guild.count}}** - The guilds User count. Example: 8241",
                            Fields = new List<EmbedFieldBuilder>()
                            {
                                new EmbedFieldBuilder()
                                {
                                    Name = "Raw:",
                                    Value = $"{ws.WelcomeMessage}"
                                },
                                new EmbedFieldBuilder()
                                {
                                    Name = "Compiled:",
                                    Value = $"{ws.GenerateWelcomeMessage(Context.User as SocketGuildUser, Context.Guild)}"
                                }
                            },
                            Color = Blurple
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    string newMessage = string.Join(" ", args.Skip(1));
                    ws.WelcomeMessage = newMessage;
                    GuildSettings.SaveGuildSettings();
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Author = new EmbedAuthorBuilder()
                        {
                            IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                            Name = "Success!"
                        },
                        Description = $"Welcome Message has been changed!",
                        Fields = new List<EmbedFieldBuilder>()
                            {
                                new EmbedFieldBuilder()
                                {
                                    Name = "Raw:",
                                    Value = $"{ws.WelcomeMessage}"
                                },
                                new EmbedFieldBuilder()
                                {
                                    Name = "Compiled:",
                                    Value = $"{ws.GenerateWelcomeMessage(Context.User as SocketGuildUser, Context.Guild)}"
                                }
                            },
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    return;
                case "dm":
                    if(args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Author = new EmbedAuthorBuilder()
                            {
                                IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                                Name = "Welcomer Settings"
                            },
                            Description = $"Welcome messages are sent in {(ws.DMs ? "Dm's!" : $"<#{ws.WelcomeChannel}>!")} (Dm's: {ws.DMs})",
                            Color = Blurple
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if(args[1].ToLower() == "true")
                    {
                        ws.DMs = true;
                        GuildSettings.SaveGuildSettings();
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Author = new EmbedAuthorBuilder()
                            {
                                IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                                Name = "Success!"
                            },
                            Description = $"User's will now recieve there welcome message in DM's!",
                            Color = Color.Green
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if (args[1].ToLower() == "false")
                    { 
                        ws.DMs = false;
                        GuildSettings.SaveGuildSettings();
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Author = new EmbedAuthorBuilder()
                            {
                                IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                                Name = "Success!"
                            },
                            Description = $"User's will no longer recieve there welcome message in DM's. Welcome messages will be sent in <#{ws.WelcomeChannel}>!",
                            Color = Color.Green
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Author = new EmbedAuthorBuilder()
                        {
                            IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                            Name = "Error!"
                        },
                        Description = $"Please either use \"true\" or \"false\"!",
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build());
                    return;
                case "mentions":
                    if(args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Author = new EmbedAuthorBuilder()
                            {
                                IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                                Name = "Welcome Settings!"
                            },
                            Description = $"New users will {(ws.MentionsUsers ? "" : "not")} be pinged with there welcome card",
                            Color = Blurple
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if (args[1].ToLower() == "true")
                    {
                        ws.MentionsUsers = true;
                        GuildSettings.SaveGuildSettings();
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Author = new EmbedAuthorBuilder()
                            {
                                IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                                Name = "Success!"
                            },
                            Description = $"New user's will be pinged on there welcome card!",
                            Color = Color.Green
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if (args[1].ToLower() == "false")
                    {
                        ws.MentionsUsers = false;
                        GuildSettings.SaveGuildSettings();
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Author = new EmbedAuthorBuilder()
                            {
                                IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                                Name = "Success!"
                            },
                            Description = $"New user's will no longer be pinged on there welcome card!",
                            Color = Color.Green
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Author = new EmbedAuthorBuilder()
                        {
                            IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                            Name = "Error!"
                        },
                        Description = $"Please either use \"true\" or \"false\"!",
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build());
                    return;
                    //Commented below line for obvious reasons
                    //break;
                case "enable":
                    ws.isEnabled = true;
                    GuildSettings.SaveGuildSettings();
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Author = new EmbedAuthorBuilder()
                        {
                            IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                            Name = "Success!"
                        },
                        Description = $"The welcomer is now enabled!",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    return;
                case "disable":
                    ws.isEnabled = false;
                    GuildSettings.SaveGuildSettings();
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Author = new EmbedAuthorBuilder()
                        {
                            IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                            Name = "Success!"
                        },
                        Description = $"The welcomer is now disabled!",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    return;
                case "on":
                    ws.isEnabled = true;
                    GuildSettings.SaveGuildSettings();
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Author = new EmbedAuthorBuilder()
                        {
                            IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                            Name = "Success!"
                        },
                        Description = $"The welcomer is now on!",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    return;
                case "off":
                    ws.isEnabled = false;
                    GuildSettings.SaveGuildSettings();
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Author = new EmbedAuthorBuilder()
                        {
                            IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                            Name = "Success!"
                        },
                        Description = $"The welcomer is now off!",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    return;
            }
        }
        [DiscordCommand("leave-message",
            commandHelp ="`(PREFIX)leave-message <setting> <value>\n`" +
                         "`(PREFIX)leave-message on/off`\n" +
                         "`(PREFIX)leave-message title [new leave embed title]`\n" +
                         "`(PREFIX)leave-message message [new leave message]`\n"+
                         "`(PREFIX)leave-message channel #newleavemsgchnl`\n" + 
                         "\n\n" +
                         "*Use the below variables for the leave message:* \n"+
                         "*{user} - Username of the user who left*\n"+
                         "*{guild} - Name of your server*\n" + 
                         "*{guild.count} - The number of members in your server*"
            ,
            description ="gives the leave message settings", 
            RequiredPermission = true)]
        [Alt("leave")]
        public async Task leaveChanger(params string[] args)
        {
            if (args.Length == 0)
            {
                if (GuildSettings.leaveMessage == null)
                {
                    GuildSettings.leaveMessage = new Public_Bot.State.Types.LeaveMessage(GuildSettings, Context.Guild as IGuild);
                }
                var mBED = new EmbedBuilder
                {
                    Title = "Current Leave Message Settings"
                }
                .AddField("Enabled?",GuildSettings.leaveMessage.isEnabled)
                .AddField("Leave Embed Title", GuildSettings.leaveMessage.leaveTitle)
                .AddField("Embed Message", GuildSettings.leaveMessage.leaveMessage)
                .AddField("Leave Channel", $"<#{GuildSettings.leaveMessage.leaveChannel}>")
                .WithFooter($"Do {GuildSettings.Prefix}help leave-message to get the variables!!")
                .WithCurrentTimestamp().Build();
                await Context.Channel.SendMessageAsync("", false, mBED);
                return;
            }
            var x = args[0].ToLower();
            //Alright, so the attributes for the leaveMessage are~
            //isEnabled, GuildID, leaveChannel, leaveMessage, leaveTitle
            //INEFFICIENT CODE BELOW, WILL IMPROVE LATER
            switch (x)
            {
                case "enable":
                    GuildSettings.leaveMessage.isEnabled = true;
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder { Title = "Leave Message Settings Updated", Description = "Leave Message is now enabled!" }.WithCurrentTimestamp().Build());
                    break;
                case "on":
                    GuildSettings.leaveMessage.isEnabled = true;
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder { Title = "Leave Message Settings Updated", Description = "Leave Message is now enabled!" }.WithCurrentTimestamp().Build());
                    break;
                case "true":
                    GuildSettings.leaveMessage.isEnabled = true;
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder { Title = "Leave Message Settings Updated", Description = "Leave Message is now enabled!" }.WithCurrentTimestamp().Build());
                    break;
                case "disable":
                    GuildSettings.leaveMessage.isEnabled = false;
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder { Title = "Leave Message Settings Updated", Description = "Leave Message is now disabled!" }.WithCurrentTimestamp().Build());
                    break;
                case "off":
                    GuildSettings.leaveMessage.isEnabled = false;
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder { Title = "Leave Message Settings Updated", Description = "Leave Message is now disabled!" }.WithCurrentTimestamp().Build());
                    break;
                case "false":
                    GuildSettings.leaveMessage.isEnabled = false;
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder { Title = "Leave Message Settings Updated", Description = "Leave Message is now disabled!" }.WithCurrentTimestamp().Build());
                    break;
                case "message":
                    if (args.Length < 2) { 
                        await Context.Channel.SendMessageAsync("You got to give the updated leave message!\n");
                        return; 
                    }
                    GuildSettings.leaveMessage.leaveMessage = string.Join(' ', args.Skip(1));
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder { Title = "Leave Message Settings Updated", Description = $"Leave Message is now {GuildSettings.leaveMessage.leaveMessage}" }.WithCurrentTimestamp().Build());
                    break;
                case "title":
                    if (args.Length < 2)
                    {
                        await Context.Channel.SendMessageAsync("You got to give the updated leave message title!\n");
                        return;
                    }
                    GuildSettings.leaveMessage.leaveTitle = string.Join(' ', args.Skip(1));
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder { Title = "Leave Message Settings Updated", Description = $"Leave Message Title is now {GuildSettings.leaveMessage.leaveTitle}" }.WithCurrentTimestamp().Build());
                    break;
                case "channel":
                    if (!Context.Message.MentionedChannels.Any())
                    {
                        await Context.Channel.SendMessageAsync("You need to mention the channel you want to set as the Leave Message Channel!");
                        return;
                    }
                    GuildSettings.leaveMessage.leaveChannel = Context.Message.MentionedChannels.First().Id;
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder { Title = "Leave Message Settings Updated", Description = $"Leave Message Channel is now <#{GuildSettings.leaveMessage.leaveChannel}>" }.WithCurrentTimestamp().Build());
                    break;
                case "color":
                    if (args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("You need to specify the colour you want to set as the Leave Embed Colour!");
                        return;
                    }
                    await Context.Channel.SendMessageAsync("We havent implemented this yet ;)");
                    break;
            };
            GuildSettings.SaveGuildSettings();
            return;
        }
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
                    if("👨🏼‍💻 General 👨🏼‍💻".Contains(args[1]))
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = $"Why?",
                            Description = $"You cant disable the General Commands module :/",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if ("⚙️ Settings ⚙️".Contains(args[1]))
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
        [GuildPermissions(GuildPermission.ManageRoles)]
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
        [GuildPermissions(GuildPermission.ManageRoles)]
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
                new Thread(async () => await MuteHandler.SetupMutedRole(Context.Guild, Context.Channel.Id, Context.User.Id)).Start();
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
            if (role == null)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Invalid Role!",
                    Description = "The role you provided was invalid!",
                    Color = Color.Red,
                }.WithCurrentTimestamp().Build());
                return;
            }
            if(GuildSettings.PermissionRoles.Any(x => x == role.Id))
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "That role is already added!",
                    Description = "The roles is already in the permission list!",
                    Color = Color.Orange,
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
            if (role == null)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Invalid Role!",
                    Description = "The role you provided was invalid!",
                    Color = Color.Red,
                }.WithCurrentTimestamp().Build());
                return;
            }
            if (!GuildSettings.PermissionRoles.Any(x => x == role.Id))
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "That role isn't added!",
                    Description = "That role isn't in the permission list",
                    Color = Color.Orange,
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
        [GuildPermissions(GuildPermission.ViewAuditLog)]
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
        
        [GuildPermissions(GuildPermission.ManageRoles)]
        [DiscordCommand("joinrole", description = "Gives new users a role", commandHelp = "Usage - `(PREFIX)joinrole <role>", RequiredPermission = true)]
        public async Task Joinrole(string r)
        {
            var role = GetRole(r);
            if(role == null)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Invalid Role!",
                    Description = "The role you provided was invalid!",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            GuildSettings.NewMemberRole = role.Id;
            GuildSettings.SaveGuildSettings();
            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
            {
                Title = "Success!",
                Description = $"New members will now get the role {role.Mention}",
                Color = Color.Green
            }.WithCurrentTimestamp().Build());
        }
    }
}
