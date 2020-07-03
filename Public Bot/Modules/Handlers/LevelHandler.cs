using Discord;
using Discord.WebSocket;
using Public_Bot.Modules.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Public_Bot.Modules.Handlers
{
    [DiscordHandler]
    class LevelHandler
    {
        public static string LeaderboardFolder = $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}Levels";
        public static DiscordShardedClient client;
        public static List<string> facts { get; set; }
        public static List<GuildLeaderboards> GuildLevels { get; set; }
        public static int Update = 0;
        public class GuildLevelSettings
        {
            public Dictionary<uint, ulong> RankRoles { get; set; } = new Dictionary<uint, ulong>();
            public double LevelMultiplier { get; set; } = 1.10409;
            public uint maxlevel { get; set; } = 100;
            public uint DefaultBaseLevelXp { get; set; } = 30;
            public double XpPerMessage { get; set; } = 1;
            public double XpPerVCMinute { get; set; } = 5;
            public ulong LevelUpChan { get; set; }
            public color EmbedColor { get; set; } = new color(0, 255, 0);
            public color RankBackgound { get; set; } = new color(40, 40, 40);
            public List<ulong> BlacklistedChannels { get; set; } = new List<ulong>();
            public class color
            {
                public int R { get; set; }
                public int G { get; set; }
                public int B { get; set; }
                public Color Get()
                    => new Color(this.R, this.G, this.B);
                public color() { }
                public color(int r, int g, int b) { this.R = r; this.G = g; this.B = b; }

            }
            public static GuildLevelSettings Get(ulong id)
            {
                return GuildLevels.Any(x => x.GuildID == id) ? GuildLevels.Find(x => x.GuildID == id).Settings : null;
            }
        }
        public class GuildLeaderboards
        {
            public ulong GuildID { get; set; }
            public GuildLevelSettings Settings { get; set; } = new GuildLevelSettings();
            public List<LevelUser> CurrentUsers { get; set; } = new List<LevelUser>();
            public static GuildLeaderboards Get(ulong id)
            {
                return GuildLevels.Any(x => x.GuildID == id) ? GuildLevels.Find(x => x.GuildID == id) : null;
            }
            public static void Save() => SaveLevels();
            public void SaveCurrent() => SaveLevels();

            public GuildLeaderboards() { }
            public GuildLeaderboards(IGuild g)
            {
                this.GuildID = g.Id;
                if (g.AFKChannelId.HasValue)
                    this.Settings.BlacklistedChannels.Add(g.AFKChannelId.Value);
                this.Settings.LevelUpChan = g.DefaultChannelId;
                GuildLevels.Add(this);
                SaveLevels();
            }

        }
        public class LevelUser
        {
            public ulong GuildID { get; set; }
            public ulong UserID { get; set; }
            public string Username { get; set; } = "";
            public uint CurrentLevel { get; set; } = 0;
            public double CurrentXP { get; set; } = 0;
            public double NextLevelXP { get; set; } = 30;
            public LevelUser() { }
            public LevelUser(SocketGuildUser user)
            {
                UserID = user.Id;
                Username = user.ToString();
                GuildID = user.Guild.Id;
            }
        }
        public LevelHandler(DiscordShardedClient c)
        {
            client = c;
            facts = new List<string>()
            {
                "The Empathy Banana is a rare find on Discord.",
"Discord 404 pages have a hidden secret.",
"Discord provides users tools to create custom Discord bots.",
"The Discord app is available on the official website, Play Store, and Apple store.",
"Discord users can create customized servers on the app.",
"The Discord website has a page that lets you view the status of the app.",
"You can log into Discord using any device that has an active internet connection.",
"Discord allows its users to create custom emojis and stickers.",
"Music bots can be added to your Discord server’s channel.",
"Discord Nitro is a monthly subscription that gives users extra benefits on the app.",
"Custom emojis can only be used in the Discord server it was uploaded in.",
"The Discord logo is named Clyde.",
"BetterDiscord is a popular extension for the app.",
"You can use real-time voice changers on Discord.",
"There are simple typing tricks you can use on any Discord channel.",
"You can keep your text hidden by using the spoiler feature.",
"Discord users can also change the text color of usernames.",
"There are several themes and skins on Discord.",
"You can go live with Discord.",
"Discord’s brand considers itself playful.",
"Discord web has several limitations, unlike mobile and desktop apps.",
"A Discord server can have up to 500 channels.",
"A user cannot be on more than 100 servers.",
"A Discord server is limited to a size maximum of 250,000 - 500,000 members.",
"Canary is Discord’s alpha testing program.",
"Discord has over 750 verified servers.",
"The official Fortnite server has more than 180,000 members.",
"Discord now supports soundboards.",
"The default discord icon image size is 128 px X 128 px.",
"The Discord no route error can occur when attempting to connect to voice channels.",
"Discord has several quick keyboard commands.",
"The Discord overlay can be toggled on and off.",
"Discord has stated that it can handle 2.6 million concurrent voice chats.",
"Discord is designed to protect sensitive information.",
"A Discord user’s status will be displayed as idle once they are inactive for more than five minutes.",
"Roles can be assigned in any Discord server.",
"There is a limited number of roles per server.",
"The Discord pvpcraft bot serves many purposes.",
"You can make transparent emojis on Discord.",
"Discord Stock servers exist.",
"You can delete private messages sent on Discord.",
"The welcomer bot is the most popular image welcome bot on the app.",
"Discord allows you to export your chat logs.",
"The Discord app does not have a screen capture tool.",
"Only certain servers on Discord allows its members to see offline members.",
"Level bots award server points to members based on their activities.",
"Webhooks are a simple way to receive automated messages on a server.",
"Ice checking indicates that your network has blocked Discord from connecting to a voice server.",
"Discord allows its users to download videos from its platform.",
"Secret Hitler is a popular board game played on Discord.",
"Users can change their usernames for each server they join.",
"You can play the radio on Discord.",
"You can hide your Discord status from other people.",
"Users can appear offline when they are active on Discord.",
"Discord supports numerous file formats.",
"Discord channels can be made private.",
"You can check how long it has been since you joined Discord.",
"Your local server region can be changed on Discord.",
"Auto-embed can be disabled when sending links in chat rooms.",
"There are two ways to leave a Discord server.",
"Discord allows you to play playlists from Spotify.",
"Discord has a Text-to-Speech (TTS) feature you can enable.",
"HypeSquad allows users to support their favorite gaming community.",
"Servers can be joined by using invite-only links.",
"In 2018, Discord introduced a game storefront beta.",
"The chat platform gained popularity with Alternative Rights groups.",
"Discord has banned drawn pornography of underaged subjects.",
"A trust and safety team has been established to monitor reports on Discord.",
"Discord discontinued its free game service in 2019.",
"Admins in a server can activate slow mode in a channel.",
"Server admins can delete their servers.",
"Discord is against the use of advertisements for profit.",
"Users are notified by the app if they have been kicked out of a server.",
"Each Discord has a unique tag.",
"Calls you make on the app can be recorded.",
"Discord is worth more than $2 billion.",
"Discord can be linked with Twitch.",
"The app was almost named Wyvern.",
"There is no special meaning behind the name Discord.",
"Discord has a mascot named Wumpus.",
"Discord sends emails that contain fun facts and trivia.",
"Blocking a person on Discord will hide your messages from them.",
"Users can make custom statuses.",
"You can switch between light and dark mode.",
"Anyone can apply for a partnership with Discord.",
"Discord offers a guide for parents with children.",
"Discord allows you to sell games on the platform.",
"Community-led servers cannot get verified on Discord.",
"Rich Presence allows users to integrate their game with Discord.",
"The ChillZone warning on Discord warns that you are sending too many messages.",
"Discord does not hold formal interviews for its possible employees.",
"Destiny 2’s official Discord server has over 55,000 members.",
"Pokecord allows users on a server to catch Pokemon.",
"DiceMaiden is a popular bot used for TRPG games.",
"Users can link their Steam account to Discord.",
"Your YouTube account can also be linked to Discord.",
"Users can assign their keybindings.",
"Server occasions are a popular way for communities on Discord to share interests.",
"Discord is rumored to shut down in November 2020.",
            };
            try
            {
                GuildLevels = StateHandler.LoadObject<List<GuildLeaderboards>>("levels");
            }
            catch { GuildLevels = new List<GuildLeaderboards>(); CommandHandler.CurrentGuildSettings.Where(x => x.ModulesSettings["🧪 Levels 🧪"]).ToList().ForEach(x => GuildLevels.Add(new GuildLeaderboards(client.GetGuild(x.GuildID)))); }
            new System.Timers.Timer() { AutoReset = true, Interval = 5000, Enabled = true }.Elapsed += HandleSaving;
            new System.Timers.Timer() { AutoReset = true, Interval = 60000, Enabled = true }.Elapsed += GiveVCPoints;
            client.MessageReceived += HandleLevelAdd;

        }

        private void GiveVCPoints(object sender, ElapsedEventArgs e)
        {
            foreach(var guild in client.Guilds)
            {
                if (GuildLevels.Any(x => x.GuildID == guild.Id))
                {
                    var gs = GuildSettings.Get(guild.Id);
                    if (gs.ModulesSettings["🧪 Levels 🧪"]) 
                    {
                        foreach (var user in guild.Users.Where(x => x.VoiceChannel != null))
                        {
                            var gl = GuildLevels.Find(x => x.GuildID == guild.Id);
                            if (!gl.Settings.BlacklistedChannels.Contains(user.VoiceChannel.Id))
                            {
                                if (gl.CurrentUsers.Any(x => x.UserID == user.Id))
                                {
                                    var usr = gl.CurrentUsers.Find(x => x.UserID == user.Id);
                                    usr.CurrentXP = usr.CurrentXP + gl.Settings.XpPerVCMinute;
                                    if (usr.CurrentXP >= usr.NextLevelXP)
                                        LevelUpUser(usr);
                                    Logger.Write($"{user} - L:{usr.CurrentLevel} XP:{usr.CurrentXP}");
                                    Update++;
                                }
                                else
                                {
                                    var usr = new LevelUser(user);
                                    usr.CurrentXP = usr.CurrentXP + gl.Settings.XpPerVCMinute;
                                    if (usr.CurrentXP >= usr.NextLevelXP)
                                        LevelUpUser(usr);
                                    gl.CurrentUsers.Add(usr);
                                    Logger.Write($"{user} - L:{usr.CurrentLevel} XP:{usr.CurrentXP}");
                                    Update++;
                                }
                            }

                        }
                    }
                    
                }
            }
        }

        private void HandleSaving(object sender, ElapsedEventArgs e)
        {
            if (Update > 0)
            { SaveLevels(); Update = 0; }
        }
        public static void SaveLevels()
        {
                StateHandler.SaveObject<List<GuildLeaderboards>>("levels", GuildLevels);
        }
        public static async void LevelUpUser(LevelUser user)
        {
            var gu = GuildLeaderboards.Get(user.GuildID);
            var gs = CommandHandler.GetGuildSettings(user.GuildID);
            if (gs.ModulesSettings["🧪 Levels 🧪"])
            {
                if (user.CurrentLevel < gu.Settings.maxlevel)
                {
                    while (user.CurrentXP >= user.NextLevelXP)
                    {
                        user.CurrentXP = Math.Round(user.CurrentXP - user.NextLevelXP);
                        user.NextLevelXP = user.NextLevelXP * gu.Settings.LevelMultiplier;
                        user.CurrentLevel++;
                    }
                    bool GotRole = false;
                    List<string> roles = new List<string>();
                    if (gu.Settings.RankRoles.Count != 0)
                    {
                        if (gu.Settings.RankRoles.Keys.Any(x => x <= user.CurrentLevel))
                        {
                            foreach(var rid in gu.Settings.RankRoles.Where(x => x.Key <= user.CurrentLevel))
                            {
                                try
                                {
                                    var usr = client.GetGuild(gs.GuildID).GetUser(user.UserID);
                                    if (!usr.Roles.Any(x => x.Id == rid.Value))
                                    {
                                        await usr.AddRoleAsync(client.GetGuild(user.GuildID).GetRole(rid.Value));
                                        roles.Add($"<@&{rid.Value}>");
                                        GotRole = true;
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                    var chan = client.GetGuild(user.GuildID).GetTextChannel(gu.Settings.LevelUpChan);
                    if (chan != null)
                    {
                        await chan.SendMessageAsync($"<@{user.UserID}>", false, new EmbedBuilder()
                        {
                            Title = $"You Reached Level {user.CurrentLevel}!",
                            Description = GotRole ? $"Wowzers, you got the role {string.Join(", ", roles)}. Gg!" : $"Good job {client.GetUser(user.UserID).Username}, only {gu.Settings.maxlevel - user.CurrentLevel} more levels to go!",
                            Fields = new List<EmbedFieldBuilder>()
                        {
                            new EmbedFieldBuilder()
                            {
                                Name = "Heres a discord fact:",
                                Value = facts[new Random().Next(0, 99)],
                            }
                        },
                            ThumbnailUrl = client.GetUser(user.UserID).GetAvatarUrl(),
                            Color = new Color(gu.Settings.EmbedColor.R, gu.Settings.EmbedColor.G, gu.Settings.EmbedColor.B)
                        }.WithCurrentTimestamp().Build());
                    }
                    else
                        await client.GetGuild(user.GuildID).DefaultChannel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Hey!",
                            Description = $"The level up channel doesnt exist anymore, please set one with the `{gs.Prefix}setlevelchannel <#channel>` command!",
                            Color = Color.Orange
                        }.WithCurrentTimestamp().Build());
                }
            }
        }
        private async Task HandleLevelAdd(SocketMessage arg)
        {
            if (arg.Author.IsBot)
                return;
            if (arg.Channel.GetType() == typeof(SocketDMChannel))
                return;
            var sm = arg as SocketUserMessage;
            if (sm == null)
                return;
            var g = (sm.Channel as SocketGuildChannel).Guild;
            if (g == null)
                return;
            var gs = GuildSettings.Get(g.Id);
            if (gs.ModulesSettings["🧪 Levels 🧪"])
            {
                if (GuildLevels.Any(x => x.GuildID == g.Id))
                {
                    var gl = GuildLevels.Find(x => x.GuildID == g.Id);
                    if (!gl.Settings.BlacklistedChannels.Contains(sm.Channel.Id))
                    {
                        if (gl.CurrentUsers.Any(x => x.UserID == sm.Author.Id))
                        {
                            var usr = gl.CurrentUsers.Find(x => x.UserID == sm.Author.Id);
                            if (usr.Username != sm.Author.ToString())
                                usr.Username = sm.Author.ToString();
                            usr.CurrentXP = usr.CurrentXP + gl.Settings.XpPerMessage;
                            if (usr.CurrentXP >= usr.NextLevelXP)
                                LevelUpUser(usr);
                            Logger.Write($"{sm.Author} - L:{usr.CurrentLevel} XP:{usr.CurrentXP}");
                            Update++;
                        }
                        else
                        {
                            var usr = new LevelUser(arg.Author as SocketGuildUser);
                            if (usr.Username != sm.Author.ToString())
                                usr.Username = sm.Author.ToString();
                            usr.CurrentXP = usr.CurrentXP + gl.Settings.XpPerMessage;
                            if (usr.CurrentXP >= usr.NextLevelXP)
                                LevelUpUser(usr);
                            gl.CurrentUsers.Add(usr);
                            Logger.Write($"{sm.Author} - L:{usr.CurrentLevel} XP:{usr.CurrentXP}");
                            Update++;
                        }
                    }
                }
            }
        }
        public static void GiveForNewRole(GuildLeaderboards gl, SocketRole role, uint level, SocketGuildUser runner, SocketTextChannel chan)
        {
            var guild = client.GetGuild(gl.GuildID);
            int count = 0;
            foreach(var user in guild.Users.Where(x => gl.CurrentUsers.Find(y => y.UserID == x.Id) != null && gl.CurrentUsers.Find(y => y.UserID == x.Id).CurrentLevel >= level))
            {
                if(!user.Roles.Contains(role))
                {
                    user.AddRoleAsync(role).GetAwaiter().GetResult();
                    count++;
                    Thread.Sleep(3000);
                }
            }
            if(count > 0)
            {
                _=chan.SendMessageAsync($"{runner.Mention}", false, new EmbedBuilder()
                {
                    Title = "Finnished adding Roles!",
                    Description = $"We added the {role.Mention} to {count} different users who had a level equal or greater than {level}",
                    Color = Color.Green
                }.WithCurrentTimestamp().Build()).Result;
            }
            else
            {
                _=chan.SendMessageAsync($"{runner.Mention}", false, new EmbedBuilder()
                {
                    Title = "That was easy!",
                    Description = $"Looks like there was no one to give {role.Mention} to.",
                    Color = Color.Green 
                }.WithCurrentTimestamp().Build()).Result;
            }
            LevelCommands.CurrentRF.Remove(gl.GuildID);
        }
        public async Task RemoveFromRole(GuildLeaderboards gl, SocketRole role)
        {

        }
    }
}
