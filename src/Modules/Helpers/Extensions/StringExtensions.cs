using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot
{
    public static class StringExtensions
    {
        public static string CompileVarMessage(this string s, SocketGuild guild, SocketUser user)
        {
            return s.Replace("{guild}", guild.Name)
                   .Replace("{guild.name}", guild.Name)
                   .Replace("{guild.count.format}", guild.MemberCount.DisplayWithSuffix())
                   .Replace("{guild.count}", guild.MemberCount.ToString())
                   .Replace("{user}", user.Mention)
                   .Replace("{user.name}", user.Username)
                   .Replace("{user.username}", user.ToString())
                   .Replace("{user.tag}", user.DiscriminatorValue.ToString());

        }
    }
}
