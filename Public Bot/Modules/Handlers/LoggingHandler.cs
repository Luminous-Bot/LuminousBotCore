using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot.Modules.Handlers
{
    [DiscordHandler]
    class LoggingHandler : ModuleBase
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
            client.UserUnbanned += Client_UserUnbanned;
            // client.UserUpdated += Client_UserUpdated;
            client.MessagesBulkDeleted += Client_MessagesBulkDeleted;
        }

        /**private async Task Client_UserUpdated(SocketUser before, SocketUser after)
        {
            SocketGuild arg2 = (SocketGuild)Context.Guild;
            var gs = GuildSettings.Get(arg2.Id);
            if (gs.LogChannel != 0 && gs.Logging)
            {
                var logchan = arg2.GetTextChannel(gs.LogChannel);
                if (logchan != null)
                {

                    if (before.Username != after.Username)
                    {
                        EmbedBuilder builder = new EmbedBuilder();

                        builder.WithTitle("⚡ User Updated ⚡");
                        builder.WithDescription($"**Before**: `{before}`   **After**: `{after}`");
                        builder.WithColor(Color.Green);

                        await logchan.SendMessageAsync("", false, builder.Build());
                    }
                }
            }
        }
        **/

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

        private async Task Client_RoleUpdated(SocketRole arg1, SocketRole arg2)
        {
            var gs = GuildSettings.Get(arg2.Guild.Id);
            if (gs.LogChannel != 0 && gs.Logging)
            {
                var logchan = arg2.Guild.GetTextChannel(gs.LogChannel);
                string body = "Can't find changes :(";
                if (arg1.Name != arg2.Name)
                    body = $"Name Changed:\n> Old Name: {arg1.Name}\n> New Name: {arg2.Name}\n\n";
                if(!arg1.Permissions.Equals(arg2.Permissions))
                {
                    body = "Permission Changed:\n";
                    var type = typeof(SocketRole);
                    var props = type.GetProperties();
                    foreach(var prop in props.Where(x => x.PropertyType == typeof(bool)))
                    {
                        bool oldVal = (bool)prop.GetValue(arg1);
                        bool newVal = (bool)prop.GetValue(arg2);
                        if (oldVal != newVal)
                            body += $"> **{prop.Name}**\n> Old Value: {(oldVal ? "✅" : "❌")}\n> New Value: {(newVal ? "✅" : "❌")}\n";
                    }
                    body += "\n";
                }
                if (arg1.Color != arg2.Color)
                    body += $"Color Changed:\n> Old Color: {arg1.Color.ToString()}\n> New Color: {arg2.Color.ToString()}\n\n";
                if (arg1.IsMentionable != arg2.IsMentionable)
                    body += $"Mention Changed:\n> Old Value: {(arg1.IsMentionable ? "✅" : "❌")}\n> New Value: {(arg2.IsMentionable ? "✅" : "❌")}\n\n";
                if (arg1.IsHoisted != arg2.IsHoisted)
                    body += $"Hoisted Changed:\n> Old Value: {(arg1.IsHoisted)}\n> New Value: {(arg1.IsHoisted ? "✅" : "❌")}";
                
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

        private async Task Client_MessageUpdated(Cacheable<IMessage, ulong> arg1, SocketMessage arg2, ISocketMessageChannel arg3)
        {
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
                    if (arg2.Id == gs.LogChannel)
                        return;
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
                                    //IsInline = true,
                                },
                                new EmbedFieldBuilder()
                                {
                                    Name = "Message:",
                                    Value = arg1.Value.Content == null || arg1.Value.Content == "" ? "__\n__" : arg1.Value.Content,
                                    //IsInline = true,
                                }
                            } : new List<EmbedFieldBuilder>(),
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
