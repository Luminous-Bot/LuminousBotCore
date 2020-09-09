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
    public class Levelsettings : CommandModuleBase
    {
        public static List<ulong> CurrentRF = new List<ulong>();

        [DiscordCommand("levelsettings", RequiredPermission = true, description = "Change how the level settings works!", commandHelp = "Usage:" +
            "`(PREFIX)levelsettings list`\n" +
            "`(PREFIX)levelsettings MaxLevel/messagexp/voicexp/defaultxp/levelmultiplier/blacklist/ranks/refresh`")]
        public async Task LevelSettings(params string[] args)
        {
            var ls = GuildLevelSettings.Get(Context.Guild.Id);
            if (ls == null)
                ls = new GuildLevelSettings();
            var gl = GuildLeaderboards.Get(Context.Guild.Id);

            if (args.Length == 0)
            {
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
                foreach (var chan in ls.RankRoles.OrderBy(x => x.Level * -1))
                {
                    rl.Add($"{chan.Level} - <@&{chan.Role}>");
                }
                Dictionary<string, string> gnrl = new Dictionary<string, string>();
                gnrl.Add("XP Multiplier:", ls.LevelMultiplier.ToString());
                gnrl.Add("XP/Message:", ls.XpPerMessage.ToString());
                gnrl.Add("XP/Minute in VC:", ls.XpPerVCMinute.ToString());
                gnrl.Add("Xp/Minute of Streaming:", ls.XpPerVCStream.ToString());
                gnrl.Add("Max Level:", ls.MaxLevel.ToString());
                gnrl.Add("Levelup Channel:", ch.Name);
                gnrl.Add("Default XP:", ls.DefaultBaseLevelXp.ToString());
                int leng = gnrl.Keys.Max(x => x.Length);
                List<string> final = new List<string>();
                foreach (var itm in gnrl)
                    final.Add(itm.Key.PadRight(leng) + " " + itm.Value);
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Here's your guilds Level settings",
                    Description = $"All settings are customizable",
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
                            },
                            new EmbedFieldBuilder()
                            {
                                Name = "Here's how to change your settings:",
                                Value = $"```\n{GuildSettings.Prefix}levelsettings list\n{GuildSettings.Prefix}levelsettings maxlevel\n{GuildSettings.Prefix}levelsettings maxlevel <max_level>\n{GuildSettings.Prefix}levelsettings messagexp \n{GuildSettings.Prefix}levelsettings messagexp <msg_xp>\n{GuildSettings.Prefix}levelsettings voicexp\n{GuildSettings.Prefix}levelsettings voicexp <voice_xp>\n{GuildSettings.Prefix}levelsettings defaultxp \n{GuildSettings.Prefix}levelsettings defaultxp <default_xp>\n{GuildSettings.Prefix}levelsettings xpmultiplier \n{GuildSettings.Prefix}levelsettings xpmultiplier <new_value>\n{GuildSettings.Prefix}levelsettings blacklist\n{GuildSettings.Prefix}levelsettings blacklist add <channel>\n{GuildSettings.Prefix}levelsettings blacklist remove <channel>\n{GuildSettings.Prefix}levelsettings ranks\n{GuildSettings.Prefix}levelsettings ranks add <@role> <level>\n{GuildSettings.Prefix}levelsettings ranks remove <@role>\n{GuildSettings.Prefix}levelsettings refresh <@role>```"
                            }
                        },
                    Color = Color.Green
                }.WithCurrentTimestamp().Build());
                return;
            }
            

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
                    foreach (var chan in ls.RankRoles.OrderBy(x => x.Level * -1))
                    {
                        rl.Add($"{chan.Level} - <@&{chan.Role}>");
                    }
                    Dictionary<string, string> gnrl = new Dictionary<string, string>();
                    gnrl.Add("XP Multiplier:", ls.LevelMultiplier.ToString());
                    gnrl.Add("XP/Message:", ls.XpPerMessage.ToString());
                    gnrl.Add("XP/Minute in VC:", ls.XpPerVCMinute.ToString());
                    gnrl.Add("Xp/Minute of Streaming:", ls.XpPerVCStream.ToString());
                    gnrl.Add("Max Level:", ls.MaxLevel.ToString());
                    gnrl.Add("Levelup Channel:", ch == null ? "none" : "#" +ch.Name);
                    gnrl.Add("Default XP:", ls.DefaultBaseLevelXp.ToString());
                    int leng = gnrl.Keys.Max(x => x.Length);
                    List<string> final = new List<string>();
                    foreach (var itm in gnrl)
                        final.Add(itm.Key.PadRight(leng) + " " + itm.Value);
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Heres your guilds Level settings",
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
                            },
                            new EmbedFieldBuilder()
                            {
                                Name = "Here's how to change your settings:",
                                Value = $"```\n{GuildSettings.Prefix}levelsettings list\n{GuildSettings.Prefix}levelsettings maxlevel\n{GuildSettings.Prefix}levelsettings maxlevel <max_level>\n{GuildSettings.Prefix}levelsettings messagexp \n{GuildSettings.Prefix}levelsettings messagexp <msg_xp>\n{GuildSettings.Prefix}levelsettings voicexp\n{GuildSettings.Prefix}levelsettings voicexp <voice_xp>\n{GuildSettings.Prefix}levelsettings defaultxp \n{GuildSettings.Prefix}levelsettings defaultxp <default_xp>\n{GuildSettings.Prefix}levelsettings xpmultiplier \n{GuildSettings.Prefix}levelsettings xpmultiplier <new_value>\n{GuildSettings.Prefix}levelsettings blacklist\n{GuildSettings.Prefix}levelsettings blacklist add <channel>\n{GuildSettings.Prefix}levelsettings blacklist remove <channel>\n{GuildSettings.Prefix}levelsettings ranks\n{GuildSettings.Prefix}levelsettings ranks add <@role> <level>\n{GuildSettings.Prefix}levelsettings ranks remove <@role>\n{GuildSettings.Prefix}levelsettings refresh <@role>```"
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
                    gl.Save();
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Levelup Channel",
                        Description = $"Levelup channel is now set to <#{ls.LevelUpChan}> [Jump](https://discord.com/channels/{Context.Guild.Id}/{ls.LevelUpChan} \"We're no strangers to love, You know the rules and so do I, A full commitment's what I'm thinking of, You wouldn't get this from any other guy, I just wanna tell you how I'm feeling, Gotta make you understand, Never gonna give you up, Never gonna let you down, Never gonna run around and desert you, Never gonna make you cry, Never gonna say goodbye, Never gonna tell a lie and hurt you, We've known each other for so long, Your heart's been aching but you're too shy to say it, Inside we both know what's been going on, We know the game and we're gonna play it, And if you ask me how I'm feeling, Don't tell me you're too blind to see, Never gonna give you up, Never gonna let you down, Never gonna run around and desert you, Never gonna make you cry, Never gonna say goodbye, Never gonna tell a lie and hurt you, Never gonna give you up, Never gonna let you down, Never gonna run around and desert you, Never gonna make you cry, Never gonna say goodbye, Never gonna tell a lie and hurt you, Never gonna give, never gonna give, (Give you up), (Ooh) Never gonna give, never gonna give, (Give you up), We've known each other for so long, Your heart's been aching but you're too shy to say it, Inside we both know what's been going on, We know the game and we're gonna play it, I just wanna tell you how I'm feeling, Gotta make you understand, Never gonna give you up, Never gonna let you down, Never gonna run around and desert you, Never gonna make you cry, Never gonna say goodbye, Never gonna tell a lie and hurt you, Never gonna give you up, Never gonna let you down, Never gonna run around and desert you, Never gonna make you cry, Never gonna say goodbye, Never gonna tell a lie and hurt you, Never gonna give you up, Never gonna let you down, Never gonna run around and desert you, Never gonna make you cry\")!\nTo Change the levelup channel run `{GuildSettings.Prefix}levelsettings channel <#channel>`",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    break;
                case "maxlevel":
                    if (args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Max Level",
                            Description = $"The current max level is {ls.MaxLevel}.\nTo Change the max level run `{GuildSettings.Prefix}levelsettings MaxLevel <level>`",
                            Color = Color.Green
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if (args.Length == 2)
                    {
                        if (uint.TryParse(args[1], out var res))
                        {
                            gl.Settings.MaxLevel = res;
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Max Level",
                                Description = $"Max level is now set to {ls.MaxLevel}!",
                                Color = Color.Green
                            }.WithCurrentTimestamp().Build());
                            gl.Save();
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
                            gl.Save();
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
                            gl.Save();
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
                            gl.Save();
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
                            gl.Save();
                            return;
                        }
                    }
                    break;
                case "streamxp":
                    {
                        if (args.Length == 1)
                        {
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "XP per Streaming Minute",
                                Description = $"Users who are streaming in a vc will get {ls.XpPerVCStream} xp every minute.",
                                Color = Blurple
                            }.WithCurrentTimestamp().Build());
                            return;
                        }
                        if (args.Length == 2)
                        {
                            if (uint.TryParse(args[1], out var res))
                            {
                                gl.Settings.XpPerVCStream = res;
                                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                {
                                    Title = "XP per Streaming Minute",
                                    Description = $"Users will now get {ls.XpPerVCMinute} xp every minute of streaming!",
                                    Color = Color.Green
                                }.WithCurrentTimestamp().Build());
                                gl.Save();
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
                                gl.Save();
                                return;
                            }
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
                            gl.Save();
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
                            gl.Save();
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
                            gl.Save();
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
                            gl.Save();
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
                            gl.Save();
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Blacklisted Channels",
                                Description = $"Added the channel `{chan.Name}` to the Blacklisted Channels!",
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
                            gl.Save();
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Blacklisted Channels",
                                Description = $"Removed the channel `{chan.Name}` from the Blacklisted Channels!",
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
                        foreach (var chan in ls.RankRoles.OrderBy(x => x.Level * -1))
                        {
                            ranks.Add($"Level {chan.Level} - <@&{chan.Role}>");
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
                            if (ls.RankRoles.Any(x => x.Role == role.Id))
                            {
                                gl.Settings.RankRoles.Remove(gl.Settings.RankRoles.First(x => x.Role == role.Id));
                                gl.Save();

                                List<string> ranks = new List<string>();
                                foreach (var chan in gl.Settings.RankRoles.OrderBy(x => x.Level * -1))
                                {
                                    ranks.Add($"Level {chan.Level} - <@&{chan.Role}>");
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
                                if (ls.RankRoles.Any(x => x.Role == role.Id))
                                {
                                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                    {
                                        Title = "That role is already added!",
                                        Description = $"The role {role.Mention} is already added for level {ls.RankRoles.First(x => x.Role == role.Id).Level}!",
                                        Color = Color.Red,

                                    }.WithCurrentTimestamp().Build());
                                    return;
                                }
                                if (ls.RankRoles.Any(x => x.Level == res))
                                {
                                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                    {
                                        Title = "That Level already has a role!",
                                        Description = $"The level {res} has the role <@&{ls.RankRoles.Find(x => x.Level == res).Role}> assigned to it! therefor we can't add another role!",
                                        Color = Color.Red,

                                    }.WithCurrentTimestamp().Build());
                                    return;
                                }
                                gl.Settings.RankRoles.Add(new RankRole() { Level = res, Role = role.Id });
                                gl.Save();
                                List<string> ranks = new List<string>();
                                foreach (var chan in gl.Settings.RankRoles.OrderBy(x => x.Level * -1))
                                {
                                    ranks.Add($"Level {chan.Level} - <@&{chan.Role}>");
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
                                Title = "Theres a refresh already happening!",
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
                                    Title = "Theres a refresh already happening!",
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
                            if (ls.RankRoles.Any(x => x.Role == role.Id))
                            {
                                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                {
                                    Title = "We're on it!",
                                    Description = "We are working on giving the users there roles! I'l send a message here when the process is finnished",
                                    Color = Color.Green
                                }.WithCurrentTimestamp().Build());
                                CurrentRF.Add(Context.Guild.Id);
                                new Thread(() => LevelHandler.GiveForNewRole(gl, role, ls.RankRoles.First(x => x.Role == role.Id).Level, Context.User as SocketGuildUser, Context.Channel as SocketTextChannel)).Start();
                            }
                        }
                    }
                    break;
            }
        }
    }
}