using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using RestSharp.Extensions;

namespace Public_Bot.State.Types
{
    public class LeaveMessage
    {
        public bool isEnabled { get; set; }
        public string leaveMessage { get; set; } = "{user} has left us!! We now have {guild.count} members!";
        public string leaveTitle { get; set; } = "We lost a comrade";
        public ulong leaveChannel { get; set; }
        public ulong GuildID { get; set; }
        public string EmbedColor { get; set; } = $"#7289DA";
       
        public async Task<Embed> GenerateLeaveMessage(SocketUser x, SocketGuild gld)
        {
            var title = leaveTitle.CompileVarMessage(gld, x);
            if (title.Length > 256)
                title = new string(title.Take(252).ToArray()) + "...";
            var body = leaveMessage.CompileVarMessage(gld, x);
            if (body.Length > 1024)
                body = new string(body.Take(1020).ToArray()) + "...";
            var xyz = new EmbedBuilder
            {
                Author = new EmbedAuthorBuilder()
                {
                    IconUrl = x.GetAvatarUrl(),
                    Name = x.ToString()
                },
                Title = title,
                Description = body,
                Color = HexColorConverter.DiscordColorFromHex(EmbedColor),
                
            }.WithCurrentTimestamp().Build();
            return xyz;
        }
        public LeaveMessage() { }
        public LeaveMessage(GuildSettings gs, IGuild gld)
        {
            this.isEnabled = false;
            this.GuildID = gs.GuildID;
            this.leaveChannel = gld.SystemChannelId.HasValue ? gld.SystemChannelId.Value : gld.DefaultChannelId;
        }
    }
}
