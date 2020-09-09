using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Color = Discord.Color;
namespace Public_Bot.Modules.Commands.General_Commands
{
    [DiscordCommandClass("👨🏼‍💻 General 👨🏼‍💻", "General bot commands for everyone!")]
    public class Help : CommandModuleBase
    {
        [DiscordCommand("help", description = "shows all help messages for all enabled modules", commandHelp = "Usage: `(PREFIX)help`, `(PREFIX)help <command_name>`" )]
        public async Task help(params string[] args)
        {
            if(args.Length == 0)
            {
                HelpMessageHandler.BuildHelpPages(GuildSettings);
                var msg = await Context.Channel.SendMessageAsync("", false, HelpMessageHandler.HelpEmbedBuilder(1, HelpMessageHandler.CalcHelpPage(Context.Guild.GetUser(Context.Message.Author.Id), GuildSettings)));
            }
            else if(args[0] == "setup")
            {
                var perms = Context.Guild.GetUser(Context.Client.CurrentUser.Id).GuildPermissions;
                var pdin = GuildSettings.ModulesSettings.Keys.Max(x => string.Join(' ', x.Split(' ').Skip(1).Take(x.Split(' ').Length - 2)).Length) + 2;
                List<string> md = new List<string>();
                foreach (var m in GuildSettings.ModulesSettings)
                    md.Add($"{string.Join(' ', m.Key.Split(' ').Skip(1).Take(m.Key.Split(' ').Length - 2)).PadRight(pdin)} {(m.Value ? "✅" : "❌")}\n");
                Dictionary<string, string> prm = new Dictionary<string, string>();
                prm.Add("Admin:", $"{(perms.Administrator ? "✅" : "❌")}");
                prm.Add("Kick:", $"{(perms.KickMembers ? "✅" : "❌")}");
                prm.Add("Ban:", $"{(perms.BanMembers ? "✅" : "❌")}");
                prm.Add("Mentions:", $"{(perms.MentionEveryone ? "✅" : "❌")}");
                prm.Add("Manage Guild:", $"{(perms.ManageGuild ? "✅" : "❌")}");
                prm.Add("Manage Msg's:", $"{(perms.ManageMessages ? "✅" : "❌")}");
                prm.Add("Channels:", $"{(perms.ViewChannel ? "✅" : "❌")}");
                prm.Add("Manage Roles:", $"{(perms.ManageRoles ? "✅" : "❌")}");
                int leng = prm.Keys.Max(x => x.Length);
                List<string> final = new List<string>();
                foreach (var itm in prm)
                    final.Add(itm.Key.PadRight(leng) + " " + itm.Value);
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Here are some tips to setup this bot",
                    Description = "You can join our [Discord](https://discord.com/invite/w8EcwBy) for more help setting up the bot in your server",
                    Fields = new List<EmbedFieldBuilder>()
                    {
                        new EmbedFieldBuilder()
                        {
                            IsInline = true,
                            Name = "Bot Permissions",
                            Value = $"> Here's the bot's current Discord permissions:\n```{string.Join('\n', final)}```\n> Some Modules like the moderation module require kick and ban permissions."
                        },
                        new EmbedFieldBuilder()
                        {
                            IsInline = true,
                            Name = "Modules",
                            Value = $"> These are the current Module Settings with their statuses\n```{string.Join('\n', md)}```\n> You can Enable/Disable Modules with the `{GuildSettings.Prefix}modules` command."
                        },
                        new EmbedFieldBuilder()
                        {
                            IsInline = true,
                            Name = "Permission roles",
                            Value = $">>> These roles have elevated permissions and have access to all commands within the bot\n\n<@&{string.Join(">\n<@&", GuildSettings.PermissionRoles)}>\n\nTo add one, use `{GuildSettings.Prefix}addpermission <@role>`\nTo remove one, use `{GuildSettings.Prefix}removepermission <@role>`"
                        },
                        new EmbedFieldBuilder()
                        {
                            IsInline = true,
                            Name = "Levels",
                            Value = $">>> This bot features a leveling system where users can obtain roles with levels. It's currently {(GuildSettings.ModulesSettings["🧪 Levels 🧪"] ? $"Enabled - you can configure it with `{GuildSettings.Prefix}levelsettings list`. If you are stuck with setting up levels, try `{GuildSettings.Prefix}help levelsettings`" : $"Disabled - you can enable it with `{GuildSettings.Prefix}modules enable Levels`")}"
                        },
                        new EmbedFieldBuilder()
                        {
                            IsInline = true,
                            Name = "Logs",
                            Value = $">>> This bot has logging; You can set a channel for logs with `{GuildSettings.Prefix}logs channel <channel>`" //`(PREFIX)logs channel <channel>`, `(PREFIX)logs on/off`
                        },

                    },
                    Color = Color.Green
                }.Build());
            }
            else if(args.Length == 1)
            {
                var cmds = ReadCurrentCommands(GuildSettings.Prefix);
                var perm = HelpMessageHandler.CalcHelpPage(Context.User as SocketGuildUser, GuildSettings);
                if (cmds.Any(x => x.HasName(args[0].ToLower())))
                {
                    var cmd = cmds.Find(x => x.HasName(args[0].ToLower()));
                    if (perm == HelpMessageHandler.HelpPages.Public && cmd.RequiresPermission)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "**You can't access this command!**",
                            Description = "You don't have permission to use this command, therefore we're not gonna show you how.",
                            Color = Color.Red,
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = $"**{GuildSettings.Prefix}{cmd.CommandName}**",
                        Description = $"Here's some info about the command {cmd.CommandName}",
                        Fields = new List<EmbedFieldBuilder>()
                        {
                            new EmbedFieldBuilder()
                            {
                                Name = "Command Description",
                                Value = $"```\n{cmd.CommandDescription}```",
                            },
                            new EmbedFieldBuilder()
                            {
                                Name = "Command Help",
                                Value = $"\n{cmd.CommandHelpMessage}",
                            },
                            cmd.Alts.Count > 0 ? new EmbedFieldBuilder()
                            {
                                Name = "Alternative Command Names (alts)",
                                Value = $"```{string.Join(", ", cmd.Alts) }```",
                            } : new EmbedFieldBuilder() 
                            {
                                Name = "Alternative Command Names (alts)",
                                Value = "None",
                            },

                        },
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    return;
                }
            }

        }
    }
}