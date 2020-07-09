using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot.State.Types
{
    class InfractionPages : StateEntity<InfractionPages>
    {
        public string UserID { get; set; }
        public string GuildID { get; set; }
        public int Page { get; set; }
        public string PageOwner { get; set; }
        public List<Infraction> Modlogs { get; set; }
    }
}
