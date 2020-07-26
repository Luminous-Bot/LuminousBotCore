using Discord;
using Discord.Commands;
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

namespace Public_Bot.Modules.Commands
{
    [DiscordCommandClass("👨🏼‍💻 Fun 👨🏼‍💻", "Fun bot commands to make your server more enjoyable!")]
    public class FunCommands : CommandModuleBase
    {
        [DiscordCommand("moduletest", commandHelp = "`(PREFIX)moduletest`", description = "moduletest")]
        public async Task ModuleTest()
        {
            await Context.Channel.SendMessageAsync("it worked I guess");
        }
    }
}
