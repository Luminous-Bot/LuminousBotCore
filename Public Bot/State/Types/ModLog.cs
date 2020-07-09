using Public_Bot.Modules.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot
{
    public class ModLog
    {
        public Moderator Moderator { get; set; }
        public ulong GuildID { get; set; }
        public ulong UserID { get; set; }
        public ModCommands.Action Action { get; set; }
        public DateTime Time { get; set; }
        public string Reason { get; set; }
    }
}
