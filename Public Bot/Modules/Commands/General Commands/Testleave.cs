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
    public class Testleave : CommandModuleBase
    {
        [DiscordCommand("testleave",description ="test your leave message!",commandHelp ="`(PREFIX)testleave`")]
        public async Task HHE(params string[] _args)
        {
            var reqmBeD = await GuildSettings.leaveMessage.GenerateLeaveMessage(Context.User, Context.Guild);
            await Context.Channel.SendMessageAsync("", false, reqmBeD);
        }
    }
}