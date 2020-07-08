using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Color = Discord.Color;

namespace Public_Bot.Modules.Commands
{
    [DiscordCommandClass("👨🏼‍💻 General 👨🏼‍💻", "General bot commands for everyone!")]
    class GeneralCommands : CommandModuleBase
    {
        [DiscordCommand("help", description = "shows all help messages for all enabled modules", commandHelp = "Usage: `(PREFIX)help`, `(PREFIX)help <command_name>`" )]
        public async Task help(params string[] args)
        {
            if(args.Length == 0)
            {
                HelpMessageHandler.BuildHelpPages(GuildSettings);
                var msg = await Context.Channel.SendMessageAsync("", false, HelpMessageHandler.HelpEmbedBuilder(1, HelpMessageHandler.CalcHelpPage(Context.Guild.GetUser(Context.Message.Author.Id), GuildSettings)));
            }
            else if(args[0] == "setup")
            {
                var perms = Context.Guild.GetUser(Context.Client.CurrentUser.Id).GuildPermissions;
                var pdin = GuildSettings.ModulesSettings.Keys.Max(x => string.Join(' ', x.Split(' ').Skip(1).Take(x.Split(' ').Length - 2)).Length) + 2;
                List<string> md = new List<string>();
                foreach (var m in GuildSettings.ModulesSettings)
                    md.Add($"{string.Join(' ', m.Key.Split(' ').Skip(1).Take(m.Key.Split(' ').Length - 2)).PadRight(pdin)} {(m.Value ? "✅" : "❌")}\n");
                Dictionary<string, string> prm = new Dictionary<string, string>();
                prm.Add("Admin:", $"{(perms.Administrator ? "✅" : "❌")}");
                prm.Add("Kick:", $"{(perms.KickMembers ? "✅" : "❌")}");
                prm.Add("Ban:", $"{(perms.BanMembers ? "✅" : "❌")}");
                prm.Add("Mentions:", $"{(perms.MentionEveryone ? "✅" : "❌")}");
                prm.Add("Manage Guild:", $"{(perms.ManageGuild ? "✅" : "❌")}");
                prm.Add("Manage Msg's:", $"{(perms.ManageMessages ? "✅" : "❌")}");
                prm.Add("Channels:", $"{(perms.ViewChannel ? "✅" : "❌")}");
                prm.Add("Manage Roles:", $"{(perms.ManageRoles ? "✅" : "❌")}");
                int leng = prm.Keys.Max(x => x.Length);
                List<string> final = new List<string>();
                foreach (var itm in prm)
                    final.Add(itm.Key.PadRight(leng) + " " + itm.Value);
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Here are some tips to setup this bot",
                    Description = "You can join our [Discord](https://discord.gg/KDErQR) for more help setting up the bot in your server",
                    Fields = new List<EmbedFieldBuilder>()
                    {
                        new EmbedFieldBuilder()
                        {
                            IsInline = true,
                            Name = "Bot Permissions",
                            Value = $"> Here's the bot's current Discord permissions:\n```{string.Join('\n', final)}```\n> Some Modules like the moderation module require kick and ban permissions."
                        },
                        new EmbedFieldBuilder()
                        {
                            IsInline = true,
                            Name = "Modules",
                            Value = $"> These are the current Module Settings with their statuses\n```{string.Join('\n', md)}```\n> You can Enable/Disable Modules with the `{GuildSettings.Prefix}modules` command."
                        },
                        new EmbedFieldBuilder()
                        {
                            IsInline = true,
                            Name = "Permission roles",
                            Value = $">>> These roles have elevated permissions and have access to all commands within the bot\n\n<@&{string.Join(">\n<@&", GuildSettings.PermissionRoles)}>\n\nTo add one, use `{GuildSettings.Prefix}addpermission <@role>`\nTo remove one, use `{GuildSettings.Prefix}removepermission <@role>`"
                        },
                        new EmbedFieldBuilder()
                        {
                            IsInline = true,
                            Name = "Levels",
                            Value = $">>> This bot features a leveling system where users can obtain roles with levels. It's currently {(GuildSettings.ModulesSettings["🧪 Levels 🧪"] ? $"Enabled - you can configure it with `{GuildSettings.Prefix}levelsettings list`. If you are stuck with setting up levels, try `{GuildSettings.Prefix}help levelsettings`" : $"Disabled - you can enable it with `{GuildSettings.Prefix}modules enable Levels`")}"
                        },
                        new EmbedFieldBuilder()
                        {
                            IsInline = true,
                            Name = "Logs",
                            Value = $">>> This bot has logging; You can set a channel for logs with `{GuildSettings.Prefix}logs channel <channel>`" //`(PREFIX)logs channel <channel>`, `(PREFIX)logs on/off`
                        },

                    },
                    Color = Color.Green
                }.Build());
            }
            else if(args.Length == 1)
            {
                var cmds = ReadCurrentCommands(GuildSettings.Prefix);
                var perm = HelpMessageHandler.CalcHelpPage(Context.User as SocketGuildUser, GuildSettings);
                if (cmds.Any(x => x.HasName(args[0].ToLower())))
                {
                    var cmd = cmds.Find(x => x.HasName(args[0].ToLower()));
                    if (perm == HelpMessageHandler.HelpPages.Public && cmd.RequiresPermission)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "**You can't access this command!**",
                            Description = "You don't have permission to use this command, therefore we're not gonna show you how.",
                            Color = Color.Red,
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = $"**{GuildSettings.Prefix}{cmd.CommandName}**",
                        Description = $"Here's some info about the command {cmd.CommandName}",
                        Fields = new List<EmbedFieldBuilder>()
                        {
                            new EmbedFieldBuilder()
                            {
                                Name = "Command Description",
                                Value = $"```\n{cmd.CommandDescription}```",
                            },
                            new EmbedFieldBuilder()
                            {
                                Name = "Command Help",
                                Value = $"\n{cmd.CommandHelpMessage}",
                            },
                            cmd.Alts.Count > 0 ? new EmbedFieldBuilder()
                            {
                                Name = "Alternative Command Names (alts)",
                                Value = $"```{string.Join(", ", cmd.Alts) }```",
                            } : new EmbedFieldBuilder() 
                            {
                                Name = "Alternative Command Names (alts)",
                                Value = "None",
                            },

                        },
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    return;
                }
            }

        }
        [DiscordCommand("invite", description = "Provides an invite for this bot")]
        public async Task invite()
        {
            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
            {
                Title = "Here's my invite!",
                Description = "You can invite me [here](https://discord.com/api/oauth2/authorize?client_id=722435272532426783&permissions=427683062&scope=bot)",
                Color = Color.Green
            }.WithCurrentTimestamp().Build());
        }
        class PingData
        {
            public partial class DiscordApiPing
            {
                public Period Period { get; set; }
                public List<MetricElement> Metrics { get; set; }
                public DiscordApiPingSummary Summary { get; set; }
            }

            public partial class MetricElement
            {
                public MetricMetric Metric { get; set; }
                public MetricSummary Summary { get; set; }
                public List<Datum> Data { get; set; }
            }

            public partial class Datum
            {
                public long Timestamp { get; set; }
                public long Value { get; set; }
            }

            public partial class MetricMetric
            {
                public string Name { get; set; }
                public string MetricIdentifier { get; set; }
                public DateTimeOffset CreatedAt { get; set; }
                public DateTimeOffset UpdatedAt { get; set; }
                public string Id { get; set; }
                public string MetricsProviderId { get; set; }
                public string MetricsDisplayId { get; set; }
                public DateTimeOffset MostRecentDataAt { get; set; }
                public bool Backfilled { get; set; }
                public DateTimeOffset LastFetchedAt { get; set; }
                public long BackfillPercentage { get; set; }
            }

            public partial class MetricSummary
            {
                public double Sum { get; set; }
                public double Mean { get; set; }
            }

            public partial class Period
            {
                public long Count { get; set; }
                public long Interval { get; set; }
                public string Identifier { get; set; }
            }

            public partial class DiscordApiPingSummary
            {
                public double Sum { get; set; }
                public double Mean { get; set; }
                public long Last { get; set; }
            }
        }
        [DiscordCommand("ping", description = "Gets the bot's ping to Discord", commandHelp = "Usage `(PREFIX)ping`")]
        public async Task ping()
        {
            var msg = await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
            {
                Title = "Discord Ping and Status",
                Color = Color.Green,
                Description = $"You can view Discord's status page [here](https://status.discord.com/)\n" +
                              $"```\nGateway:     Fetching...\n" +
                              $"Api Latest:  Fetching...\n" +
                              $"Api Average: Fetching...```",
                Footer = new EmbedFooterBuilder()
                {
                    Text = "Last Updated: Fetching..."
                }
            }.Build());
            
            HttpClient c = new HttpClient();
            var resp = await c.GetAsync("https://discord.statuspage.io/metrics-display/ztt4777v23lf/day.json");
            var cont = await resp.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<PingData.DiscordApiPing>(cont);
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var tm = epoch.AddSeconds(data.Metrics.First().Data.Last().Timestamp);
            await msg.ModifyAsync(x => x.Embed = new EmbedBuilder()
            {
                Title = "Discord Ping and Status",
                Color = Color.Green,
                Description = $"You can view Discord's status page [here](https://status.discord.com/)" +
                              $"```Gateway:     {this.Context.Client.Latency}\n" +
                              $"Api Latest:  {data.Summary.Last}\n" +
                              $"Api Average: {data.Summary.Mean}```",
                Timestamp = tm,
                Footer = new EmbedFooterBuilder()
                {
                    Text = "Last Updated: "
                }
            }.Build());
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
                    g.FillPath(new SolidBrush(System.Drawing.Color.FromArgb(40, 40, 40)), LevelCommands.RankBuilder.RoundedRect(new Rectangle(0, 0, 960, 540), 30));
                else
                {
                    byte[] bytes = wc.DownloadData(bannerurl);
                    MemoryStream ms = new MemoryStream(bytes);
                    System.Drawing.Image bannr = System.Drawing.Image.FromStream(ms);
                    g.DrawImage(RoundCorners(bannr, 30), new PointF(0, 0));
                    g.FillPath(new SolidBrush(System.Drawing.Color.FromArgb(200, 40, 40, 40)), LevelCommands.RankBuilder.RoundedRect(new Rectangle(30, 30, 900, 480), 30));

                }
                if(serverlogo != null)
                {
                    byte[] bytes2 = wc.DownloadData(serverlogo);
                    MemoryStream ms2 = new MemoryStream(bytes2);
                    var img = System.Drawing.Image.FromStream(ms2);
                    var Icon = LevelCommands.RankBuilder.ResizeImage(img, 150, 150);
                    img.Dispose();

                    g.DrawImage(LevelCommands.RankBuilder.ClipToCircle(Icon, new PointF(Icon.Width / 2, Icon.Height / 2), Icon.Width / 2, System.Drawing.Color.Transparent), new PointF(baseImg.Width / 2 - Icon.Width / 2, 120));
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
            private static System.Drawing.Image RoundCorners(System.Drawing.Image image, int cornerRadius)
            {
                cornerRadius *= 2;
                Bitmap roundedImage = new Bitmap(960, 540);
                GraphicsPath gp = new GraphicsPath();
                gp.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
                gp.AddArc(0 + roundedImage.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
                gp.AddArc(0 + roundedImage.Width - cornerRadius, 0 + roundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
                gp.AddArc(0, 0 + roundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
                using (Graphics g = Graphics.FromImage(roundedImage))
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.SetClip(gp);
                    g.DrawImage(LevelCommands.RankBuilder.ResizeImage(image, 960, 540), Point.Empty);
                }
                return roundedImage;
            }
        }

        [Alt("guildstats")]
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
}
