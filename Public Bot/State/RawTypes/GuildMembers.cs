using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot.State.Types
{
    public class GuildMembers : StateEntity<GuildMembers>
    {
        public string GuildID { get; set; }
        public string UserID { get; set; }
        public List<NameRecord> Nicknames { get; set; }
        public List<Infraction> Infractions { get; set; }
        public Users User { get; set; }

    }
}
