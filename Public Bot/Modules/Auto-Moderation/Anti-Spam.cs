using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Public_Bot.Modules.Auto_Moderation
{
    public class Anti_Spam
    {
        /// <summary>
        /// Boolean to decide whether to DM user on delete or not.
        /// </summary>
        public bool DMUser { get; set; } = true;

        /// <summary>
        /// The maximum <b>characters</b> allowed as not spam.
        /// </summary>
        public uint MaxChars { get; set; } = 70;


        /// <summary>
        /// No parameter constructor of Antispam.
        /// </summary>
        public Anti_Spam() { }

        /// <summary>
        /// Constructor taking only the <b>DM User Boolean</b>
        /// </summary>
        /// <param name="DMUse"></param>
        public Anti_Spam(bool DMUse)
        {
            DMUser = DMUse;
        }

        /// <summary>
        /// Checks the message for exceeding the maximum permitted character length.
        /// </summary>
        /// <param name="ctxt">The SocketCommandContext required for deletion and user DM</param>
        /// <returns></returns>
        public async Task SimpleSpamCheck(SocketCommandContext ctxt)
        {
            if (ctxt.Message.Content.Length >= MaxChars) {
                await ctxt.Message.DeleteAsync();
                if (!DMUser) return;
                EmbedBuilder embed = new EmbedBuilder {
                    Title = "Spam Warning",
                    Description = $"Due to your message in channel <#{ctxt.Channel.Id}> of **{ctxt.Guild}** exceeding the permitted message character limit, your message has been deleted.",
                    Color = Color.DarkRed,
                    Footer = new EmbedFooterBuilder
                    {
                        Text = "Auto-Moderation by Luminous"
                    }
                }.WithCurrentTimestamp();
                await ctxt.User.SendMessageAsync("", false, embed.Build());
            }
            return;
        }
    }
}
