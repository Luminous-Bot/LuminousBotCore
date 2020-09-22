using Discord;
using Discord.WebSocket;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static Public_Bot.Modules.Handlers.LevelHandler;
using Color = Discord.Color;
namespace Public_Bot.Modules.Commands.Level_Commands
{
    [DiscordCommandClass("🧪 Levels 🧪", "Add ranks and leaderboards for your server with Levels!")]
    public class Leaderboard : CommandModuleBase
    {
        [Alt("lb")]
        [DiscordCommand("leaderboard", commandHelp = "Usage - `(PREFIX)leaderboard`", description = "Shows the leaderboard for the server")]
        public async Task leaderboard()
        {
            var gl = GuildLeaderboards.Get(Context.Guild.Id);

            var levelmembers = gl.GetTop(9);
            //foreach(var item in levelmembers)
            List<EmbedFieldBuilder> f = new List<EmbedFieldBuilder>();
            for (int i = 0; i != levelmembers.Count; i++)
            {
                var user = levelmembers[i];
                var emote = "";
                switch (i)
                {
                    case 0:
                        emote = ":first_place:";
                        break;
                    case 1:
                        emote = ":second_place:";
                        break;
                    case 2:
                        emote = ":third_place:";
                        break;
                    case 3:
                        emote = ":four:";
                        break;
                    case 4:
                        emote = ":five:";
                        break;
                    case 5:
                        emote = ":six:";
                        break;
                    case 6:
                        emote = ":seven:";
                        break;
                    case 7:
                        emote = ":eight:";
                        break;
                    case 8:
                        emote = ":nine:";
                        break;
                    case 9:
                        emote = ":keycap_ten:";
                        break;
                };
                string msg = $"> Level {(int)user.CurrentLevel}\n> XP {(long)user.CurrentXP}/{(long)user.NextLevelXP}\n\n";
                f.Add(new EmbedFieldBuilder()
                {
                    IsInline = true,
                    Name = emote + $" {user.Username}",
                    Value = msg
                });

            }
            string url = null;
            try
            {
                url = await LeaderboardImageHelper.GetLeaderboardImageURL(levelmembers, gl.Settings);
            }
            catch(Exception x) 
            {
                Console.WriteLine(x);
            }

            var embed = new EmbedBuilder()
            {
                Author = new EmbedAuthorBuilder()
                {
                    Name = Context.Guild.Name,
                    IconUrl = Context.Guild.IconUrl
                },
                Color = Blurple,
                Fields = f,
                ImageUrl = url
            }.WithCurrentTimestamp();
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
    }
}