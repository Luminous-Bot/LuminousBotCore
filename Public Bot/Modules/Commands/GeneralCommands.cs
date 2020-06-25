using Discord;
using Discord.WebSocket;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot.Modules.Commands
{
    [DiscordCommandClass("👨🏼‍💻 General Commands 👨🏼‍💻", "General bot commands for everyone!")]
    class GeneralCommands : CommandModuleBase
    {
        [DiscordCommand("help", description = "shows all help messages for all enabled modules", commandHelp = "Usage: `(PREFIX)help`, `(PREFIX)help <command_name>`" )]
        public async Task help(params string[] args)
        {
            if(args.Length == 0)
            {
                HelpMessageHandler.BuildHelpPages(GuildSettings);
                var msg = await Context.Channel.SendMessageAsync("", false, HelpMessageHandler.HelpEmbedBuilder(1, HelpMessageHandler.CalcHelpPage(Context.Guild.GetUser(Context.Message.Author.Id), GuildSettings)));
                //var emote1 = new Emoji("\U000027A1");
                //var emote2 = new Emoji("\U00002B05");
                //await msg.AddReactionAsync(emote2);
                //await msg.AddReactionAsync(emote1);
                //HelpMessageHandler.CurrentHelpMessages.Add(new HelpMessageHandler.HelpPage() 
                //{
                //    GuildID = Context.Guild.Id,
                //    MessageID = msg.Id,
                //    page = 1,
                //    pageOwner = Context.User.Id,
                //});
                //HelpMessageHandler.SaveHelpMessages();
            }
            else if(args[0] == "setup")
            {
                var perms = Context.Guild.GetUser(Context.Client.CurrentUser.Id).GuildPermissions;
                List<string> md = new List<string>();
                foreach (var m in GuildSettings.ModulesSettings)
                    md.Add($"{string.Join(' ', m.Key.Split(' ').Skip(1).Take(m.Key.Split(' ').Length - 2))}\nEnabled?: {m.Value}\n");
                Dictionary<string, string> prm = new Dictionary<string, string>();
                prm.Add("Admin:", $"{perms.Administrator}");
                prm.Add("Kick:", $"{perms.KickMembers}");
                prm.Add("Ban:", $"{perms.BanMembers}");
                prm.Add("Mentions:", $"{perms.MentionEveryone}");
                prm.Add("Manage Guild:", $"{perms.ManageGuild}");
                prm.Add("Channels:", $"{perms.ViewChannel}");
                prm.Add("Manage Roles:", $"{perms.ManageRoles}");
                int leng = prm.Keys.Max(x => x.Length);
                List<string> final = new List<string>();
                foreach (var itm in prm)
                    final.Add(itm.Key.PadRight(leng) + " " + itm.Value);
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Heres some tips to setup this bot",
                    Description = "You can join our [Discord](https://discord.gg/KDErQR) for more help setting up the bot in your server",
                    Fields = new List<EmbedFieldBuilder>()
                    {
                        new EmbedFieldBuilder()
                        {
                            IsInline = true,
                            Name = "Bot Permissions",
                            Value = $"Heres the bots current discord permissions:\n```{string.Join('\n', final)}```\nSome Modules like the moderation module need kick and ban permissions."
                        },
                        new EmbedFieldBuilder()
                        {
                            IsInline = true,
                            Name = "Modules",
                            Value = $"This is the current Module Settings with there status\n```{string.Join('\n', md)}```\nYou can Enable/Disable Modules with the {GuildSettings.Prefix}modules command."
                        },
                        new EmbedFieldBuilder()
                        {
                            IsInline = true,
                            Name = "Permission roles",
                            Value = $"These roles have elevated permissions and have access to all commands within the bot\n\n<@&{string.Join(">\n<@&", GuildSettings.PermissionRoles)}>\n\nTo add one use `{GuildSettings.Prefix}addpermission <@role>`\nTo remove one use `{GuildSettings.Prefix}removepermission <@role>`"
                        }
                    }
                }.Build());
            }
            else if(args.Length == 1)
            {
                var cmds = ReadCurrentCommands(GuildSettings.Prefix);
                var perm = HelpMessageHandler.CalcHelpPage(Context.User as SocketGuildUser, GuildSettings);
                if (cmds.Any(x => x.CommandName == args[0]))
                {
                    var cmd = cmds.Find(x => x.CommandName == args[0]);
                    if (perm == HelpMessageHandler.HelpPages.Public && cmd.RequiresPermission)
                    {
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "**You cant access this command!**",
                            Description = "You dont have permission to use this command, therefor were not gonna show you how.",
                            Color = Color.Red,
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = $"**{GuildSettings.Prefix}{cmd.CommandName}**",
                        Description = $"{cmd.CommandDescription}\n{cmd.CommandHelpMessage}",
                        Color = Color.Green
                    }.WithCurrentTimestamp().Build());
                    return;
                }
            }

        }
        [DiscordCommand("invite", description = "Provides an invite for this bot")]
        public async Task invite()
        {
            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
            {
                Title = "Heres my invite!",
                Description = "You can invite me [Here](https://discord.com/api/oauth2/authorize?client_id=722435272532426783&permissions=8&scope=bot)",
                Color = Color.Green
            }.WithCurrentTimestamp().Build());
        }
    }
}
