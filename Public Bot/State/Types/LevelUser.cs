using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Public_Bot
{
    public class LevelUser : IDoubleEntityID
    {
        [GraphQLSVar]
        [GraphQLProp]
        public ulong GuildID { get; set; }
        [GraphQLProp, GraphQLSVar, GraphQLName("MemberID"), JsonProperty("MemberID")]
        public ulong Id { get; set; }  
        [GraphQLProp]
        public string Username { get; set; }
        [GraphQLProp]
        [GraphQLSVar]
        public uint CurrentLevel { get; set; } = 0;
        [GraphQLProp]
        [GraphQLSVar]
        public double CurrentXP { get; set; } = 0;
        [GraphQLProp]
        [GraphQLSVar]
        public double NextLevelXP { get; set; } = 30;
        [GraphQLProp, GraphQLSVar]
        public double TotalXP { get; set; } = 0;
        [GraphQLProp]
        public string BarColor { get; set; } = "00ff00";
        [GraphQLProp]
        public string BackgroundColor { get; set; } = "262626";
        [GraphQLProp]
        public bool MentionOnLevelup { get; set; } = true;
        [GraphQLProp]
        public string BackgroundUrl { get; set; } = null;

        public string HexFromColor(System.Drawing.Color c)
            => $"{c.R.ToString("X2")}{c.G.ToString("X2")}{c.B.ToString("X2")}";
        public string HexFromColor(Discord.Color c)
            => $"{c.R.ToString("X2")}{c.G.ToString("X2")}{c.B.ToString("X2")}";
        public System.Drawing.Color ColorFromHex(string hex)
            => System.Drawing.ColorTranslator.FromHtml($"#{hex}");
        public Discord.Color DiscordColorFromHex(string hex)
        {
            var c = System.Drawing.ColorTranslator.FromHtml($"#{hex}");
            var fin = new Discord.Color(c.R, c.G, c.B);
            return fin;
        }
        public long GetRank()
        {
            var rank = StateService.Query<long>($"{{\"operationName\":null,\"variables\":{{}},\"query\":\"{{ levelMemberRank(GuildID: \\\"{this.GuildID}\\\", UserID: \\\"{this.Id}\\\") }} \"}}");
            return rank;
        }
        public double CalculateTotalXP()
        {
            if (CurrentLevel == 0)
                return CurrentXP;
            var g = GuildCache.GetGuild(GuildID);
            double res = NextLevelXP;
            double total = res;
            for (int x = 1; x != CurrentLevel; x++)
            {
                res /= g.Leaderboard.Settings.LevelMultiplier;
                total += res;
            }
            total += CurrentXP;
            return (int)total;
        }
        public LevelUser Save()
        {
            if (!UserCache.UserExists(this.Id))
                UserCache.CreateUser(this.Id);

            var guild = GuildCache.GetGuild(this.GuildID);
            if (!guild.GuildMembers.GuildMemberExists(this.Id))
                guild.GuildMembers.CreateGuildMember(this.Id);
            TotalXP = CalculateTotalXP();
            return StateService.Mutate<LevelUser>(GraphQLParser.GenerateGQLMutation<LevelUser>("createOrUpdateLevelMember", true, this, "data", "CreateLevelMemberInput!"));
        }
        public static LevelUser Fetch(ulong GuildId, ulong UserId)
            => StateService.Query<LevelUser>(GraphQLParser.GenerateGQLQuery<LevelUser>("levelMember", ("GuildID", $"{GuildId}"), ("UserID", $"{UserId}")));
        public static bool Exists(ulong GuildId, ulong UserId)
            => StateService.Exists<LevelUser>(GraphQLParser.GenerateGQLQuery<LevelUser>("levelMember", ("GuildID", $"{GuildId}"), ("UserID", $"{UserId}")));
        public LevelUser() { }
        public LevelUser(SocketGuildUser user)
        {
            Id = user.Id;
            Username = GraphQLParser.CleanUserContent(user.ToString());
            GuildID = user.Guild.Id;
            Save();
        }
    }
}
