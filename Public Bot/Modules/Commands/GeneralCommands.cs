using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
                    Title = "Heres some tips to setup this bot",
                    Description = "You can join our [Discord](https://discord.gg/KDErQR) for more help setting up the bot in your server",
                    Fields = new List<EmbedFieldBuilder>()
                    {
                        new EmbedFieldBuilder()
                        {
                            IsInline = true,
                            Name = "Bot Permissions",
                            Value = $"Heres the bots current discord permissions:\n```{string.Join('\n', final)}```\nSome Modules like the moderation module need kick and ban permissions."
                        },
                        new EmbedFieldBuilder()
                        {
                            IsInline = true,
                            Name = "Modules",
                            Value = $"This is the current Module Settings with there status\n```{string.Join('\n', md)}```\nYou can Enable/Disable Modules with the `{GuildSettings.Prefix}modules` command."
                        },
                        new EmbedFieldBuilder()
                        {
                            IsInline = true,
                            Name = "Permission roles",
                            Value = $"These roles have elevated permissions and have access to all commands within the bot\n\n<@&{string.Join(">\n<@&", GuildSettings.PermissionRoles)}>\n\nTo add one use `{GuildSettings.Prefix}addpermission <@role>`\nTo remove one use `{GuildSettings.Prefix}removepermission <@role>`"
                        },
                        new EmbedFieldBuilder()
                        {
                            IsInline = true,
                            Name = "Levels",
                            Value = $"This bot features a leveling system where users can obtain roles with levels, its currently {(GuildSettings.ModulesSettings["🧪 Levels 🧪"] ? $"Enabled, You can configure it with `{GuildSettings.Prefix}levelsettings list`, if your stuck with setting up levels try `{GuildSettings.Prefix}help levelsettings`" : $"Disabled, You can enable it with `{GuildSettings.Prefix}modules enable Levels`")}"
                        },
                        new EmbedFieldBuilder()
                        {
                            IsInline = true,
                            Name = "Logs",
                            Value = $"This bot has logging, You can set a channel for logs with `{GuildSettings.Prefix}logs channel <channel>`" //`(PREFIX)logs channel <channel>`, `(PREFIX)logs on/off`
                        },

                    },
                    Color = Color.Green
                }.Build());
            }
            else if(args.Length == 1)
            {
                var cmds = ReadCurrentCommands(GuildSettings.Prefix);
                var perm = HelpMessageHandler.CalcHelpPage(Context.User as SocketGuildUser, GuildSettings);
                if (cmds.Any(x => x.CommandName == args[0]))
                {
                    var cmd = cmds.Find(x => x.CommandName == args[0]);
                    if (perm == HelpMessageHandler.HelpPages.Public && cmd.RequiresPermission)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "**You cant access this command!**",
                            Description = "You dont have permission to use this command, therefor were not gonna show you how.",
                            Color = Color.Red,
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = $"**{GuildSettings.Prefix}{cmd.CommandName}**",
                        Description = $"{cmd.CommandDescription}\n{cmd.CommandHelpMessage}",
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
                Title = "Heres my invite!",
                Description = "You can invite me [Here](https://discord.com/api/oauth2/authorize?client_id=722435272532426783&permissions=8&scope=bot)",
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
        [DiscordCommand("ping", description = "Gets the bots ping to discord", commandHelp = "Usage `(PREFIX)ping`")]
        public async Task ping()
        {
            var msg = await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
            {
                Title = "Discord Ping and Status",
                Color = Color.Green,
                Description = $"You can view Discord's status page [Here](https://status.discord.com/]\n)" +
                              $"```Gateway:     Fetching...\n" +
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
                Description = $"You can view Discord's status page [Here](https://status.discord.com/]\n)" +
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
       
    }
}
