using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot.State.Types
{
    class Guilds : StateEntity<Guilds>
    {
        public string GuildID { get; set; }
        public string GuildName { get; set; }
        public List<GuildMembers> GuildMembers { get; set; }
    }
}
