using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot
{
    public class RoleCardItem
    {
        [GraphQLProp, GraphQLSVar]
        public ulong RoleID { get; set; }
        [GraphQLProp, GraphQLSVar]
        public ulong EmoteID { get; set; }
    }
}
