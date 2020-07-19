using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot
{
    public class WelcomeCard
    {
        public bool isEnabled { get; set; }
        public string BackgroundUrl { get; set; } = "https://image.freepik.com/free-vector/luminous-stadium-light-effect_23-2148366134.jpg";
        public ulong GuildId { get; set; }
        public ulong WelcomeChannel { get; set; }
        public string WelcomeMessage { get; set; } = "Welcome {user.name} to {guild.name}! you are the {guild.count} member!";
        public string GenerateWelcomeMessage(SocketGuildUser user, SocketGuild g)
            => WelcomeMessage.Replace("{user.name}", user.ToString()).Replace("{guild.name}", g.Name).Replace("{guild.count}", g.MemberCount.ToString());

        public WelcomeCard() { }
        public WelcomeCard(GuildSettings gs)
        {
            this.isEnabled = false;
            this.GuildId = gs.GuildID;
        }
        public WelcomeCard(GuildSettings gs, IGuild guild)
        {
            this.isEnabled = false;
            this.GuildId = gs.GuildID;
            WelcomeChannel = guild.GetSystemChannelAsync(CacheMode.AllowDownload).Result.Id;
            BackgroundUrl = guild.BannerUrl;
        }
    }
}
