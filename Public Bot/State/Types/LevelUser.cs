using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot
{
    public class LevelUser
    {
        [GraphQLSVar]
        [GraphQLProp]
        public ulong GuildID { get; set; }
        [GraphQLProp]
        [GraphQLSVar]
        [GraphQLName("MemberID")]
        public ulong UserID { get; set; }
        public string Username { get; set; } = "";
        [GraphQLProp]
        [GraphQLSVar]
        public uint CurrentLevel { get; set; } = 0;
        [GraphQLProp]
        [GraphQLSVar]
        public double CurrentXP { get; set; } = 0;
        [GraphQLProp]
        [GraphQLSVar]
        public double NextLevelXP { get; set; } = 30;
        [GraphQLProp]
        [GraphQLName("BarColor")]
        public string EmbedColor { get; set; } = "00ff00";
        [GraphQLProp]
        [GraphQLName("BackgroundColor")]
        public string RankBackgound { get; set; } = "262626";
        [GraphQLProp]
        [GraphQLName("MentionOnLevelup")]

        public bool MentionLevelup { get; set; } = true;
        [GraphQLProp]
        [GraphQLName("BackgroundUrl")]
        public string bkurl { get; set; } = null;

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
        public LevelUser Save()
        {
            if (!User.UserExists(this.UserID))
                UserHandler.CreateUser(this.UserID);
            if (!GuildMember.Exists(this.UserID, this.GuildID))
                GuildHandler.CreateGuildMember(this.UserID, this.GuildID);
            return StateService.Mutate<LevelUser>(GraphQLParser.GenerateGQLMutation<LevelUser>("createOrUpdateLevelMember", true, this, "data", "CreateLevelMemberInput!"));
        }
        public LevelUser() { }
        public LevelUser(SocketGuildUser user)
        {
            UserID = user.Id;
            Username = user.ToString();
            GuildID = user.Guild.Id;
            Save();
        }
    }
}
