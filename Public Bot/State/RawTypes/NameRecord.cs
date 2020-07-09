using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot.State.Types
{
    public class NameRecord : StateEntity<NameRecord>
    {
        public string Name { get; set; }
        public DateTime Time { get; set; }
    }
}
