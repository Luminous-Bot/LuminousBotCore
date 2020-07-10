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
using System.Threading;
using System.Threading.Tasks;
using static Public_Bot.Modules.Handlers.LevelHandler;
using static Public_Bot.Modules.Handlers.LevelHandler.GuildLevelSettings;
using Color = Discord.Color;

namespace Public_Bot.Modules.Commands
{
    [DiscordCommandClass("🧪 Levels 🧪", "Add ranks and leaderboards for your server with Levels!")]
    class LevelCommands : CommandModuleBase
    {
        [Alt("lb")]
        [Alt("leaderboards")]
        [DiscordCommand("leaderboard", commandHelp = "Usage - `(PREFIX)leaderboard`", description = "Shows the leaderboard for the server")]
        public async Task Leaderboard()
        {
            var gl = GuildLeaderboards.Get(Context.Guild.Id);
            List<string> rl = new List<string>();
            foreach (var chan in gl.Settings.RankRoles.OrderBy(x => x.Key * -1))
            {
                rl.Add($"Level {chan.Key}: <@&{chan.Value}>");
            }
            List<string> lm = new List<string>();
            int rnk = 1;
            int space = gl.CurrentUsers.OrderBy(x => x.CurrentLevel * -1).Take(15).Select(x => x.Username).Max(x => x.Length);
            foreach (var item in gl.CurrentUsers.OrderBy(x => x.CurrentLevel * -1).Take(15))
            {
                lm.Add($"#{rnk} - {(item.Username == "" ? Context.Guild.GetUser(item.UserID).ToString().PadRight(space) : item.Username.PadRight(space))} Level: {item.CurrentLevel} XP: {(uint)item.CurrentXP}/{(uint)item.NextLevelXP}");
                rnk++;
            }
            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
            {
                Title = $"{Context.Guild.Name}",
                Description = $"```{string.Join('\n', lm)}```" + (rl.Count > 0 ? $"\n\nAchievable roles:\n{string.Join('\n', rl)}" : ""),
                Color = Color.Green,
                ThumbnailUrl = Context.Guild.IconUrl
            }.WithCurrentTimestamp().Build());
        }
        public class RankBuilder
        {
            public static System.Drawing.Image MakeRank(string username, string avtr, int level, int curXP, int nxtXP, System.Drawing.Color embc, int Rank, System.Drawing.Color bgc)
            {
                WebClient wc = new WebClient();
                byte[] bytes = wc.DownloadData(avtr);
                MemoryStream ms = new MemoryStream(bytes);
                System.Drawing.Image pfp = System.Drawing.Image.FromStream(ms);
                pfp = ResizeImage(pfp, 200, 200);
                var btmp = new Bitmap(913, 312);
                var canv = Graphics.FromImage(btmp);
                canv.SmoothingMode = SmoothingMode.AntiAlias;
                canv.FillPath(new SolidBrush(bgc), RoundedRect(new Rectangle(0, 0, 913, 312), 30));
                //draw pfp
                var rpfp = ClipToCircle(pfp, new PointF(pfp.Width / 2, pfp.Height / 2), pfp.Width / 2, bgc);
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
            if (gl.CurrentUsers.Any(x => x.UserID == user.Id))
            {
                var cu = gl.CurrentUsers.OrderBy(x => x.CurrentLevel * -1).ToList();
                var userlvl = cu.Find(x => x.UserID == user.Id);

                var img = RankBuilder.MakeRank(userlvl.Username,
                    Context.Guild.GetUser(userlvl.UserID).GetAvatarUrl(),
                    (int)userlvl.CurrentLevel,
                    (int)userlvl.CurrentXP,
                    (int)userlvl.NextLevelXP,
                    System.Drawing.Color.FromArgb(userlvl.EmbedColor.R, userlvl.EmbedColor.G, userlvl.EmbedColor.B),
                    gl.CurrentUsers.OrderBy(x => x.CurrentLevel * -1).ToList().IndexOf(userlvl) + 1,
                    System.Drawing.Color.FromArgb(userlvl.RankBackgound.R, userlvl.RankBackgound.G, userlvl.RankBackgound.B));

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
        [DiscordCommand("rankcard", description = "Change your Rank Card's settings!", commandHelp = "Usage:\n`(PREFIX)rankcard ping <on/off>`\n`(PREFIX)rankcard color <R> <G> <B>`\n`(PREFIX)rankcard backgroundcolor <R> <G> <B>`")]
        public async Task rc(params string[] args)
        {
            //get the guilds level settings
            var levelsettings = LevelHandler.GuildLeaderboards.Get(Context.Guild.Id);
            if (levelsettings == null)
                return;

            LevelUser usr;
            if (!levelsettings.CurrentUsers.Any(x => x.UserID == Context.User.Id))
            {
                usr = new LevelUser(Context.Guild.GetUser(Context.User.Id));
                levelsettings.CurrentUsers.Add(usr);
            }
            else
                usr = levelsettings.CurrentUsers.Find(x => x.UserID == Context.User.Id);

            if (args.Length == 0)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Your Rank Card",
                    Description = $"Here's your Settings for your Rank Card\n```\nMentions? {usr.MentionLevelup}\nColor: R:{usr.EmbedColor.R} G:{usr.EmbedColor.G} B:{usr.EmbedColor.B}\nBackground Color: R:{usr.RankBackgound.R} G:{usr.RankBackgound.G} B:{usr.RankBackgound.B}```\nYou can change these settings with these commands:\n`{GuildSettings.Prefix}rankcard ping <on/off>`\n`{GuildSettings.Prefix}rankcard color <R> <G> <B>`\n`{GuildSettings.Prefix}rankcard backgroundcolor <R> <G> <B>`",
                    Color = Discord.Color.Green
                }.WithCurrentTimestamp().Build());
                return;
            }

