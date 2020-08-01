using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace Public_Bot.Modules.Auto_Moderation
{
    public class AntiMassCapsSpam
    {
        /// <summary>
        /// Whether <b>Anti Mass Caps Spam</b> is enabled.
        /// </summary>
        public bool Enabled { get; set; } = false;
        /// <summary>
        /// Whether to DM the user on Message Deletion/Violation.
        /// </summary>
        public bool DMUser { get; set; } = true;
        public ushort Percentage { get; set; } = 80;
        public AntiMassCapsSpam() { }
        /// <summary>
        /// Basic Constructor.
        /// </summary>
        public AntiMassCapsSpam(bool DMUs) {
            DMUser = DMUs;
        }
        /// <summary>
        /// Checks whether the message is majorly has Mass-Caps spam.
        /// </summary>
        /// <param name="ctxt">The SocketCommandContext needed for deletion</param>
        /// <returns></returns>
        public async Task MassCaps(SocketCommandContext ctxt)
        {
            var check = ctxt.Message.Content;
            if (check.Split(' ').Length > 3 && (string.Concat(check.Where(c => c >= 'A' && c <= 'Z')).Count() * 100 /check.Length) > Percentage)
            {
                await ctxt.Message.DeleteAsync();
                if (!DMUser) return;
                EmbedBuilder embed = new EmbedBuilder
                {
                    Title = "Mass-Caps Warning",
                    Description = $"Due to your message in channel <#{ctxt.Channel.Id}> of **{ctxt.Guild}** being detected as Mass Caps with the set percentage, your message has been deleted.",
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
