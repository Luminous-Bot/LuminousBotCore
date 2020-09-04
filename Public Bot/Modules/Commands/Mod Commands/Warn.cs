using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Public_Bot.Modules.Handlers.MuteHandler;
namespace Public_Bot.Modules.Commands.Mod_Commands
{
    [DiscordCommandClass("🔨 Mod Commands 🔨", "Make your staff team more efficient with this module, you can keep track of user infractions and keep your server in order!")]
    public class Warn : CommandModuleBase
    {
        [DiscordCommand("warn", RequiredPermission = true, description = "Warns a user", commandHelp = "Usage - `(PREFIX)warn <@user> <reason>`")]
        public async Task warn(params string[] args)
            => await ModActionHandler.CreateAction(args, Action.Warned, Context, GuildSettings);
    }
}