using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot
{
    public class Message
    {
        [GraphQLProp, GraphQLSVar]
        public ulong Id { get; set; }
        [GraphQLProp, GraphQLSVar]
        public ulong TextChannelID { get; set; }
        //txtchanObj
        [GraphQLProp, GraphQLSVar]
        public ulong GuildID { get; set; }
        [GraphQLObj]
        public Guild Guild { get; set; }
        [GraphQLObj]
        public User Author { get; set; }
        [GraphQLSVar]
        public ulong AuthorID { get; set; }
        [GraphQLObj]
        public GuildMember GuildMember { get; set; }
        [GraphQLProp, GraphQLSVar]
        public DateTime Date { get; set; } = DateTime.UtcNow;
        [GraphQLProp]
        public bool Deleted { get; set; } = false;
        [GraphQLProp]
        public DateTime DeletedOn { get; set; }
        [GraphQLObj]
        public List<MessageRevision> Revisions { get; set; }
        [GraphQLProp, GraphQLSVar]
        public string Content { get; set; }
        public Message() { }
        public Message(SocketMessage msg, SocketTextChannel chan)
        {
            this.Id = msg.Id;
            this.AuthorID = msg.Author.Id;
            this.GuildID = chan.Guild.Id;
            //this.Author = UserHandler.GetUser(msg.Author.Id);
            this.TextChannelID = chan.Id;
            //this.GuildMember = GuildHandler.GetGuildMember(msg.Author.Id, chan.Guild.Id);
            this.Content = msg.Content;

            //createMutation
            var gql = GraphQLParser.GenerateGQLMutation<Message>("createMessage", true, this, "messageDetails", "NewMessageDataInput!");
            StateService.Mutate<Message>(gql);
        }
    }
}
