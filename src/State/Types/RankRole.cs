using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot
{
    public class RankRole
    {
        [GraphQLProp, GraphQLSVar]
        public uint Level { get; set; }
        [GraphQLProp, GraphQLSVar]
        public ulong Role { get; set; }
    }
}
