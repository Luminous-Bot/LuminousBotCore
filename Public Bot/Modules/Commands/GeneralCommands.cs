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
using Color = Discord.Color;

namespace Public_Bot.Modules.Commands
{
    [DiscordCommandClass("👨🏼‍💻 General 👨🏼‍💻", "General bot commands for everyone!")]
    class GeneralCommands : CommandModuleBase
    {
        [DiscordCommand("testwelcome", commandHelp = "`(PREFIX)testwelcome`", description = "Test your welcome message!")]
        public async Task Tw()
        {
            if (GuildSettings.WelcomeCard.isEnabled)
            {
                //var img = WelcomeHandler.GenerateWelcomeImage(Context.User as SocketGuildUser, Context.Guild, GuildSettings.WelcomeCard);
                //img.Save($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}WelcomeCard.png", System.Drawing.Imaging.ImageFormat.Png);
                await Context.Channel.SendMessageAsync("", false, GuildSettings.WelcomeCard.BuildEmbed(Context.User as SocketGuildUser, Context.Guild));
            }
        }
        [DiscordCommand("avatar",commandHelp ="`(PREFIX)avatar <user>`",description ="Shows the users avatar")]
        public async Task AvatarShows(params string[] args)
        {
            SocketGuildUser use;
            if (args.Length == 0)
            {
                use = Context.User as SocketGuildUser;
            }
            else
            {
                use = GetUser(args[0]);
            }
            if (use == null)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                {
                    Title = "Error",
                    Description = "That user is invalid!",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            var png = use.GetAvatarUrl(ImageFormat.Png, 1024);

            var jpeg = use.GetAvatarUrl(ImageFormat.Jpeg,1024);
            var webp = use.GetAvatarUrl(ImageFormat.WebP,1024);
            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder {
                Title = "User Avatar",
                Description = $"Here are the avatar links:\n1. **[PNG]({png})**\n2. **[JPEG]({jpeg})**\n3. **[WEBP]({webp})**",
                ImageUrl = jpeg,
                Color = Blurple
            }.WithCurrentTimestamp().Build());
        }
        [DiscordCommand("testleave",description ="test your leave message!",commandHelp ="`(PREFIX)testleave`")]
        public async Task HHE(params string[] _args)
        {
            var reqmBeD = await GuildSettings.leaveMessage.GenerateLeaveMessage(Context.User, Context.Guild);
            await Context.Channel.SendMessageAsync("", false, reqmBeD);
        }
        [DiscordCommand("youngest",commandHelp ="`(PREFIX)youngest`\n`(PREFIX)youngest <number_of_users>`",description ="Finds the x users newest to Discord")]
        public async Task Yu(params string[] argz)
        {
            var test = 10;
            if (int.TryParse(argz.FirstOrDefault(), out int retest)){
                test = retest;
            }
            if (test >= Context.Guild.MemberCount)
            {
                await Context.Channel.SendMessageAsync("You dont even have so many users :rofl:");
                return;
            }
            var yus = Context.Guild.Users;
            string cty = "```";
            var tenYoungestUsers = yus.ToList();
            tenYoungestUsers.RemoveAll(x => x.IsBot);
            tenYoungestUsers.Sort((prev, next) => 1/DateTimeOffset.Compare(prev.CreatedAt, next.CreatedAt));
            tenYoungestUsers.Reverse();
            var current = tenYoungestUsers.GetRange(0,test);
            current.ForEach(x => cty += (x.Username + '\t' + $"{x.CreatedAt.Month}/{x.CreatedAt.Day}/{x.CreatedAt.Year}" + '\n'));
            cty += "```";
            var mmbed = new EmbedBuilder
            {
                Title = "Youngest Users!",
                Description = cty,
                Color = Blurple
            }.WithCurrentTimestamp().Build();
            await Context.Channel.SendMessageAsync("",false,mmbed);
        }
        [DiscordCommand("oldest", commandHelp = "`(PREFIX)oldest`\n`(PREFIX)oldest <number_of_users>`", description = "Finds the x users eldest on Discord")]
        [Alt("eldest")]
        public async Task El(params string[] argz)
        {
            var test = 10;
            if (int.TryParse(argz.FirstOrDefault(), out int retest))
            {
                test = retest;
            }
            if (test >= Context.Guild.MemberCount)
            {
                await Context.Channel.SendMessageAsync("You dont even have so many users :rofl:");
                return;
            }
            var yus = Context.Guild.Users;
            string cty = "```";
            var tenYoungestUsers = yus.ToList();
            tenYoungestUsers.RemoveAll(x => x.IsBot);
            tenYoungestUsers.Sort((prev, next) => 1 / DateTimeOffset.Compare(prev.CreatedAt, next.CreatedAt));
            //tenYoungestUsers.Reverse();
            var current = tenYoungestUsers.GetRange(0, test);
            current.ForEach(x => cty += (x.Username + '\t' + $"{x.CreatedAt.Month}/{x.CreatedAt.Day}/{x.CreatedAt.Year}" + '\n'));
            cty += "```";
            var mmbed = new EmbedBuilder
            {
                Title = "Eldest Users!",
                Description = cty,
                Color = Blurple
            }.WithCurrentTimestamp().Build();
            await Context.Channel.SendMessageAsync("", false, mmbed);
        }
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
        [GuildPermissions(GuildPermission.ManageNicknames)]
        [DiscordCommand("nickname", commandHelp = "`(PREFIX)nickname <@user> <nickname>`", description = "Changes a user's nickname")]
        [Alt("nick")]
        public async Task NicknameUpdate(params string[] args)
        {

            if (args.Length == 0)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                {
                    Author = new EmbedAuthorBuilder()
                    {
                        Name = "Provide some Arguments!"
                    },
                    Color = Color.Orange,
                    Description = "You didnt provide any arguments!"
                }.WithCurrentTimestamp().Build());
                return;
            }
            var cgu = Context.Guild.GetUser(Context.User.Id);

