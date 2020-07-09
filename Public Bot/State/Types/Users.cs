using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot
{
    public class Users
    {
        public string UserName { get; set; }
        public ulong UserID { get; set; }
        public List<ModLog> ModLogs { get; set; }
    }
}
