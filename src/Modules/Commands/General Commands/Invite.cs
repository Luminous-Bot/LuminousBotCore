using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Color = Discord.Color;
namespace Public_Bot.Modules.Commands.General_Commands
{
    [DiscordCommandClass("👨🏼‍💻 General 👨🏼‍💻", "General bot commands for everyone!")]
    public class Invite : CommandModuleBase
    {
        [DiscordCommand("invite", description = "Provides an invite for this bot")]
        public async Task invite()
        {
            string link = "https://discord.com/api/oauth2/authorize?client_id=722435272532426783&permissions=427683062&scope=bot";
            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder() 
            {
                Author = new EmbedAuthorBuilder()
                {
                    IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                    Name = Context.Client.CurrentUser.Username
                },
                Title = "Invite!",
                Description = $"Thanks for considering to invite {Context.Client.CurrentUser.Username}. You can find alot of information about how to setup {Context.Client.CurrentUser.Username} on our [Discord](https://disocrd.com/invite/w8EcwBy)!",
                Fields = new List<EmbedFieldBuilder>()
                {
                    new EmbedFieldBuilder()
                    {
                        Name = "__Minimum Permissions__",
                        Value = "https://discord.com/api/oauth2/authorize?client_id=722435272532426783&permissions=427683062&scope=bot",
                        IsInline = true,
                    },
                    new EmbedFieldBuilder()
                    {
                        Name = "__Full Permissions__",
                        Value = "https://discord.com/api/oauth2/authorize?client_id=722435272532426783&permissions=8&scope=bot",
                        IsInline = true
                    }
                },
                Color = Blurple
            }.WithCurrentTimestamp().Build());
        }
    }
}