            var user = GetUser(args[0]);

            if (user != null && user.Id != cgu.Id)
            {
                if (user.Id == Context.Guild.OwnerId)
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                    {
                        Author = new EmbedAuthorBuilder
                        {
                            IconUrl = cgu.GetAvatarUrl(),
                            Name = "Error!"
                        },
                        ThumbnailUrl = "https://cdn.hapsy.net/947e21c9-0551-4043-a942-338cec178ad2",
                        Color = Color.Red,
                        Title = "Missing Permissions",
                        Description = "The bot can't nick the owner... only he can."
                    }.WithCurrentTimestamp().Build());
                    return;
                }
                if (user.Hierarchy >= cgu.Hierarchy)
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                    {
                        Author = new EmbedAuthorBuilder
                        {
                            IconUrl = cgu.GetAvatarUrl(),
                            Name = "Error!"
                        },
                        Color = Color.Red,
                        Title = "Missing Permissions",
                        Description = "You can't change a users nickname who has the same role or a role above yours!"
                    }.WithCurrentTimestamp().Build());
                    return;
                }

                if (cgu.GuildPermissions.ManageNicknames || cgu.GuildPermissions.Administrator)
                {
                    try
                    {
                        await (user as SocketGuildUser).ModifyAsync(x => x.Nickname = string.Join(' ', args.Skip(1)));
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                        {
                            Author = new EmbedAuthorBuilder
                            {
                                IconUrl = cgu.GetAvatarUrl(),
                                Name = "Success!"
                            },
                            Color = Color.Green,
                            Description = $"Changed {user.Username}s nickname to {string.Join(' ', args.Skip(1))}"
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    catch
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                        {
                            Author = new EmbedAuthorBuilder
                            {
                                IconUrl = cgu.GetAvatarUrl(),
                                Name = "Error!"
                            },
                            Color = Color.Red,
                            Title = "Missing Permissions",
                            Description = "The bot doesn't have the `Manage Nicknames` permission!"
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                    {
                        Author = new EmbedAuthorBuilder
                        {
                            IconUrl = cgu.GetAvatarUrl(),
                            Name = "Error!"
                        },
                        Title = "Missing Permissions",
                        Description = "You need the `Manage Nicknames` permission to nick other users",
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build());
                    return;
                }

            }
            else
            {
                if (cgu.Id == Context.Guild.OwnerId)
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                    {
                        Author = new EmbedAuthorBuilder
                        {
                            IconUrl = cgu.GetAvatarUrl(),
                            Name = "Error!"
                        },
                        ThumbnailUrl = "https://cdn.hapsy.net/947e21c9-0551-4043-a942-338cec178ad2",
                        Color = Color.Red,
                        Title = "Missing Permissions",
                        Description = "The bot can't nick the owner... only he can."
                    }.WithCurrentTimestamp().Build());
                    return;
                }
                if (cgu.Hierarchy >= Context.Guild.CurrentUser.Hierarchy)
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                    {
                        Author = new EmbedAuthorBuilder
                        {
                            IconUrl = cgu.GetAvatarUrl(),
                            Name = "Error!"
                        },
                        Color = Color.Red,
                        Title = "Missing Permissions",
                        Description = "The bot can't change a users nickname that has the same role or a role above the bots!"
                    }.WithCurrentTimestamp().Build());
                    return;
                }
                string newNick = "";
                if (user == null)
                    newNick = string.Join(' ', args);
                else
                    newNick = string.Join(' ', args.Skip(1));
                if (cgu.GuildPermissions.ChangeNickname || cgu.GuildPermissions.Administrator)
                {
                    await cgu.ModifyAsync(x => x.Nickname = newNick);
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                    {
                        Author = new EmbedAuthorBuilder
                        {
                            IconUrl = cgu.GetAvatarUrl(),
                            Name = "Success!"
                        },
                        Color = Color.Green,
                        Description = $"Your nickname is now `{newNick}`"
                    }.WithCurrentTimestamp().Build());
                    return;
                }
                else
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                    {
                        Author = new EmbedAuthorBuilder
                        {
                            IconUrl = cgu.GetAvatarUrl(),
                            Name = "Error!"
                        },
                        Title = "Missing Permissions",
                        Description = "You need the `Change Nickname` permission to nick other users",
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build());
                    return;
                }
            }
        }
        [DiscordCommand("invite", description = "Provides an invite for this bot")]
        public async Task invite()
        {
            var x = new Random();
            string link = "https://discord.com/api/oauth2/authorize?client_id=722435272532426783&permissions=427683062&scope=bot";
            if (x.Next(0,500) == 28)
            {
                link = "https://www.youtube.com/watch?v=dQw4w9WgXcQ \"You're a lucky man\"";
            }
            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
            {
                //Title = "Here's my invite!",
                //Description = $"You can invite me [here]({link})",
                Color = Color.Green
            }.AddField("Here's my invite!", $"You can invite me [here]({link})")
            .WithCurrentTimestamp().Build());
        }
        
        [DiscordCommand("whois", description = "Shows information about the mentioned user", commandHelp = "Usage: `(PREFIX)whois <@user>`")]
        public async Task WhoIs(params string[] user)
        {
            SocketGuildUser userAccount;
            if (user.Length == 0)
                userAccount = Context.User as SocketGuildUser;
            else userAccount = GetUser(user[0]);

            if (userAccount == null)
            {
                EmbedBuilder error = new EmbedBuilder()
                {
                    Title = "That user is invalid ¯\\_(ツ)_/¯",
                    Description = "Please provide a valid user",
                    Color = Color.Red
                };
                await Context.Channel.SendMessageAsync("", false, error.Build());
                return;
            }
            string perms = "```\n";
            string permsRight = "";
            var props = typeof(Discord.GuildPermissions).GetProperties();
            var boolProps = props.Where(x => x.PropertyType == typeof(bool));
            var pTypes = boolProps.Where(x => (bool)x.GetValue(userAccount.GuildPermissions) == true).ToList();
            var nTypes = boolProps.Where(x => (bool)x.GetValue(userAccount.GuildPermissions) == false).ToList();
            var pd = boolProps.Max(x => x.Name.Length) + 1;
            if(nTypes.Count == 0)
                perms += "Administrator: ✅```";
            else
            {
                foreach (var perm in pTypes)
                    perms += $"{perm.Name}:".PadRight(pd) + " ✅\n";
                perms += "```";
                permsRight = "```\n";
                foreach (var nperm in nTypes)
                    permsRight += $"{nperm.Name}:".PadRight(pd) + " ❌\n";
                permsRight += "```";
            }
            var orderedroles = userAccount.Roles.OrderBy(x => x.Position * -1).ToArray();
            string roles = "";
            for(int i = 0; i < orderedroles.Count(); i++)
            {
                var role = orderedroles[i];
                if (roles.Length + role.Mention.Length < 1024)
                    roles += role.Mention + "\n";
                else
                {
                    roles += $"+ {orderedroles.Length - i + 1} more";
                    break;
                }
            }
            string stats = $"Nickname?: {(userAccount.Nickname == null ? "None" : userAccount.Nickname)}\n" +
                              $"Id: {userAccount.Id}\n" +
                              $"Creation Date: {userAccount.CreatedAt.UtcDateTime.ToString("r")}\n" +
                              $"Joined At: {userAccount.JoinedAt.Value.UtcDateTime.ToString("r")}\n";

            EmbedBuilder whois = new EmbedBuilder()
            {
                Author = new EmbedAuthorBuilder()
                {
                    Name = userAccount.ToString(),
                    IconUrl = userAccount.GetAvatarUrl()
                },
                Color = Blurple,
                Description = permsRight == "" ? "**Stats**\n" + stats : "",
                Fields = permsRight == "" ? new List<EmbedFieldBuilder>()
                {
                    new EmbedFieldBuilder()
                    {
                        Name = "Roles",
                        Value = roles,
                    },
                    new EmbedFieldBuilder()
                    {
                        Name = "Permissions ✅",
                        Value = perms,
                        IsInline = true
                    }
                } : new List<EmbedFieldBuilder>()
                {
                    new EmbedFieldBuilder()
                    { 
                        Name = "Stats",
                        Value = stats,
                        IsInline = true,

                    },
                    new EmbedFieldBuilder()
                    {
                        Name = "Roles",
                        Value = roles,
                        IsInline = false,

                    },
                    new EmbedFieldBuilder()
                    {
                        Name = "Permissions ✅",
                        Value = perms,
                        IsInline = true,
                    }
                }
            }.WithCurrentTimestamp();
            if (permsRight != "")
                whois.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Permissions ❌",
                    Value = permsRight,
                    IsInline = true
                });
            await Context.Channel.SendMessageAsync("", false, whois.Build());
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
            var data = JsonConvert.DeserializeObject<PingGenerator.PingData.DiscordApiPing>(cont);
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var tm = epoch.AddSeconds(data.Metrics.First().Data.Last().Timestamp);
            var gfp = await PingGenerator.Generate(data);
            gfp.Save($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}Ping.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            await msg.ModifyAsync(x => x.Embed = new EmbedBuilder()
            {
                Title = "Discord Ping and Status",
                Color = Color.Green,
                Description = $"You can view Discord's status page [here](https://status.discord.com/)" +
                              $"```Gateway:     {this.Context.Client.Latency}ms\n" +
                              $"Api Latest:  {data.Summary.Last}ms\n" +
                              $"Api Average: {data.Summary.Mean}ms```",
                Timestamp = tm,
                Footer = new EmbedFooterBuilder()
                {
                    Text = "Last Updated: "
                },
                Author = new EmbedAuthorBuilder()
                {
                    Name = "Powered by Hapsy",
                    IconUrl = "https://cdn.discordapp.com/avatars/223707551013797888/a_9f25c6c6374f4b57c5c9fb45baa5a8e8.png?size=256"
                },
                ImageUrl = PingGenerator.GetImageLink($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}Ping.jpg").GetAwaiter().GetResult()
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
