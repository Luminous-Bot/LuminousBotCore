using Discord;
using Discord.WebSocket;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using static Public_Bot.Modules.Handlers.LevelHandler;
using static Public_Bot.Modules.Handlers.LevelHandler.GuildLevelSettings;

namespace Public_Bot.Modules.Commands
{
    [DiscordCommandClass("🧪 Levels 🧪", "Add ranks and leaderboards for your server with Levels!")]
    class LevelCommands : CommandModuleBase
    {
        [DiscordCommand("levelsettings")]
        public async Task LevelSettings(params string[] args)
        {
            if(args.Length == 0)
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
                    foreach(var chan in ls.BlacklistedChannels)
                    {
                        var rch = Context.Guild.GetChannel(chan);
                        if (rch.GetType() == typeof(SocketTextChannel))
                            bc.Add($"⌨️ - {rch.Name}");
                        if(rch.GetType() == typeof(SocketVoiceChannel))
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
                    gnrl.Add("Color:", $"R:{ls.EmbedColor.R} G:{ls.EmbedColor.G} B:{ls.EmbedColor.B}");
                    gnrl.Add("Default XP", ls.DefaultBaseLevelXp.ToString());
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
                            }
                        },
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    break;
                case "maxlevel":
                    if(args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Max Level",
                            Description = $"The current max level is {ls.maxlevel}",
                            Color = Color.Green
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if(args.Length == 2)
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
                            gl.SaveCurrent();
                            return;
                        }
                    }
                    break;
                case "messagexp":
                    if(args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "XP per Message",
                            Description = $"The current XP per Message is {ls.XpPerMessage}",
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
                            Description = $"The current XP per Voice Channel Minute is {ls.XpPerVCMinute}",
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
                            Description = $"The current Default XP is {ls.DefaultBaseLevelXp}",
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
                case "levelmultiplier":
                    if (args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Level Multiplier",
                            Description = $"The current Level Multiplier is {ls.LevelMultiplier}",
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
                                Title = "Level Multiplier",
                                Description = $"Level Multiplier is now set to {ls.LevelMultiplier}!",
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
                case "color":
                    if (args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Level Embed Color",
                            Description = $"The current Level Embed Color is this embeds color (R:{ls.EmbedColor.R} G:{ls.EmbedColor.G} B:{ls.EmbedColor.B}",
                            Color = new Color(ls.EmbedColor.R, ls.EmbedColor.G, ls.EmbedColor.B)
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if (args.Length == 4)
                    {
                        if(int.TryParse(args[1], out var R) && int.TryParse(args[2], out var G) && int.TryParse(args[3], out var B))
                        {
                            if(R > 255 || G > 255 || B > 255)
                            {
                                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                {
                                    Title = "Invalid parameters",
                                    Description = $"Please use the RGB format values between 0 and 255, for example: `{GuildSettings.Prefix}levelsettings color 25 255 0`",
                                    Color = Color.Red
                                }.WithCurrentTimestamp().Build());
                                return;
                            }
                            gl.Settings.EmbedColor = new color(R, G, B);
                            gl.SaveCurrent();
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Level Embed Color",
                                Description = $"The current Level Embed Color is now set to this embeds color (R:{gl.Settings.EmbedColor.R} G:{gl.Settings.EmbedColor.G} B:{gl.Settings.EmbedColor.B})",
                                Color = new Color(ls.EmbedColor.R, ls.EmbedColor.G, ls.EmbedColor.B)
                            }.WithCurrentTimestamp().Build());
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Invalid parameters",
                                Description = $"Please use the RGB format, for example: `{GuildSettings.Prefix}levelsettings color 25 255 0`",
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
                            Description = $"Please use the RGB format, for example: `{GuildSettings.Prefix}levelsettings color 25 255 0`",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
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
                            Description = $"The current Blacklisted Channels are:\n{(blc.Count > 0 ? string.Join("\n", blc) : "None.")}",
                            Color = Color.Green
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if (args.Length >= 3)
                    {
                        var name = string.Join(' ', args.Skip(2));
                        var chan = GetChannel(name);
                        if(chan == null)
                        {
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Not Found!",
                                Description = $"We didnt find a channel with the name/id of \"{name}\"!",
                                Color = Color.Red
                            }.WithCurrentTimestamp().Build());
                            return;
                        }
                        if(args[1] == "add")
                        {
                            if(gl.Settings.BlacklistedChannels.Contains(chan.Id))
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
            }
        }
    }
}
