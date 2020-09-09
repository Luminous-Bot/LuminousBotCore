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
    public class Addpermission : CommandModuleBase
    {
        [DiscordCommand("addpermission", RequiredPermission = true, commandHelp = "`(PREFIX)addpermission <@role>`", description = "Adds a role to the permission list")]
        public async Task addmodrole(string r)
        {
            var role = GetRole(r);
            if (role == null)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Invalid Role!",
                    Description = "The role you provided was invalid!",
                    Color = Color.Red,
                }.WithCurrentTimestamp().Build());
                return;
            }
            if(GuildSettings.PermissionRoles.Any(x => x == role.Id))
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "That role is already added!",
                    Description = "The roles is already in the permission list!",
                    Color = Color.Orange,
                }.WithCurrentTimestamp().Build());
                return;
            }
            GuildSettings.PermissionRoles.Add(role.Id);
            GuildSettings.SaveGuildSettings();
            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
            {
                Title = "Added and Saved!",
                Description = $"Added {role.Mention} to the Permissions list. here are all the roles with permissions\n\n<@&{string.Join(">\n<@&", GuildSettings.PermissionRoles)}>\n\nTo remove one use `{GuildSettings.Prefix}removepermission <@role>`",
                Color = Color.Green,
            }.WithCurrentTimestamp().Build());
        }
    }
}