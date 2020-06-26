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
                Color = gl.Settings.EmbedColor.Get(),
                ThumbnailUrl = Context.Guild.IconUrl
            }.WithCurrentTimestamp().Build());
        }
        [DiscordCommand("rank", description = "Shows your current rank!", commandHelp = "Usage - `(PREFIX)rank`, `(PREFIX)rank <user>`")]
        
        public async Task rank(params string[] args)
        {
            var user = Context.User;
            if (args.Length == 1)
                user = GetUser(args[0]);
            if(user == null)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Invalid User!",
                    Description = $"The user \"{args[0]}\" is invalid",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            var gl = GuildLeaderboards.Get(Context.Guild.Id);
            if (gl.CurrentUsers.Any(x => x.UserID == user.Id))
            {
                var cu = gl.CurrentUsers.OrderBy(x => x.CurrentLevel * -1).ToList();
                var userlvl = cu.Find(x => x.UserID == user.Id);
                var usrIndx = cu.IndexOf(userlvl);
                string usrTop = "Leaderboards";
                string usrMid = $"**{(cu[usrIndx].Username == "" ? Context.Guild.GetUser(cu[usrIndx].UserID).ToString() : cu[usrIndx].Username)}**~~".PadRight(32, '-') + $"~~> Rank: {usrIndx + 1} Level: {cu[usrIndx].CurrentLevel} XP: {(uint)cu[usrIndx].CurrentXP}/{(uint)cu[usrIndx].NextLevelXP}";
                string userBtm = "Wow you're at the bottom buddy :(";
                if (usrIndx > 0)
                    usrTop = $"{(cu[usrIndx - 1].Username == "" ? Context.Guild.GetUser(cu[usrIndx - 1].UserID).ToString() : cu[usrIndx - 1].Username)}~~".PadRight(30, '-') + $"~~> Rank: {usrIndx} Level: {cu[usrIndx - 1].CurrentLevel} XP: {(uint)cu[usrIndx - 1].CurrentXP}/{(uint)cu[usrIndx - 1].NextLevelXP}";
                if(usrIndx < cu.Count - 1)
                    userBtm = $"{(cu[usrIndx + 1].Username == "" ? Context.Guild.GetUser(cu[usrIndx + 1].UserID).ToString() : cu[usrIndx + 1].Username)}~~".PadRight(30, '-') + $"~~> Rank: {usrIndx + 2} Level: {cu[usrIndx + 1].CurrentLevel} XP: {(uint)cu[usrIndx + 1].CurrentXP}/{(uint)cu[usrIndx + 1].NextLevelXP}";

                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = $"{user}",
                    ThumbnailUrl = user.GetAvatarUrl(),
                    Fields = new List<EmbedFieldBuilder>()
                    {
                        new EmbedFieldBuilder()
                        {
                            Name = usrTop,
                            Value = usrMid
                        },
                        new EmbedFieldBuilder()
                        {
                            Name = userBtm,
                            Value = "__\n__"
                        }
                    },
                    Color = gl.Settings.EmbedColor.Get(),
                }.WithCurrentTimestamp().Build());
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
                        if(res > lusr.CurrentLevel)
                        {
                            for (uint i = lusr.CurrentLevel; i != res; i++)
                            {
                                lusr.NextLevelXP *= gl.Settings.LevelMultiplier;
                            }
                        }
                        if(res < lusr.CurrentLevel)
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
            if(args.Length == 0)
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
            if(user == null)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "That user is invalid",
                    Description = $"The user \"{args[0]}\" is invalid!",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            if(args.Length == 1)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "How many levels?",
                    Description = $"How many levels do you want to give {user.Mention}? use `{GuildSettings.Prefix}givelevel <@user> <ammount>`",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            if(args.Length == 2)
            {
                if(uint.TryParse(args[1], out var res))
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
                    if(gl.CurrentUsers.Any(x => x.UserID == user.Id))
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
        [DiscordCommand("levelsettings", RequiredPermission = true, description = "Change how the level settings works!", commandHelp = "Usage:" +
            "`(PREFIX)levelsettings list`\n" +
            "`(PREFIX)levelsettings maxlevel/messagexp/voicexp/defaultxp/levelmultiplier/color/blacklist/ranks/refresh`")]
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
                case "channel":
                    if(args.Length == 1)
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
                    if(channel == null)
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
                    if(args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Max Level",
                            Description = $"The current max level is {ls.maxlevel}.\nTo Change the max level run `{GuildSettings.Prefix}levelsettings maxlevel <level>`",
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
                            //gl.SaveCurrent();
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
                case "color":
                    if (args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Level Embed Color",
                            Description = $"The current Level Embed Color is this embeds color (R:{ls.EmbedColor.R} G:{ls.EmbedColor.G} B:{ls.EmbedColor.B}\nTo change the Level Embeds color Run `{GuildSettings.Prefix}levelsettings color <R> <G> <B>`",
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
                            Description = $"The current Blacklisted Channels are:\n\n{(blc.Count > 0 ? string.Join("\n", blc) : "None.")}\n\nTo add a blacklisted channel Run `{GuildSettings.Prefix}levelsettings blacklist add <channel>`\nTo remove a blacklisted channel Run `{GuildSettings.Prefix}levelsettings blacklist remove <channel>`",
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
                case "ranks":
                    if(args.Length == 1)
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
                    if(args.Length == 3)
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
                    if(args.Length == 4)
                    {
                        var role = GetRole(args[2]);
                        if(role == null)
                        {
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "That role is invalid!",
                                Description = "Please make sure you spell the role name right, or provided a valid id or mention.",
                                Color = Color.Red,

                            }.WithCurrentTimestamp().Build());
                        }
                        if(uint.TryParse(args[3], out var res))
                        {
                            if(args[1].ToLower() == "add")
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
                    if(args.Length == 1)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "What do you want to refresh",
                            Description = $"You can refresh ranks for users by running `{GuildSettings.Prefix}levelsettings refresh <role>`",
                            Color = Color.Orange,

                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    if(args.Length == 2)
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
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Are you sure?",
                            Description = $"This process can take some time depending on the ammount of users you have in your guild. please type \"{GuildSettings.Prefix}levelsettings refresh {role.Mention} confirm\"",
                            Color = Color.Orange
                        }.WithCurrentTimestamp().Build());
                    }
                    if(args.Length == 3)
                    {
                        if(args[2].ToLower() == "confirm")
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
                            }
                            if (ls.RankRoles.ContainsValue(role.Id))
                            {
                                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                {
                                    Title = "We're on it!",
                                    Description = "We are working on giving the users there roles! I'l send a message here when the process is finnished",
                                    Color = Color.Green
                                }.WithCurrentTimestamp().Build());
                                LevelHandler.GiveForNewRole(gl, role, ls.RankRoles.First(x => x.Value == role.Id).Key, Context.User as SocketGuildUser, Context.Channel as SocketTextChannel);
                            }
                        }
                    }
                    break; 
            }
        }
    }
}
