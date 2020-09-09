using Public_Bot.Modules.Auto_Moderation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot.State.Types
{
    public class AutoMod
    {
        public bool AntiSpamEnabled { get; set; } = true;
        public SpamAction SpamAction { get; set; } = SpamAction.Delete;
        public int SpamMuteMinutes { get; set; } = 15;

    }
}
