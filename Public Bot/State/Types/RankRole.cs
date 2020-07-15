using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot
{
    public class RankRole
    {
        [GraphQLProp]
        public uint Level { get; set; }
        [GraphQLProp]
        public ulong Role { get; set; }
    }
}
