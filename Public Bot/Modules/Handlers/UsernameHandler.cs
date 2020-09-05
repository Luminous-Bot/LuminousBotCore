using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot.Modules.Handlers
{
    [DiscordHandler]
    public class UsernameHandler
    {
        public DiscordShardedClient client;
        public UsernameHandler(DiscordShardedClient c)
        {
            client = c;

            client.UserUpdated += Client_UserUpdated;
        }

        private async Task Client_UserUpdated(SocketUser arg1, SocketUser arg2)
        {
            if (arg1.ToString() != arg2.ToString())
            {
                if (UserCache.UserExists(arg2.Id))
                {
                    var newuser = await StateService.MutateAsync<User>(GraphQLParser.GenerateGQLMutation<User>("addUserNewUsername", false, null, "", "",
                        ("username", GraphQLParser.CleanUserContent(GraphQLParser.CleanUserContent(arg2.ToString()))), // Because the graphql string needs to be cleaned as well we have to clean^2
                        ("id", arg2.Id)
                    ));
                    UserCache.UpdateUser(newuser);
                }
            }
        }
    }
}
