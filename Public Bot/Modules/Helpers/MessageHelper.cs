using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Public_Bot.StateService;

namespace Public_Bot.Modules.Helpers
{
    public class MessageHelper
    {
        public static bool MessageExists(ulong messageId)
        {
            var re = StateService.Query<Message>("{\"operationName\":null,\"variables\":{},\"query\":\"{\n message(id: \\\"" + messageId + "\\\") {\n    GuildID\n  }\n}\n\"}");
            return re == null;
        }

        public static Message GetMessage(ulong id)
            => GetMessageAsync(id).GetAwaiter().GetResult();
        public static async Task<Message> GetMessageAsync(ulong id)
        {
            var q = GraphQLParser.GenerateGQLQuery<Message>("message", new KeyValuePair<string, object>("id", id));
            return await StateService.QueryAsync<Message>(q);
        }
    }
}
