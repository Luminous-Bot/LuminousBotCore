using Public_Bot.Modules.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot.State.Types
{
    public class Infraction : StateEntity<Infraction>
    {
        public GuildMembers Member { get; set; }
        public ModCommands.Action Action { get; set; }
        public DateTime Time { get; set; }
        public string Reason { get; set; }
        public GuildMembers Moderator { get; set; }
        public string GuildID { get; set; }
        public string UserID { get; set; }
    }
}
