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
    public class Createmutedrole : CommandModuleBase
    {
        [GuildPermissions(GuildPermission.ManageRoles)]
        [DiscordCommand("createmutedrole", RequiredPermission = true,
            description = "Creates a role to give to people when running `(PREFIX)mute`\n\n**NOTE**: This command will add permission overwrites to channels, this isn't the most reliable method. Please try to make a role for muted users yourself and use `(PREFIX)setmutedrole <@role>` for that role.",
            commandHelp = "`(PREFIX)createmutedrole`"
        )]
        public async Task cmr(params string[] args)
        {
            if (args.Length == 0)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "**Are you sure?**",
                    Description = $"This will create a role and modify each text channel to make that role unable to text. this will take some time.\n\n**if you have a muted role already** please add it by typing `{GuildSettings.Prefix}setmutedrole <@role>`\n\nOtherwise **Type `{GuildSettings.Prefix}createmutedrole confirm` to continue**",
                    Color = Color.Blue,
                    Timestamp = DateTime.Now
                }.Build());
                return;
            }
            else if (args[0] == "confirm")
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "**We're on it!**",
                    Description = $"We are setting up the Muted Role, depending on how many channels you have this will take some time",
                    Color = Color.Blue,
                    Timestamp = DateTime.Now
                }.Build());
                new Thread(async () => await MuteHandler.SetupMutedRole(Context.Guild, Context.Channel.Id, Context.User.Id)).Start();
            }
        }
    }
}