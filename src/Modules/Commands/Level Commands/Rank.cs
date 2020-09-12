using Discord;
using Discord.WebSocket;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static Public_Bot.Modules.Handlers.LevelHandler;
using Color = Discord.Color;
namespace Public_Bot.Modules.Commands.Level_Commands
{
    [DiscordCommandClass("🧪 Levels 🧪", "Add ranks and leaderboards for your server with Levels!")]
    public class Rank : CommandModuleBase
    {
        [Alt("r")]
        [DiscordCommand("rank", description = "Shows your current rank!", commandHelp = "Usage - `(PREFIX)rank`, `(PREFIX)rank <user>`")]
        public async Task rank(params string[] args)
        {
            var user = Context.User;
            if (args.Length == 1)
                user = GetUser(args[0]);
            if (user == null)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Invalid User!",
                    Description = $"The user \"{args[0]}\" is invalid",
                    Color = Discord.Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            var gl = GuildLeaderboards.Get(Context.Guild.Id);
            var gob = GuildCache.GetGuild(Context.Guild.Id);
            if (gl.CurrentUsers.LevelUserExists(user.Id))
            {
                var userlvl = gob.Leaderboard.CurrentUsers.GetLevelUser(user.Id);
                var u = Context.Guild.GetUser(userlvl.Id);
                var av = u.GetAvatarUrl();
                if (av == null)
                    av = u.GetDefaultAvatarUrl();
                var img = RankBuilder.MakeRank(userlvl.Username,
                    av,
                    (int)userlvl.CurrentLevel,
                    (int)userlvl.CurrentXP,
                    (int)userlvl.NextLevelXP,
                    userlvl.ColorFromHex(userlvl.BarColor),
                    userlvl.GetRank(),
                    userlvl.ColorFromHex(userlvl.BackgroundColor),
                    userlvl.BackgroundUrl);

                img.Save($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}rank.png", System.Drawing.Imaging.ImageFormat.Png);
                await Context.Channel.SendFileAsync($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}rank.png");
            }
            else
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Hmm",
                    Description = "That user doesnt have any levels or XP in this guild!",
                    Color = Color.Orange
                }.WithCurrentTimestamp().Build());
            }
        }
        public class RankBuilder
        {
            public static System.Drawing.Image MakeRank(string username, string avtr, int level, int curXP, int nxtXP, System.Drawing.Color embc, long Rank, System.Drawing.Color bgc, string bkurl = null)
            {
                WebClient wc = new WebClient();
                byte[] bytes = wc.DownloadData(avtr);
                MemoryStream ms = new MemoryStream(bytes);
                System.Drawing.Image pfp = System.Drawing.Image.FromStream(ms);
                pfp = ResizeImage(pfp, 200, 200);
                var btmp = new Bitmap(913, 312);
                var canv = Graphics.FromImage(btmp);
                canv.SmoothingMode = SmoothingMode.AntiAlias;
                if (bkurl == null)
                    canv.FillPath(new SolidBrush(bgc), RoundedRect(new Rectangle(0, 0, 913, 312), 30));
                else
                {
                    byte[] btz = wc.DownloadData(bkurl);
                    MemoryStream mems = new MemoryStream(btz);
                    System.Drawing.Image bannr = System.Drawing.Image.FromStream(mems);
                    var fin = ResizeImage(bannr, 913, 312);
                    canv.DrawImage(Public_Bot.Modules.Commands.General_Commands.GuildStatBuilder.RoundCorners(fin, 30, 913, 312), new Point(0, 0));
                }
                //draw pfp
                var rpfp = ClipToCircle(pfp, new PointF(pfp.Width / 2, pfp.Height / 2), pfp.Width / 2, System.Drawing.Color.Transparent);
                canv.DrawImage(rpfp, 40, 25);
                //draw progressbar
                var g = RoundedRect(new Rectangle(20, 312 - 60, (int)(913 - 60), 30), 15);
                //draw lvl
                var mxWidth = (int)(913 - 60);
                var mnWidth = 30;
                var prc = (double)curXP / (double)nxtXP;
                var fnl = Math.Ceiling(mxWidth * prc);
                if (fnl < mnWidth)
                    fnl = mnWidth;
                var prg = RoundedRect(new Rectangle(20, 312 - 60, (int)fnl, 30), 15);
                canv.FillPath(new SolidBrush(System.Drawing.Color.FromArgb(50, 50, 50)), g);
                canv.FillPath(new SolidBrush(embc), prg);

                //draw username
                var usrnam = username.Split('#');
                var usr = string.Join("#", usrnam.Take(usrnam.Length - 1));
                var tag = "#" + usrnam[usrnam.Length - 1];
                if (usr.Length >= 17)
                    usr = new string(usr.Take(14).ToArray()) + "...";
                canv.DrawString(usr + tag, new Font("Bahnschrift", 42), new SolidBrush(System.Drawing.Color.White), new PointF(260, 32));
                //draw Rank
                canv.DrawString("Rank #" + Rank, new Font("Bahnschrift", 30), new SolidBrush(System.Drawing.Color.Gold), new PointF(260, 100));
                //draw xp
                canv.DrawString($"XP:  {KiloFormat(curXP)} / {KiloFormat(nxtXP)}", new Font("Bahnschrift", 20), new SolidBrush(System.Drawing.Color.White), new PointF(btmp.Width - 60, btmp.Height - 100), new StringFormat(StringFormatFlags.DirectionRightToLeft));
                //draw level
                canv.DrawString("Level " + level, new Font("Bahnschrift", 30), new SolidBrush(System.Drawing.Color.White), new PointF(260, 150));
                canv.Save();
                return btmp;
            }
            public static Bitmap ResizeImage(System.Drawing.Image image, int width, int height)
            {
                return new Bitmap(image, new Size(width, height));
                //var destRect = new Rectangle(0, 0, width, height);
                //using(Bitmap destImage = new Bitmap(width, height, PixelFormat.Format32bppArgb))
                //{
                //    Console.WriteLine($"W:{width} H:{height} Null?: {(destImage == null ? "True" : "false")}");
                //    destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                //    var graphics = Graphics.FromImage(destImage);
                //    graphics.CompositingMode = CompositingMode.SourceCopy;
                //    graphics.CompositingQuality = CompositingQuality.HighQuality;
                //    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //    graphics.SmoothingMode = SmoothingMode.HighQuality;
                //    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                //    using (var wrapMode = new ImageAttributes())
                //    {
                //        wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                //        graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                //    }
                //    graphics.Save();
                //    return destImage;
                //}

            }
            public static GraphicsPath RoundedRect(Rectangle bounds, int radius)
            {
                int diameter = radius * 2;
                Size size = new Size(diameter, diameter);
                Rectangle arc = new Rectangle(bounds.Location, size);
                GraphicsPath path = new GraphicsPath();

                if (radius == 0)
                {
                    path.AddRectangle(bounds);
                    return path;
                }

                // top left arc  
                path.AddArc(arc, 180, 90);

                // top right arc  
                arc.X = bounds.Right - diameter;
                path.AddArc(arc, 270, 90);

                // bottom right arc  
                arc.Y = bounds.Bottom - diameter;
                path.AddArc(arc, 0, 90);

                // bottom left arc 
                arc.X = bounds.Left;
                path.AddArc(arc, 90, 90);

                path.CloseFigure();
                return path;
            }
            public static System.Drawing.Image ClipToCircle(System.Drawing.Image srcImage, PointF center, float radius, System.Drawing.Color backGround)
            {
                System.Drawing.Image dstImage = new Bitmap(srcImage.Width, srcImage.Height, srcImage.PixelFormat);

                using (Graphics g = Graphics.FromImage(dstImage))
                {
                    RectangleF r = new RectangleF(center.X - radius, center.Y - radius,
                                                             radius * 2, radius * 2);

                    // enables smoothing of the edge of the circle (less pixelated)
                    //g.SmoothingMode = SmoothingMode.AntiAlias;

                    // fills background color
                    using (Brush br = new SolidBrush(backGround))
                    {
                        g.FillRectangle(br, 0, 0, dstImage.Width, dstImage.Height);
                    }

                    // adds the new ellipse & draws the image again 
                    GraphicsPath path = new GraphicsPath();
                    path.AddEllipse(r);
                    g.SetClip(path);
                    g.DrawImage(srcImage, 0, 0);

                    return dstImage;
                }
            }
            public static string KiloFormat(int num)
            {
                if (num >= 100000000)
                    return (num / 1000000).ToString("#,0M");

                if (num >= 10000000)
                    return (num / 1000000).ToString("0.#") + "M";

                if (num >= 100000)
                    return (num / 1000).ToString("#,0K");

                if (num >= 10000)
                    return (num / 1000).ToString("0.#") + "K";

                return num.ToString("#,0");
            }
        }
    }
}