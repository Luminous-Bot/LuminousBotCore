using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Public_Bot.Modules.Handlers;
using Public_Bot.State.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Public_Bot.Modules.Handlers.MuteHandler;

namespace Public_Bot.Modules.Commands
{
    [DiscordHandler]
    public class ModCommands
    {
        public static DiscordShardedClient client { get; set; }
        public ModCommands(DiscordShardedClient _client)
        {
            client = _client;
            LoadInfracs();
        }
        public async void LoadInfracs()
        {
            CurrentGuildModLogs = new List<GuildModLogs>();
            //query the gql
            var res = await StateHandler.Postgql<List<Guilds>>("{ guilds{ Id Name GuildMembers{ GuildID UserID User{ Usernames { Name } } Infractions{ UserID GuildID Action Reason Time Moderator{ UserID User{ Usernames { Name } } } } } } }");
            foreach(var guild in res)
            {
                if (!guild.GuildMembers.Any(x => x.Infractions.Count > 0))
                    continue;
                var gml = new GuildModLogs();
                gml.GuildID = ulong.Parse(guild.GuildID);
                gml.GuildName = guild.GuildName;
                gml.Users = new List<Users>();
                foreach (var member in guild.GuildMembers.Where(x => x.Infractions.Count > 0))
                {
                    Users u = new Users();
                    u.UserID = ulong.Parse(member.UserID);
                    u.UserName = member.User.Usernames.First().Name;
                    u.ModLogs = new List<ModLog>();

                    member.Infractions.ForEach(x => u.ModLogs.Add(new ModLog()
                    {
                        Action = x.Action,
                        GuildID = gml.GuildID,
                        Moderator = new Moderator()
                        {
                            UserID = ulong.Parse(x.Moderator.UserID),
                            UserName = x.Moderator.User.Usernames.First().Name
                        },
                        Reason = x.Reason,
                        Time = x.Time,
                        UserID = u.UserID
                    }));

                    gml.Users.Add(u);
                }
                CurrentGuildModLogs.Add(gml);
            }
        }
        public static void SaveModlogs()
        {
            StateHandler.SaveObject<List<GuildModLogs>>("GuildLogs", CurrentGuildModLogs);
        }
        public static List<GuildModLogs> CurrentGuildModLogs { get; set; }
        public static void AddModlog(ModLog log)
        {
            var g = client.GetGuild(log.GuildID);
            if (g == null)
                return;
            var u = g.GetUser(log.UserID);
            if (u == null)
                throw new Exception("User is not in guild");
            if (!CurrentGuildModLogs.Any(x => x.GuildID == log.GuildID))
            {
                CurrentGuildModLogs.Add(new GuildModLogs()
                {
                    GuildID = log.GuildID,
                    GuildName = g.Name,
                    Users = new List<Users>()
                    {
                        new Users()
                        {
                            ModLogs = new List<ModLog>(){ log },
                            UserID = log.UserID,
                            UserName = $"{u}"
                        }
                    }
                });
            }
            else
            {
                var guildLogs = CurrentGuildModLogs.Find(x => x.GuildID == log.GuildID);
                if (!guildLogs.Users.Any(x => x.UserID == log.UserID))
                {
                    guildLogs.Users.Add(new Users()
                    {
                        UserID = log.UserID,
                        UserName = $"{u}",
                        ModLogs = new List<ModLog>() { log }
                    });
                }
                else
                {
                    guildLogs.Users.Find(x => x.UserID == log.UserID).ModLogs.Add(log);
                }
            }
        }
        public enum Action
        {
            Warned,
            Kicked,
            Banned,
            Muted,
            Voicebanned,
            Tempbanned
        }
        [DiscordCommandClass("🔨 Mod Commands 🔨", "Make your staff team more efficent with this module, you can keep track of user infractions and keep your server in order!")]
        public class ModCommandsModule : CommandModuleBase
        {
            public async Task CreateAction(string[] args, Action action, ICommandContext context)
            {
                var curUser = await context.Guild.GetCurrentUserAsync();
                if (!(curUser.GuildPermissions.BanMembers && curUser.GuildPermissions.KickMembers && curUser.GuildPermissions.ManageMessages && curUser.GuildPermissions.ManageRoles))
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "**The bot need better permissions!**",
                        Description = @$"The bot doesnt have the correct permissions for this module, run `{GuildSettings.Prefix}help setup` to see the bots permissions",
                        Color = Color.Red,
                        Timestamp = DateTimeOffset.Now,
                    }.Build());
                    return;
                }

