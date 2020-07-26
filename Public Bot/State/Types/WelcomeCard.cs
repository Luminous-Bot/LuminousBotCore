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
        public bool DMs { get; set; } = false;
        public bool MentionsUsers { get; set; } = true;
        public string WelcomeMessage { get; set; } = "Welcome {user.name} to {guild.name}! you are the {guild.count} member!";
        public string GenerateWelcomeMessage(SocketGuildUser user, SocketGuild g)
            => WelcomeMessage.Replace("{user}", user.ToString()).Replace("{user.name}", user.Username).Replace("{guild.name}", g.Name).Replace("{guild.count}", g.MemberCount.ToString());
        public Embed BuildEmbed(SocketGuildUser usr, SocketGuild g)
        {
            EmbedBuilder b = new EmbedBuilder()
            {
                Author = new EmbedAuthorBuilder()
                {
                    Name = usr.ToString(),
                    IconUrl = usr.GetAvatarUrl()
                },
                Description = GenerateWelcomeMessage(usr, g),
                Color = CommandModuleBase.Blurple,
                Footer = new EmbedFooterBuilder() { Text = g.Name },
            }.WithCurrentTimestamp();
            return b.Build();
        }

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
            WelcomeChannel = guild.SystemChannelId.HasValue ? guild.SystemChannelId.Value : guild.DefaultChannelId;
            if (guild.BannerUrl != null)
                BackgroundUrl = guild.BannerUrl;
        }
    }
}
