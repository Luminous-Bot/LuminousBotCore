using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot.Services
{
    public class ReactionService
    {
        private static DiscordShardedClient _client;
        private static Dictionary<ulong, Func<Discord.Cacheable<Discord.IUserMessage, ulong>, ISocketMessageChannel, SocketReaction, Task>> Handlers = new Dictionary<ulong, Func<Discord.Cacheable<Discord.IUserMessage, ulong>, ISocketMessageChannel, SocketReaction, Task>>();
        /// <summary>
        /// Creates the Reaction service
        /// </summary>
        /// <param name="c"></param>
        internal static void Create(DiscordShardedClient c)
        {
            _client = c;

            _client.ReactionAdded += HandleReactionAdded;
        }

        private static async Task HandleReactionAdded(Discord.Cacheable<Discord.IUserMessage, ulong> arg1, ISocketMessageChannel arg2, SocketReaction arg3)
        {
            
            if (Handlers.ContainsKey(arg1.Id))
            {
                var task = Handlers[arg1.Id];
                await task.Invoke(arg1, arg2, arg3).ConfigureAwait(false);
            }
        }

        public static void AddReactionHandler(ulong MessageId, Func<Discord.Cacheable<Discord.IUserMessage, ulong>, ISocketMessageChannel, SocketReaction, Task> Callback)
        {
            if (Handlers.ContainsKey(MessageId))
                throw new Exception($"{MessageId} already contains a handler!");
            Handlers.Add(MessageId, Callback);
        }
        public static void RemoveReactionHandler(ulong MessageId)
        {
            if(Handlers.ContainsKey(MessageId))
                Handlers.Remove(MessageId);
        }
    }
}
