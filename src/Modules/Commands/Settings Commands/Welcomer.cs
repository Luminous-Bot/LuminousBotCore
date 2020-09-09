using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
namespace Public_Bot.Modules.Commands.Settings_Commands
{
    [DiscordCommandClass("⚙️ Settings ⚙️", "Change how this bot works in your server!")]
    public class Welcomer : CommandModuleBase
    {
        [DiscordCommand("welcomer",
            commandHelp = "`(PREFIX)welcomer`\n" +
                          "`(PREFIX)welcomer channel <#channel>`\n" +
                          "`(PREFIX)welcomer message <your welcome message for new users>`\n" +
                          "`(PREFIX)welcomer <enable/on>`\n" +
                          "`(PREFIX)welcomer <disable/off>`\n" +
                          "`(PREFIX)welcomer dm <true/false>`\n" +
                          "`(PREFIX)welcomer mentions <true/false>`\n\n" +
                          "You can use custom variables in the welcome message, here they are:\n" +
                            "**{user}** - Mentions the user\n" +
                            "**{user.name}** - The user's name without the tag. Example: quin\n" +
                            "**{user.username}** - The users full username. Example: quin#3017" +
                            "**{user.tag}** - The users discriminator. Example: 3017" +
                            "**{guild}** - The guilds name. Example: Swiss001 Official Discord Server" +
                            "**{guild.count}** - The guilds User count. Example: 8241" +
                            "**{guild.count.format}** - The guilds User count with st, nd, rd, and th. Example: 8241st",
            description = "Welcomes new users into your guild!", RequiredPermission = true)]
        public async Task welcomer(params string[] args)
        {
            var ws = GuildSettings.WelcomeCard;
            var welcomechan = Context.Guild.GetTextChannel(ws.WelcomeChannel);
            if (args.Length == 0)
            {
                //var img = WelcomeHandler.GenerateWelcomeImage(Context.User as SocketGuildUser, Context.Guild, GuildSettings.WelcomeCard);
                //img.Save($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}WelcomeCard.png", System.Drawing.Imaging.ImageFormat.Png);
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Author = new EmbedAuthorBuilder()
                    {
                        IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                        Name = "Welcomer Settings"
                    },
                    Description = "Here is how to setup the welcomer ```\n" + (
                          "(PREFIX)welcomer\n" +
                          "(PREFIX)welcomer channel <#channel>\n" +
                          "(PREFIX)welcomer message <your welcome message for new users>\n" +
                          "(PREFIX)welcomer image <image_url>" +
                          "(PREFIX)welcomer <enable/on>\n" +
                          "(PREFIX)welcomer <disable/off>\n" +
                          "(PREFIX)welcomer dm <true/false>\n" +
                          "(PREFIX)welcomer mentions <true/false>```\n Here's the current settings:").Replace("(PREFIX)", GuildSettings.Prefix),
                    Color = Blurple,
                    Fields = new List<EmbedFieldBuilder>()
                    {
                        new EmbedFieldBuilder()
                        {
                            Name = "Enabled?",
                            Value = ws.isEnabled.ToString()
                        },
                        new EmbedFieldBuilder()
                        {
                            Name = "Welcome Channel:",
                            Value = $"<#{ws.WelcomeChannel}>"
                        },
                        new EmbedFieldBuilder()
                        {
                            Name = "Image?:",
                            Value = ws.BackgroundUrl == null ? "None" : $"[Click me!]({ws.BackgroundUrl} \"ooo you found a easter egg! maybe theres more...?\")"
                        },
                        new EmbedFieldBuilder()
                        {
                            Name = "Mentions User:",
                            Value = $"{ws.MentionsUsers}"
                        },
                        new EmbedFieldBuilder()
                        {
                            Name = "Sent in Dm's:",
                            Value = $"{ws.DMs}"
                        },
                        new EmbedFieldBuilder()
                        {
                            Name = "Raw Welcome Message",
                            Value = $"```\n{ws.WelcomeMessage}```"
                        },
                        new EmbedFieldBuilder()
                        {
                            Name = "Compiled Welcome Message",
                            Value = $"{ws.WelcomeMessage.CompileVarMessage(Context.Guild, Context.User)}"
                        }
                    },
                    //ImageUrl = PingGenerator.GetImageLink($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}WelcomeCard.png").GetAwaiter().GetResult()
                }.WithCurrentTimestamp().Build());
                return;
            }

