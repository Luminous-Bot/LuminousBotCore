using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot.Modules.Commands.General_Commands
{
    [DiscordCommandClass("👨🏼‍💻 General 👨🏼‍💻", "General bot commands for everyone!")]
    public class Cachestatus : CommandModuleBase
    {
        [Alt("cs")]
        [DiscordCommand("cachestatus",
            commandHelp = "`(PREFIX)cachestatus`",
            description = "Shows information about the Cache")]
        public async Task CacheStatus()
        {
            var guild = GuildCache.GetGuild(Context.Guild.Id);
            await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
            {
                Author = new EmbedAuthorBuilder()
                {
                    IconUrl = Context.Guild.IconUrl,
                    Name = Context.Guild.Name
                },
                Title = "Caches",
                Description = "Cache makes me go *yes*",
                Fields = new List<EmbedFieldBuilder>()
                {
                    new EmbedFieldBuilder()
                    {
                        Name = "Total Counts:",
                        Value = "```cs\n" +
                        $"Guild Members: {GuildCache.TotalGuildMembers}\n" +
                        $"Level Members: {GuildCache.TotalLevelMembers}\n```",
                        IsInline = true
                    },
                    new EmbedFieldBuilder()
                    {
                        Name = "Static Caches:",
                        Value = "```cs\n" +
                        $"Users Cache:  {UserCache.Count}\n" +
                        $"Guild Cache:  {GuildCache.Count}\n" +
                        $"```",
                        IsInline = true
                    },
                    new EmbedFieldBuilder()
                    {
                        Name = $"{Context.Guild.Name}'s Caches:",
                        Value = "```cs\n" +
                        $"Guild Members: {guild.GuildMembers.Count}\n" +
                        $"Level Members: {guild.Leaderboard.CurrentUsers.Count}```",
                        IsInline = true
                    }
                },
                Color = Color.Gold
            }.WithCurrentTimestamp().Build());
        }
    }
}
