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
        public string EmoteID { get; set; }
    }
}
