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
    public class Whois : CommandModuleBase
    {
        [DiscordCommand("whois", description = "Shows information about the mentioned user", commandHelp = "Usage: `(PREFIX)whois <@user>`")]
        public async Task WhoIs(params string[] user)
        {
            SocketGuildUser userAccount;
            if (user.Length == 0)
                userAccount = Context.User as SocketGuildUser;
            else userAccount = GetUser(user[0]);

            if (userAccount == null)
            {
                EmbedBuilder error = new EmbedBuilder()
                {
                    Title = "That user is invalid ¯\\_(ツ)_/¯",
                    Description = "Please provide a valid user",
                    Color = Color.Red
                };
                await Context.Channel.SendMessageAsync("", false, error.Build());
                return;
            }
            string perms = "```\n";
            string permsRight = "";
            var props = typeof(Discord.GuildPermissions).GetProperties();
            var boolProps = props.Where(x => x.PropertyType == typeof(bool));
            var pTypes = boolProps.Where(x => (bool)x.GetValue(userAccount.GuildPermissions) == true).ToList();
            var nTypes = boolProps.Where(x => (bool)x.GetValue(userAccount.GuildPermissions) == false).ToList();
            var pd = boolProps.Max(x => x.Name.Length) + 1;
            if(nTypes.Count == 0)
                perms += "Administrator: ✅```";
            else
            {
                foreach (var perm in pTypes)
                    perms += $"{perm.Name}:".PadRight(pd) + " ✅\n";
                perms += "```";
                permsRight = "```\n";
                foreach (var nperm in nTypes)
                    permsRight += $"{nperm.Name}:".PadRight(pd) + " ❌\n";
                permsRight += "```";
            }
            var orderedroles = userAccount.Roles.OrderBy(x => x.Position * -1).ToArray();
            string roles = "";
            for(int i = 0; i < orderedroles.Count(); i++)
            {
                var role = orderedroles[i];
                if (roles.Length + role.Mention.Length < 1024)
                    roles += role.Mention + "\n";
                else
                {
                    roles += $"+ {orderedroles.Length - i + 1} more";
                    break;
                }
            }
            string stats = $"Nickname?: {(userAccount.Nickname == null ? "None" : userAccount.Nickname)}\n" +
                              $"Id: {userAccount.Id}\n" +
                              $"Creation Date: {userAccount.CreatedAt.UtcDateTime.ToString("r")}\n" +
                              $"Joined At: {userAccount.JoinedAt.Value.UtcDateTime.ToString("r")}\n";

            EmbedBuilder whois = new EmbedBuilder()
            {
                Author = new EmbedAuthorBuilder()
                {
                    Name = userAccount.ToString(),
                    IconUrl = userAccount.GetAvatarUrl()
                },
                Color = Blurple,
                Description = permsRight == "" ? "**Stats**\n" + stats : "",
                Fields = permsRight == "" ? new List<EmbedFieldBuilder>()
                {
                    new EmbedFieldBuilder()
                    {
                        Name = "Roles",
                        Value = roles,
                    },
                    new EmbedFieldBuilder()
                    {
                        Name = "Permissions ✅",
                        Value = perms,
                        IsInline = true
                    }
                } : new List<EmbedFieldBuilder>()
                {
                    new EmbedFieldBuilder()
                    { 
                        Name = "Stats",
                        Value = stats,
                        IsInline = true,

                    },
                    new EmbedFieldBuilder()
                    {
                        Name = "Roles",
                        Value = roles,
                        IsInline = false,

                    },
                    new EmbedFieldBuilder()
                    {
                        Name = "Permissions ✅",
                        Value = perms,
                        IsInline = true,
                    }
                }
            }.WithCurrentTimestamp();
            if (permsRight != "")
                whois.Fields.Add(new EmbedFieldBuilder()
                {
                    Name = "Permissions ❌",
                    Value = permsRight,
                    IsInline = true
                });
            await Context.Channel.SendMessageAsync("", false, whois.Build());
        }
    }
}