using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Public_Bot.Modules.Handlers
{
    public class ModActionHandler
    {
        public static async Task CreateAction(string[] args, Action action, ShardedCommandContext context, GuildSettings GuildSettings)
        {
            var curUser = context.Guild.CurrentUser;
            if (!(curUser.GuildPermissions.BanMembers && curUser.GuildPermissions.KickMembers && curUser.GuildPermissions.ManageMessages && curUser.GuildPermissions.ManageRoles))
            {
                await context.Channel.SendMessageAsync("", false, new EmbedBuilder()
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
                await context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "**Who?**",
                    Description = @"You didnt provide any arguments ¯\_(ツ)_/¯",
                    Color = Color.Red,
                    Timestamp = DateTimeOffset.Now,
                }.Build());
                return;
            }

            if (args.Length == 1)
            {
                await context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "**Why?**",
                    Description = @$"You didnt provide any reason to {actionString} {args[0]} ¯\_(ツ)_/¯",
                    Color = Color.Red,
                    Timestamp = DateTimeOffset.Now,
                }.Build());
                return;
            }

            string reason = string.Join(' ', args.Skip(1));

            var g = GuildCache.GetGuild(context.Guild.Id);
            var md = g.GuildMembers.GetGuildMember(context.User.Id);

            SocketGuildUser user = CommandModuleBase.GetUser(args[0], context);

            if(action == Action.Banned && user == null)
            {
                // Check if the arg is an id
                if (Regex.IsMatch(args[0], @"(\d{18}|\d{17})"))
                {
                    var id = ulong.Parse(Regex.Match(args[0], @"(\d{18}|\d{17})").Groups[1].Value);

                    await context.Guild.AddBanAsync(id, reason: reason);

                    Infraction infrac = new Infraction(id, context.User.Id, context.Guild.Id, action, reason, DateTime.UtcNow);

                    await context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = $"Successfully {action} {args[0]}",
                        Fields = new List<EmbedFieldBuilder>()
                        {
                            new EmbedFieldBuilder()
                            {
                                Name ="Moderator",
                                Value = $"<@{infrac.ModeratorID}>\n{(md == null ? "" : md.Username)}",
                                IsInline = true
                            },
                            new EmbedFieldBuilder()
                            {
                                Name = "Reason",
                                Value = infrac.Reason,
                                IsInline = true
                            }
                        },
                        Color = Color.DarkGreen,
                        Timestamp = DateTime.Now
                    }.Build());
                }
                else
                {
                    await context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "**Who?**",
                        Description = @$"The user you provided is invalid ¯\_(ツ)_/¯",
                        Color = Color.Red,
                        Timestamp = DateTimeOffset.Now,
                    }.Build());
                    return;
                }
            } 
            else if (user == null)
            {
                await context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "**Who?**",
                    Description = @$"The user you provided is invalid ¯\_(ツ)_/¯",
                    Color = Color.Red,
                    Timestamp = DateTimeOffset.Now,
                }.Build());
                return;
            }

            var sgu = context.Guild.GetUser(context.User.Id);

            if (user.Hierarchy >= sgu.Hierarchy)
            {
                await context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "**Not gonna happen**",
                    Description = @$"You cant {actionString} someone whos rank is above yours ¯\_(ツ)_/¯",
                    Color = Color.Red,
                    Timestamp = DateTimeOffset.Now,
                }.Build());
                return;
            }
           

            Infraction m = new Infraction(user.Id, context.User.Id, context.Guild.Id, action, reason, DateTime.UtcNow);
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
                    await user.KickAsync($"{m.Reason}{(md == null ? "" : " - " + md.Username)}");
                }
                catch (Exception ex)
                {
                    await context.Channel.SendMessageAsync("", false, new EmbedBuilder()
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
                    await context.Channel.SendMessageAsync("", false, new EmbedBuilder()
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

            var gs = GuildSettings.Get(context.Guild.Id);
            if (gs.LogChannel != 0 && gs.Logging)
            {
                var logchan = context.Guild.GetTextChannel(gs.LogChannel);
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
    }
}
