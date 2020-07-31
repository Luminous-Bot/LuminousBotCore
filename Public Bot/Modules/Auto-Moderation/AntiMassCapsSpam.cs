using System;
using System.Collections.Generic;
using System.Text;

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
        /// <summary>
        /// Basic Constructor.
        /// </summary>
        public AntiMassCapsSpam() { }
    }
}
