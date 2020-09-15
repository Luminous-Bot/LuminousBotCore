using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot
{
    public class ReactionRoleCard
    {
        [GraphQLProp, GraphQLSVar]
        public ulong MessageID { get; set; }
        [GraphQLProp, GraphQLSVar]
        public ulong GuildID { get; set; }
        [GraphQLProp]
        public DateTime DateCreated { get; set; }

        [GraphQLSObj, GraphQLObj]
        public List<RoleCardItem> Roles { get; set; }


        public async Task<ReactionRoleCard> Create()
            => await StateService.MutateAsync<ReactionRoleCard>(GraphQLParser.GenerateGQLMutation<ReactionRoleCard>("createOrUpdateReactionRoleCard", true, this, "data", "ReactionRoleCardInput!"));
    }
}
