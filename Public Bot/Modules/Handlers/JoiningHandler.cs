using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Public_Bot.Modules.Handlers
{
    [DiscordHandler]
    class JoiningHandler
    {
        public static DiscordShardedClient client { get; set; }
        public JoiningHandler(DiscordShardedClient c)
        {
            client = c;

            client.JoinedGuild += Client_JoinedGuild;
        }

        private async Task Client_JoinedGuild(SocketGuild arg)
        {
            GuildSettings s = new GuildSettings(arg);
            var perms = arg.CurrentUser.GuildPermissions;
            List<string> md = new List<string>();
            foreach (var m in s.ModulesSettings)
                md.Add($"{string.Join(' ', m.Key.Split(' ').Skip(1).Take(m.Key.Split(' ').Length - 2))}\nEnabled?: {m.Value}\n");
            Dictionary<string, string> prm = new Dictionary<string, string>();
            prm.Add("Admin:", $"{(perms.Administrator ? "✅" : "❌")}");
            prm.Add("Kick:", $"{(perms.KickMembers ? "✅" : "❌")}");
            prm.Add("Ban:", $"{(perms.BanMembers ? "✅" : "❌")}");
            prm.Add("Mentions:", $"{(perms.MentionEveryone ? "✅" : "❌")}");
            prm.Add("Manage Guild:", $"{(perms.ManageGuild ? "✅" : "❌")}");
            prm.Add("Channels:", $"{(perms.ViewChannel ? "✅" : "❌")}");
            prm.Add("Manage Roles:", $"{(perms.ManageRoles ? "✅" : "❌")}");
            int leng = prm.Keys.Max(x => x.Length);
            List<string> final = new List<string>();
            foreach (var itm in prm)
                final.Add(itm.Key.PadRight(leng) + " " + itm.Value);

            await arg.DefaultChannel.SendMessageAsync("", false, new EmbedBuilder()
            {
                Title = $"Thanks for adding {client.CurrentUser.Username}!",
                Description = $"We recommend you look at the `{s.Prefix}help setup` command for help setting up this bot. This bot has Setup itself with these settings. if you need further assistance join our [Discord!](https://discord.gg/w8EcwBy)",
                Color = Color.Green,
                Timestamp = DateTime.Now,
                //ThumbnailUrl = client.CurrentUser.GetAvatarUrl(ImageFormat.WebP),
                Fields = new List<EmbedFieldBuilder>()
                    {
                        new EmbedFieldBuilder()
                        {
                            IsInline = true,
                            Name = "Bot Permissions",
                            Value = $"> Here's the bots current discord permissions:\n```{string.Join('\n', final)}```\n> Some Modules like the moderation module need kick and ban permissions."
                        },
                        new EmbedFieldBuilder()
                        {
                            IsInline = true,
                            Name = "Modules",
                            Value = $"> This is the current Module Settings with there status\n```{string.Join('\n', md)}```\n> You can Enable/Disable Modules with the {s.Prefix}modules command."
                        },
                        s.PermissionRoles.Count > 0 ?
                        new EmbedFieldBuilder()
                        {
                            IsInline = true,
                            Name = "Permission roles",
                            Value = $">>> These roles have elevated permissions and have access to all commands within the bot\n\n<@&{string.Join(">\n<@&", s.PermissionRoles)}>\n\nTo add one use `{s.Prefix}addpermission <@role>`\nTo remove one use `{s.Prefix}removepermission <@role>`"
                        }
                        :
                        new EmbedFieldBuilder()
                        {
                            IsInline = true,
                            Name = "Permission roles",
                            Value = $">>> Please set up some roles with permissions because right now only the guilds owner can setup Luminous.\nTo add one use `{s.Prefix}addpermission <@role>`\nTo remove one use `{s.Prefix}removepermission <@role>`"
                        }
                    },
                Footer = new EmbedFooterBuilder()
                {
                    Text = "Luminous: Made by quin#3017"
                }
            }.Build());
        }
    }
}
