using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot
{
    public static class HexColorConverter
    {
        public static string HexFromColor(System.Drawing.Color c)
            => $"{c.R.ToString("X2")}{c.G.ToString("X2")}{c.B.ToString("X2")}";
        public static string HexFromColor(Discord.Color c)
            => $"{c.R.ToString("X2")}{c.G.ToString("X2")}{c.B.ToString("X2")}";
        public static System.Drawing.Color ColorFromHex(string hex)
            => System.Drawing.ColorTranslator.FromHtml($"{(hex.StartsWith("#") ? hex : "" + hex)}");
        public static Discord.Color DiscordColorFromHex(string hex)
        {
            var c = System.Drawing.ColorTranslator.FromHtml($"{(hex.StartsWith("#") ? hex : "" + hex)}");
            var fin = new Discord.Color(c.R, c.G, c.B);
            return fin;
        }
    }
}
