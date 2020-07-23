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
        public string leaveMessage { get; set; } = "{user} has left us!! We now have {guild.count} members!";
        public string leaveTitle { get; set; } = "We lost a comrade";
        public ulong leaveChannel { get; set; }
        public ulong GuildID { get; set; }
        public Color Clr { get; set; }

        public async Task<Embed> GenerateLeaveMessage(SocketUser x, SocketGuild gld)
        {
            var xyz = new EmbedBuilder
            {
                Title = leaveTitle,
                Color = Clr
            }.WithDescription(leaveMessage.Replace("{user}", x.Username).Replace("{guild}", gld.Name).Replace("{guild.count}".Replace("\\n","\n"), gld.MemberCount.ToString())).WithCurrentTimestamp().Build();
            return xyz;
        }
        public LeaveMessage() { }
        public LeaveMessage(GuildSettings gs)
        {
            this.isEnabled = false;
            this.GuildID = gs.GuildID;
        }
        public LeaveMessage(GuildSettings gs, IGuild gld)
        {
            this.isEnabled = false;
            this.GuildID = gs.GuildID;
            this.leaveChannel = gld.GetSystemChannelAsync(CacheMode.AllowDownload).Result.Id;
        }
    }
}
