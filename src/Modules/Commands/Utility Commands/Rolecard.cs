using System;
using System.Collections.Generic;
using System.Linq;
using Discord;
using System.Threading.Tasks;

namespace Public_Bot.Modules.Commands.Utility_Commands
{
    [DiscordCommandClass("🎚 Utilities 🎚", "Enable some vital features to improve your overall server fuctionality!")]
    public class Rolecard : CommandModuleBase
    {
        [DiscordCommand("rolecard",
            RequiredPermission = true,
            description = "Create, Edit, and Delete your rolecards!",
            // TODO: Change help message if need be
            commandHelp = "`(PREFIX)rolecard`"
        )]
        // create <#channel> <messageId> [<Emote> <Role> <Description>]
        // add
        // remove
        // delete
        // list
        public async Task Rolecard_Command(params string[] args)
        {
            if(args.Length == 0)
            {
                // List the guilds role cards
            }

            switch (args[0].ToLower())
            {
                case "create":

                    if(args.Length == 1)
                    {
                        // Show params
                    }

                    if(args.Length == 2)
                    {
                        // 
                    }

                    return;

                case "delete":

                    return;

                case "add":
                    await AddTo(args);
                    return;
                case "addto":
                    await AddTo(args);
                    return;

                case "removefrom":
                    await RemoveFrom(args);
                    return;
                case "remove":
                    await RemoveFrom(args);
                    return;

            }
        }
        public async Task AddTo(string[] args)
        {

        }
        public async Task RemoveFrom(string[] args)
        {

        }
    }
}
