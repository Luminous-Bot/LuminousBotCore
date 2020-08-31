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
    public class MessageHandler
    {
        public DiscordShardedClient client;
        public MessageHandler(DiscordShardedClient c)
        {
            client = c;
            client.MessageUpdated += UpdateMessage;
#if DEBUG
            Logger.Write("Message Loggin DISABLED OWO", Logger.Severity.State);
#else
            Logger.Write("Message logging enabled", Logger.Severity.State);
            client.MessageReceived += AddMessage;
            
            client.MessageDeleted += DeleteMessage;
#endif
        }

        private async Task DeleteMessage(Discord.Cacheable<Discord.IMessage, ulong> arg1, ISocketMessageChannel arg2)
        {
            if (MessageHelper.MessageExists(arg1.Id))
                await StateService.MutateAsync<Message>(GraphQLParser.GenerateGQLMutation<Message>("deleteMessage", false, null, "", "", ("id", arg1.Id)));
        }

        private async Task UpdateMessage(Discord.Cacheable<Discord.IMessage, ulong> arg1, SocketMessage arg2, ISocketMessageChannel arg3)
        {
            if (arg2.Author.IsBot || arg2.Author.IsWebhook)
                return;
            if (MessageHelper.MessageExists(arg2.Id))
            {
                var msg = MessageHelper.GetMessage(arg2.Id);
                if(msg.IsPinned != arg2.IsPinned)
                {
                    await StateService.MutateAsync<Message>(
                        GraphQLParser.GenerateGQLMutation<Message>(
                            "updateMessagePinned", 
                            false,
                            null, 
                            "", 
                            "", 
                            ("MessageID", arg2.Id), 
                            ("IsPinned", arg2.IsPinned)
                        )
                   );
                }
                else if(msg.Content != arg2.Content)
                    new MessageRevision(arg2);
            }
        }

        private async Task AddMessage(SocketMessage arg)
        {
            if (arg.Channel.GetType() != typeof(SocketTextChannel))
                return;
            if (arg.Author.IsBot || arg.Author.IsWebhook)
                return;
            if (arg.Content == null && !arg.Attachments.Any())
                return;
            if (arg.Content == "" && !arg.Attachments.Any())
                return;
            var channel = (SocketTextChannel)arg.Channel;
            var guild = channel.Guild;
            var gs = GuildSettings.Get(guild.Id);

            if (gs == null)
                return;
            if (!UserCache.UserExists(arg.Author.Id))
                UserCache.CreateUser(arg.Author.Id);

            var g = GuildCache.GetGuild(guild.Id);
            if (!g.GuildMembers.GuildMemberExists(arg.Author.Id))
                g.GuildMembers.CreateGuildMember(arg.Author.Id);

            if (gs.Logging)
                new Message(arg, channel);
        }
    }
}
