using Discord.WebSocket;
using Public_Bot.Modules.Helpers;
using System;
using System.Collections.Generic;
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
            client.MessageReceived += AddMessage;
            client.MessageUpdated += UpdateMessage;
            client.MessageDeleted += DeleteMessage;
        }

        private async Task DeleteMessage(Discord.Cacheable<Discord.IMessage, ulong> arg1, ISocketMessageChannel arg2)
        {
            if (MessageHelper.MessageExists(arg2.Id))
                await StateService.MutateAsync<Message>(GraphQLParser.GenerateGQLMutation<Message>("deleteMessage", false, null, "", "", new KeyValuePair<string, object>("id", arg1.Id)));
        }

        private async Task UpdateMessage(Discord.Cacheable<Discord.IMessage, ulong> arg1, SocketMessage arg2, ISocketMessageChannel arg3)
        {
            if (MessageHelper.MessageExists(arg2.Id))
                new MessageRevision(arg2);
        }

        private async Task AddMessage(SocketMessage arg)
        {
            if (arg.Channel.GetType() != typeof(SocketTextChannel))
                return;
            if (arg.Author.IsBot)
                return;
            var channel = (SocketTextChannel)arg.Channel;
            var guild = channel.Guild;
            var gs = GuildSettings.Get(guild.Id);

            if (gs.Logging)
                new Message(arg, channel);
        }
    }
}
