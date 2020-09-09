using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot.Modules.Commands.Settings_Commands
{
    [DiscordCommandClass("⚙️ Settings ⚙️", "Change how this bot works in your server!")]
    class Prefix : CommandModuleBase
    {
        [DiscordCommand("prefix", RequiredPermission = true, description = "Changes the prefix of the Bot", commandHelp = "Usage - `(PREFIX)prefix <prefix>`")]
        public async Task prefix(string prefix)
        {
            GuildSettings.Prefix = prefix;
            GuildSettings.SaveGuildSettings();
            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
            {
                Title = "Success!",
                Description = $"The prefix is now `{prefix}`!",
                Color = Color.Green
            }.WithCurrentTimestamp().Build());
        }
    }
}
