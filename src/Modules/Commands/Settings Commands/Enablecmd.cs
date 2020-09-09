using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot.Modules.Commands.Settings_Commands
{
    [DiscordCommandClass("⚙️ Settings ⚙️", "Change how this bot works in your server!")]
    class Enablecmd : CommandModuleBase
    {
        [Alt("enablecommand")]
        [DiscordCommand("enablecmd", 
            RequiredPermission = true,
            description = "Enables a disabled command",
            commandHelp = "`(PREFIX)enablecmd <command>`")]
        public async Task EnableCmd(params string[] args)
        {
            if (args.Length == 0)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "What command?",
                    Description = $"Please specify the command you want to enable! If you would like a list of enabled and disabled commands, please run `{GuildSettings.Prefix}listcmd`",
                    Color = Color.Orange
                }.WithCurrentTimestamp().Build());
                return;
            }

            string cmd = "";
            if (args[0].StartsWith(GuildSettings.Prefix))
                cmd = args[0].Replace(GuildSettings.Prefix, "");
            else
                cmd = args[0];
            if (!Commands.Any(x => x.CommandName == cmd))
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Unknown Command",
                    Description = $"The command `{cmd}` doesn't exist!",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            if (GuildSettings.DisabledCommands.Contains(cmd))
            {
                GuildSettings.DisabledCommands.Remove(cmd);
                GuildSettings.SaveGuildSettings();
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Success!",
                    Color = Color.Green,
                    Description = $"The command `{cmd}` is now Enabled!"
                }.WithCurrentTimestamp().Build());
                return;
            }
            else
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Command already Enabled!",
                    Color = Color.Red,
                    Description = $"The command `{cmd}` is already Enabled!"
                }.WithCurrentTimestamp().Build());
                return;
            }
        }
    }
}
