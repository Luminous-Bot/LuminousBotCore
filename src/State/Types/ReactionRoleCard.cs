using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot
{
    public class ReactionRoleCard
    {
        [GraphQLProp, GraphQLSVar]
        public ulong MessageID { get; set; }
        [GraphQLProp, GraphQLSVar]
        public ulong GuildID { get; set; }
        [GraphQLProp, GraphQLSVar]
        public DateTime DateCreated { get; set; }

        [GraphQLSObj, GraphQLObj]
        public List<RoleCardItem> Roles { get; set; }
    }
}
