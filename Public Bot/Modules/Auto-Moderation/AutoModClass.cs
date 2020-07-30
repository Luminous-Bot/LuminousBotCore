using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot.Modules.Auto_Moderation
{
    public class AutoModClass
    {
        /// <summary>
        /// The boolean that decides whether the Automod Class is enabled
        /// </summary>
        public bool Enabled { get; set; } = false;
        /// <summary>
        /// If true, then the AutoModeration will apply on Bots, else it won't.
        /// </summary>
        public bool ApplyOnBots { get; set; } = false;
        /// <summary>
        /// If true, then the AutoModeration will apply on Admins, else it won't.
        /// </summary>
        public bool ApplyOnAdmins { get; set; } = false;
        /// <summary>
        /// The AntiSpam class for the Bot.
        /// </summary>
        public Anti_Spam Antispam { get; set; } = new Anti_Spam();
        public AutoModClass() { }
        /// <summary>
        /// A function for execution of AutoModeration
        /// </summary>
        /// <param name="alfa">The SocketCommandContext required.</param>
        /// <returns></returns>
        public async Task AutoModeration(SocketCommandContext alfa)
        {
            if (!Enabled) return;
            if (alfa.User as SocketGuildUser == null) return;
            if ((alfa.User as SocketGuildUser).GuildPermissions.Administrator && !ApplyOnAdmins) return;
            if (alfa.User.IsBot && !ApplyOnBots) return;
            await Antispam.SimpleSpamCheck(alfa);
        }
    }
}
