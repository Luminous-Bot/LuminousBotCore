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
    public class Setmutedrole : CommandModuleBase
    {
        [GuildPermissions(GuildPermission.ManageRoles)]
        [DiscordCommand("setmutedrole", RequiredPermission = true, 
            commandHelp = "`(PREFIX)setmutedrole <@role>`",
            description = "Sets the muted role to use for `(PREFIX)mute`")]
        public async Task setmrole(string t)
        {
            var role = GetRole(t);
            if(role == null)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Invalid Role!",
                    Description = $"The role \"{t}\" is invalid!",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            GuildSettings.MutedRoleID = role.Id;
            GuildSettings.SaveGuildSettings();
            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
            {
                Title = "The muted role has been set!",
                Color = Color.Green,
                Description = $"The role {role.Mention} will be given when someone does {GuildSettings.Prefix}mute"
            }.WithCurrentTimestamp().Build());
        }
    }
}