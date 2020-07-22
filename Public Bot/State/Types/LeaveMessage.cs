using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
namespace Public_Bot.State.Types
{
    public class LeaveMessage
    {
        public bool isEnabled { get; set; }
        public string leaveMessage { get; set; } = "";
        public string leaveTitle { get; set; } = "We lost a comrade";

        public Task<Embed> GenerateLeaveMessage(SocketUser x, string GuildName)
        {
            var xyz = new EmbedBuilder
            {
                Title = leaveTitle
            }.AddField(x.Username + x.Discriminator, leaveMessage.Replace("{user}","")).WithCurrentTimestamp().Build();
            return xyz;
        }
    }
}
