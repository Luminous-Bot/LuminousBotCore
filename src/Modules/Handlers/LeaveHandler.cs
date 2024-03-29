﻿using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Public_Bot.Modules.Handlers
{
    [DiscordHandler()]
    public class LeaveHandler
    {
        public DiscordShardedClient client { get; set; }
        public LeaveHandler(DiscordShardedClient c)
        {
            client = c;
            client.UserLeft += Client_UserLeft;
        }

        private async Task Client_UserLeft(SocketGuildUser arg)
        {
            var guild = GuildCache.GetGuild(arg.Guild.Id);
            var gm = guild.GuildMembers.GetGuildMember(arg.Id, arg.Guild.Id);
            if(gm != null)
                gm = await gm.UpdateIsInServer(false);

            var guildSettings = GuildSettingsHelper.GetGuildSettings(arg.Guild.Id);
            if (guildSettings.leaveMessage.isEnabled)
            {
                var embed = await guildSettings.leaveMessage.GenerateLeaveMessage(arg,arg.Guild);
                var channel = arg.Guild.GetTextChannel(guildSettings.leaveMessage.leaveChannel);
                if (channel != null)
                {
                    try
                    {
                        await channel.SendMessageAsync("", false, embed);
                    }
                    catch { }
                }
            }
        }
    }
}
