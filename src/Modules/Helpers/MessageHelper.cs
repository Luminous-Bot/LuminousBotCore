using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Public_Bot.StateService;

namespace Public_Bot.Modules.Helpers
{
    public class MessageHelper
    {
        public static bool MessageExists(ulong messageId)
        {
            var re = StateService.Query<bool>("{\"operationName\":null,\"variables\":{},\"query\":\"{ messageExists(MessageID: \\\"" + messageId + "\\\") }\"}", true);

            // TODO: maybe change this signature?
            return re;
        }

        
        public static Message GetMessage(ulong id)
            => GetMessageAsync(id).GetAwaiter().GetResult();
        public static async Task<Message> GetMessageAsync(ulong id)
        {
            var q = GraphQLParser.GenerateGQLQuery<Message>("message", ("id", id));

            var r = await StateService.QueryAsync<Message>(q);

            return r;
        }
    }
}
