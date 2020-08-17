using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ulong MemberID { get; set; }
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
        public LevelUser Save()
        {
            if(!UserHandler.Exists(this.MemberID))
                UserHandler.CreateUser(this.MemberID);
            if(!GuildHandler.GuildMemberExists(this.MemberID, this.GuildID))
                if (!GuildMember.Exists(this.MemberID, this.GuildID))
                    GuildHandler.CreateGuildMember(this.MemberID, this.GuildID);
            return StateService.Mutate<LevelUser>(GraphQLParser.GenerateGQLMutation<LevelUser>("createOrUpdateLevelMember", true, this, "data", "CreateLevelMemberInput!"));
        }
        public LevelUser() { }
        public LevelUser(SocketGuildUser user)
        {
            MemberID = user.Id;
            Username = GraphQLParser.CleanUserContent(user.ToString());
            GuildID = user.Guild.Id;
            Save();
        }
    }
}
