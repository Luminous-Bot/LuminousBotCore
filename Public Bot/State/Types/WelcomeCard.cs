using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Public_Bot
{
    public class WelcomeCard
    {
        public bool isEnabled { get; set; }
        public string BackgroundUrl { get; set; }
        public ulong GuildId { get; set; }
        public ulong WelcomeChannel { get; set; }
        public string EmbedColor { get; set; } = "#7289DA";
        public bool DMs { get; set; } = false;
        public bool MentionsUsers { get; set; } = true;
        public string WelcomeMessage { get; set; } = "Welcome {user} to {guild}! you are the {guild.count.format} member!";
        public Embed BuildEmbed(SocketGuildUser usr, SocketGuild g)
        {
            //var title = leaveTitle.CompileVarMessage(gld, x);
            //if (title.Length > 256)
            //    title = new string(title.Take(252).ToArray()) + "...";
            var body = WelcomeMessage.CompileVarMessage(g, usr);
            if (body.Length > 1024)
                body = new string(body.Take(1020).ToArray()) + "...";
            EmbedBuilder b = new EmbedBuilder()
            {
                Author = new EmbedAuthorBuilder()
                {
                    Name = usr.ToString(),
                    IconUrl = usr.GetAvatarUrl()
                },
                Description = body,
                Color = HexColorConverter.DiscordColorFromHex(EmbedColor),
                Footer = new EmbedFooterBuilder() { Text = g.Name },
            }.WithCurrentTimestamp();
            if (this.BackgroundUrl != null)
                b.ImageUrl = this.BackgroundUrl;
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
