using Discord.WebSocket;
using Public_Bot.Modules.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Public_Bot.Modules.Commands.GeneralCommands;

namespace Public_Bot.Modules.Handlers
{
    [DiscordHandler]
    public class WelcomeHandler
    {
        public DiscordShardedClient client { get; set; }
        public WelcomeHandler(DiscordShardedClient c)
        {
            client = c;

            client.UserJoined += Client_UserJoined;
        }

        private async Task Client_UserJoined(SocketGuildUser arg)
        {
            var GuildSettings = CommandHandler.GetGuildSettings(arg.Guild.Id);
            if (GuildSettings.WelcomeCard.isEnabled)
            {
                var img = GenerateWelcomeImage(arg, arg.Guild, GuildSettings.WelcomeCard);
                img.Save($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}WelcomeCard.png", ImageFormat.Png);
                var chan = arg.Guild.GetTextChannel(GuildSettings.WelcomeCard.WelcomeChannel);
                if (chan != null)
                    await chan.SendFileAsync($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}WelcomeCard.png", "");
            }
        }
        static Bitmap WelcomeImage = new Bitmap(960, 540);
        static Graphics WelcomeGraphics = Graphics.FromImage(WelcomeImage);
        static WebClient wc = new WebClient();
        public static Image GenerateWelcomeImage(SocketGuildUser user, SocketGuild guild, WelcomeCard welc)
        {
            WelcomeGraphics.Clear(Color.Transparent);
            WelcomeGraphics.SmoothingMode = SmoothingMode.AntiAlias;

            //background
            if (welc.BackgroundUrl == null)
                WelcomeGraphics.FillPath(new SolidBrush(System.Drawing.Color.FromArgb(40, 40, 40)), LevelCommands.RankBuilder.RoundedRect(new Rectangle(0, 0, 960, 540), 30));
            else
            {
                byte[] bytes = wc.DownloadData(welc.BackgroundUrl);
                MemoryStream ms = new MemoryStream(bytes);
                System.Drawing.Image bannr = System.Drawing.Image.FromStream(ms);
                WelcomeGraphics.DrawImage(GuildStatBuilder.RoundCorners(bannr, 30), new PointF(0, 0));
                WelcomeGraphics.FillPath(new SolidBrush(System.Drawing.Color.FromArgb(200, 40, 40, 40)), LevelCommands.RankBuilder.RoundedRect(new Rectangle(30, 30, 900, 480), 30));

            }
            //server logo
            if (guild.IconUrl != null)
            {
                byte[] bytes2 = wc.DownloadData(guild.IconUrl);
                MemoryStream ms2 = new MemoryStream(bytes2);
                var img = System.Drawing.Image.FromStream(ms2);
                var Icon = LevelCommands.RankBuilder.ResizeImage(img, 150, 150);
                img.Dispose();
                WelcomeGraphics.DrawImage(LevelCommands.RankBuilder.ClipToCircle(Icon, new PointF(Icon.Width / 2, Icon.Height / 2), Icon.Width / 2, System.Drawing.Color.Transparent), new PointF(WelcomeImage.Width / 2 - Icon.Width / 2, 120));
            }
            //text
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            // stringFormat.LineAlignment = StringAlignment.Center;

            var font = new Font("Bahnschrift", 30, FontStyle.Regular);
            var brush = new SolidBrush(System.Drawing.Color.White);
            WelcomeGraphics.DrawString(guild.Name, new Font("Bahnschrift", 40), new SolidBrush(System.Drawing.Color.White), new RectangleF(60, 50, WelcomeImage.Width - 120, 60), stringFormat);
            var textArea = new Rectangle(50, 310, 860, 200);
            //WelcomeGraphics.FillPath(new SolidBrush(System.Drawing.Color.FromArgb(200, 255, 40, 40)), LevelCommands.RankBuilder.RoundedRect(textArea, 30));
            WelcomeGraphics.DrawString($"{welc.GenerateWelcomeMessage(user, guild)}", font, brush, textArea, stringFormat);
           
            return WelcomeImage;
        }
    }
}
