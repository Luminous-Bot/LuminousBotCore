using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
namespace Public_Bot.Modules.Commands.Settings_Commands
{
    [DiscordCommandClass("⚙️ Settings ⚙️", "Change how this bot works in your server!")]
    public class Listcmd : CommandModuleBase
    {
        [Alt("listcommands")]
        [DiscordCommand("listcmd", 
            RequiredPermission = true, 
            description = "Lists all commands with their enabled/disabled status",
            commandHelp = "`(PREFIX)listcmd`")]
        public async Task ListCmd()
        {
            List<EmbedFieldBuilder> fields = new List<EmbedFieldBuilder>();
            //int paddingLenth = Commands.Max(x => x.CommandName.Length) + 1;
            
            foreach(var command in Commands)
            {
                if(fields.Any(x => x.Name == command.ModuleName))
                {
                    var f = fields.Find(x => x.Name == command.ModuleName);
                    string res = "";
                    bool enabled = !GuildSettings.DisabledCommands.Contains(command.CommandName);
                    if (!GuildSettings.ModulesSettings[command.ModuleName])
                        enabled = false;
                    res = $"{(enabled ? "✅" : "❌")} {command.CommandName}";
                    f.Value += $"\n{res}";
                }
                else
                {
                    EmbedFieldBuilder field = new EmbedFieldBuilder();
                    field.IsInline = true;
                    field.Name = command.ModuleName;
                    string res = "";
                    bool enabled = !GuildSettings.DisabledCommands.Contains(command.CommandName);
                    if (!GuildSettings.ModulesSettings[command.ModuleName])
                        enabled = false;
                    res = $"{(enabled ? "✅" : "❌")} {command.CommandName}";
                    field.Value = $"```\n{res}";
                    fields.Add(field);
                }
            }
            foreach (var f in fields)
                f.Value += "```";
            fields = fields.OrderBy(x => x.Value.ToString().Count(x => x == '\n') * -1).ToList();

            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder() 
            {
                Title = "Commands",
                Description = $"Here are all the enabled and disabled commands. You can enable one with `{GuildSettings.Prefix}enablecmd <command>` and disable one with `{GuildSettings.Prefix}disablecmd <command>`",
                Color = Blurple,
                Fields = fields
            }.WithCurrentTimestamp().Build());
        }
    }
}