                string actionString =
                    action == Action.Warned
                    ? "Warn" : action == Action.Kicked
                    ? "Kick" : action == Action.Banned
                    ? "Ban" : $"{action}";
                if (args.Length == 0)
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "**Who?**",
                        Description = @"You didnt provide any arguments ¯\_(ツ)_/¯",
                        Color = Color.Red,
                        Timestamp = DateTimeOffset.Now,
                    }.Build());
                    return;
                }
                SocketGuildUser user = GetUser(args[0]);
                if (user == null)
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "**Who?**",
                        Description = @$"The user you provided is invalid ¯\_(ツ)_/¯",
                        Color = Color.Red,
                        Timestamp = DateTimeOffset.Now,
                    }.Build());
                    return;
                }
                var sgu = Context.Guild.GetUser(context.User.Id);
                if (user.Hierarchy >= sgu.Hierarchy)
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "**Not gonna happen**",
                        Description = @$"You cant {actionString} someone whos rank is above yours ¯\_(ツ)_/¯",
                        Color = Color.Red,
                        Timestamp = DateTimeOffset.Now,
                    }.Build());
                    return;
                }
                if (args.Length == 1)
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "**Why?**",
                        Description = @$"You didnt provide any reason to {actionString} **{user}** ¯\_(ツ)_/¯",
                        Color = Color.Red,
                        Timestamp = DateTimeOffset.Now,
                    }.Build());
                    return;
                }

                ModLog m = new ModLog()
                {
                    Action = action,
                    GuildID = context.Guild.Id,
                    Reason = string.Join(' ', args.Skip(1)),
                    Time = DateTime.UtcNow,
                    UserID = user.Id,
                    Moderator = new Moderator()
                    {
                        UserID = context.Message.Author.Id,
                        UserName = context.Message.Author.ToString()
                    },
                };
                AddModlog(m);
                SaveModlogs();
                bool Dmed = true;
                try
                {
                    await user.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = $"**You have been {action} on {context.Guild.Name}**",
                        Fields = new List<EmbedFieldBuilder>()
                        {
                            new EmbedFieldBuilder()
                            {
                                Name = "Moderator",
                                Value = $"<@{m.Moderator.UserID}>\n{m.Moderator.UserName}",
                                IsInline = true,
                            },
                            new EmbedFieldBuilder()
                            {
                                Name = "Reason",
                                Value = m.Reason,
                                IsInline = true
                            }
                        },
                        Color = action == Action.Warned ? Color.Orange : Color.Red,
                        Timestamp = DateTime.Now
                    }.Build());
                }
                catch
                {
                    Dmed = false;
                }
                if (action == Action.Kicked)
                {
                    try
                    {
                        await user.KickAsync($"{m.Reason} - {m.Moderator.UserName}");
                    }
                    catch (Exception ex)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "**There was an error!**",
                            Description = $"{ex.Message}",
                            Color = Color.Red,
                            Timestamp = DateTimeOffset.Now,
                        }.Build());
                        return;
                    }
                }
                else if (action == Action.Banned)
                {
                    try
                    {
                        await user.BanAsync(7, $"{m.Reason} - {m.Moderator.UserName}");
                    }
                    catch (Exception ex)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "**There was an error!**",
                            Description = $"{ex.Message}",
                            Color = Color.Red,
                            Timestamp = DateTimeOffset.Now,
                        }.Build());
                        return;
                    }
                }
                await context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = $"Successfully {action} user {user.Username}",
                    Fields = new List<EmbedFieldBuilder>()
                    {
                        new EmbedFieldBuilder()
                        {
                            Name ="Moderator",
                            Value = $"<@{m.Moderator.UserID}>\n{m.Moderator.UserName}",
                            IsInline = true
                        },
                        new EmbedFieldBuilder()
                        {
                            Name = "Reason",
                            Value = m.Reason,
                            IsInline = true
                        },
                        new EmbedFieldBuilder()
                        {
                            Name = "Notified in DM?",
                            Value = Dmed,
                            IsInline = true
                        }
                    },
                    Color = Color.DarkGreen,
                    Timestamp = DateTime.Now
                }.Build());

            }
            [DiscordCommand("warn", RequiredPermission = true, description = "Warns a user", commandHelp = "Usage - `(PREFIX)warn <@user> <reason>`")]
            public async Task Warn(params string[] args)
            {
                await CreateAction(args, Action.Warned, Context);
            }
            [DiscordCommand("kick", RequiredPermission = true, description = "Kicks a user", commandHelp = "Usage - `(PREFIX)kick <@user> <reason>`")]
            public async Task Kick(params string[] args)
            {
                await CreateAction(args, Action.Kicked, Context);
            }
            [DiscordCommand("ban", RequiredPermission = true, description = "Bans a user", commandHelp = "Usage - `(PREFIX)ban <@user> <reason>`")]
            public async Task Ban(params string[] args)
            {
                await CreateAction(args, Action.Banned, Context);
            }
            [DiscordCommand("unmute", RequiredPermission = true, description = "Unmutes a muted user", commandHelp = "Usage - `(PREFIX)unmute <@user>`")]
            public async Task unmute(params string[] args)
            {
                if (args.Length == 0)
                {
                    await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                    {
                        Title = "Who..?",
                        Description = "Who do you want me to unmute",
                        Color = Color.Red,
                        Timestamp = DateTime.Now
                    }.Build());
                    return;
                }
                var user = GetUser(args[0]);
                if (user == null)
                {
                    await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                    {
                        Title = "**Who..?**",
                        Description = "That user is invalid!",
                        Color = Color.Red,
                        Timestamp = DateTime.Now
                    }.Build());
                    return;
                }
                if (user.Roles.Any(x => x.Id == GuildSettings.MutedRoleID))
                {
                    var role = Context.Guild.GetRole(GuildSettings.MutedRoleID);
                    try
                    {
                        await user.RemoveRoleAsync(role);
                        MuteHandler.CurrentMuted.Remove(MuteHandler.CurrentMuted.Find(x => x.UserID == user.Id));
                        MuteHandler.SaveMuted();
                        Embed b2 = new EmbedBuilder()
                        {
                            Title = $"**Successfully Unmuted user {user.ToString()}**",
                            Fields = new List<EmbedFieldBuilder>()
                            {
                                { new EmbedFieldBuilder(){
                                    Name = "Moderator",
                                    Value = Context.Message.Author.ToString(),
                                    IsInline = true
                                } }
                            },
                            Timestamp = DateTime.Now,
                            Color = Color.Green
                        }.Build();
                        await Context.Channel.SendMessageAsync("", false, b2);
                    }
                    catch (Exception ex)
                    {
                        await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                        {
                            Title = "**There was an Error**",
                            Description = $"Looks like we faild trying to remove the muted role, Here's the error message: {ex.Message}",
                            Color = Color.Red,
                            Timestamp = DateTime.Now
                        }.Build());
                        return;
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                    {
                        Title = "**That user is not Muted**",
                        Description = "There not muted lol. dont know what else you want me to say",
                        Color = Color.Red,
                        Timestamp = DateTime.Now
                    }.Build());
                    return;
                }

            }
            [DiscordCommand("mute", RequiredPermission = true, description = "Mutes a user", commandHelp = "Usage - `(PREFIX)mute <@user> <timespan> <reason>`\nTimespans:\n`10m` - Ten minutes\n`1h` - One hour\n`45s` - Forty five seconds\n`2d` - Two days\n`1y` - One year (dont recommend)")]
            public async Task Mute(params string[] args)
            {
                if (GuildSettings.MutedRoleID == 0)
                {
                    await Context.Channel.SendMessageAsync($"This command requires a \"Muted Role\", To set one up do `{GuildSettings.Prefix}createmutedrole`");
                    return;
                }

                if (args.Length == 0)
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "**Who? How long? and Why?**",
                        Description = "Please provide a user, time, and reason",
                        Timestamp = DateTime.Now,
                        Color = Color.Orange
                    }.Build());
                    return;
                }
                if (args.Length == 1)
                {
                    await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                    {
                        Title = "Give me a time!",
                        Description = $"if you wanted to mute for 10 minutes use `{GuildSettings.Prefix}mute <user> 10m <reason>`",
                        Color = Color.Orange,
                        Timestamp = DateTime.Now
                    }.Build());
                    return;
                }
                if (args.Length == 2)
                {
                    await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                    {
                        Title = "Give me a Reason!",
                        Description = $"You need to provide a reason",
                        Color = Color.Orange,
                        Timestamp = DateTime.Now
                    }.Build());
                    return;
                }
                var user = GetUser(args[0]);
                if (user == null)
                {
                    await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                    {
                        Title = "Who?",
                        Description = $"That user is invalid!",
                        Color = Color.Red,
                        Timestamp = DateTime.Now
                    }.Build());
                    return;
                }
                if (user.Roles.Any(x => x.Id == GuildSettings.MutedRoleID))
                {
                    await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                    {
                        Title = "That user is already muted!",
                        Description = "We cant mute someone whos already muted :/",
                        Color = Color.Red,
                        Timestamp = DateTime.Now
                    }.Build());
                    return;
                }
                var regex = new Regex(@"^(\d*)([a-z])$");
                var datetime = DateTime.UtcNow;
                //TimeSpan s = new TimeSpan();
                if (regex.IsMatch(args[1].ToLower()))
                {
                    var r = regex.Match(args[1].ToLower());
                    switch (r.Groups[2].Value)
                    {
                        case "s":
                            datetime = datetime.AddSeconds(double.Parse(r.Groups[1].Value));
                            break;
                        case "m":
                            datetime = datetime.AddMinutes(double.Parse(r.Groups[1].Value));
                            break;
                        case "h":
                            datetime = datetime.AddHours(double.Parse(r.Groups[1].Value));
                            break;
                        case "d":
                            datetime = datetime.AddDays(double.Parse(r.Groups[1].Value));
                            break;
                        case "y":
                            datetime = datetime.AddYears(int.Parse(r.Groups[1].Value));
                            break;
                        default:
                            await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                            {
                                Title = "**How long?**",
                                Description = $"Please provide a valid time span, here are some examples\n" +
                                $"`10m` - Ten minutes\n" +
                                $"`1h` - One hour\n" +
                                $"`45s` - Forty five seconds\n" +
                                $"`2d` - Two days\n" +
                                $"`1y` - One year (dont recommend)",
                                Color = Color.Red,
                                Timestamp = DateTime.Now
                            }.Build());
                            return;
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                    {
                        Title = "**How long?**",
                        Description = $"Please provide a valid time span, here are some examples\n" +
                        $"`10m` - Ten minutes\n" +
                        $"`1h` - One hour\n" +
                        $"`45s` - Forty five seconds\n" +
                        $"`2d` - Two days\n" +
                        $"`1y` - One year (dont recommend)",
                        Color = Color.Red,
                        Timestamp = DateTime.Now
                    }.Build());
                    return;
                }
                string reason = string.Join(' ', args.Skip(2));
                try
                {
                    await user.AddRoleAsync(Context.Guild.GetRole(GuildSettings.MutedRoleID));
                }
                catch (Exception ex)
                {
                    await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                    {
                        Title = "**Welp, that didnt work!**",
                        Description = $"We couldn't add the muted role to {user}, Here's the reason: {ex.Message}",
                        Color = Color.Red,
                        Timestamp = DateTime.Now
                    }.Build());
                    return;
                }

                Embed b = new EmbedBuilder()
                {
                    Title = $"**You have been Muted on {Context.Guild.Name}**",
                    Fields = new List<EmbedFieldBuilder>()
                    {
                        { new EmbedFieldBuilder(){
                            Name = "Moderator",
                            Value = Context.Message.Author.ToString(),
                            IsInline = true
                        } },
                        {new EmbedFieldBuilder()
                        {
                            Name = "Reason",
                            Value = reason,
                            IsInline = true
                        } }
                    },
                    Timestamp = DateTime.Now,
                    Color = Color.Orange
                }.Build();
                bool dmed = true;
                try
                {
                    await user.SendMessageAsync("", false, b);
                }
                catch
                { dmed = false; }
                Embed b2 = new EmbedBuilder()
                {
                    Title = $"Successfully **Muted** user **{user}**",
                    Fields = new List<EmbedFieldBuilder>()
                    {
                        { new EmbedFieldBuilder(){
                            Name = "Moderator",
                            Value = Context.Message.Author.ToString(),
                            IsInline = true
                        } },
                        {new EmbedFieldBuilder()
                        {
                            Name = "Reason",
                            Value = reason,
                            IsInline = true
                        } },
                        {new EmbedFieldBuilder()
                        {
                            Name = "Notified in DM?",
                            Value = dmed,
                            IsInline = true
                        } }
                    },
                    Timestamp = DateTime.Now,
                    Color = Color.Green
                }.Build();
                await Context.Channel.SendMessageAsync("", false, b2);
                AddModlog(new ModLog()
                {
                    Action = Action.Muted,
                    GuildID = Context.Guild.Id,
                    UserID = user.Id,
                    Moderator = new Moderator() { UserID = Context.User.Id, UserName = Context.User.ToString() },
                    Reason = reason,
                    Time = DateTime.UtcNow
                });
                SaveModlogs();
                Handlers.MuteHandler.AddNewMuted(user.Id, datetime, GuildSettings);
            }
            [DiscordCommand("modlogs", RequiredPermission = true, description = "View a users modlogs", commandHelp = "Usage `(PREFIX)modlogs <@user>`")]
            public async Task modlogs(params string[] args)
            {
                if (args.Length == 0)
                {
                    await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                    {
                        Title = "**Who's logs do you want to view?**",
                        Description = "Please provide a user",
                        Color = Color.Red,
                        Timestamp = DateTime.Now
                    }.Build());
                    return;
                }
                var user = GetUser(args[0]);
                if (user == null)
                {
                    await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                    {
                        Title = "**Who's that?**",
                        Description = "Please provide a valid user",
                        Color = Color.Red,
                        Timestamp = DateTime.Now
                    }.Build());
                    return;
                }
                if (CurrentGuildModLogs.Any(x => x.GuildID == Context.Guild.Id))
                {
                    var modlogs = CurrentGuildModLogs.Find(x => x.GuildID == Context.Guild.Id);
                    if (modlogs.Users.Any(x => x.UserID == user.Id && x.ModLogs.Count > 0))
                    {
                        var userlog = modlogs.Users.Find(x => x.UserID == user.Id);
                        var pg = ModlogsPageHandler.BuildHelpPage(userlog.ModLogs, 0, user.Id, Context.Guild.Id, Context.User.Id);
                        var emb = ModlogsPageHandler.BuildHelpPageEmbed(pg, 1);
                        var msg = await Context.Channel.SendMessageAsync("", false, emb.Build());
                        pg.MessageID = msg.Id;
                        ModlogsPageHandler.CurrentPages.Add(pg);
                        ModlogsPageHandler.SaveMLPages();
                        await msg.AddReactionsAsync(new IEmote[] { new Emoji("\U00002B05"), new Emoji("\U000027A1") });
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                        {
                            Title = $"Modlogs for **{user}**",
                            Description = "This user has no logs! :D",
                            Color = Color.Green,
                            Timestamp = DateTime.Now
                        }.Build());
                        return;
                    }
                }
            }
            [DiscordCommand("clearlogs", description = "Clears a users logs", commandHelp = "Usage - `(PREFIX)clearlogs <@user>`, `(PREFIX)clearlogs <@user> <log_number>`", RequiredPermission = true)]
            public async Task ClearLogs(params string[] args)
            {
                if (args.Length == 0)
                {
                    await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                    {
                        Title = "Who's log do you want to clear?",
                        Description = $"You didnt provide any arguments, if your stuck do `{GuildSettings.Prefix}help clearlogs`",
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build());
                    return;
                }
                var usr = GetUser(args[0]);
                if (usr == null)
                {
                    await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                    {
                        Title = $"Who?",
                        Description = $"I couldn't find a user with the name or id of \"{args[0]}\"!",
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build());
                    return;
                }
                if (CurrentGuildModLogs.Any(x => x.GuildID == Context.Guild.Id))
                {
                    var Gmodlogs = CurrentGuildModLogs.Find(x => x.GuildID == Context.Guild.Id);
                    if (Gmodlogs.Users.Any(x => x.UserID == usr.Id && x.ModLogs.Count > 0))
                    {
                        var usrlogs = Gmodlogs.Users.Find(x => x.UserID == usr.Id);
                        if (args.Length == 1)
                        {
                            await modlogs(args);
                        }
                        if (args.Length == 2)
                        {
                            if (args[1] == "all" || args[1] == "clear")
                            {
                                usrlogs.ModLogs.Clear();
                                SaveModlogs();
                                await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                                {
                                    Title = $"Cleared {usrlogs.UserName}'s Logs!",
                                    Description = "This user has no logs! :D",
                                    Color = Color.Green,
                                    Timestamp = DateTime.Now
                                }.Build());
                                return;
                            }
                            else
                            {
                                if (uint.TryParse(args[1], out var res))
                                {
                                    usrlogs.ModLogs.RemoveAt((int)res - 1);
                                    SaveModlogs();
                                    var pg = ModlogsPageHandler.BuildHelpPage(usrlogs.ModLogs, 0, usrlogs.UserID, Context.Guild.Id, Context.User.Id);
                                    var emb = ModlogsPageHandler.BuildHelpPageEmbed(pg, 1);
                                    var msg = await Context.Channel.SendMessageAsync($"Removed log number {args[1]}", false, emb.Build());
                                    pg.MessageID = msg.Id;
                                    ModlogsPageHandler.CurrentPages.Add(pg);
                                    ModlogsPageHandler.SaveMLPages();
                                    await msg.AddReactionsAsync(new IEmote[] { new Emoji("\U00002B05"), new Emoji("\U000027A1") });
                                }
                                else
                                {
                                    await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                                    {
                                        Title = $"Invalid number",
                                        Description = $"\"{args[1]}\" is an invalid number!",
                                        Color = Color.Red
                                    }.WithCurrentTimestamp().Build());
                                    return;
                                }
                            }

                        }
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                        {
                            Title = $"Modlogs for **{usr}**",
                            Description = "This user has no logs! :D",
                            Color = Color.Green,
                            Timestamp = DateTime.Now
                        }.Build());
                        return;
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                    {
                        Title = $"Modlogs for **{usr}**",
                        Description = "This user has no logs! :D",
                        Color = Color.Green,
                        Timestamp = DateTime.Now
                    }.Build());
                    return;
                }
            }
            [DiscordCommand("purge", RequiredPermission = true, commandHelp = "Usage - `(PREFIX)purge <ammount>`, `(PREFIX)purge <@user> <ammount>`", description = "Deletes `x` ammount of messages")]
            public async Task purge(uint amount)
            {
                var messages = await Context.Channel.GetMessagesAsync((int)amount + 1).FlattenAsync();
                await ((ITextChannel)Context.Channel).DeleteMessagesAsync(messages);
                const int delay = 2000;
                var m = await Context.Channel.SendMessageAsync($"Purge completed!");
                await Task.Delay(delay);
                await m.DeleteAsync();
            }
            [DiscordCommand("purge")]
            public async Task purge(string usr, uint ammount)
            {

                var user = GetUser(usr);
                if (user == null)
                {
                    await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                    {
                        Title = "Invalid ID",
                        Description = "The user is not in the server or the ID is invalid!",
                        Color = Color.Red
                    }.Build());
                    return;
                }
                var tmp = await Context.Channel.GetMessagesAsync(100).FlattenAsync();
                if (!tmp.Any(x => x.Author.Id == user.Id))
                {
                    await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                    {
                        Title = "Unable to find messages",
                        Description = $"we cant find any messages from <@{user.Id}>!",
                        Color = Color.Red
                    }.Build());
                    return;
                }
                var messages = tmp.Where(x => x.Author.Id == user.Id).Take((int)ammount);
                await ((ITextChannel)Context.Channel).DeleteMessagesAsync(messages);
                const int delay = 2000;
                var m = await Context.Channel.SendMessageAsync($"Purge completed!");
                await Task.Delay(delay);
                await m.DeleteAsync();

            }

            [DiscordCommand("slowmode", RequiredPermission = true, description = "Change the slowmode for a channel", commandHelp = "Usage - `(PREFIX)slowmode <time>`\nTimes:\n`10m` - Ten minutes\n`1h` - One hour\n`45s` - Forty five seconds\n")]
            public async Task slowmode(params string[] args)
            {
                if (args.Length == 0)
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "How much slowmode?",
                        Description = "Prove some arguments!",
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
                            Description = $"{Context.User.Mention} set the slowmode of <%{Context.Channel.Id}> to {t} Seconds!",
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
                                Description = $"{Context.User.Mention} set the slowmode of <%{Context.Channel.Id}> to {timespan.TotalSeconds} Seconds!",
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
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Success!",
                                Description = $"{Context.User.Mention} turned off slowmode for <%{Context.Channel.Id}>",
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
}
