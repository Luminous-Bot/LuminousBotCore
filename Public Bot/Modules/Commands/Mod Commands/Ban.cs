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
    public class Ban : CommandModuleBase
    {
        [GuildPermissions(GuildPermission.BanMembers)]
        [DiscordCommand("ban", RequiredPermission = true, description = "Bans a user", commandHelp = "Usage - `(PREFIX)ban <@user> <reason>`")]
        public async Task ban(params string[] args)
            => await ModActionHandler.CreateAction(args, Action.Banned, Context, GuildSettings);
    }
}