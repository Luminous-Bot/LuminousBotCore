﻿using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Public_Bot
{
    public class Message : IEntityID
    {
        [GraphQLProp, GraphQLSVar]
        public ulong Id { get; set; }
        [GraphQLProp, GraphQLSVar]
        public ulong TextChannelID { get; set; }
        //txtchanObj
        [GraphQLProp, GraphQLSVar]
        public ulong GuildID { get; set; }
        //[GraphQLObj]
        //public Guild Guild { get; set; }
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
        public Nullable<DateTime> DeletedOn { get; set; }
        [GraphQLObj]
        public List<MessageRevision> Revisions { get; set; }
        [GraphQLProp, GraphQLSVar]
        public string Content { get; set; }
        [GraphQLProp, GraphQLSVar]
        public List<string> Attachments { get; set; } = new List<string>();
        [GraphQLProp]
        public bool IsPinned { get; set; } = false;
        public Message() { }
        public Message(SocketMessage msg, SocketTextChannel chan)
        {
            this.Id = msg.Id;
            this.AuthorID = msg.Author.Id;
            this.GuildID = chan.Guild.Id;
            this.Attachments.AddRange(msg.Attachments.ToList().Select(x => x.ProxyUrl));
            //this.Author = UserHandler.GetUser(msg.Author.Id);
            this.TextChannelID = chan.Id;
            //this.GuildMember = GuildHandler.GetGuildMember(msg.Author.Id, chan.Guild.Id);
            string sc = JsonConvert.SerializeObject(msg.Content);
            this.Content = sc.Remove(0, 1).Remove(sc.Length - 2, 1);

            //createMutation
            var gql = GraphQLParser.GenerateGQLMutation<Message>("createMessage", true, this, "messageDetails", "NewMessageDataInput!");
            StateService.Mutate<Message>(gql);
        }
    }
}
