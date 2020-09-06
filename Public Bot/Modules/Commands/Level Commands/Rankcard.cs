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
    public class Rankcard : CommandModuleBase
    {
        [DiscordCommand("rankcard", description = "Change your Rank Card's settings!", commandHelp = "Usage:\n`(PREFIX)rankcard ping <on/off>`\n`(PREFIX)rankcard color <color_hex>`\n`(PREFIX)rankcard backgroundcolor <color_hex>`")]
        public async Task rc(params string[] args)
        {
            //get the guilds level settings
            var levelsettings = GuildLeaderboards.Get(Context.Guild.Id);
            if (levelsettings == null)
                return;

            LevelUser usr;
            if (!levelsettings.CurrentUsers.LevelUserExists(Context.User.Id))
            {
                usr = new LevelUser(Context.Guild.GetUser(Context.User.Id));
                levelsettings.CurrentUsers.AddLevelUser(usr);
            }
            else
                usr = levelsettings.CurrentUsers.GetLevelUser(Context.User.Id);

            if (args.Length == 0)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Your Rank Card",
                    Description = $"Here's your Settings for your Rank Card\n```\nMentions? {usr.MentionOnLevelup}\nBar Color: #{usr.BarColor}\nBackground Color: #{usr.BackgroundColor}```\nYou can change these settings with these commands:\n`{GuildSettings.Prefix}rankcard ping <on/off>`\n`{GuildSettings.Prefix}rankcard color <color_hex>`\n`{GuildSettings.Prefix}rankcard backgroundcolor <color_hex>`",
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
                        Description = $"Here's your Settings for your Rank Card\n```\nMentions? {usr.MentionOnLevelup}\nBar Color: #{usr.BarColor}\nBackground Color: #{usr.BackgroundColor}```\nYou can change these settings with these commands:\n`{GuildSettings.Prefix}rankcard ping <on/off>`\n`{GuildSettings.Prefix}rankcard color <color_hex>`\n`{GuildSettings.Prefix}rankcard backgroundcolor <color_hex>`",
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
                                    usr.MentionOnLevelup = true;
                                    usr.Save();
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
                                    usr.MentionOnLevelup = false;
                                    usr.Save();
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
                            Description = $"The current Rank Card's Color is this Embed's Color ({usr.BarColor})\nTo change the Rank Cards color Run `{GuildSettings.Prefix}rankcard color <hex_color>`",
                            Color = usr.DiscordColorFromHex(usr.BarColor)
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if (args.Length == 2)
                    {
                        string hexColor = args[1];
                        var regex = new Regex(@"(\d|[a-f]){6}");
                        if (regex.IsMatch(hexColor))
                        {
                            var hex = regex.Match(hexColor).Groups[0].Value;
                            usr.BarColor = hex;
                            usr.Save();
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Success!",
                                Description = $"Set the Rank Card Color's to this Embed's Color ({usr.BarColor})",
                                Color = usr.DiscordColorFromHex(hex)
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
                                    Description = $"Please use the RGB format values between 0 and 255, for example: `{GuildSettings.Prefix}rankcard color 25 255 0`",
                                    Color = Color.Red
                                }.WithCurrentTimestamp().Build());
                                return;
                            }
                            usr.BarColor = usr.HexFromColor(new Color(R, G, B));
                            usr.Save();
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Success!",
                                Description = $"Set the Rank Card's Color to this Embed's Color ({usr.BarColor})",
                                Color = usr.DiscordColorFromHex(usr.BarColor)
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
                            Description = $"The current Rank Cards Background Color is this embeds color ({usr.BackgroundColor})\nTo change the Rank Cards Background Color Run `{GuildSettings.Prefix}rankcard backgroundcolor <R> <G> <B>`",
                            Color = usr.DiscordColorFromHex(usr.BackgroundColor)
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if (args.Length == 2)
                    {
                        string hexColor = args[1];
                        var regex = new Regex(@"(\d|[a-f]){6}");
                        if (regex.IsMatch(hexColor))
                        {
                            var hex = regex.Match(hexColor).Groups[0].Value;
                            usr.BackgroundColor = hex;
                            usr.Save();
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Success!",
                                Description = $"Set the Rank Card's Backgound Color to this Embed's Color ({usr.BackgroundColor})",
                                Color = usr.DiscordColorFromHex(hex)
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
                                    Description = $"Please use the RGB format values between 0 and 255, for example: `{GuildSettings.Prefix}rankcard backgoundcolor 25 255 0`",
                                    Color = Color.Red
                                }.WithCurrentTimestamp().Build());
                                return;
                            }
                            usr.BackgroundColor = usr.HexFromColor(new Color(R, G, B));
                            usr.Save();
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Rank Card Background Color",
                                Description = $"The current Rank Cards Background Color is now set to this embeds color ({usr.BarColor})",
                                Color = usr.DiscordColorFromHex(usr.BackgroundColor)
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
    }
}