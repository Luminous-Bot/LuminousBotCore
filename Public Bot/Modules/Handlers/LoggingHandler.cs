using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Public_Bot.Modules.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
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
            client.UserUnbanned += Client_UserUnbanned;
            client.UserUpdated += Client_UserUpdated;
            client.MessagesBulkDeleted += Client_MessagesBulkDeleted;
            
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
                        }

                        await logchan.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "⚡ User Updated ⚡",
                            Color = Color.Orange,
                            Description = $"<@{after.Id}> was Updated",
                            Fields = fields
                        }.Build());
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
                    var type = typeof(GuildPermissions);
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
                    body += $"__Hoisted Changed:__\n> Old Value:  {(arg1.IsHoisted ? "✅" : "❌")}\n> New Value: {(arg1.IsHoisted ? "✅" : "❌")}\n__\n__\n";
                if (body == "")
                    body = "Can't find changes :(";
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
                    if (logchan != null)
                    {
                        List<EmbedFieldBuilder> fields = new List<EmbedFieldBuilder>();
                        
                        if(arg1.HasValue)
                        {
                            var oval = "{no content}";
                            if (arg1.Value.Content != null)
                                if (arg1.Value.Content != "")
                                    oval = arg1.Value.Content;
                            fields.Add(new EmbedFieldBuilder()
                            {
                                Name = "Old Message:",
                                Value = oval,
                                IsInline = true,

                            });
                        }
                        else
                        {
                            if (MessageHelper.MessageExists(arg1.Id))
                            {
                                var msg = await MessageHelper.GetMessageAsync(arg1.Id);
                                fields.Add(new EmbedFieldBuilder()
                                {
                                    Name = "Old Message:",
                                    Value = msg.Content,
                                    IsInline = true,
                                });
                            }
                        }
                        var val = "{no content}";
                        if (arg2.Content != null)
                            if (arg2.Content != "")
                                val = arg2.Content;
                        fields.Add(new EmbedFieldBuilder()
                        {
                            Name = "New Message:",
                            Value = val,
                            IsInline = true,

                        });

                        if (fields[0].Name == "{no content}" && fields[1].Name == "{no content}")
                            return;
                        if (fields[0].Value == fields[1].Value)
                            return;

                        fields.Add(new EmbedFieldBuilder()
                        {
                            Name = "Author:",
                            Value = arg2.Author.Mention,
                            //IsInline = true,
                        });
                        await logchan.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "⚡ Message Edited ⚡",
                            Description = $"Message edited in {sgc.Mention}",
                            Fields = fields,
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
                                Value = msg.Content == null ? "{no content}" : msg.Content == "" ? "{no content}" : ""
                            });
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
