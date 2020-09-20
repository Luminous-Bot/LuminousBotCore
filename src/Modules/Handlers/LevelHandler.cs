using Discord;
using Discord.WebSocket;
using Public_Bot.Modules.Commands;
using Public_Bot.Modules.Commands.Level_Commands;
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
        //public static List<GuildLeaderboards> GuildLevels { get; set; }
        public static XPBucket _xpBucket = new XPBucket();
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
            //GuildLevels = StateService.Query<List<GuildLeaderboards>>(GraphQLParser.GenerateGQLQuery<GuildLeaderboards>("guildLeaderboards"));
            new System.Timers.Timer() { AutoReset = true, Interval = 60000, Enabled = true }.Elapsed += GiveVCPoints;
            new System.Timers.Timer() { AutoReset = true, Interval = 3000, Enabled = true }.Elapsed += ClearBucket;
            client.MessageReceived += HandleLevelAdd;
        }

        private async void ClearBucket(object sender, ElapsedEventArgs e)
        {
            var bRex = _xpBucket.BuildAndClear();
            if (bRex == null)
                return;
            await StateService.ExecuteNoReturnAsync<List<LevelUser>>(bRex);
        }

        public class XPBucket
        {
            private List<XPBucketItem> _bucket = new List<XPBucketItem>();
            public class XPBucketItem
            {
                public ulong GuildId { get; set; }
                public ulong UserId { get; set; }
                public LevelUser LevelUser { get; set; }
            }
            public void Add(LevelUser u)
            {
                var gid = u.GuildID;
                var uid = u.Id;
                if (_bucket.Any(x => x.GuildId == gid && x.UserId == uid))
                    _bucket[_bucket.IndexOf(_bucket.Find(x => x.GuildId == gid && x.UserId == uid))] = new XPBucketItem()
                    {
                        GuildId = gid,
                        UserId = uid,
                        LevelUser = u
                    };
                else
                    _bucket.Add(new XPBucketItem()
                    {
                        GuildId = gid,
                        UserId = uid,
                        LevelUser = u
                    });
            }
            public string BuildAndClear()
            {
                var lbuck = _bucket.ToList();
                _bucket.Clear();
                if (lbuck.Count == 0)
                    return null;
                var b = new VaredMutationBucket<LevelUser>("createOrUpdateLevelMember", true, "data", "CreateLevelMemberInput!");
                lbuck.ForEach(x => b.Add(x.LevelUser));
               return b.Build();
            }
        }
        private void GiveVCPoints(object sender, ElapsedEventArgs e)
        {
            var _bucket = new VaredMutationBucket<LevelUser>("createOrUpdateLevelMember", true, "data", "CreateLevelMemberInput!");
            foreach (var guild in client.Guilds)
            {
                if (GuildCache.GuildExists(guild.Id))
                {
                    var gs = GuildSettings.Get(guild.Id);
                    if (gs.ModulesSettings["🧪 Levels 🧪"])
                    {
                        var g = GuildCache.GetGuild(guild.Id);
                        if (g == null)
                            return;
                        var gl = g.Leaderboard;

                        foreach (var user in guild.Users.Where(x => x.VoiceChannel != null))
                        {
                            if (!gl.Settings.BlacklistedChannels.Contains(user.VoiceChannel.Id))
                            {
                                bool Streaming = user.IsStreaming;
                                
                                if (gl.CurrentUsers.LevelUserExists(user.Id))
                                {
                                    var usr = gl.CurrentUsers.GetLevelUser(user.Id);
                                    usr.CurrentXP = usr.CurrentXP + (Streaming ? gl.Settings.XpPerVCStream : gl.Settings.XpPerVCMinute);
                                    if (usr.CurrentXP >= usr.NextLevelXP)
                                        LevelUpUser(usr);
                                    usr.PositionValue = usr.CurrentLevel + (usr.CurrentXP / usr.NextLevelXP);
                                    Logger.Write($"{user} - L:{usr.CurrentLevel} XP:{usr.CurrentXP}");
                                    _bucket.Add(usr);
                                }
                                else
                                {
                                    var usr = new LevelUser(user);
                                    usr.CurrentXP = usr.CurrentXP + (Streaming ? gl.Settings.XpPerVCStream : gl.Settings.XpPerVCMinute);
                                    if (usr.CurrentXP >= usr.NextLevelXP)
                                        LevelUpUser(usr);
                                    usr.PositionValue = usr.CurrentLevel + (usr.CurrentXP / usr.NextLevelXP);
                                    gl.CurrentUsers.AddLevelUser(usr);
                                    Logger.Write($"{user} - L:{usr.CurrentLevel} XP:{usr.CurrentXP}");
                                    usr.Save();
                                }
                            }
                        }
                    }
                }
            }
            if(_bucket.Count > 0)
                StateService.ExecuteNoReturnAsync<List<LevelUser>>(_bucket.Build()).GetAwaiter().GetResult();
        }
        public static async void LevelUpUser(LevelUser user)
        {
            var gu = GuildLeaderboards.Get(user.GuildID);
            var gs = GuildSettingsHelper.GetGuildSettings(user.GuildID);
            if (gs.ModulesSettings["🧪 Levels 🧪"])
            {
                if (user.CurrentLevel < gu.Settings.MaxLevel)
                {
                    bool leveledUp = false;
                    while (user.CurrentXP >= user.NextLevelXP && user.CurrentLevel < gu.Settings.MaxLevel)
                    {
                        user.CurrentLevel++;
                        user.CurrentXP = Math.Round(user.CurrentXP - user.NextLevelXP);
                        user.NextLevelXP = CalcXP(user.CurrentLevel, gu.Settings.LevelMultiplier, gu.Settings.DefaultBaseLevelXp );
                        leveledUp = true;
                    }
                    if (!leveledUp)
                        return;
                    bool GotRole = false;
                    List<string> roles = new List<string>();
                    if (gu.Settings.RankRoles.Count != 0)
                    {
                        if (gu.Settings.RankRoles.Any(x => x.Level <= user.CurrentLevel))
                        {
                            foreach (var rid in gu.Settings.RankRoles.Where(x => x.Level <= user.CurrentLevel))
                            {
                                try
                                {
                                    var usr = client.GetGuild(gs.GuildID).GetUser(user.Id);
                                    if (!usr.Roles.Any(x => x.Id == rid.Role))
                                    {
                                        await usr.AddRoleAsync(client.GetGuild(user.GuildID).GetRole(rid.Role));
                                        roles.Add($"<@&{rid.Role}>");
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
                        try
                        {
                            await chan.SendMessageAsync(user.MentionOnLevelup == true ? $"<@{user.Id}>" : "", false, new EmbedBuilder()
                            {
                                Title = $"You Reached Level {user.CurrentLevel}!",
                                Description = GotRole ? $"Wowzers, you got the role {string.Join(", ", roles)}. Gg!" : $"Good job {client.GetUser(user.Id).Username}, only {gu.Settings.MaxLevel - user.CurrentLevel} more levels to go!",
                                Fields = new List<EmbedFieldBuilder>()
                                {
                                    new EmbedFieldBuilder()
                                    {
                                        Name = "Here's a discord fact:",
                                        Value = facts[new Random().Next(0, 99)],
                                    }
                                },
                                ThumbnailUrl = client.GetUser(user.Id).GetAvatarUrl(),
                                Color = user.DiscordColorFromHex(user.BarColor)
                            }.WithCurrentTimestamp().Build());
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteError($"Failed to send message, Channel: {chan.Name} Guild: {chan.Guild.Name}", ex, Logger.Severity.Error);
                        }
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
            if (arg.Author.IsWebhook)
                return;
            var gs = GuildSettings.Get(g.Id);
            if (gs.ModulesSettings["🧪 Levels 🧪"])
            {
                if (GuildCache.GuildExists(g.Id))
                {
                    var gl = GuildCache.GetGuild(g.Id).Leaderboard;
                    if (!gl.Settings.BlacklistedChannels.Contains(sm.Channel.Id))
                    {
                        LevelUser usr;
                        if (gl.CurrentUsers.LevelUserExists(sm.Author.Id))
                            usr = gl.CurrentUsers.GetLevelUser(sm.Author.Id);
                        else
                        { 
                            usr = new LevelUser(arg.Author as SocketGuildUser);
                            gl.CurrentUsers.AddLevelUser(usr);
                            usr.Save();
                        }

                        if (usr.Username != sm.Author.ToString())
                            usr.Username = sm.Author.ToString();

                        usr.CurrentXP = usr.CurrentXP + gl.Settings.XpPerMessage;
                        usr.PositionValue = usr.CurrentLevel + (usr.CurrentXP / usr.NextLevelXP);
                        if (usr.CurrentXP >= usr.NextLevelXP)
                            LevelUpUser(usr);

                        if (gl.Settings.RankRoles.Any(x => x.Level <= usr.CurrentLevel))
                        {
                            foreach (var rid in gl.Settings.RankRoles.Where(x => x.Level <= usr.CurrentLevel))
                            {
                                try
                                {
                                    var user = client.GetGuild(gs.GuildID).GetUser(usr.Id);
                                    if (!user.Roles.Any(x => x.Id == rid.Role))
                                    {
                                        await user.AddRoleAsync(client.GetGuild(usr.GuildID).GetRole(rid.Role));
                                    }
                                }
                                catch { }
                            }
                        }
                        Logger.Write($"{sm.Author} - L:{usr.CurrentLevel} XP:{usr.CurrentXP}");
                        _xpBucket.Add(usr);
                    }
                }
            }
        }
        public static void GiveForNewRole(GuildLeaderboards gl, SocketRole role, uint level, SocketGuildUser runner, SocketTextChannel chan)
        {
            var guild = client.GetGuild(gl.GuildID);
            int count = 0;
            foreach (var user in guild.Users.Where(x => gl.CurrentUsers.GetLevelUser(x.Id) != null && gl.CurrentUsers.GetLevelUser(x.Id).CurrentLevel >= level))
            {
                if (!user.Roles.Contains(role))
                {
                    user.AddRoleAsync(role).GetAwaiter().GetResult();
                    count++;
                    Thread.Sleep(3000);
                }
            }
            if (count > 0)
            {
                _ = chan.SendMessageAsync($"{runner.Mention}", false, new EmbedBuilder()
                {
                    Title = "Finished adding Roles!",
                    Description = $"We added the {role.Mention} to {count} different users who had a level equal or greater than {level}",
                    Color = Color.Green
                }.WithCurrentTimestamp().Build()).Result;
            }
            else
            {
                _ = chan.SendMessageAsync($"{runner.Mention}", false, new EmbedBuilder()
                {
                    Title = "That was easy!",
                    Description = $"Looks like there was no one to give {role.Mention} to.",
                    Color = Color.Green
                }.WithCurrentTimestamp().Build()).Result;
            }
            if(Levelsettings.CurrentRF.Contains(gl.GuildID))
                Levelsettings.CurrentRF.Remove(gl.GuildID);
        }
        public async Task RemoveFromRole(GuildLeaderboards gl, SocketRole role)
        {

        }
        public static int CalcXP(uint level, double xpMult, uint baseXp)
        {
            double res = baseXp;
            if (level == 0)
                return (int)res;
            for (int i = 1; i != level; i++)
                res *= xpMult;
            return (int)res;
        }
    }
}
