using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static Public_Bot.RolecardParseResult;

namespace Public_Bot
{
    public class RolecardParseResult
    {
        public bool isSuccess { get; set; } = false;
        public string FailReason { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Color? Color { get; set; }
        public List<RoleCardParseItem> Items { get; set; } = new List<RoleCardParseItem>();
        public class RoleCardParseItem
        {
            public string Emote { get; set; }
            public string Description { get; set; }
            public ulong RoleID { get; set; }
        }
    }

    public class RolecardCommandParser
    {
        public static RolecardParseResult Parse(string args, ShardedCommandContext Context)
        {
            var res = new RolecardParseResult();

            string[] splits = args.Split(',');

            if(splits.Length == 0)
            {
                // didnt specify multiple

            }

            // multiple items
            foreach(var item in splits)
            {
                string[] vals = item.Split(' ');

                if(vals.Length < 3)
                {
                    res.FailReason = $"Invalid emote role description pair. expected <emote> <role> <description> but got \"{item}\"";
                    return res;
                }

                var role = CommandModuleBase.GetRole(vals[1], Context);
                if(role == null)
                {
                    res.FailReason = $"Invalid Role: Expected Role, Got \"{vals[1]}\" in \"{item}\"";
                    return res;
                }

                RoleCardParseItem i = new RoleCardParseItem()
                {
                    Description = string.Join(' ', vals.Skip(2)),
                    RoleID = role.Id,
                    Emote = vals[0]
                };
                res.Items.Add(i);
                //res
            }


            return res;
        }
    }
}
