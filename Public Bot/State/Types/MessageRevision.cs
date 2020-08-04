using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot
{
    public class MessageRevision
    {
        [GraphQLSVar]
        public ulong Id { get; set; }
        [GraphQLSVar, GraphQLProp]
        public DateTime Date { get; set; }
        [GraphQLSVar, GraphQLProp]
        public string Content { get; set; }
        public MessageRevision() { }
        public MessageRevision(SocketMessage msg)
        {
            this.Id = msg.Id;
            this.Content = JsonConvert.SerializeObject(msg.Content);
            this.Date = msg.EditedTimestamp.HasValue ? msg.EditedTimestamp.Value.DateTime : DateTime.UtcNow;

            //mutation
            string q = GraphQLParser.GenerateGQLMutation<MessageRevision>("updateMessage", true, this, "data", "UpdateMessageInput!");
            StateService.Mutate<MessageRevision>(q);
        }
    }
}
