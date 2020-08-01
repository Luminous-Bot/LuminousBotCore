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

namespace Public_Bot.Modules.Commands
{
    [DiscordHandler]
    public class ModCommands
    {
        public static DiscordShardedClient client { get; set; }
        public ModCommands(DiscordShardedClient _client)
        {
            client = _client;
        }
        [DiscordCommandClass("🔨 Mod Commands 🔨", "Make your staff team more efficient with this module, you can keep track of user infractions and keep your server in order!")]
        public class ModCommandsModule : CommandModuleBase
        {
            public async Task CreateAction(string[] args, Action action, ICommandContext context)
            {
                var curUser = await context.Guild.GetCurrentUserAsync();
                if (!(curUser.GuildPermissions.BanMembers && curUser.GuildPermissions.KickMembers && curUser.GuildPermissions.ManageMessages && curUser.GuildPermissions.ManageRoles))
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "**The bot needs better permissions!**",
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

                string reason = string.Join(' ', args.Skip(1));
                Infraction m = new Infraction(user.Id, context.User.Id, context.Guild.Id, action, reason, DateTime.UtcNow);
                bool Dmed = true;
                var md = GuildHandler.GetGuildMember(m.ModeratorID, m.GuildID);
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
                                Value = $"<@{m.ModeratorID}>\n{(md == null ? "" : md.Username)}",
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
                        await user.KickAsync($"{m.Reason}{(md == null ? "" : " - " +md.Username)}");
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
                        await user.BanAsync(7, $"{m.Reason}{(md == null ? "" : " - " + md.Username)}");
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
                            Value = $"<@{m.ModeratorID}>\n{(md == null ? "" : md.Username)}",
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

                var gs = GuildSettings.Get(Context.Guild.Id);
                if (gs.LogChannel != 0 && gs.Logging)
                {
                    var logchan = Context.Guild.GetTextChannel(gs.LogChannel);
                    if (logchan != null)
                    {
                        await logchan.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = $"⚡ User {action} ⚡",
                            Description = $"The user {user.Username} was {action} by <@{m.ModeratorID}> for {reason}",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                    }
                }

            }
            [DiscordCommand("warn", RequiredPermission = true, description = "Warns a user", commandHelp = "Usage - `(PREFIX)warn <@user> <reason>`")]
            public async Task Warn(params string[] args)
            {
                await CreateAction(args, Action.Warned, Context);
            }
            [GuildPermissions(GuildPermission.KickMembers)]
            [DiscordCommand("kick", RequiredPermission = true, description = "Kicks a user", commandHelp = "Usage - `(PREFIX)kick <@user> <reason>`")]
            public async Task Kick(params string[] args)
            {
                await CreateAction(args, Action.Kicked, Context);
            }
            [GuildPermissions(GuildPermission.BanMembers)]
            [DiscordCommand("ban", RequiredPermission = true, description = "Bans a user", commandHelp = "Usage - `(PREFIX)ban <@user> <reason>`")]
            public async Task Ban(params string[] args)
            {
                await CreateAction(args, Action.Banned, Context);
            }
            [GuildPermissions(GuildPermission.ManageRoles)]
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
                        Description = "They are not muted lol. dont know what else you want me to say",
                        Color = Color.Red,
                        Timestamp = DateTime.Now
                    }.Build());
                    return;
                }

            }
            [GuildPermissions(GuildPermission.ManageRoles)]
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
                        Title = "**Welp, that didn't work!**",
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
                Infraction infrac = new Infraction(user.Id, Context.User.Id, Context.Guild.Id, Action.Muted, reason, DateTime.UtcNow);
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
                var guild = GuildHandler.GetGuild(Context.Guild.Id);

                if (guild.GuildMembers.Any(x => x.UserID == user.Id && x.Infractions.Count > 0))
                {
                    var userlog = guild.GuildMembers.Find(x => x.UserID == user.Id);
                    var pg = ModlogsPageHandler.BuildHelpPage(userlog.Infractions, 0, user.Id, Context.Guild.Id, Context.User.Id);
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

                var guild = GuildHandler.GetGuild(Context.Guild.Id);

                if (guild.GuildMembers.Any(x => x.UserID == usr.Id))
                {
                    var usrlogs = guild.GuildMembers.Find(x => x.UserID == usr.Id);
                    if (args.Length == 1)
                    {
                        await modlogs(args);
                    }
                    if (args.Length == 2)
                    {
                        if (args[1] == "all" || args[1] == "clear")
                        {
                            var bucket = new MutationBucket<Infraction>("deleteInfraction");
                            usrlogs.Infractions.ForEach(x => bucket.Add(x, new KeyValuePair<string, object>("id", x.Id)));
                            await StateService.MutateAsync<dynamic>(bucket.Build());
                            usrlogs.Infractions.Clear();
                            //deleteLogs
                            await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                            {
                                Title = $"Cleared {usrlogs.Username}'s Logs!",
                                Description = $"Cleared all of <@{usrlogs.UserID}>'s infractions!",
                                Color = Color.Green,
                                Timestamp = DateTime.Now
                            }.Build());
                            return;
                        }
                        else
                        {
                            if (uint.TryParse(args[1], out var res))
                            {
                                if(usrlogs.Infractions.Count < res || res == 0)
                                {
                                    await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                                    {
                                        Title = $"Invalid number",
                                        Description = $"\"{args[1]}\" is an invalid number! Make sure the user has a log number of {args[1]}",
                                        Color = Color.Red
                                    }.WithCurrentTimestamp().Build());
                                    return;
                                }
                                await StateService.MutateAsync<dynamic>(GraphQLParser.GenerateGQLMutation<Infraction>("deleteInfraction", false, usrlogs.Infractions[(int)res - 1], "", "", new KeyValuePair<string, object>("id", usrlogs.Infractions[(int)res - 1].Id)));
                                usrlogs.Infractions.RemoveAt((int)res - 1);
                                await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                                {
                                    Title = $"Cleared {usrlogs.Username}'s Logs!",
                                    Description = $"Cleared all of <@{usrlogs.UserID}>'s infractions!",
                                    Color = Color.Green,
                                    Timestamp = DateTime.Now
                                }.Build());
                                var pg = ModlogsPageHandler.BuildHelpPage(usrlogs.Infractions, 0, usrlogs.UserID, Context.Guild.Id, Context.User.Id);
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
                    else if(args.Length > 2)
                    {
                        var bucket = new MutationBucket<Infraction>("deleteInfraction");
                        List<Infraction> removed = new List<Infraction>();
                        foreach(var item in args.Skip(1))
                        {
                            if (uint.TryParse(args[1], out var res))
                            {
                                if (usrlogs.Infractions.Count < res || res == 0)
                                {
                                    await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                                    {
                                        Title = $"Invalid number",
                                        Description = $"\"{args[1]}\" is an invalid number! Make sure the user has a log number of {args[1]}",
                                        Color = Color.Red
                                    }.WithCurrentTimestamp().Build());
                                    return;
                                }
                                var infrac = usrlogs.Infractions[(int)res - 1];
                                bucket.Add(infrac, new KeyValuePair<string, object>("id", infrac.Id));
                                removed.Add(infrac);
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
                        await StateService.MutateAsync<dynamic>(bucket.Build());
                        removed.ForEach(x => usrlogs.Infractions.Remove(x));
                        removed.Clear();

                        if (usrlogs.Infractions.Count == 0)
                        {
                            await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                            {
                                Title = $"Cleared {usrlogs.Username}'s Logs!",
                                Description = $"Cleared all of <@{usrlogs.UserID}>'s infractions!",
                                Color = Color.Green,
                                Timestamp = DateTime.Now
                            }.Build());
                        }
                        var pg = ModlogsPageHandler.BuildHelpPage(usrlogs.Infractions, 0, usrlogs.UserID, Context.Guild.Id, Context.User.Id);
                        var emb = ModlogsPageHandler.BuildHelpPageEmbed(pg, 1);
                        var msg = await Context.Channel.SendMessageAsync($"Removed log numbers {string.Join(", ", args.Skip(1))}", false, emb.Build());
                        pg.MessageID = msg.Id;
                        ModlogsPageHandler.CurrentPages.Add(pg);
                        ModlogsPageHandler.SaveMLPages();
                        await msg.AddReactionsAsync(new IEmote[] { new Emoji("\U00002B05"), new Emoji("\U000027A1") });
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
            [GuildPermissions(GuildPermission.ManageMessages)]
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
            //[DiscordCommand("hardmute",RequiredPermission =true,commandHelp ="`(PREFIX)hardmute <user> <time> <reason>`",description ="Removes all roles from the user, and then mutes them.")]
            public async Task hm(params string[] cmdargs)
            {
                if (cmdargs.Length < 1)
                {
                    await Context.Channel.SendMessageAsync(embed: new EmbedBuilder
                    {
                        Title = "Invalid Arguments",
                        Description = "You havent even given a user."
                    }.WithCurrentTimestamp().Build());
                    return;
                }
                var myUser = GetUser(cmdargs[0]);
                if (myUser == null)
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
                if (myUser.Hierarchy >= (Context.User as SocketGuildUser).Hierarchy)
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                    {
                        Title = "Error!",
                        Description = "That user is hierarchically above you.",
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build()
                    );
                    return;
                }
                IReadOnlyCollection<SocketRole> hisRoleList;
                try
                {
                    hisRoleList = myUser.Roles;
                    foreach(var rl in myUser.Roles)
                    {
                        if (rl == Context.Guild.EveryoneRole) continue;
                        await myUser.RemoveRoleAsync(rl);
                    }
                } catch (Exception e)
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder {
                        Title="Error!",
                        Description=$"I am missing permissions to hardmute the mentioned user.\n{e}",
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build());
                    return;
                }
                await Mute(cmdargs);
                await myUser.AddRolesAsync(hisRoleList);
            }
            [GuildPermissions(GuildPermission.ManageMessages)]
            [DiscordCommand("purge",RequiredPermission =true)]
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
            [GuildPermissions(GuildPermission.ManageChannels)]
            [DiscordCommand("lock", RequiredPermission = true, commandHelp = "`(PREFIX)lock #channel`", description = "locks the mentioned channel")]
            public async Task Lock(params string[] args)
            {
                SocketGuildChannel lockchnl;
                if (!Context.Message.MentionedChannels.Any())
                {
                    //Assuming they want to lock the current channel!
                    lockchnl = Context.Channel as SocketGuildChannel;
                }
                else
                {
                    lockchnl = Context.Message.MentionedChannels.First();
                }
                var lockMSGchnl = lockchnl as SocketTextChannel;
                EmbedBuilder alfa = new EmbedBuilder();
                try
                {
                    if (lockMSGchnl == null)
                    {
                        var lockVOICE = lockchnl as SocketVoiceChannel;
                        var xyz = new OverwritePermissions(connect: PermValue.Deny);
                        await lockVOICE.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, xyz);
                        alfa.Title = $"Locked Voice Channel {lockVOICE.Name}";
                        alfa.Description = "The aforementioned voice channel has been locked.";
                        alfa.WithCurrentTimestamp();
                    }
                    else
                    {
                        var sry = new OverwritePermissions(sendMessages: PermValue.Deny);
                        await lockchnl.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, sry);
                        alfa.Title = $"Locked Text Channel {lockMSGchnl.Name}";
                        alfa.Description = $"{lockMSGchnl.Mention} has been locked";
                        alfa.WithCurrentTimestamp();
                    }
                    await Context.Channel.SendMessageAsync("", false, alfa.Build());
                }
                catch
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                    {
                        Title = "Missing Permissions",
                        Description = "I do not have perms :sob:"
                    }.WithCurrentTimestamp().Build());
                }
            }
            [GuildPermissions(GuildPermission.ManageChannels)]
            [DiscordCommand("unlock", commandHelp = "`(PREFIX)unlock #channel`", description = "Unlocks the mentioned channel",RequiredPermission = true)]
            public async Task Unlock(params string[] args)
            {
                SocketGuildChannel lockchnl;
                if (!Context.Message.MentionedChannels.Any())
                {
                    //Assuming they want to lock the current channel.
                    lockchnl = Context.Channel as SocketGuildChannel;
                }
                else
                {
                    lockchnl = Context.Message.MentionedChannels.First();
                }
                var lockMSGchnl = lockchnl as SocketTextChannel;
                EmbedBuilder alfa = new EmbedBuilder();
                try
                {
                    if (lockMSGchnl == null)
                    {
                        var lockVOICE = lockchnl as SocketVoiceChannel;
                        var xyz = new OverwritePermissions(connect: PermValue.Inherit);
                        await lockVOICE.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, xyz);
                        alfa.Title = $"Unlocked Voice Channel {lockVOICE.Name}";
                        alfa.Description = "The aforementioned voice channel has been unlocked.";
                        alfa.WithCurrentTimestamp();
                    }
                    else
                    {
                        var sry = new OverwritePermissions(sendMessages: PermValue.Inherit);
                        await lockchnl.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, sry);
                        alfa.Title = $"Unlocked Text Channel {lockMSGchnl.Name}";
                        alfa.Description = $"{lockMSGchnl.Mention} has been unlocked";
                        alfa.WithCurrentTimestamp();
                    }
                    await Context.Channel.SendMessageAsync("", false, alfa.Build());
                }
                catch
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                    {
                        Title = "Missing Permissions",
                        Description = "I do not have perms :sob:"
                    }.WithCurrentTimestamp().Build());
                }
            }
            [GuildPermissions(GuildPermission.BanMembers)]
            [DiscordCommand("softban",commandHelp ="`(PREFIX)softban <user>`", description ="Bans and instantly unbans the user to delete 48 hours of their messages.")]
            public async Task SoftBan(params string[] args)
            {
                if (args.Length == 0)
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                    {
                        Title = "No user specified",
                        Description = "Who are we supposed to softban :thinking:",
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build());
                    return;
                }
                var user = GetUser(args[0]);
                if (user == null)
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                    {
                        Title = "No user or id specified",
                        Description = "Who are we supposed to softban :thinking:",
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build());
                    return;
                }
                var id = user.Id;
                await Context.Guild.AddBanAsync(id, 2, args[1]);
                await Context.Guild.RemoveBanAsync(id);
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                {
                    Title = $"Successfully softbanned user",
                    Description = $"<@{id}> was successfully softbanned",
                    Color = Blurple,
                    Footer = new EmbedFooterBuilder
                    {
                        Text = "Moderation by Luminous"
                    }
                }.WithCurrentTimestamp().Build());
            }
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
}
