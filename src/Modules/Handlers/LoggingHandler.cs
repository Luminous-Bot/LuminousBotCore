﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Public_Bot.Modules.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Public_Bot.Modules.Handlers
{
    [DiscordHandler]
    class LoggingHandler
    {
        public static DiscordShardedClient client;
        private Thread _loggingThread;
        public LoggingHandler(DiscordShardedClient c)
        {
            _loggingThread = new Thread(() => 
            {
                client = c;
                //subscribe
                client.ChannelCreated += Client_ChannelCreated;
                client.ChannelDestroyed += Client_ChannelDestroyed;
                client.ChannelUpdated += Client_ChannelUpdated;
                client.GuildUpdated += Client_GuildUpdated;
                client.MessageDeleted += Client_MessageDeleted;
                //client.MessageUpdated += Client_MessageUpdated; < moved to Message Handler
                client.RoleCreated += Client_RoleCreated;
                client.RoleDeleted += Client_RoleDeleted;
                client.RoleUpdated += Client_RoleUpdated;
                client.UserUnbanned += Client_UserUnbanned;
                client.UserUpdated += Client_UserUpdated;
                client.GuildMemberUpdated += Client_GuildMemberUpdated;
                client.UserVoiceStateUpdated += Client_UserVoiceStateUpdated;
                client.MessagesBulkDeleted += Client_MessagesBulkDeleted;
                client.UserJoined += Client_UserJoined;
                client.UserLeft += Client_UserLeft;
                client.InviteCreated += Client_InviteCreated;
                client.InviteDeleted += Client_InviteDeleted;
            });
            _loggingThread.Start();
        }

        private async Task Client_InviteDeleted(Cacheable<SocketGuildInvite, string> arg1, SocketGuild arg2)
        {
            var gs = GuildSettings.Get(arg2.Id);
            if (gs.LogChannel == 0 || !gs.Logging)
                return;
            var logchan = arg2.GetTextChannel(gs.LogChannel);
            if (logchan == null)
                return;

            var em = new EmbedBuilder()
            {
                Title = "⚡ Invite Deleted ⚡",
                Color = Color.Red,
                Description = $"The invite https://discord.com/invite/{arg1.Id} was deleted!",
                
            }.WithCurrentTimestamp();
            if (arg1.HasValue)
            {
                var invite = arg1.Value;
                var channel = invite.Channel.GetType() == typeof(SocketVoiceChannel) ? $"🔊 {invite.Channel.Name}" : $"<#{invite.Channel.Id}>";
                em.Fields = new List<EmbedFieldBuilder>()
                {
                    new EmbedFieldBuilder()
                    {
                        Name = "Invite",
                        Value =
                        $"Expires: {(invite.MaxAge.HasValue ? $"{invite.MaxAge.Value.ToString("h'h 'm'm 's's'")}" : "Never")}\n" +
                        $"Channel: {channel}\n" +
                        $"Max Uses: {(invite.MaxUses.HasValue ? $"{invite.MaxUses.Value}" : "Infinite")}\n" +
                        $"Temporary? {invite.Temporary.CheckOrX()}\n" +
                        $"Creator: {invite.Inviter.Mention}"
                    }
                };
            }
            await logchan.SendMessageAsync("", false, em.Build());
        }

        private async Task Client_InviteCreated(SocketGuildInvite arg)
        {
            var invites = await arg.Guild.GetInvitesAsync();
            var gs = GuildSettings.Get(arg.Guild.Id);
            if (gs.LogChannel == 0 || !gs.Logging)
                return;
            var logchan = arg.Guild.GetTextChannel(gs.LogChannel);
            if (logchan == null)
                return;

            var channel = arg.Channel.GetType() == typeof(SocketVoiceChannel) ? $"🔊 {arg.Channel.Name}" : $"<#{arg.Channel.Id}>";

            await logchan.SendMessageAsync("", false, new EmbedBuilder() 
            {
                Title = "⚡ Invite Created ⚡",
                Color = Color.Green,
                Description = $"{arg.Inviter.Mention} created the invite {arg.Url}",
                Fields = new List<EmbedFieldBuilder>()
                {
                    new EmbedFieldBuilder()
                    {
                        Name = "Invite",
                        Value = 
                        $"Expires: {(arg.MaxAge.HasValue ? $"{(arg.MaxAge.Value.TotalSeconds == 0 ? "Never" : arg.MaxAge.Value.ToString("h'h 'm'm 's's'"))}" : "Never")}\n" +
                        $"Channel: {channel}\n" +
                        $"Max Uses: {(arg.MaxUses.HasValue ? $"{(arg.MaxUses.Value == 0 ? "Infinite" : $"{arg.MaxUses.Value}")}" : "Infinite")}\n" +
                        $"Temporary? {arg.Temporary.CheckOrX()}"
                    }
                }
            }.WithCurrentTimestamp().Build());
        }

        private async Task Client_UserLeft(SocketGuildUser arg)
        {
            var gs = GuildSettings.Get(arg.Guild.Id);
            if (gs.LogChannel == 0 || !gs.Logging)
                return;
            var logchan = arg.Guild.GetTextChannel(gs.LogChannel);
            if (logchan == null)
                return;

            await logchan.SendMessageAsync("", false, new EmbedBuilder()
            {
                Title = "⚡ User Left ⚡",
                Description = $"{arg.Mention} left the server!",
                Color = Color.Red,
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Members: {arg.Guild.MemberCount}",
                    IconUrl = arg.Guild.IconUrl
                }
            }.WithCurrentTimestamp().Build());
        }

        private async Task Client_UserJoined(SocketGuildUser arg)
        {
            var gs = GuildSettings.Get(arg.Guild.Id);
            if (gs.LogChannel == 0 || !gs.Logging)
                return;
            var logchan = arg.Guild.GetTextChannel(gs.LogChannel);
            if (logchan == null)
                return;

            await logchan.SendMessageAsync("", false, new EmbedBuilder() 
            { 
                Title = "⚡ New User ⚡",
                Description = $"{arg.Mention} joined the server!",
                Color = Color.Green,
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Members: {arg.Guild.MemberCount}",
                    IconUrl = arg.Guild.IconUrl
                }
            }.WithCurrentTimestamp().Build());
        }

        private async Task Client_GuildMemberUpdated(SocketGuildUser arg1, SocketGuildUser arg2)
        {
            var gs = GuildSettings.Get(arg1.Guild.Id);
            if (gs.LogChannel == 0 || !gs.Logging)
                return;
            var logchan = arg1.Guild.GetTextChannel(gs.LogChannel);
            if (logchan == null)
                return;
            ulong[]  old = arg1.Roles.Select(x => x.Id).ToArray();
            ulong[] _new = arg2.Roles.Select(x => x.Id).ToArray();
            if (!old.SequenceEqual(_new))
            {
                //role change
                List<SocketRole> RemovedRoles = new List<SocketRole>();
                List<SocketRole> AddedRoles = new List<SocketRole>();
                foreach (var item in arg1.Roles)
                    if (!arg2.Roles.Contains(item))
                        RemovedRoles.Add(item);
                foreach (var item in arg2.Roles)
                    if (!arg1.Roles.Contains(item))
                        AddedRoles.Add(item);

                List<EmbedFieldBuilder> fields = new List<EmbedFieldBuilder>();
                if (AddedRoles.Any())
                {
                    fields.Add(new EmbedFieldBuilder()
                    {
                        IsInline = true,
                        Name = $"Got Role{(AddedRoles.Count == 1 ? "" : "s")}:",
                        Value = $"{string.Join("\n", AddedRoles.Select(x => x.Mention))}"
                    });
                }
                if (RemovedRoles.Any())
                {
                    fields.Add(new EmbedFieldBuilder()
                    {
                        IsInline = true,
                        Name = $"Lost Role{(RemovedRoles.Count == 1 ? "" : "s")}:",
                        Value = $"{string.Join(",\n", RemovedRoles.Select(x => x.Mention))}"
                    });
                }

                await logchan.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "⚡ User Roles Changed ⚡",
                    Color = Color.Green,
                    Description = $"{arg1.Mention} Roles were changed",
                    Fields = fields,
                    Footer = new EmbedFooterBuilder()
                    {
                        Text = $"User Id {arg1.Id}"
                    }
                }.WithCurrentTimestamp().Build());
            }
            if(arg1.Nickname != arg2.Nickname)
            {
                //nick change
                List<EmbedFieldBuilder> fields = new List<EmbedFieldBuilder>();
                fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Old Nickname:",
                    Value = $"{(arg1.Nickname == null ? "none" : arg1.Nickname)}"
                });
                fields.Add(new EmbedFieldBuilder() 
                { 
                    Name = "New Nickname",
                    Value = $"{(arg2.Nickname == null ? "none" : arg2.Nickname)}"
                });
                await logchan.SendMessageAsync("", false, new EmbedBuilder() 
                {
                    Title = "⚡ User Nickname Changed ⚡",
                    Description = $"{arg1.Mention} Nickname was changed.\n",
                    Color = Color.Green,
                    Fields = fields
                }.Build());

            }
            if(arg1.PremiumSince != arg2.PremiumSince)
            {
                //boost change
                if(!arg1.PremiumSince.HasValue && arg2.PremiumSince.HasValue)
                {
                    //they boosted
                    await logchan.SendMessageAsync("", false, new EmbedBuilder() 
                    {
                        Title = "✨ Server Boosted ✨",
                        Description = $"{arg1.Mention} boosted the server!",
                        Color = new Color(244, 127, 255),
                        Footer = new EmbedFooterBuilder()
                        {
                            Text = $"Total Server Boosts: {arg1.Guild.PremiumSubscriptionCount}",
                            IconUrl = "https://ponyvilleplaza.com/files/img/boost.png"
                        }
                    }.WithCurrentTimestamp().Build());
                }
                if(arg1.PremiumSince.HasValue && !arg2.PremiumSince.HasValue)
                {
                    //there boost expired
                    await logchan.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "✨ Server Boost Lost ✨",
                        Description = $"{arg1.Mention} boost expired!",
                        Color = new Color(244, 127, 255),
                        Footer = new EmbedFooterBuilder()
                        {
                            Text = $"Total Server Boosts: {arg1.Guild.PremiumSubscriptionCount}",
                            IconUrl = "https://ponyvilleplaza.com/files/img/boost.png"
                        }
                    }.WithCurrentTimestamp().Build());
                }
            }
        }

        private async Task Client_UserVoiceStateUpdated(SocketUser arg1, SocketVoiceState arg2, SocketVoiceState arg3)
        {
            var chan = arg2.VoiceChannel == null ? arg3.VoiceChannel : arg2.VoiceChannel;
            if (chan == null)
                return;
            var guild = chan.Guild;
            if (guild == null)
                return;
            var gs = GuildSettings.Get(guild.Id);

            if (arg2.VoiceChannel == null && arg3.VoiceChannel != null)
            {
                //user joined
                if (gs.LogChannel != 0 && gs.Logging)
                {
                    var logchan = guild.GetTextChannel(gs.LogChannel);
                    if (logchan == null)
                        return;
                    var av = arg1.GetAvatarUrl();
                    if (av == null)
                        av = arg1.GetDefaultAvatarUrl();
                    await logchan.SendMessageAsync("", false, new EmbedBuilder() 
                    {
                        Title = $"🔊 User Joined Voice Channel 🔊",
                        Description = $"{arg1.Mention} joined `{chan.Name}`",
                        Color = Color.Teal
                    }.WithCurrentTimestamp().Build());
                }
            }
            if(arg2.VoiceChannel != null && arg3.VoiceChannel == null)
            {
                //user left
                if (gs.LogChannel != 0 && gs.Logging)
                {
                    var logchan = guild.GetTextChannel(gs.LogChannel);
                    if (logchan == null)
                        return;
                    var av = arg1.GetAvatarUrl();
                    if (av == null)
                        av = arg1.GetDefaultAvatarUrl();
                    await logchan.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = $"🔊 User Left Voice Channel 🔊",
                        Description = $"{arg1.Mention} Left `{chan.Name}`",
                        Color = Color.Orange
                    }.WithCurrentTimestamp().Build());
                }
            }
            if((arg2.VoiceChannel != null && arg3.VoiceChannel != null) && arg2.VoiceChannel.Id != arg3.VoiceChannel.Id)
            {
                //switched
                if (gs.LogChannel != 0 && gs.Logging)
                {
                    var logchan = guild.GetTextChannel(gs.LogChannel);
                    if (logchan == null)
                        return;
                    var av = arg1.GetAvatarUrl();
                    if (av == null)
                        av = arg1.GetDefaultAvatarUrl();
                    await logchan.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = $"🔊 User Switched Voice Channel 🔊",
                        Description = $"{arg1.Mention} switched from `{arg2.VoiceChannel.Name}` to `{arg3.VoiceChannel.Name}`",
                        Color = Color.Purple
                    }.WithCurrentTimestamp().Build());
                }
            }
            if(!arg2.IsMuted && arg3.IsMuted)
            {
                //user got server muted lol
                if (gs.LogChannel != 0 && gs.Logging)
                {
                    var logchan = guild.GetTextChannel(gs.LogChannel);
                    if (logchan == null)
                        return;
                    var av = arg1.GetAvatarUrl();
                    if (av == null)
                        av = arg1.GetDefaultAvatarUrl();
                    await logchan.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = $"🔊 User Was Server Muted 🔊",
                        Description = $"{arg1.Mention} was server muted in `{chan.Name}`",
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build());
                }
            }
            if(arg2.IsMuted && !arg3.IsMuted)
            {
                //user was unmuted
                if (gs.LogChannel != 0 && gs.Logging)
                {
                    var logchan = guild.GetTextChannel(gs.LogChannel);
                    if (logchan == null)
                        return;
                    var av = arg1.GetAvatarUrl();
                    if (av == null)
                        av = arg1.GetDefaultAvatarUrl();
                    await logchan.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = $"🔊 User Was Server Un-Muted 🔊",
                        Description = $"{arg1.Mention} was server unmuted in `{chan.Name}`",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                }
            }
            if(!arg2.IsDeafened && arg3.IsDeafened)
            {
                //user was server deafened
                if (gs.LogChannel != 0 && gs.Logging)
                {
                    var logchan = guild.GetTextChannel(gs.LogChannel);
                    if (logchan == null)
                        return;
                    var av = arg1.GetAvatarUrl();
                    if (av == null)
                        av = arg1.GetDefaultAvatarUrl();
                    await logchan.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = $"🔊 User Was Server Deafened 🔊",
                        Description = $"{arg1.Mention} was server deafened in `{chan.Name}`",
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build());
                }
            }
            if(arg2.IsDeafened && !arg3.IsDeafened)
            {
                //user was undeafened
                if (gs.LogChannel != 0 && gs.Logging)
                {
                    var logchan = guild.GetTextChannel(gs.LogChannel);
                    if (logchan == null)
                        return;
                    var av = arg1.GetAvatarUrl();
                    if (av == null)
                        av = arg1.GetDefaultAvatarUrl();
                    await logchan.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = $"🔊 User Was Server Un-Deafened 🔊",
                        Description = $"{arg1.Mention} was server undeafened in `{chan.Name}`",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                }
            }
        }

        private async Task Client_UserUpdated(SocketUser before, SocketUser after)
        {
            //SocketGuild arg2 = (SocketGuild)Context.Guild;
            foreach(var mu in after.MutualGuilds)
            {
                var gs = GuildSettings.Get(mu.Id);
                if (gs.LogChannel != 0 && gs.Logging)
                {
                    var logchan = mu.GetTextChannel(gs.LogChannel);
                    if (logchan != null)
                    {
                        var fields = new List<EmbedFieldBuilder>();
                        string url = "";
                        if (before.Username != after.Username)
                        {
                            fields.Add(new EmbedFieldBuilder()
                            {
                                Name = "Username Changed",
                                Value = $"> **Old Username:**\n> {before.Username}\n> **New Username**\n> {after.Username}"
                            });
                        }
                        if (before.Discriminator != after.Discriminator)
                        {
                            fields.Add(new EmbedFieldBuilder()
                            {
                                Name = "Discriminator Changed",
                                Value = $"> **Old Discriminator:**\n> {before.Discriminator}\n> **New Discriminator**\n> {after.Discriminator}"
                            });
                        }
                        if (before.AvatarId != after.AvatarId)
                        {
                            fields.Add(new EmbedFieldBuilder()
                            {
                                Name = "Avatar Changed",
                                Value = $"> **Old Avatar:**\n> [Link]({before.GetAvatarUrl()})\n> **New Avatar**\n> [Link]({after.GetAvatarUrl()})"
                            });
                            var old = before.GetAvatarUrl(ImageFormat.Auto, 256);
                            if (old == null)
                                old = before.GetDefaultAvatarUrl();
                            var _new = after.GetAvatarUrl(ImageFormat.Auto, 256);
                            try
                            {
                                url = await ProfileChangeHelper.BuildImage(old, _new);
                            }
                            catch { }
                        }

                        await logchan.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "⚡ User Updated ⚡",
                            Color = Color.Orange,
                            Description = $"<@{after.Id}> was Updated",
                            Fields = fields,
                            ImageUrl = url == "" ? null : url,
                            Footer = new EmbedFooterBuilder() 
                            {
                                Text = $"User ID: {after.Id}"
                            }
                            
                        }.WithCurrentTimestamp().Build());
                    }
                }
            }
        }
        
        private async Task Client_MessagesBulkDeleted(IReadOnlyCollection<Cacheable<IMessage, ulong>> arg1, ISocketMessageChannel arg2)
        {
            if (arg2.GetType() == typeof(SocketTextChannel))
            {
                var sgc = arg2 as SocketTextChannel;
                var gs = GuildSettings.Get(sgc.Guild.Id);
                if (gs.LogChannel != 0 && gs.Logging)
                {
                    var logchan = sgc.Guild.GetTextChannel(gs.LogChannel);
                    if (logchan != null)
                    {
                        await logchan.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "⚡ Bulk Messages Deleted ⚡",
                            Description = $"{arg1.Count} Messages were deleted in {sgc.Mention}",
                            Color = Color.Orange
                        }.WithCurrentTimestamp().Build());
                    }
                }
            }
        }

        private async Task Client_UserUnbanned(SocketUser arg1, SocketGuild arg2)
        {
            var gs = GuildSettings.Get(arg2.Id);
            if (gs.LogChannel != 0 && gs.Logging)
            {
                var logchan = arg2.GetTextChannel(gs.LogChannel);
                if (logchan != null)
                {
                    await logchan.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "⚡ User Unbanned ⚡",
                        Description = $"The user {arg1.ToString()} was Unbanned",
                        Color = Color.DarkMagenta
                    }.WithCurrentTimestamp().Build());
                }
            }
        }

        private async Task Client_RoleUpdated(SocketRole arg1, SocketRole arg2)
        {
            var gs = GuildSettings.Get(arg2.Guild.Id);
            if (gs.LogChannel != 0 && gs.Logging)
            {
                var logchan = arg2.Guild.GetTextChannel(gs.LogChannel);
                string body = "";
                if (arg1.Name != arg2.Name)
                    body += $"__Name Changed:__\n> Old Name:  {arg1.Name}\n> New Name: {arg2.Name}\n__\n__\n";
                if(!arg1.Permissions.Equals(arg2.Permissions))
                {
                    body += "__Permission Changed:__\n";
                    var type = typeof(Discord.GuildPermissions);
                    var props = type.GetProperties();
                    foreach(var prop in props.Where(x => x.PropertyType == typeof(bool)))
                    {
                        bool oldVal = (bool)prop.GetValue(arg1.Permissions);
                        bool newVal = (bool)prop.GetValue(arg2.Permissions);
                        if (oldVal != newVal)
                            body += $"> **{prop.Name}**\n> Old Value:  {(oldVal ? "✅" : "❌")}\n> New Value: {(newVal ? "✅" : "❌")}\n\n";
                    }
                    body += "\n";
                }
                if (arg1.Color != arg2.Color)
                    body += $"__Color Changed:__\n> Old Color:  {arg1.Color.ToString()}\n> New Color: {arg2.Color.ToString()}\n__\n__\n";
                if (arg1.IsMentionable != arg2.IsMentionable)
                    body += $"__Mention Changed:__\n> Old Value:  {(arg1.IsMentionable ? "✅" : "❌")}\n> New Value: {(arg2.IsMentionable ? "✅" : "❌")}\n__\n__\n";
                if (arg1.IsHoisted != arg2.IsHoisted)
                    body += $"__Hoisted Changed:__\n> Old Value:  {(arg1.IsHoisted ? "✅" : "❌")}\n> New Value: {(arg2.IsHoisted ? "✅" : "❌")}\n__\n__\n";
                if (body == "")
                    return;
                if (logchan != null)
                {
                    await logchan.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "⚡ Role Updated ⚡",
                        Description = $"The role {arg2.Mention} was Updated.",
                        Fields = new List<EmbedFieldBuilder>()
                        {
                            new EmbedFieldBuilder()
                            {
                                Name = "Changes:",
                                Value = body
                            }
                        },
                        Color = Color.Orange
                    }.WithCurrentTimestamp().Build());;
                }
            }
        }

        private async Task Client_RoleDeleted(SocketRole arg)
        {
            var gs = GuildSettings.Get(arg.Guild.Id);
            if (gs.LogChannel != 0 && gs.Logging)
            {
                var logchan = arg.Guild.GetTextChannel(gs.LogChannel);
                if (logchan != null)
                {
                    await logchan.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "⚡ Role Deleted ⚡",
                        Description = $"The role {arg.Name} was Deleted.",
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build());
                }
            }
        }

        private async Task Client_RoleCreated(SocketRole arg)
        {
            var gs = GuildSettings.Get(arg.Guild.Id);
            if (gs.LogChannel != 0 && gs.Logging)
            {
                var logchan = arg.Guild.GetTextChannel(gs.LogChannel);
                if (logchan != null)
                {
                    await logchan.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "⚡ Role Created ⚡",
                        Description = $"The role {arg.Mention} was created.",
                        Color = Color.Blue
                    }.WithCurrentTimestamp().Build());
                }
            }
        }

        public async Task Client_MessageUpdated(Cacheable<IMessage, ulong> arg1, SocketMessage arg2, ISocketMessageChannel arg3)
        {
            if (arg2.Author.IsBot || arg2.Author.IsWebhook)
                return;
            if (arg3.GetType() == typeof(SocketTextChannel))
            {
                var sgc = arg3 as SocketTextChannel;
                var gs = GuildSettings.Get(sgc.Guild.Id);
                if (gs.LogChannel != 0 && gs.Logging)
                {
                    if (arg2.Id == gs.LogChannel)
                        return;
                    var logchan = sgc.Guild.GetTextChannel(gs.LogChannel);
                    if (arg3.Id == logchan.Id)
                        return;
                    if (logchan == null)
                        return;

                    if (MessageHelper.MessageExists(arg1.Id))
                    {
                        var msg = await MessageHelper.GetMessageAsync(arg1.Id);
                        if(msg.Content != arg2.Content)
                        {
                            await logchan.SendMessageAsync("", false, new EmbedBuilder
                            {
                                Title = "⚡ Message Edited ⚡",
                                Description = $"Message Edited in <#{arg2.Channel.Id}>",
                                Fields = new List<EmbedFieldBuilder>()
                                {
                                    new EmbedFieldBuilder()
                                    {
                                        Name = "Author:",
                                        Value = arg2.Author.Mention
                                    },
                                    new EmbedFieldBuilder()
                                    {
                                        Name = "Old Content:",
                                        Value = msg.Content != null ? msg.Content != "" ? msg.Content : "{no content}" : "{no content}"
                                    },
                                    new EmbedFieldBuilder()
                                    {
                                        Name = "New Content:",
                                        Value = arg2.Content != null ? arg2.Content != "" ? arg2.Content : "{no content}" : "{no content}"
                                    }
                                },
                                Color = Color.Orange,
                                Footer = new EmbedFooterBuilder
                                {
                                    Text = $"Message Id: {arg2.Id}"
                                }
                            }.WithCurrentTimestamp().Build());
                            return;
                        }
                        else if(arg2.IsPinned != msg.IsPinned)
                        {
                            if (arg2.IsPinned)
                            {
                                await logchan.SendMessageAsync("", false, new EmbedBuilder
                                {
                                    Title = "📌 Message Pinned 📌",
                                    Description = $"Message Pinned in <#{arg2.Channel.Id}>",
                                    Fields = new List<EmbedFieldBuilder>()
                                {
                                    new EmbedFieldBuilder()
                                    {
                                        Name = "Author:",
                                        Value = arg2.Author.Mention
                                    },
                                    new EmbedFieldBuilder()
                                    {
                                        Name = "Content:",
                                        Value = arg2.Content != null ? arg2.Content != "" ? arg2.Content : "{no content}" : "{no content}"
                                    }
                                },
                                    Color = Color.Green,
                                    Footer = new EmbedFooterBuilder
                                    {
                                        Text = $"Message Id: {arg2.Id}"
                                    }
                                }.WithCurrentTimestamp().Build());
                                return;
                            }
                            else
                            {
                                await logchan.SendMessageAsync("", false, new EmbedBuilder
                                {
                                    Title = "📌 Message Unpinned 📌",
                                    Description = $"Message Unpinned in <#{arg2.Channel.Id}>",
                                    Fields = new List<EmbedFieldBuilder>()
                                {
                                    new EmbedFieldBuilder()
                                    {
                                        Name = "Author:",
                                        Value = arg2.Author.Mention
                                    },
                                    new EmbedFieldBuilder()
                                    {
                                        Name = "Content:",
                                        Value = arg2.Content != null ? arg2.Content != "" ? arg2.Content : "{no content}" : "{no content}"
                                    }
                                },
                                    Color = Color.Red,
                                    Footer = new EmbedFooterBuilder
                                    {
                                        Text = $"Message Id: {arg2.Id}"
                                    }
                                }.WithCurrentTimestamp().Build());
                                return;
                            }
                        }
                    }
                    else if(arg1.HasValue)
                    {
                        var msg = arg1.Value;
                        if (msg.Content != arg2.Content)
                        {
                            await logchan.SendMessageAsync("", false, new EmbedBuilder
                            {
                                Title = "⚡ Message Edited ⚡",
                                Description = $"Message Edited in <#{arg2.Channel.Id}>",
                                Fields = new List<EmbedFieldBuilder>()
                                {
                                    new EmbedFieldBuilder()
                                    {
                                        Name = "Author:",
                                        Value = arg2.Author.Mention
                                    },
                                    new EmbedFieldBuilder()
                                    {
                                        Name = "Old Content:",
                                        Value = msg.Content != null ? msg.Content != "" ? msg.Content : "{no content}" : "{no content}"
                                    },
                                    new EmbedFieldBuilder()
                                    {
                                        Name = "New Content:",
                                        Value = arg2.Content != null ? arg2.Content != "" ? arg2.Content : "{no content}" : "{no content}"
                                    }
                                },
                                Color = Color.Orange,
                                Footer = new EmbedFooterBuilder
                                {
                                    Text = $"Message Id: {arg2.Id}"
                                }
                            }.WithCurrentTimestamp().Build());
                            return;
                        }
                        else if (arg2.IsPinned != msg.IsPinned)
                        {
                            if (arg2.IsPinned)
                            {
                                await logchan.SendMessageAsync("", false, new EmbedBuilder
                                {
                                    Title = "📌 Message Pinned 📌",
                                    Description = $"Message Pinned in <#{arg2.Channel.Id}>",
                                    Fields = new List<EmbedFieldBuilder>()
                                {
                                    new EmbedFieldBuilder()
                                    {
                                        Name = "Author:",
                                        Value = arg2.Author.Mention
                                    },
                                    new EmbedFieldBuilder()
                                    {
                                        Name = "Content:",
                                        Value = arg2.Content != null ? arg2.Content != "" ? arg2.Content : "{no content}" : "{no content}"
                                    }
                                },
                                    Color = Color.Green,
                                    Footer = new EmbedFooterBuilder
                                    {
                                        Text = $"Message Id: {arg2.Id}"
                                    }
                                }.WithCurrentTimestamp().Build());
                                return;
                            }
                            else
                            {
                                await logchan.SendMessageAsync("", false, new EmbedBuilder
                                {
                                    Title = "📌 Message Unpinned 📌",
                                    Description = $"Message Unpinned in <#{arg2.Channel.Id}>",
                                    Fields = new List<EmbedFieldBuilder>()
                                {
                                    new EmbedFieldBuilder()
                                    {
                                        Name = "Author:",
                                        Value = arg2.Author.Mention
                                    },
                                    new EmbedFieldBuilder()
                                    {
                                        Name = "Content:",
                                        Value = arg2.Content != null ? arg2.Content != "" ? arg2.Content : "{no content}" : "{no content}"
                                    }
                                },
                                    Color = Color.Red,
                                    Footer = new EmbedFooterBuilder
                                    {
                                        Text = $"Message Id: {arg2.Id}"
                                    }
                                }.WithCurrentTimestamp().Build());
                                return;
                            }
                        }
                    }
                }
            }
        }

        private async Task Client_MessageDeleted(Cacheable<IMessage, ulong> arg1, ISocketMessageChannel arg2)
        {
            if(arg2.GetType() == typeof(SocketTextChannel))
            {
                var sgc = arg2 as SocketTextChannel;
                var gs = GuildSettings.Get(sgc.Guild.Id);

                if (gs.LogChannel != 0 && gs.Logging)
                {
                    if (arg2.Id == gs.LogChannel)
                        return;
                    var logchan = sgc.Guild.GetTextChannel(gs.LogChannel);
                    if (logchan != null)
                    {
                        var fields = new List<EmbedFieldBuilder>();
                        if (MessageHelper.MessageExists(arg1.Id))
                        {
                            var msg = await MessageHelper.GetMessageAsync(arg1.Id);
                            if (msg == null) return;
                            fields.Add(new EmbedFieldBuilder()
                            {
                                Name = "Author:",
                                Value = $"<@{msg.Author.Id}>"
                            });
                            fields.Add(new EmbedFieldBuilder()
                            {
                                Name = "Message:",
                                Value = msg.Content == null ? "{no content}" : msg.Content == "" ? "{no content}" : msg.Content
                            });
                            if (msg.Attachments.Any())
                            {
                                fields.Add(new EmbedFieldBuilder()
                                {
                                    Name = "Attachments:",
                                    Value = string.Join("\n", msg.Attachments)
                                });
                            }
                        }

                        await logchan.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "⚡ Message Deleted ⚡",
                            Description = $"Message deleted in {sgc.Mention}",
                            Fields = fields,
                            Color = Color.Red,
                            Footer = new EmbedFooterBuilder()
                            {
                                Text = $"Message ID: {arg1.Id}"
                            }
                        }.WithCurrentTimestamp().Build());
                    }
                }
            }
        }
        private async Task Client_GuildUpdated(SocketGuild arg1, SocketGuild arg2)
        {
            var gs = GuildSettings.Get(arg2.Id);
            if (gs.LogChannel != 0 && gs.Logging)
            {
                string url = "";
                string text = "Guild got updated!\n(We were unable to find what was, though)";
                string Id = "";
                var logchan = arg2.GetTextChannel(gs.LogChannel);
                if (!arg1.Emotes.All(x => arg2.Emotes.Contains(x)))
                {
                    if (arg1.Emotes.Count > arg2.Emotes.Count)
                    {
                        var removedEmote = arg1.Emotes.First(x => !arg2.Emotes.Contains(x));
                        string animated = "stationary";
                        if (removedEmote.Animated)
                        {
                            animated = "animated";
                        }
                        text = $"The {animated} emote {removedEmote.Name} was removed";
                        url = removedEmote.Url;
                        Id = $"Emote ID: {removedEmote.Id}";

                    } else if (arg2.Emotes.Count < arg1.Emotes.Count)
                    {
                        var addedEmote = arg2.Emotes.First(x => !arg1.Emotes.Contains(x));
                        string animated = "stationary";
                        if (addedEmote.Animated)
                        {
                            animated = "animated";
                        }
                        text = $"The {animated} emote `:{addedEmote.Name}:` was added";
                        url = addedEmote.Url;
                        Id = $"Emote ID: {addedEmote.Id}";
                    }
                    else
                    {
                        var newEmote = arg2.Emotes.First(x => !arg1.Emotes.Any(y => y.Name == x.Name));
                        var oldEmoteName = arg1.Emotes.First(x => x.Url == newEmote.Url).Name;
                        text = $"The emote `:{oldEmoteName}:` was renamed to `:{newEmote.Name}:`";
                        url = newEmote.Url;
                        Id = $"Emote ID: {newEmote.Id}";
                    }
                }
                if (arg1.IconId != arg2.IconId)
                {
                    var old = arg1.IconUrl;
                    var _new = arg2.IconUrl;
                    text = $"The Icon of the guild was Updated\nPrevious icon: [link]({old})\nNew icon: [link]({_new})";
                    Console.WriteLine($"OLD: {old} NEW {_new}");
                    try
                    {
                        url = await ProfileChangeHelper.BuildImage(old, _new, false);
                    }
                    catch (Exception e){
                        Console.WriteLine(e);
                    }
                }
                if (arg1.Name != arg2.Name)
                {
                    text = $"The server name has been updated from {arg1.Name} to {arg2.Name}";
                }
                if (logchan != null)
                {
                    string footerTxt = "";
                    if (Id == "")
                    {
                        footerTxt = $"Guild Id: {arg1.Id}";
                    } else
                    {
                        footerTxt = Id;
                    }
                    await logchan.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "⚡ Guild Updated ⚡",
                        Description = text,
                        Color = Color.Green,
                        ImageUrl = url,
                        Footer = new EmbedFooterBuilder { Text = footerTxt }
                    }.WithCurrentTimestamp().Build());
                }
            }
        }

        private async Task Client_ChannelUpdated(SocketChannel arg1, SocketChannel arg2)
        {
            if (arg1.GetType() == typeof(SocketGuildChannel))
            {
                var Bgcn = arg1 as SocketGuildChannel;
                var Agcn = arg1 as SocketGuildChannel;
                var gs = GuildSettings.Get(Bgcn.Guild.Id);
                if (gs.LogChannel != 0 && gs.Logging)
                {
                    var logchan = Bgcn.Guild.GetTextChannel(gs.LogChannel);
                    if (logchan != null)
                    {
                        if(Bgcn.Name != Agcn.Name)
                        {
                            await logchan.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "⚡ Channel Updated ⚡",
                                Description = $"{(Bgcn.GetType() == typeof(SocketTextChannel) ? "⌨️" : Bgcn.GetType() == typeof(SocketVoiceChannel) ? "🔊" : "")}{Bgcn.Name} is now {Agcn.Name}",
                                Color = Color.Orange
                            }.WithCurrentTimestamp().Build());
                            return;
                        }
                        if (Bgcn.PermissionOverwrites != Agcn.PermissionOverwrites)
                        {
                            
                            await logchan.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "⚡ Channel Updated ⚡",
                                Description = $"{(Bgcn.GetType() == typeof(SocketTextChannel) ? "⌨️" : Bgcn.GetType() == typeof(SocketVoiceChannel) ? "🔊" : "")}{Bgcn.Name} Permissions got updated. (More in depth descriptions coming soon)",
                                Color = Color.Orange
                            }.WithCurrentTimestamp().Build());
                        }

                    }
                }
            }
        }

        private async Task Client_ChannelDestroyed(SocketChannel arg)
        {
            if (arg as SocketGuildChannel != null)
            {
                var gcn = arg as SocketGuildChannel;
                var gs = GuildSettings.Get(gcn.Guild.Id);
                if (gs.LogChannel != 0 && gs.Logging)
                {
                    var logchan = gcn.Guild.GetTextChannel(gs.LogChannel);
                    if (logchan != null)
                    {
                        await logchan.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "⚡ Channel Deleted ⚡",
                            Description = $"{(gcn.GetType() == typeof(SocketTextChannel) ? "💻 Text channel " : gcn.GetType() == typeof(SocketVoiceChannel) ? "🔊 Voice Channel " : "")}`{gcn.Name}` was deleted!!!",
                            Color = Color.Red,
                            Footer = new EmbedFooterBuilder
                            {
                                Text = $"Channel Id: {gcn.Id}"
                            }
                        }.WithCurrentTimestamp().Build());
                    }
                }
            }
        }

        private async Task Client_ChannelCreated(SocketChannel arg)
        {
            if(arg as SocketGuildChannel != null)
            {
                var gcn = arg as SocketGuildChannel;
                var gs = GuildSettings.Get(gcn.Guild.Id);
                if(gs.LogChannel != 0 && gs.Logging)
                {
                    var logchan = gcn.Guild.GetTextChannel(gs.LogChannel);
                    if(logchan != null)
                    {
                        await logchan.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "⚡ Channel Created ⚡",
                            Description = $"{(gcn.GetType() == typeof(SocketTextChannel) ? "💻 Text channel " : gcn.GetType() == typeof(SocketVoiceChannel) ? "🔊 Voice Channel " : "")}`{gcn.Name}` was created!!!",
                            Color = Color.Blue,
                            Footer = new EmbedFooterBuilder
                            {
                                Text = $"Channel Id: {gcn.Id}"
                            }
                        }.WithCurrentTimestamp().Build());
                    }
                }
            }
        }
    }
}