            switch (args[0].ToLower())
            {
                case "channel":
                    if(args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder() 
                        {
                            Author = new EmbedAuthorBuilder()
                            {
                                IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                                Name = "Welcomer Settings"
                            },
                            Description = $"The current welcome channel is <#{ws.WelcomeChannel}> [Click here to jump to channel](https://discord.com/channels/{Context.Guild.Id}/{ws.WelcomeChannel})",
                            Color = Blurple
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    var chan = GetChannel(args[1]);
                    if(chan == null)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Author = new EmbedAuthorBuilder()
                            {
                                IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                                Name = "Error"
                            },
                            Description = $"The channel you provided is invalid!",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    ws.WelcomeChannel = chan.Id;
                    GuildSettings.SaveGuildSettings();
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Author = new EmbedAuthorBuilder()
                        {
                            IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                            Name = "Success!"
                        },
                        Description = $"The channel is now set to <#{ws.WelcomeChannel}>",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    break;
                case "message":

                   // s.Replace("{guild}", guild.Name)
                   //.Replace("{guild.count.format}", guild.MemberCount.DisplayWithSuffix())
                   //.Replace("{guild.count}", guild.MemberCount.ToString())
                   //.Replace("{user}", user.Mention)
                   //.Replace("{user.name}", user.Username)
                   //.Replace("{user.username}", user.ToString())
                   //.Replace("{user.tag}", user.Discriminator);

                    if (args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Author = new EmbedAuthorBuilder()
                            {
                                IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                                Name = "Welcomer Settings"
                            },
                            Description = $"You can specify variables, heres a list of them:\n" +
                            $"**{{user}}** - Mentions the user\n" +
                            $"**{{user.name}}** - The user's name without the tag. Example: quin\n" +
                            $"**{{user.username}}** - The users full username. Example: quin#3017\n" +
                            $"**{{user.tag}}** - The users discriminator. Example: 3017\n" +
                            $"**{{guild}}** - The guilds name. Example: Swiss001 Official Discord Server\n" +
                            $"**{{guild.count}}** - The guilds User count. Example: 8241\n" +
                            $"**{{guild.count.format}}** - The guilds User count with st, nd, rd, and th. Example: 8241st",
                            Fields = new List<EmbedFieldBuilder>()
                            {
                                new EmbedFieldBuilder()
                                {
                                    Name = "Raw:",
                                    Value = $"```\n{ws.WelcomeMessage}```",
                                },
                                new EmbedFieldBuilder()
                                {
                                    Name = "Compiled:",
                                    Value = $"{ws.WelcomeMessage.CompileVarMessage(Context.Guild, Context.User)}",
                                }
                            },
                            Color = Blurple
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    string newMessage = string.Join(" ", args.Skip(1));
                    ws.WelcomeMessage = newMessage;
                    GuildSettings.SaveGuildSettings();
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Author = new EmbedAuthorBuilder()
                        {
                            IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                            Name = "Success!"
                        },
                        Description = $"Welcome Message has been changed!",
                        Fields = new List<EmbedFieldBuilder>()
                            {
                               new EmbedFieldBuilder()
                                {
                                    Name = "Raw:",
                                    Value = $"```\n{ws.WelcomeMessage}```",
                                },
                                new EmbedFieldBuilder()
                                {
                                    Name = "Compiled:",
                                    Value = $"{ws.WelcomeMessage.CompileVarMessage(Context.Guild, Context.User)}",
                                }
                            },
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    return;
                case "image":
                    if(args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Welcomer Image",
                            Description = ws.BackgroundUrl == null ? $"No image set, you can set one with `{GuildSettings.Prefix}welcomer image <url>" : $"The current image is set to [this]({ws.BackgroundUrl})",
                            ImageUrl = ws.BackgroundUrl == null ? "" : ws.BackgroundUrl,
                            Color = Blurple
                        }.Build());
                        return;
                    }
                    var url = args[1];

                    if(url == "clear")
                    {
                        GuildSettings.WelcomeCard.BackgroundUrl = null;
                        GuildSettings.SaveGuildSettings();
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder() 
                        {
                            Title = "Success!",
                            Description = "Cleared the welcomer image!",
                            Color = Color.Green
                        }.WithCurrentTimestamp().Build());
                        return;
                    }

                    bool o = false;
                    if(args.Length == 3)
                        if (args[2] == "-o")
                            o = true;

                    if (o)
                    {
                        GuildSettings.WelcomeCard.BackgroundUrl = url;
                        GuildSettings.SaveGuildSettings();
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Success!",
                            Description = $"Image now set to [this]({url}) image!",
                            Color = Color.Green
                        }.WithCurrentTimestamp().Build());
                        return;
                    }

                    if(!IsImageUrl(url))
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Invalid Image!",
                            Description = $"The image you provided was invalid, this is cause because the server didnt tell us its an image. if your 100% sure the url is an image you can add `-o` to the end of the command to overwrite it, like this: `{GuildSettings.Prefix}welcomer image {url} -o`",
                            Color = Color.Red
                        }.Build());
                        return;
                    }
                    GuildSettings.WelcomeCard.BackgroundUrl = url;
                    GuildSettings.SaveGuildSettings();
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Success!",
                        Description = $"Image now set to [this]({url}) image!",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());

                    break;
                case "dm":
                    if(args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Author = new EmbedAuthorBuilder()
                            {
                                IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                                Name = "Welcomer Settings"
                            },
                            Description = $"Welcome messages are sent in {(ws.DMs ? "Dm's!" : $"<#{ws.WelcomeChannel}>!")} (Dm's: {ws.DMs})",
                            Color = Blurple
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if(args[1].ToLower() == "true")
                    {
                        ws.DMs = true;
                        GuildSettings.SaveGuildSettings();
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Author = new EmbedAuthorBuilder()
                            {
                                IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                                Name = "Success!"
                            },
                            Description = $"User's will now recieve there welcome message in DM's!",
                            Color = Color.Green
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if (args[1].ToLower() == "false")
                    { 
                        ws.DMs = false;
                        GuildSettings.SaveGuildSettings();
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Author = new EmbedAuthorBuilder()
                            {
                                IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                                Name = "Success!"
                            },
                            Description = $"User's will no longer recieve there welcome message in DM's. Welcome messages will be sent in <#{ws.WelcomeChannel}>!",
                            Color = Color.Green
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Author = new EmbedAuthorBuilder()
                        {
                            IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                            Name = "Error!"
                        },
                        Description = $"Please either use \"true\" or \"false\"!",
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build());
                    return;
                case "mentions":
                    if(args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Author = new EmbedAuthorBuilder()
                            {
                                IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                                Name = "Welcome Settings!"
                            },
                            Description = $"New users will {(ws.MentionsUsers ? "" : "not")} be pinged with there welcome card",
                            Color = Blurple
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if (args[1].ToLower() == "true")
                    {
                        ws.MentionsUsers = true;
                        GuildSettings.SaveGuildSettings();
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Author = new EmbedAuthorBuilder()
                            {
                                IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                                Name = "Success!"
                            },
                            Description = $"New user's will be pinged on there welcome card!",
                            Color = Color.Green
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if (args[1].ToLower() == "false")
                    {
                        ws.MentionsUsers = false;
                        GuildSettings.SaveGuildSettings();
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Author = new EmbedAuthorBuilder()
                            {
                                IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                                Name = "Success!"
                            },
                            Description = $"New user's will no longer be pinged on there welcome card!",
                            Color = Color.Green
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Author = new EmbedAuthorBuilder()
                        {
                            IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                            Name = "Error!"
                        },
                        Description = $"Please either use \"true\" or \"false\"!",
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build());
                    return;
                    //Commented below line for obvious reasons
                    //break;
                case "enable":
                    ws.isEnabled = true;
                    GuildSettings.SaveGuildSettings();
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Author = new EmbedAuthorBuilder()
                        {
                            IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                            Name = "Success!"
                        },
                        Description = $"The welcomer is now enabled!",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    return;
                case "disable":
                    ws.isEnabled = false;
                    GuildSettings.SaveGuildSettings();
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Author = new EmbedAuthorBuilder()
                        {
                            IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                            Name = "Success!"
                        },
                        Description = $"The welcomer is now disabled!",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    return;
                case "on":
                    ws.isEnabled = true;
                    GuildSettings.SaveGuildSettings();
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Author = new EmbedAuthorBuilder()
                        {
                            IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                            Name = "Success!"
                        },
                        Description = $"The welcomer is now on!",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    return;
                case "off":
                    ws.isEnabled = false;
                    GuildSettings.SaveGuildSettings();
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Author = new EmbedAuthorBuilder()
                        {
                            IconUrl = Context.Client.CurrentUser.GetAvatarUrl(),
                            Name = "Success!"
                        },
                        Description = $"The welcomer is now off!",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    return;
                case "color":
                    if (args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Embed Color",
                            Description = $"The leave message color is {GuildSettings.WelcomeCard.EmbedColor}. Thats this embeds color",
                            Color = HexColorConverter.DiscordColorFromHex(GuildSettings.leaveMessage.EmbedColor)
                        }.Build());
                        return;
                    }

                    if (args.Length == 2)
                    {
                        string hexColor = args[1];
                        var regex = new Regex(@"(\d|[a-f]){6}");
                        if (regex.IsMatch(args[1]))
                        {
                            var hex = regex.Match(args[1]).Groups[0].Value;
                            //set here
                            this.GuildSettings.WelcomeCard.EmbedColor = "#" + hex;
                            GuildSettings.SaveGuildSettings();
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Success!",
                                Description = $"Set the Welcome Card messages Color to this Embed's Color ({GuildSettings.WelcomeCard.EmbedColor})",
                                Color = HexColorConverter.DiscordColorFromHex(GuildSettings.WelcomeCard.EmbedColor)
                            }.WithCurrentTimestamp().Build());
                            return;
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Invalid Hex Code!",
                                Description = $"The hex code you provided was invalid!",
                                Color = Color.Red
                            }.WithCurrentTimestamp().Build());
                            return;
                        }
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
                                    Description = $"Please use the RGB format values between 0 and 255, for example: `25 255 0`",
                                    Color = Color.Red
                                }.WithCurrentTimestamp().Build());
                                return;
                            }
                            //set here
                            this.GuildSettings.WelcomeCard.EmbedColor = HexColorConverter.HexFromColor(System.Drawing.Color.FromArgb(R, G, B));
                            GuildSettings.SaveGuildSettings();
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Success!",
                                Description = $"Set the Welcome Card's message Color to this Embed's Color ({GuildSettings.WelcomeCard.EmbedColor})",
                                Color = HexColorConverter.DiscordColorFromHex(GuildSettings.WelcomeCard.EmbedColor)
                            }.WithCurrentTimestamp().Build());
                            return;
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Invalid parameters",
                                Description = $"Please use the RGB format, for example: `25 255 0`",
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
                            Description = $"Please use the RGB format or Hex format, for example: `25 255 0` or `#00ff00`",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    break;
            }
        }
        bool IsImageUrl(string URL)
        {
            var req = (HttpWebRequest)HttpWebRequest.Create(URL);
            req.Method = "HEAD";
            using (var resp = req.GetResponse())
            {
                return resp.ContentType.ToLower(CultureInfo.InvariantCulture)
                           .StartsWith("image/");
            }
        }
    }
}