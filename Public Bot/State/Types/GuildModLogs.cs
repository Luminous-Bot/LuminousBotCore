using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot
{
    public class GuildModLogs
    {
        public string GuildName { get; set; }
        public ulong GuildID { get; set; }
        public List<Users> Users { get; set; } = new List<Users>();
    }
}
