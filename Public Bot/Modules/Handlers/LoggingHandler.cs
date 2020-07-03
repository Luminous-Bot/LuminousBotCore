using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot.Modules.Handlers
{
    [DiscordHandler]
    class LoggingHandler
    {
        public static DiscordShardedClient client;
        public LoggingHandler(DiscordShardedClient c)
        {
            client = c;
            //subscribe
            client.ChannelCreated += Client_ChannelCreated;
            client.ChannelDestroyed += Client_ChannelDestroyed;
            client.ChannelUpdated += Client_ChannelUpdated;
            client.GuildUpdated += Client_GuildUpdated;
            client.MessageDeleted += Client_MessageDeleted;
            client.MessageUpdated += Client_MessageUpdated;
            client.RoleCreated += Client_RoleCreated;
            client.RoleDeleted += Client_RoleDeleted;
            client.RoleUpdated += Client_RoleUpdated;
            client.UserBanned += Client_UserBanned;
            client.UserUnbanned += Client_UserUnbanned;
            client.MessagesBulkDeleted += Client_MessagesBulkDeleted;
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
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build());
                }
            }
        }

        private async Task Client_UserBanned(SocketUser arg1, SocketGuild arg2)
        {
            var gs = GuildSettings.Get(arg2.Id);
            if (gs.LogChannel != 0 && gs.Logging)
            {
                var logchan = arg2.GetTextChannel(gs.LogChannel);
                if (logchan != null)
                {
                    await logchan.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "⚡ User Banned ⚡",
                        Description = $"The user {arg1.ToString()} was Banned",
                        Color = Color.Red
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
                if (logchan != null)
                {
                    await logchan.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "⚡ Role Updated ⚡",
                        Description = $"The role {arg2.Mention} was Updated. (More in depth descriptions coming soon)",
                        Color = Color.Orange
                    }.WithCurrentTimestamp().Build());
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

        private async Task Client_MessageUpdated(Cacheable<IMessage, ulong> arg1, SocketMessage arg2, ISocketMessageChannel arg3)
        {
            if (arg3.GetType() == typeof(SocketTextChannel))
            {
                var sgc = arg3 as SocketTextChannel;
                var gs = GuildSettings.Get(sgc.Guild.Id);
                if (gs.LogChannel != 0 && gs.Logging)
                {
                    var logchan = sgc.Guild.GetTextChannel(gs.LogChannel);
                    if (arg3.Id == logchan.Id)
                        return;
                    if (logchan != null)
                    {
                        if (arg1.HasValue)
                            if (arg1.Value.Embeds.Count > 0)
                                return;
                        if (arg2.Embeds.Count > 0)
                            return;
                        await logchan.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "⚡ Message Edited ⚡",
                            Description = $"Message edited in {sgc.Mention}",
                            Fields = arg1.HasValue ? new List<EmbedFieldBuilder>()
                            {
                                new EmbedFieldBuilder()
                                {
                                    Name = "Author:",
                                    Value = arg1.Value.Author.Mention,
                                    IsInline = true,
                                },
                                new EmbedFieldBuilder()
                                {
                                    Name = "Old Message:",
                                    Value = arg1.Value.Content,                                   
                                    IsInline = true,

                                },
                                new EmbedFieldBuilder()
                                {
                                    Name = "New Message:",
                                    Value = arg2.Content,
                                    IsInline = true,

                                }
                            } : new List<EmbedFieldBuilder>() 
                            {
                                new EmbedFieldBuilder()
                                {
                                    Name = "Author:",
                                    Value = arg2.Author.Mention,
                                    IsInline = true,
                                },
                                new EmbedFieldBuilder()
                                {
                                    Name = "New Message:",
                                    Value = arg2.Content == null ? "{no content}" : arg2.Content,
                                    IsInline = true,

                                }
                            },
                            Color = Color.Orange
                        }.WithCurrentTimestamp().Build());
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
                    var logchan = sgc.Guild.GetTextChannel(gs.LogChannel);
                    if (logchan != null)
                    {
                        await logchan.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "⚡ Message Deleted ⚡",
                            Description = $"Message deleted in {sgc.Mention}",
                            Fields = arg1.HasValue ? new List<EmbedFieldBuilder>()
                            {
                                new EmbedFieldBuilder()
                                {
                                    Name = "Author:",
                                    Value = arg1.Value.Author.Mention,
                                    IsInline = true,
                                },
                                new EmbedFieldBuilder()
                                {
                                    Name = "Message:",
                                    Value = arg1.Value.Content,
                                    IsInline = true,
                                }
                            } : new List<EmbedFieldBuilder>(),
                            Color = Color.Red
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
                var logchan = arg2.GetTextChannel(gs.LogChannel);
                if (logchan != null)
                {
                    await logchan.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "⚡ Guild Updated ⚡",
                        Description = $"{arg2.Name} got updated (More in depth descriptions coming soon)",
                        Color = Color.Green
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
            if (arg.GetType() == typeof(SocketGuildChannel))
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
                            Description = $"{(gcn.GetType() == typeof(SocketTextChannel) ? "⌨️" : gcn.GetType() == typeof(SocketVoiceChannel) ? "🔊" : "")}{gcn.Name}",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                    }
                }
            }
        }

        private async Task Client_ChannelCreated(SocketChannel arg)
        {
            if(arg.GetType() == typeof(SocketGuildChannel))
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
                            Description = $"{(gcn.GetType() == typeof(SocketTextChannel) ? "⌨️" : gcn.GetType() == typeof(SocketVoiceChannel) ? "🔊" : "")}{gcn.Name}",
                            Color = Color.Blue
                        }.WithCurrentTimestamp().Build());
                    }
                }
            }
        }
    }
}
