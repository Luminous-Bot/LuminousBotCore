using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static Public_Bot.Modules.Commands.Level_Commands.Rank;
using Color = Discord.Color;
namespace Public_Bot.Modules.Commands.General_Commands
{
    [DiscordCommandClass("👨🏼‍💻 General 👨🏼‍💻", "General bot commands for everyone!")]
    public class Guild : CommandModuleBase
    {
        [DiscordCommand("guild", description = "Shows the current guild's stats", commandHelp = "Usage - `(PREFIX)guild`")]
        public async Task stats()
        {
            var iconurl = Context.Guild.IconUrl;
            if(iconurl != null)
                iconurl = iconurl.Replace("webp", "jpg");
            var bannerirl = Context.Guild.BannerUrl;
            if(bannerirl != null)
                bannerirl = bannerirl.Replace("webp", "jpg");
            var img = GuildStatBuilder.MakeServerCard(Context.Guild.Name, iconurl, bannerirl, Context.Guild.Owner.ToString(), Context.Guild.MemberCount.ToString(), Context.Guild.PremiumSubscriptionCount.ToString(), Context.Guild.CreatedAt.DateTime);
            img.Save($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}gld.png", System.Drawing.Imaging.ImageFormat.Png);
            await Context.Channel.SendFileAsync($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}gld.png");
        }
    }
    public class GuildStatBuilder
    {
        public static System.Drawing.Image MakeServerCard(string servername, string serverlogo, string bannerurl, string owner, string users, string nitroboosts, DateTime Created)
        {
            WebClient wc = new WebClient();
            //createbackgound with graphic
            var baseImg = new Bitmap(960, 540);
            var g = Graphics.FromImage(baseImg);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (bannerurl == null)
                g.FillPath(new SolidBrush(System.Drawing.Color.FromArgb(40, 40, 40)), RankBuilder.RoundedRect(new Rectangle(0, 0, 960, 540), 30));
            else
            {
                byte[] bytes = wc.DownloadData(bannerurl);
                MemoryStream ms = new MemoryStream(bytes);
                System.Drawing.Image bannr = System.Drawing.Image.FromStream(ms);
                g.DrawImage(RoundCorners(bannr, 30), new PointF(0, 0));
                g.FillPath(new SolidBrush(System.Drawing.Color.FromArgb(200, 40, 40, 40)), RankBuilder.RoundedRect(new Rectangle(30, 30, 900, 480), 30));

            }
            if (serverlogo != null)
            {
                byte[] bytes2 = wc.DownloadData(serverlogo);
                MemoryStream ms2 = new MemoryStream(bytes2);
                var img = System.Drawing.Image.FromStream(ms2);
                var Icon = RankBuilder.ResizeImage(img, 150, 150);
                img.Dispose();

                g.DrawImage(RankBuilder.ClipToCircle(Icon, new PointF(Icon.Width / 2, Icon.Height / 2), Icon.Width / 2, System.Drawing.Color.Transparent), new PointF(baseImg.Width / 2 - Icon.Width / 2, 120));
            }
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            RectangleF StringRect = new RectangleF(40, 180, baseImg.Width - 80, baseImg.Height - 100);
            var font = new Font("Bahnschrift", 30, FontStyle.Regular);
            var brush = new SolidBrush(System.Drawing.Color.White);
            g.DrawString(servername, new Font("Bahnschrift", 40), new SolidBrush(System.Drawing.Color.White), new RectangleF(60, 40, baseImg.Width - 120, 60), stringFormat);

            g.DrawString($"Owner: {owner}", font, brush, new PointF(baseImg.Width / 2, 310), stringFormat);
            g.DrawString($"Users: {users}", font, brush, new PointF(baseImg.Width / 2, 360), stringFormat);
            g.DrawString($"Nitro Boosters: {nitroboosts}", font, brush, new PointF(baseImg.Width / 2, 410), stringFormat);
            g.DrawString($"Created on: {Created.ToString("r")}", font, brush, new PointF(baseImg.Width / 2, 460), stringFormat);

            //g.DrawString($"Owner: {owner}\nUsers: {users}\nNitro Boosters: {nitroboosts}\nCreated on: {Created.ToString("r")}", font, new SolidBrush(System.Drawing.Color.White), StringRect, stringFormat);


            g.Save();
            return baseImg;
        }
        public static System.Drawing.Image RoundCorners(System.Drawing.Image image, int cornerRadius, int x = 0, int y = 0)
        {
            cornerRadius *= 2;
            Bitmap roundedImage;
            if (x != 0 && y != 0)
                roundedImage = new Bitmap(x, y);
            else
                roundedImage = new Bitmap(960, 540);

            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
            gp.AddArc(0 + roundedImage.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
            gp.AddArc(0 + roundedImage.Width - cornerRadius, 0 + roundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            gp.AddArc(0, 0 + roundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            using (Graphics g = Graphics.FromImage(roundedImage))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.SetClip(gp);
                g.DrawImage(RankBuilder.ResizeImage(image, 960, 540), Point.Empty);
            }
            return roundedImage;
        }
    }
}