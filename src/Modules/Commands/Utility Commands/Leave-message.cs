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
    [DiscordCommandClass("🎚 Utilities 🎚", "Enable some vital features to improve your overall fuctionality!")]
    public class Leavemessage : CommandModuleBase
    {

        [DiscordCommand("leave-message",
            commandHelp ="`(PREFIX)leave-message <setting> <value>\n`" +
                         "`(PREFIX)leave-message on/off`\n" +
                         "`(PREFIX)leave-message title [new leave embed title]`\n" +
                         "`(PREFIX)leave-message message [new leave message]`\n"+
                         "`(PREFIX)leave-message channel #newleavemsgchnl`\n" + 
                         "\n\n" +
                         "*Use the below variables for the leave message:* \n"+
                         "**{user}** - Mentions the user\n" +
                         "**{user.name}** - The user's name without the tag. Example: quin\n" +
                            "**{user.username}** - The users full username. Example: quin#3017\n" +
                            "**{user.tag}** - The users discriminator. Example: 3017\n" +
                            "**{guild}** - The guilds name. Example: Swiss001 Official Discord Server\n" +
                            "**{guild.count}** - The guilds User count. Example: 8241\n" +
                            "**{guild.count.format}** - The guilds User count with st, nd, rd, and th. Example: 8241st",
            description ="gives the leave message settings", 
            RequiredPermission = true)]
        [Alt("leave")]
        public async Task leaveChanger(params string[] args)
        {
            if (args.Length == 0)
            {
                if (GuildSettings.leaveMessage == null)
                {
                    GuildSettings.leaveMessage = new Public_Bot.State.Types.LeaveMessage(GuildSettings, Context.Guild as IGuild);
                }
                var mBED = new EmbedBuilder
                {
                    Author = new EmbedAuthorBuilder() 
                    {
                        Name = "Leave Message Settings",
                        IconUrl = Context.Client.CurrentUser.GetAvatarUrl()
                    },
                    Color = Blurple
                }
                .AddField("**Enabled?**",GuildSettings.leaveMessage.isEnabled)
                .AddField("**Leave Embed Title:**", GuildSettings.leaveMessage.leaveTitle)
                .AddField("**Embed Message:**", GuildSettings.leaveMessage.leaveMessage)
                .AddField("**Leave Channel:**", $"<#{GuildSettings.leaveMessage.leaveChannel}>")
                .WithFooter($"Do {GuildSettings.Prefix}help leave-message to get the variables!!")
                .WithCurrentTimestamp().Build();
                await Context.Channel.SendMessageAsync("", false, mBED);
                return;
            }
            var x = args[0].ToLower();
            //Alright, so the attributes for the leaveMessage are~
            //isEnabled, GuildID, leaveChannel, leaveMessage, leaveTitle
            //INEFFICIENT CODE BELOW, WILL IMPROVE LATER
            switch (x)
            {
                case "enable":
                    GuildSettings.leaveMessage.isEnabled = true;
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder 
                    { 
                        Title = "Leave Message Settings Updated", 
                        Description = "Leave Message is now enabled!",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    break;
                case "on":
                    GuildSettings.leaveMessage.isEnabled = true;
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder 
                    { 
                        Title = "Leave Message Settings Updated", 
                        Description = "Leave Message is now enabled!",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    break;
                case "true":
                    GuildSettings.leaveMessage.isEnabled = true;
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder 
                    { 
                        Title = "Leave Message Settings Updated", 
                        Description = "Leave Message is now enabled!",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    break;
                case "disable":
                    GuildSettings.leaveMessage.isEnabled = false;
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder 
                    { 
                        Title = "Leave Message Settings Updated", 
                        Description = "Leave Message is now disabled!",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    break;
                case "off":
                    GuildSettings.leaveMessage.isEnabled = false;
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder 
                    { 
                        Title = "Leave Message Settings Updated", 
                        Description = "Leave Message is now disabled!",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    break;
                case "false":
                    GuildSettings.leaveMessage.isEnabled = false;
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder 
                    {
                        Title = "Leave Message Settings Updated", 
                        Description = "Leave Message is now disabled!",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    break;
                case "message":
                    if (args.Length < 2) {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                        {
                            Title = "Leave Message",
                            Description = "You can specify variables for the leave message, here they are\n\n" +
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
                                    Value = $"```\n{GuildSettings.leaveMessage.leaveMessage}```",
                                },
                                new EmbedFieldBuilder()
                                {
                                    Name = "Compiled:",
                                    Value = $"{GuildSettings.leaveMessage.leaveMessage.CompileVarMessage(Context.Guild, Context.User)}",
                                }
                            },
                            Color = Blurple
                        }.WithCurrentTimestamp().Build());
                        return; 
                    }
                    GuildSettings.leaveMessage.leaveMessage = string.Join(' ', args.Skip(1));
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder 
                    {
                        Title = "Leave Message Settings Updated",
                        Description = $"Leave Message is now {GuildSettings.leaveMessage.leaveMessage}",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    break;
                case "title":
                    if (args.Length < 2)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                        {
                            Title = "Leave Message",
                            Description = "You can specify variables for the leave message, here they are\n\n" +
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
                                    Name = "Raw",
                                    //IsInline = true,
                                    Value = $"```\n{GuildSettings.leaveMessage.leaveMessage}```"
                                },
                                new EmbedFieldBuilder()
                                {
                                    Name = "Compiled",
                                    //IsInline = true,
                                    Value = $"{GuildSettings.leaveMessage.leaveMessage.CompileVarMessage(Context.Guild, Context.User)}"
                                }
                            },
                            Color = Blurple
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    string msg = string.Join(' ', args.Skip(1));
                    if (msg.Length > 256)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                        {
                            Title = "Error",
                            Description = $"The max title lengh is 256 characters! The length you provided was {msg.Length}.",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    GuildSettings.leaveMessage.leaveTitle = msg;
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder 
                    { 
                        Title = "Leave Message Settings Updated", 
                        Description = $"Leave Message Title is now {GuildSettings.leaveMessage.leaveTitle}",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    break;
                case "channel":
                    if(args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                        {
                            Title = "Leave message Channel",
                            Description = $"The current channel for leave messages to be posted is <#{GuildSettings.leaveMessage.leaveChannel}>",
                            Color = Blurple
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    var chan = GetChannel(args[1]);
                    if(chan == null)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                        {
                            Title = "Invalid Channel",
                            Description = $"The channel you provided was not found, please make sure you check spelling or its a channel the bot has access to",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    GuildSettings.leaveMessage.leaveChannel = chan.Id;
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder 
                    {
                        Title = "Leave Message Settings Updated", 
                        Description = $"Leave Message Channel is now <#{GuildSettings.leaveMessage.leaveChannel}>",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    break;
                case "color":
                    if (args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Embed Color",
                            Description = $"The leave message color is {GuildSettings.leaveMessage.EmbedColor}. This embed is that color",
                            Color = HexColorConverter.DiscordColorFromHex(GuildSettings.leaveMessage.EmbedColor)
                        }.Build());
                        return;
                    }

                    if (args.Length == 2)
                    {
                        string hexColor = args[1].ToLower();
                        var regex = new Regex(@"(\d|[a-f]){6}");
                        if (regex.IsMatch(hexColor))
                        {
                            var hex = regex.Match(hexColor).Groups[0].Value;
                            //set here
                            this.GuildSettings.leaveMessage.EmbedColor = "#"+hex;
                            GuildSettings.SaveGuildSettings();
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Success!",
                                Description = $"Set the Leave messages Color to this Embed's Color ({GuildSettings.leaveMessage.EmbedColor})",
                                Color = HexColorConverter.DiscordColorFromHex(GuildSettings.leaveMessage.EmbedColor)
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
                            this.GuildSettings.leaveMessage.EmbedColor = HexColorConverter.HexFromColor(System.Drawing.Color.FromArgb(R, G, B));
                            GuildSettings.SaveGuildSettings();
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Success!",
                                Description = $"Set the Leave messages Color to this Embed's Color ({GuildSettings.leaveMessage.EmbedColor})",
                                Color = HexColorConverter.DiscordColorFromHex(GuildSettings.leaveMessage.EmbedColor)
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
            };
            GuildSettings.SaveGuildSettings();
            return;
        }
    }
}