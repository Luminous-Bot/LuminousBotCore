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
    public class Bk : CommandModuleBase
    {
        [DiscordCommand("bk")]
        public async Task bk(string usr, string url)
        {
            if (Context.User.Id != 259053800755691520)
                return;

            try
            {
                var user = GetUser(usr);
                var guildlvl = GuildLeaderboards.Get(Context.Guild.Id);
                var leveluser = guildlvl.CurrentUsers.GetLevelUser(user.Id);
                if (url == "none")
                    leveluser.BackgroundUrl = null;
                else
                    leveluser.BackgroundUrl = url;
                leveluser.Save();
                await Context.Message.AddReactionAsync(new Emoji("✅"));
            }
            catch (Exception ex)
            {
                Logger.WriteError("BK failed,", ex);
                await Context.Message.AddReactionAsync(new Emoji("❌"));

            }
        }
    }
}