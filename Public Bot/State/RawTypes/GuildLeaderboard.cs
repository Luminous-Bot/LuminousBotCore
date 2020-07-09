using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot.State.Types
{
    public class GuildLeaderboard: StateEntity<GuildLeaderboard>
    {
        //settings
        public GuildLevelSettings Settings { get; set; }
        public string GuildID { get; set; }
    }
}
