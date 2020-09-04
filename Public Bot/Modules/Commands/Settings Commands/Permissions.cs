using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Public_Bot.Modules.Commands.Settings_Commands
{
    [DiscordCommandClass("⚙️ Settings ⚙️", "Change how this bot works in your server!")]
    public class Permissions : CommandModuleBase
    {
        [DiscordCommand("permissions", RequiredPermission = true, commandHelp = "`(PREFIX)permissions`", description = "Lists all roles with elevated permissions for this bot")]
        public async Task permission()
        {
            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
            {
                Title = "Permissions!",
                Description = $"here are all the roles with permissions\n\n<@&{string.Join(">\n<@&", GuildSettings.PermissionRoles)}>\n\nTo add one use `{GuildSettings.Prefix}addpermission <@role>`\nTo remove one use `{GuildSettings.Prefix}removepermission <@role>`",
                Color = Color.Green,
            }.WithCurrentTimestamp().Build());
        }
    }
}