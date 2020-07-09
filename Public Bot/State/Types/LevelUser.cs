using Discord;
using Discord.WebSocket;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using static Public_Bot.Modules.Handlers.LevelHandler;

namespace Public_Bot
{
    public class LevelUser
    {
        public ulong GuildID { get; set; }
        public ulong UserID { get; set; }
        public string Username { get; set; } = "";
        public uint CurrentLevel { get; set; } = 0;
        public double CurrentXP { get; set; } = 0;
        public double NextLevelXP { get; set; } = 30;
        public string EmbedColor { get; set; } = "00ff00";
        public string RankBackgound { get; set; } = "262626";
        public bool MentionLevelup { get; set; } = true;
        public string bkurl { get; set; } = null;

        public string HexFromColor(System.Drawing.Color c)
            => $"{c.R.ToString("X2")}{c.G.ToString("X2")}{c.B.ToString("X2")}";
        public string HexFromColor(Discord.Color c)
            => $"{c.R.ToString("X2")}{c.G.ToString("X2")}{c.B.ToString("X2")}";
        public System.Drawing.Color ColorFromHex(string hex)
            => ColorTranslator.FromHtml($"#{hex}");
        public Discord.Color DiscordColorFromHex(string hex)
        {
            var c = ColorTranslator.FromHtml($"#{hex}");
            var fin = new Discord.Color(c.R, c.G, c.B);
            return fin;
        }

        public async Task Save()
        {
            string q = $"{{\"operationName\":\"updateUserCardDetails\",\"variables\":{{\"data\":{{\"BarColor\":\"{EmbedColor}\",\"BackgroundColor\":\"{RankBackgound}\",\"BackgroundUrl\":\"{bkurl}\",\"MentionOnLevelup\": {(MentionLevelup ? "true" : "false")}}}}},\"query\":\"mutation updateUserCardDetails($data: updateUserCardDetailsInput!) {{ updateUserCardDetails(data: $data, userID: \"\") {{Id}}}}\"}}";
            await StateHandler.Postgql(q);
        }
        public LevelUser() { }
        public LevelUser(SocketGuildUser user)
        {
            UserID = user.Id;
            Username = user.ToString();
            GuildID = user.Guild.Id;
        }
    }
}
