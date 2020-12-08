using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Color = Discord.Color;
namespace Public_Bot.Modules.Commands.General_Commands
{
    [DiscordCommandClass("👨🏼‍💻 General 👨🏼‍💻", "General bot commands for everyone!")]
    public class Nickname : CommandModuleBase
    {
        [DiscordCommand("nickname", commandHelp = "`(PREFIX)nickname <@user> <nickname>`", description = "Changes a user's nickname")]
        [Alt("nick")]
        public async Task NicknameUpdate(params string[] args)
        {
            if (args.Length == 0)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                {
                    Author = new EmbedAuthorBuilder()
                    {
                        Name = "Provide some Arguments!"
                    },
                    Color = Color.Orange,
                    Description = "You didnt provide any arguments!"
                }.WithCurrentTimestamp().Build());
                return;
            }
            var cgu = Context.Guild.GetUser(Context.User.Id);

            var user = await GetUser(args[0]);

            if (user != null && user.Id != cgu.Id && cgu.GuildPermissions.ManageNicknames)
            {
                if (user.Id == Context.Guild.OwnerId)
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                    {
                        Author = new EmbedAuthorBuilder
                        {
                            IconUrl = cgu.GetAvatarUrl(),
                            Name = "Error!"
                        },
                        ThumbnailUrl = "https://cdn.hapsy.net/947e21c9-0551-4043-a942-338cec178ad2",
                        Color = Color.Red,
                        Title = "Missing Permissions",
                        Description = "The bot can't nick the owner... only he can."
                    }.WithCurrentTimestamp().Build());
                    return;
                }
                if (user.Hierarchy >= cgu.Hierarchy)
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                    {
                        Author = new EmbedAuthorBuilder
                        {
                            IconUrl = cgu.GetAvatarUrl(),
                            Name = "Error!"
                        },
                        Color = Color.Red,
                        Title = "Missing Permissions",
                        Description = "You can't change a users nickname who has the same role or a role above yours!"
                    }.WithCurrentTimestamp().Build());
                    return;
                }
                if(user.Hierarchy >= Context.Guild.CurrentUser.Hierarchy)
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                    {
                        Author = new EmbedAuthorBuilder
                        {
                            IconUrl = cgu.GetAvatarUrl(),
                            Name = "Error!"
                        },
                        Color = Color.Red,
                        Title = "Missing Permissions",
                        Description = "You can't change a users nickname who's role is the same or above the bot's role"
                    }.WithCurrentTimestamp().Build());
                    return;
                }
                if (cgu.GuildPermissions.ManageNicknames || cgu.GuildPermissions.Administrator)
                {
                    try
                    {
                        await (user as SocketGuildUser).ModifyAsync(x => x.Nickname = string.Join(' ', args.Skip(1)));
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                        {
                            Author = new EmbedAuthorBuilder
                            {
                                IconUrl = cgu.GetAvatarUrl(),
                                Name = "Success!"
                            },
                            Color = Color.Green,
                            Description = $"Changed {user.Username}s nickname to {string.Join(' ', args.Skip(1))}"
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    catch
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                        {
                            Author = new EmbedAuthorBuilder
                            {
                                IconUrl = cgu.GetAvatarUrl(),
                                Name = "Error!"
                            },
                            Color = Color.Red,
                            Title = "Missing Permissions",
                            Description = "The bot doesn't have the `Manage Nicknames` permission!"
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                    {
                        Author = new EmbedAuthorBuilder
                        {
                            IconUrl = cgu.GetAvatarUrl(),
                            Name = "Error!"
                        },
                        Title = "Missing Permissions",
                        Description = "You need the `Manage Nicknames` permission to nick other users",
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build());
                    return;
                }

            }
            else
            {
                if (cgu.Id == Context.Guild.OwnerId)
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                    {
                        Author = new EmbedAuthorBuilder
                        {
                            IconUrl = cgu.GetAvatarUrl(),
                            Name = "Error!"
                        },
                        ThumbnailUrl = "https://cdn.hapsy.net/947e21c9-0551-4043-a942-338cec178ad2",
                        Color = Color.Red,
                        Title = "Missing Permissions",
                        Description = "The bot can't nick the owner... only he can."
                    }.WithCurrentTimestamp().Build());
                    return;
                }
                if (cgu.Hierarchy >= Context.Guild.CurrentUser.Hierarchy)
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                    {
                        Author = new EmbedAuthorBuilder
                        {
                            IconUrl = cgu.GetAvatarUrl(),
                            Name = "Error!"
                        },
                        Color = Color.Red,
                        Title = "Missing Permissions",
                        Description = "The bot can't change a users nickname that has the same role or a role above the bots!"
                    }.WithCurrentTimestamp().Build());
                    return;
                }
                string newNick = "";
                if (user == null)
                    newNick = string.Join(' ', args);
                else
                    newNick = string.Join(' ', args.Skip(1));
                if (cgu.GuildPermissions.ChangeNickname || cgu.GuildPermissions.Administrator)
                {
                    await cgu.ModifyAsync(x => x.Nickname = newNick);
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                    {
                        Author = new EmbedAuthorBuilder
                        {
                            IconUrl = cgu.GetAvatarUrl(),
                            Name = "Success!"
                        },
                        Color = Color.Green,
                        Description = $"Your nickname is now `{newNick}`"
                    }.WithCurrentTimestamp().Build());
                    return;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                    {
                        Author = new EmbedAuthorBuilder
                        {
                            IconUrl = cgu.GetAvatarUrl(),
                            Name = "Error!"
                        },
                        Title = "Missing Permissions",
                        Description = "You need the `Change Nickname` permission to nick other users",
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build());
                    return;
                }
            }
        }
    }
}