            switch (args[0].ToLower())
            {
                case "list":
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Your Rank Card",
                        Description = $"Here's your Settings for your Rank Card\n```\nMentions? {usr.MentionLevelup}\nColor: R:{usr.EmbedColor.R} G:{usr.EmbedColor.G} B:{usr.EmbedColor.B}\nBackground Color: R:{usr.RankBackgound.R} G:{usr.RankBackgound.G} B:{usr.RankBackgound.B}```\nYou can change these settings with these commands:\n`{GuildSettings.Prefix}rankcard ping <on/off>`\n`{GuildSettings.Prefix}rankcard color <R> <G> <B>`\n`{GuildSettings.Prefix}rankcard backgroundcolor <R> <G> <B>`",
                        Color = Discord.Color.Green
                    }.WithCurrentTimestamp().Build());
                    return;
                case "ping":
                    {
                        if (args.Length == 1)
                        {
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "What do you want to change?",
                                Description = $"Either use `on` or `off`\nExample: `{GuildSettings.Prefix}rankcard ping off`",
                                Color = Discord.Color.Orange
                            }.WithCurrentTimestamp().Build());
                            return;
                        }
                        switch (args[1].ToLower())
                        {
                            case "on":
                                {
                                    usr.MentionLevelup = true;
                                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                    {
                                        Title = "Success!",
                                        Description = $"The bot will now mention you on levelup's!",
                                        Color = Discord.Color.Green
                                    }.WithCurrentTimestamp().Build());
                                    return;
                                }
                            case "off":
                                {
                                    usr.MentionLevelup = false;
                                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                    {
                                        Title = "Success!",
                                        Description = $"The bot will no longer mention you on levelup's!",
                                        Color = Discord.Color.Green
                                    }.WithCurrentTimestamp().Build());
                                    return;
                                }
                            default:
                                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                {
                                    Title = "What do you want to change?",
                                    Description = $"Either use `on` or `off`\nExample: `{GuildSettings.Prefix}rankcard ping off`",
                                    Color = Discord.Color.Orange
                                }.WithCurrentTimestamp().Build());
                                return;
                        }
                    }
                    break;
                case "color":
                    if (args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Rank Card Color",
                            Description = $"The current Rank Card Color is this embeds color (R:{usr.EmbedColor.R} G:{usr.EmbedColor.G} B:{usr.EmbedColor.B}\nTo change the Rank Cards color Run `{GuildSettings.Prefix}rankcard color <R> <G> <B>`",
                            Color = new Color(usr.EmbedColor.R, usr.EmbedColor.G, usr.EmbedColor.B)
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if (args.Length == 4)
                    {
                        if (int.TryParse(args[1], out var R) && int.TryParse(args[2], out var G) && int.TryParse(args[3], out var B))
                        {
                            if (R > 255 || G > 255 || B > 255)
                            {
                                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                {
                                    Title = "Invalid parameters",
                                    Description = $"Please use the RGB format values between 0 and 255, for example: `{GuildSettings.Prefix}rankcard color 25 255 0`",
                                    Color = Color.Red
                                }.WithCurrentTimestamp().Build());
                                return;
                            }
                            usr.EmbedColor = new color(R, G, B);
                            levelsettings.SaveCurrent();
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Success!",
                                Description = $"Set the Rank Card Color to this embeds color (R:{usr.EmbedColor.R} G:{usr.EmbedColor.G} B:{usr.EmbedColor.B})",
                                Color = new Color(usr.EmbedColor.R, usr.EmbedColor.G, usr.EmbedColor.B)
                            }.WithCurrentTimestamp().Build());
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Invalid parameters",
                                Description = $"Please use the RGB format, for example: `{GuildSettings.Prefix}rankcard color 25 255 0`",
                                Color = Color.Red
                            }.WithCurrentTimestamp().Build());
                            return;
                        }
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Invalid parameters",
                            Description = $"Please use the RGB format, for example: `{GuildSettings.Prefix}rankcard color 25 255 0`",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    break;
                case "backgroundcolor":
                    if (args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Rank Card Background Color",
                            Description = $"The current Rank Cards Background Color is this embeds color (R:{usr.RankBackgound.R} G:{usr.RankBackgound.G} B:{usr.RankBackgound.B}\nTo change the Rank Cards Background Color Run `{GuildSettings.Prefix}rankcard backgroundcolor <R> <G> <B>`",
                            Color = new Color(usr.RankBackgound.R, usr.RankBackgound.G, usr.RankBackgound.B)
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if (args.Length == 4)
                    {
                        if (int.TryParse(args[1], out var R) && int.TryParse(args[2], out var G) && int.TryParse(args[3], out var B))
                        {
                            if (R > 255 || G > 255 || B > 255)
                            {
                                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                {
                                    Title = "Invalid parameters",
                                    Description = $"Please use the RGB format values between 0 and 255, for example: `{GuildSettings.Prefix}rankcard backgoundcolor 25 255 0`",
                                    Color = Color.Red
                                }.WithCurrentTimestamp().Build());
                                return;
                            }
                            usr.RankBackgound = new color(R, G, B);
                            levelsettings.SaveCurrent();
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Rank Card Background Color",
                                Description = $"The current Rank Cards Background Color is now set to this embeds color (R:{usr.RankBackgound.R} G:{usr.RankBackgound.G} B:{usr.RankBackgound.B})",
                                Color = new Color(usr.RankBackgound.R, usr.RankBackgound.G, usr.RankBackgound.B)
                            }.WithCurrentTimestamp().Build());
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Invalid parameters",
                                Description = $"Please use the RGB format, for example: `{GuildSettings.Prefix}rankcard backgoundcolor 25 255 0`",
                                Color = Color.Red
                            }.WithCurrentTimestamp().Build());
                            return;
                        }
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Invalid parameters",
                            Description = $"Please use the RGB format, for example: `{GuildSettings.Prefix}rankcard color 25 255 0`",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    break;
                default:
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "What do you want to change?",
                        Description = $"The arguements you provided are not recognized!",
                        Color = Discord.Color.Orange
                    }.WithCurrentTimestamp().Build());
                    return;
            }
        }

        [DiscordCommand("setlevel", RequiredPermission = true, description = "Sets a users levels", commandHelp = "Usage - `(PREFIX)setlevel <user> <level>`")]
        public async Task sl(params string[] args)
        {
            if (args.Length == 0)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Who? and how much?",
                    Description = "You didn't provied any arguments!",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            var user = GetUser(args[0]);
            if (user == null)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "That user is invalid",
                    Description = $"The user \"{args[0]}\" is invalid!",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            if (args.Length == 1)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "How many levels?",
                    Description = $"How many levels do you want to give {user.Mention}? use `{GuildSettings.Prefix}setlevel <@user> <level>`",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            if (args.Length == 2)
            {
                if (uint.TryParse(args[1], out var res))
                {
                    var gl = GuildLeaderboards.Get(Context.Guild.Id);
                    if (res > gl.Settings.maxlevel)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Its over 9000!",
                            Description = $"Well it's not but its over your guilds max level. if you want to see the guilds level config please run `{GuildSettings.Prefix}levelsettings list`",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    LevelUser lusr = null;
                    if (gl.CurrentUsers.Any(x => x.UserID == user.Id))
                    {
                        lusr = gl.CurrentUsers.Find(x => x.UserID == user.Id);
                        if (res > lusr.CurrentLevel)
                        {
                            for (uint i = lusr.CurrentLevel; i != res; i++)
                            {
                                lusr.NextLevelXP *= gl.Settings.LevelMultiplier;
                            }
                        }
                        if (res < lusr.CurrentLevel)
                        {
                            for (uint i = lusr.CurrentLevel; i != res; i--)
                            {
                                lusr.NextLevelXP /= gl.Settings.LevelMultiplier;
                            }
                        }
                        lusr.CurrentLevel = res;
                    }
                    else
                    {
                        lusr = new LevelUser(user);
                        if (res > lusr.CurrentLevel)
                        {
                            for (uint i = lusr.CurrentLevel; i != res; i++)
                            {
                                lusr.NextLevelXP *= gl.Settings.LevelMultiplier;
                            }
                        }
                        if (res < lusr.CurrentLevel)
                        {
                            for (uint i = lusr.CurrentLevel; i != res; i--)
                            {
                                lusr.NextLevelXP /= gl.Settings.LevelMultiplier;
                            }
                        }
                        lusr.CurrentLevel = res;
                        gl.CurrentUsers.Add(lusr);
                    }
                    gl.SaveCurrent();
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Success!",
                        Description = $"The user {lusr.Username} level is now set to {res}!",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                }
                else
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Invalid Value",
                        Description = $"Please provide a __positive whole__ number!",
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build());
                    //gl.SaveCurrent();
                    return;
                }
            }
        }
        [DiscordCommand("setxp", RequiredPermission = true, description = "Sets a users XP", commandHelp = "Usage - `(PREFIX)setxp <user> <xp>`")]
        public async Task sxp(params string[] args)
        {
            if (args.Length == 0)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Who? and how much?",
                    Description = "You didn't provied any arguments!",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            var user = GetUser(args[0]);
            if (user == null)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "That user is invalid",
                    Description = $"The user \"{args[0]}\" is invalid!",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            if (args.Length == 1)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "How much XP?",
                    Description = $"How much XP do you want to give {user.Mention}? use `{GuildSettings.Prefix}setxp <@user> <xp>`",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            if (args.Length == 2)
            {
                if (uint.TryParse(args[1], out var res))
                {
                    var gl = GuildLeaderboards.Get(Context.Guild.Id);
                    LevelUser lusr = null;
                    if (gl.CurrentUsers.Any(x => x.UserID == user.Id))
                    {
                        lusr = gl.CurrentUsers.Find(x => x.UserID == user.Id);
                        lusr.CurrentXP = res;
                    }
                    else
                    {
                        lusr = new LevelUser(user);
                        lusr.CurrentXP = res;
                        gl.CurrentUsers.Add(lusr);
                    }
                    gl.SaveCurrent();
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Success!",
                        Description = $"{lusr.Username}'s XP is now set to {res}!",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                }
                else
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Invalid Value",
                        Description = $"Please provide a __positive whole__ number!",
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build());
                    //gl.SaveCurrent();
                    return;
                }
            }
        }
        [DiscordCommand("givelevel", RequiredPermission = true, commandHelp = "Usage `(PREFIX)givelevel <user> <ammount>`", description = "Gives a user Levels")]
        public async Task gl(params string[] args)
        {
            if (args.Length == 0)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Who? and how much?",
                    Description = "You didn't provied any arguments!",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            var user = GetUser(args[0]);
            if (user == null)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "That user is invalid",
                    Description = $"The user \"{args[0]}\" is invalid!",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            if (args.Length == 1)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "How many levels?",
                    Description = $"How many levels do you want to give {user.Mention}? use `{GuildSettings.Prefix}givelevel <@user> <ammount>`",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            if (args.Length == 2)
            {
                if (uint.TryParse(args[1], out var res))
                {
                    var gl = GuildLeaderboards.Get(Context.Guild.Id);
                    if (res > gl.Settings.maxlevel)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Its over 9000!",
                            Description = $"Well it's not but its over your guilds max level. if you want to see the guilds level config please run `{GuildSettings.Prefix}levelsettings list`",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    LevelUser lusr = null;
                    if (gl.CurrentUsers.Any(x => x.UserID == user.Id))
                    {
                        lusr = gl.CurrentUsers.Find(x => x.UserID == user.Id);
                        if (res > lusr.CurrentLevel)
                        {
                            for (uint i = lusr.CurrentLevel; i != res; i++)
                            {
                                lusr.NextLevelXP *= gl.Settings.LevelMultiplier;
                            }
                        }
                        if (res < lusr.CurrentLevel)
                        {
                            for (uint i = lusr.CurrentLevel; i != res; i--)
                            {
                                lusr.NextLevelXP /= gl.Settings.LevelMultiplier;
                            }
                        }
                        lusr.CurrentLevel += res;
                    }
                    else
                    {
                        lusr = new LevelUser(user);
                        if (res > lusr.CurrentLevel)
                        {
                            for (uint i = lusr.CurrentLevel; i != res; i++)
                            {
                                lusr.NextLevelXP *= gl.Settings.LevelMultiplier;
                            }
                        }
                        if (res < lusr.CurrentLevel)
                        {
                            for (uint i = lusr.CurrentLevel; i != res; i--)
                            {
                                lusr.NextLevelXP /= gl.Settings.LevelMultiplier;
                            }
                        }
                        lusr.CurrentLevel += res;
                        gl.CurrentUsers.Add(lusr);
                    }
                    gl.SaveCurrent();
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Success!",
                        Description = $"The user {lusr.Username} level is now set to {res}!",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                }
                else
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Invalid Value",
                        Description = $"Please provide a __positive whole__ number!",
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build());
                    //gl.SaveCurrent();
                    return;
                }
            }
        }
        [DiscordCommand("givexp", RequiredPermission = true, description = "Gives a user XP", commandHelp = "Usage - `(PREFIX)givexp <user> <ammount>")]
        public async Task gxp(params string[] args)
        {
            if (args.Length == 0)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Who? and how much?",
                    Description = "You didn't provied any arguments!",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            var user = GetUser(args[0]);
            if (user == null)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "That user is invalid",
                    Description = $"The user \"{args[0]}\" is invalid!",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            if (args.Length == 1)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "How much XP?",
                    Description = $"How much XP do you want to give {user.Mention}? use `{GuildSettings.Prefix}givexp <@user> <ammount>`",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            if (args.Length == 2)
            {
                if (uint.TryParse(args[1], out var res))
                {
                    var gl = GuildLeaderboards.Get(Context.Guild.Id);
                    LevelUser lusr = null;
                    if (gl.CurrentUsers.Any(x => x.UserID == user.Id))
                    {
                        lusr = gl.CurrentUsers.Find(x => x.UserID == user.Id);
                        lusr.CurrentXP += res;
                    }
                    else
                    {
                        lusr = new LevelUser(user);
                        lusr.CurrentXP += res;
                        gl.CurrentUsers.Add(lusr);
                    }
                    gl.SaveCurrent();
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Success!",
                        Description = $"{lusr.Username}'s XP is now set to {res}!",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                }
                else
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Invalid Value",
                        Description = $"Please provide a __positive whole__ number!",
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build());
                    //gl.SaveCurrent();
                    return;
                }
            }
        }
        public static List<ulong> CurrentRF = new List<ulong>();
        [DiscordCommand("levelsettings", RequiredPermission = true, description = "Change how the level settings works!", commandHelp = "Usage:" +
            "`(PREFIX)levelsettings list`\n" +
            "`(PREFIX)levelsettings maxlevel/messagexp/voicexp/defaultxp/levelmultiplier/color/blacklist/ranks/refresh`")]
        public async Task LevelSettings(params string[] args)
        {
            if (args.Length == 0)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = $"What setting do you want to change?",
                    Description = $"Run the command `{GuildSettings.Prefix}help levelsettings` to see how to customize levels",
                    Color = Color.Orange
                }.WithCurrentTimestamp().Build());
                return;
            }
            var ls = LevelHandler.GuildLevelSettings.Get(Context.Guild.Id);
            if (ls == null)
                ls = new LevelHandler.GuildLevelSettings();
            var gl = GuildLeaderboards.Get(Context.Guild.Id);

            switch (args[0].ToLower())
            {
                case "list":
                    var ch = Context.Guild.GetTextChannel(ls.LevelUpChan);

                    List<string> bc = new List<string>();
                    foreach (var chan in ls.BlacklistedChannels)
                    {
                        var rch = Context.Guild.GetChannel(chan);
                        if (rch.GetType() == typeof(SocketTextChannel))
                            bc.Add($"⌨️ - {rch.Name}");
                        if (rch.GetType() == typeof(SocketVoiceChannel))
                            bc.Add($"🔊 - {rch.Name}");
                    }
                    List<string> rl = new List<string>();
                    foreach (var chan in ls.RankRoles.OrderBy(x => x.Key * -1))
                    {
                        rl.Add($"{chan.Key} - <@&{chan.Value}>");
                    }
                    Dictionary<string, string> gnrl = new Dictionary<string, string>();
                    gnrl.Add("XP Multiplier:", ls.LevelMultiplier.ToString());
                    gnrl.Add("XP Per Message:", ls.XpPerMessage.ToString());
                    gnrl.Add("XP Per Minute in VC:", ls.XpPerVCMinute.ToString());
                    gnrl.Add("Max Level:", ls.maxlevel.ToString());
                    gnrl.Add("Levelup Channel:", ch.Name);
                    gnrl.Add("Default XP", ls.DefaultBaseLevelXp.ToString());
                    int leng = gnrl.Keys.Max(x => x.Length);
                    List<string> final = new List<string>();
                    foreach (var itm in gnrl)
                        final.Add(itm.Key.PadRight(leng) + " " + itm.Value);
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Here's your guilds Level settings",
                        Description = $"Run the command `{GuildSettings.Prefix}help levelsettings` to see how to customize levels",
                        Fields = new List<EmbedFieldBuilder>()
                        {
                            new EmbedFieldBuilder()
                            {
                                //IsInline = true,
                                Name = "General",
                                Value = $"```{string.Join('\n', final)}```"
                            },
                            new EmbedFieldBuilder()
                            {
                                IsInline = true,
                                Name = "Blacklisted Channels",
                                Value = $"{(bc.Count > 0 ? string.Join('\n', bc) : $"You have no blacklisted channels, you can add one with `{GuildSettings.Prefix}blacklist <channel_name>`")}"
                            },
                            new EmbedFieldBuilder()
                            {
                                IsInline = true,
                                Name = "Level Roles",
                                Value = $"{(rl.Count > 0 ? string.Join('\n', rl) : $"You dont have any roles setup! Run `{GuildSettings.Prefix}levelsettings ranks`")}"
                            }
                        },
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    break;
                case "channel":
                    if (args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Levelup Channel",
                            Description = $"The current Levelup channel is <#{ls.LevelUpChan}>.\nTo Change the levelup channel run `{GuildSettings.Prefix}levelsettings channel <#channel>`",
                            Color = Color.Green
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    var channel = GetChannel(args[1]);
                    if (channel == null)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Not Found!",
                            Description = $"We didnt find a channel with the name/id of \"{args[1]}\"!",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    gl.Settings.LevelUpChan = channel.Id;
                    gl.SaveCurrent();
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Levelup Channel",
                        Description = $"Levelup channel is now set to <#{ls.LevelUpChan}>!\nTo Change the levelup channel run `{GuildSettings.Prefix}levelsettings channel <#channel>`",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    break;
                case "maxlevel":
                    if (args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Max Level",
                            Description = $"The current max level is {ls.maxlevel}.\nTo Change the max level run `{GuildSettings.Prefix}levelsettings maxlevel <level>`",
                            Color = Color.Green
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if (args.Length == 2)
                    {
                        if (uint.TryParse(args[1], out var res))
                        {
                            gl.Settings.maxlevel = res;
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Max Level",
                                Description = $"Max level is now set to {ls.maxlevel}!",
                                Color = Color.Green
                            }.WithCurrentTimestamp().Build());
                            gl.SaveCurrent();
                            return;
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Invalid Value",
                                Description = $"Please provide a __positive whole__ number!",
                                Color = Color.Red
                            }.WithCurrentTimestamp().Build());
                            //gl.SaveCurrent();
                            return;
                        }
                    }
                    break;
                case "messagexp":
                    if (args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "XP per Message",
                            Description = $"The current XP per Message is {ls.XpPerMessage}\nTo change the ammount of XP per message run `{GuildSettings.Prefix}levelsettings messagexp <ammount>`",
                            Color = Color.Green
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if (args.Length == 2)
                    {
                        if (uint.TryParse(args[1], out var res))
                        {
                            gl.Settings.XpPerMessage = res;
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "XP per Message",
                                Description = $"XP per Message is now set to {ls.XpPerMessage}!",
                                Color = Color.Green
                            }.WithCurrentTimestamp().Build());
                            gl.SaveCurrent();
                            return;
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Invalid Value",
                                Description = $"Please provide a __positive whole__ number!",
                                Color = Color.Red
                            }.WithCurrentTimestamp().Build());
                            gl.SaveCurrent();
                            return;
                        }
                    }
                    break;
                case "voicexp":
                    if (args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "XP per Voice Channel Minute",
                            Description = $"The current XP per Voice Channel Minute is {ls.XpPerVCMinute}\nTo change the ammount of XP per minute in VC run `(PREFIX)levelsettings voicexp <ammount>`",
                            Color = Color.Green
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if (args.Length == 2)
                    {
                        if (uint.TryParse(args[1], out var res))
                        {
                            gl.Settings.XpPerVCMinute = res;
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "XP per Voice Channel Minute",
                                Description = $"XP per Voice Channel Minute is now set to {ls.XpPerVCMinute}!",
                                Color = Color.Green
                            }.WithCurrentTimestamp().Build());
                            gl.SaveCurrent();
                            return;
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Invalid Value",
                                Description = $"Please provide a __positive whole__ number!",
                                Color = Color.Red
                            }.WithCurrentTimestamp().Build());
                            gl.SaveCurrent();
                            return;
                        }
                    }
                    break;
                case "defaultxp":
                    if (args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Default XP",
                            Description = $"The current Default XP is {ls.DefaultBaseLevelXp}\nTo change the defualt XP run `{GuildSettings.Prefix}levelsettings defaultxp <value>`",
                            Color = Color.Green
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if (args.Length == 2)
                    {
                        if (uint.TryParse(args[1], out var res))
                        {
                            gl.Settings.DefaultBaseLevelXp = res;
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Default XP",
                                Description = $"Default XP is now set to {ls.DefaultBaseLevelXp}!",
                                Color = Color.Green
                            }.WithCurrentTimestamp().Build());
                            gl.SaveCurrent();
                            return;
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Invalid Value",
                                Description = $"Please provide a __positive whole__ number!",
                                Color = Color.Red
                            }.WithCurrentTimestamp().Build());
                            gl.SaveCurrent();
                            return;
                        }
                    }
                    break;
                case "xpmultiplier":
                    if (args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "XP Multiplier",
                            Description = $"The current XP Multiplier is {ls.LevelMultiplier}\nTo change the XP Multiplier run `{GuildSettings.Prefix}levelsettings xpmultiplier <value>`",
                            Color = Color.Green
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if (args.Length == 2)
                    {
                        if (double.TryParse(args[1], out var res) && res > 0)
                        {
                            gl.Settings.LevelMultiplier = res;
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "XP Multiplier",
                                Description = $"XP Multiplier is now set to {ls.LevelMultiplier}!",
                                Color = Color.Green
                            }.WithCurrentTimestamp().Build());
                            gl.SaveCurrent();
                            return;
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Invalid Value",
                                Description = $"Please provide a __positive__ number!",
                                Color = Color.Red
                            }.WithCurrentTimestamp().Build());
                            gl.SaveCurrent();
                            return;
                        }
                    }
                    break;
                case "blacklist":
                    if (args.Length == 1)
                    {
                        List<string> blc = new List<string>();
                        foreach (var chan in ls.BlacklistedChannels)
                        {
                            var rch = Context.Guild.GetChannel(chan);
                            if (rch.GetType() == typeof(SocketTextChannel))
                                blc.Add($"⌨️ - <#{rch.Id}>");
                            if (rch.GetType() == typeof(SocketVoiceChannel))
                                blc.Add($"🔊 - {rch.Name}");
                        }
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Blacklisted Channels",
                            Description = $"The current Blacklisted Channels are:\n\n{(blc.Count > 0 ? string.Join("\n", blc) : "None.")}\n\nTo add a blacklisted channel Run `{GuildSettings.Prefix}levelsettings blacklist add <channel>`\nTo remove a blacklisted channel Run `{GuildSettings.Prefix}levelsettings blacklist remove <channel>`",
                            Color = Color.Green
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if (args.Length >= 3)
                    {
                        var name = string.Join(' ', args.Skip(2));
                        var chan = GetChannel(name);
                        if (chan == null)
                        {
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Not Found!",
                                Description = $"We didnt find a channel with the name/id of \"{name}\"!",
                                Color = Color.Red
                            }.WithCurrentTimestamp().Build());
                            return;
                        }
                        if (args[1] == "add")
                        {
                            if (gl.Settings.BlacklistedChannels.Contains(chan.Id))
                            {
                                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                {
                                    Title = "Already added",
                                    Description = $"\"{name}\" is already in the blacklisted channels!",
                                    Color = Color.Red
                                }.WithCurrentTimestamp().Build());
                                return;
                            }
                            gl.Settings.BlacklistedChannels.Add(chan.Id);
                            gl.SaveCurrent();
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Blacklisted Channels",
                                Description = $"Added the channel {chan.Name} to the Blacklisted Channels!",
                                Color = Color.Green
                            }.WithCurrentTimestamp().Build());
                            return;
                        }
                        if (args[1] == "remove")
                        {
                            if (!gl.Settings.BlacklistedChannels.Contains(chan.Id))
                            {
                                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                {
                                    Title = "Not Blacklisted",
                                    Description = $"\"{name}\" is not in the blacklisted channels!",
                                    Color = Color.Red
                                }.WithCurrentTimestamp().Build());
                                return;
                            }
                            gl.Settings.BlacklistedChannels.Remove(chan.Id);
                            gl.SaveCurrent();
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Blacklisted Channels",
                                Description = $"Removed the channel {chan.Name} from the Blacklisted Channels!",
                                Color = Color.Green
                            }.WithCurrentTimestamp().Build());
                            return;
                        }
                    }
                    break;
                case "ranks":
                    if (args.Length == 1)
                    {
                        List<string> ranks = new List<string>();
                        foreach (var chan in ls.RankRoles.OrderBy(x => x.Key * -1))
                        {
                            ranks.Add($"Level {chan.Key} - <@&{chan.Value}>");
                        }
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Ranks",
                            Description = $"To add a role for a level, run `{GuildSettings.Prefix}levelsettings ranks add <@role> <level>`\nTo remove a role for a level, run `{GuildSettings.Prefix}levelsettings ranks remove <@role>`\n\n{(ranks.Count > 0 ? string.Join('\n', ranks) : $"You dont have any roles setup!")}",
                            Color = Color.Green,
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if (args.Length == 3)
                    {
                        if (args[1].ToLower() == "remove")
                        {
                            var role = GetRole(args[2]);
                            if (role == null)
                            {
                                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                {
                                    Title = "That role is invalid!",
                                    Description = "Please make sure you spell the role name right, or provided a valid id or mention.",
                                    Color = Color.Red,

                                }.WithCurrentTimestamp().Build());
                            }
                            if (ls.RankRoles.ContainsValue(role.Id))
                            {
                                gl.Settings.RankRoles.Remove(gl.Settings.RankRoles.First(x => x.Value == role.Id).Key);
                                gl.SaveCurrent();

                                List<string> ranks = new List<string>();
                                foreach (var chan in gl.Settings.RankRoles.OrderBy(x => x.Key * -1))
                                {
                                    ranks.Add($"Level {chan.Key} - <@&{chan.Value}>");
                                }
                                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                {
                                    Title = "Success!",
                                    Description = $"Removed {role.Mention}!\n\n{(ranks.Count > 0 ? string.Join('\n', ranks) : $"You dont have any roles setup!")}",
                                    Color = Color.Green,

                                }.WithCurrentTimestamp().Build());
                                return;
                            }
                            else
                            {
                                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                {
                                    Title = "That role isn't added!",
                                    Description = $"The role {role.Mention} isnt in the Ranked roles list, therefor we can't remove it!",
                                    Color = Color.Red,

                                }.WithCurrentTimestamp().Build());
                                return;
                            }
                        }
                    }
                    if (args.Length == 4)
                    {
                        var role = GetRole(args[2]);
                        if (role == null)
                        {
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "That role is invalid!",
                                Description = "Please make sure you spell the role name right, or provided a valid id or mention.",
                                Color = Color.Red,

                            }.WithCurrentTimestamp().Build());
                        }
                        if (uint.TryParse(args[3], out var res))
                        {
                            if (args[1].ToLower() == "add")
                            {
                                if (ls.RankRoles.ContainsValue(role.Id))
                                {
                                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                    {
                                        Title = "That role is already added!",
                                        Description = $"The role {role.Mention} is already added for level {ls.RankRoles.First(x => x.Value == role.Id).Key}!",
                                        Color = Color.Red,

                                    }.WithCurrentTimestamp().Build());
                                    return;
                                }
                                if (ls.RankRoles.ContainsKey(res))
                                {
                                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                    {
                                        Title = "That Level already has a role!",
                                        Description = $"The level {res} has the role <@&{ls.RankRoles[res]}> assigned to it! therefor we can't add another role!",
                                        Color = Color.Red,

                                    }.WithCurrentTimestamp().Build());
                                    return;
                                }
                                gl.Settings.RankRoles.Add(res, role.Id);
                                gl.SaveCurrent();
                                List<string> ranks = new List<string>();
                                foreach (var chan in gl.Settings.RankRoles.OrderBy(x => x.Key * -1))
                                {
                                    ranks.Add($"Level {chan.Key} - <@&{chan.Value}>");
                                }
                                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                {
                                    Title = "Success!",
                                    Description = $"Added {role.Mention} to level {res}!\n\n{(ranks.Count > 0 ? string.Join('\n', ranks) + $"\n\nIf you want to add this role to people who have level {res} or higher please run {GuildSettings.Prefix}levelsettings refresh {role.Mention}" : $"You dont have any roles setup!")}",
                                    Color = Color.Green,

                                }.WithCurrentTimestamp().Build());
                                return;
                            }
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "That number is invalid!",
                                Description = "Please provide a __Positive Whole__ number!",
                                Color = Color.Red,
                            }.WithCurrentTimestamp().Build());
                        }
                    }
                    break;
                case "refresh":
                    if (args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "What do you want to refresh",
                            Description = $"You can refresh ranks for users by running `{GuildSettings.Prefix}levelsettings refresh <role>`",
                            Color = Color.Orange,

                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if (args.Length == 2)
                    {
                        var role = GetRole(args[1]);
                        if (role == null)
                        {
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "That role is invalid!",
                                Description = "Please make sure you spell the role name right, or provided a valid id or mention.",
                                Color = Color.Red,

                            }.WithCurrentTimestamp().Build());
                            return;
                        }
                        if (CurrentRF.Contains(Context.Guild.Id))
                        {
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "THere's a refresh already happening!",
                                Description = "Please wait untill the previous refresh completes",
                                Color = Color.Red,

                            }.WithCurrentTimestamp().Build());
                            return;
                        }
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Are you sure?",
                            Description = $"This process can take some time depending on the ammount of users you have in your guild. please type \"{GuildSettings.Prefix}levelsettings refresh {role.Mention} confirm\"",
                            Color = Color.Orange
                        }.WithCurrentTimestamp().Build());
                    }
                    if (args.Length == 3)
                    {
                        if (args[2].ToLower() == "confirm")
                        {
                            if (CurrentRF.Contains(Context.Guild.Id))
                            {
                                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                {
                                    Title = "THere's a refresh already happening!",
                                    Description = "Please wait untill the previous refresh completes",
                                    Color = Color.Red,

                                }.WithCurrentTimestamp().Build());
                                return;
                            }
                            var role = GetRole(args[1]);
                            if (role == null)
                            {
                                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                {
                                    Title = "That role is invalid!",
                                    Description = "Please make sure you spell the role name right, or provided a valid id or mention.",
                                    Color = Color.Red,

                                }.WithCurrentTimestamp().Build());
                            }
                            if (ls.RankRoles.ContainsValue(role.Id))
                            {
                                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                {
                                    Title = "We're on it!",
                                    Description = "We are working on giving the users there roles! I'l send a message here when the process is finnished",
                                    Color = Color.Green
                                }.WithCurrentTimestamp().Build());
                                CurrentRF.Add(Context.Guild.Id);
                                new Thread(() => LevelHandler.GiveForNewRole(gl, role, ls.RankRoles.First(x => x.Value == role.Id).Key, Context.User as SocketGuildUser, Context.Channel as SocketTextChannel)).Start();
                            }
                        }
                    }
                    break;
            }
        }
    }
}
