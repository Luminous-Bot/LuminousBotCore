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
#if DEBUG
            Logger.Write("Message Loggin DISABLED OWO", Logger.Severity.State);
#else
            Logger.Write("Message logging enabled", Logger.Severity.State);
            client.MessageReceived += AddMessage;
            client.MessageUpdated += UpdateMessage;
            client.MessageDeleted += DeleteMessage;
#endif
        }

        private async Task DeleteMessage(Discord.Cacheable<Discord.IMessage, ulong> arg1, ISocketMessageChannel arg2)
        {
            if (MessageHelper.MessageExists(arg1.Id))
                await StateService.MutateAsync<Message>(GraphQLParser.GenerateGQLMutation<Message>("deleteMessage", false, null, "", "", new KeyValuePair<string, object>("id", arg1.Id)));
        }

        private async Task UpdateMessage(Discord.Cacheable<Discord.IMessage, ulong> arg1, SocketMessage arg2, ISocketMessageChannel arg3)
        {
            if (arg2.Author.IsBot || arg2.Author.IsWebhook)
                return;
            if (MessageHelper.MessageExists(arg2.Id))
                new MessageRevision(arg2);
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

            if (!UserHandler.Users.Any(x => x.Id == arg.Author.Id))
                if (!User.UserExists(arg.Author.Id))
                    UserHandler.CreateUser(arg.Author.Id);
            if (!GuildHandler.GuildMemberExists(arg.Author.Id, guild.Id))
                if (!GuildMember.Exists(arg.Author.Id, guild.Id))
                    GuildHandler.CreateGuildMember(arg.Author.Id, guild.Id);

            if (gs.Logging)
                new Message(arg, channel);
        }
    }
}
