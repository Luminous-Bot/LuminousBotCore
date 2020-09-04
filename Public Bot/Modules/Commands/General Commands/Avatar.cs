using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Color = Discord.Color;
namespace Public_Bot.Modules.Commands.General_Commands
{
    [DiscordCommandClass("👨🏼‍💻 General 👨🏼‍💻", "General bot commands for everyone!")]
    public class Avatar : CommandModuleBase
    {
        [Alt("av")]
        [DiscordCommand("avatar",commandHelp ="`(PREFIX)avatar <user>`",description ="Shows the users avatar")]
        public async Task AvatarShows(params string[] args)
        {
            SocketGuildUser use;
            if (args.Length == 0)
            {
                use = Context.User as SocketGuildUser;
            }
            else
            {
                use = GetUser(args[0]);
            }
            if (use == null)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                {
                    Title = "Error",
                    Description = "That user is invalid!",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            var png = use.GetAvatarUrl(ImageFormat.Png, 1024);

            var jpeg = use.GetAvatarUrl(ImageFormat.Jpeg,1024);
            var webp = use.GetAvatarUrl(ImageFormat.WebP,1024);
            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder {
                Title = "User Avatar",
                Description = $"Here are the avatar links:\n1. **[PNG]({png})**\n2. **[JPEG]({jpeg})**\n3. **[WEBP]({webp})**",
                ImageUrl = jpeg,
                Color = Blurple
            }.WithCurrentTimestamp().Build());
        }
    }
}