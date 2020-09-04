using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Public_Bot.Modules.Handlers.MuteHandler;
namespace Public_Bot.Modules.Commands.Mod_Commands
{
    [DiscordCommandClass("🔨 Mod Commands 🔨", "Make your staff team more efficient with this module, you can keep track of user infractions and keep your server in order!")]
    public class Clearlogs : CommandModuleBase
    {
        [DiscordCommand("clearlogs", description = "Clears a users logs", commandHelp = "Usage - `(PREFIX)clearlogs <@user>`, `(PREFIX)clearlogs <@user> <log_number>`", RequiredPermission = true)]
        public async Task ClearLogs(params string[] args)
        {
            if (args.Length == 0)
            {
                await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                {
                    Title = "Who's log do you want to clear?",
                    Description = $"You didnt provide any arguments, if your stuck do `{GuildSettings.Prefix}help clearlogs`",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            var usr = GetUser(args[0]);
            if (usr == null)
            {
                await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                {
                    Title = $"Who?",
                    Description = $"I couldn't find a user with the name or id of \"{args[0]}\"!",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }

            var guild = GuildHandler.GetGuild(Context.Guild.Id);
            var gm = guild.GuildMembers.GetGuildMember(usr.Id);
            if (gm.Infractions.Count > 0)
            {
                var usrlogs = gm;
                if (args.Length == 1)
                {
                    await InvokeCommand("modlogs", args);
                }
                if (args.Length == 2)
                {
                    if (args[1] == "all" || args[1] == "clear")
                    {
                        var bucket = new MutationBucket<Infraction>("deleteInfraction");
                        usrlogs.Infractions.ForEach(x => bucket.Add(x, ("id", $"\\\"{x.Id}\\\"")));
                        await StateService.MutateAsync<dynamic>(bucket.Build());
                        usrlogs.Infractions.Clear();
                        //deleteLogs
                        await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                        {
                            Title = $"Cleared {usrlogs.Username}'s Logs!",
                            Description = $"Cleared all of <@{usrlogs.Id}>'s infractions!",
                            Color = Color.Green,
                            Timestamp = DateTime.Now
                        }.Build());
                        return;
                    }
                    else
                    {
                        if (uint.TryParse(args[1], out var res))
                        {
                            if (usrlogs.Infractions.Count < res || res == 0)
                            {
                                await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                                {
                                    Title = $"Invalid number",
                                    Description = $"\"{args[1]}\" is an invalid number! Make sure the user has a log number of {args[1]}",
                                    Color = Color.Red
                                }.WithCurrentTimestamp().Build());
                                return;
                            }
                            await StateService.MutateAsync<dynamic>(GraphQLParser.GenerateGQLMutation<Infraction>("deleteInfraction", false, usrlogs.Infractions[(int)res - 1], "", "", ("id", usrlogs.Infractions[(int)res - 1].Id)));
                            usrlogs.Infractions.RemoveAt((int)res - 1);
                            if (usrlogs.Infractions.Count == 0)
                            {
                                await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                                {
                                    Title = $"Cleared {usrlogs.Username}'s Logs!",
                                    Description = $"Cleared all of <@{usrlogs.Id}>'s infractions!",
                                    Color = Color.Green,
                                    Timestamp = DateTime.Now
                                }.Build());
                                return;
                            }
                            var pg = ModlogsPageHandler.BuildHelpPage(usrlogs.Infractions, 0, usrlogs.Id, Context.Guild.Id, Context.User.Id);
                            var emb = ModlogsPageHandler.BuildHelpPageEmbed(pg, 1);
                            var msg = await Context.Channel.SendMessageAsync($"Removed log number {args[1]}", false, emb.Build());
                            pg.MessageID = msg.Id;
                            ModlogsPageHandler.CurrentPages.Add(pg);
                            ModlogsPageHandler.SaveMLPages();
                            await msg.AddReactionsAsync(new IEmote[] { new Emoji("\U00002B05"), new Emoji("\U000027A1") });
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                            {
                                Title = $"Invalid number",
                                Description = $"\"{args[1]}\" is an invalid number!",
                                Color = Color.Red
                            }.WithCurrentTimestamp().Build());
                            return;
                        }
                    }

                }
                else if (args.Length > 2)
                {
                    var bucket = new MutationBucket<Infraction>("deleteInfraction");
                    List<Infraction> removed = new List<Infraction>();
                    foreach (var item in args.Skip(1))
                    {
                        if (uint.TryParse(args[1], out var res))
                        {
                            if (usrlogs.Infractions.Count < res || res == 0)
                            {
                                await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                                {
                                    Title = $"Invalid number",
                                    Description = $"\"{args[1]}\" is an invalid number! Make sure the user has a log number of {args[1]}",
                                    Color = Color.Red
                                }.WithCurrentTimestamp().Build());
                                return;
                            }
                            var infrac = usrlogs.Infractions[(int)res - 1];
                            bucket.Add(infrac, ("id", $"\\\"{infrac.Id}\\\""));
                            removed.Add(infrac);
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                            {
                                Title = $"Invalid number",
                                Description = $"\"{args[1]}\" is an invalid number!",
                                Color = Color.Red
                            }.WithCurrentTimestamp().Build());
                            return;
                        }
                    }
                    await StateService.MutateAsync<dynamic>(bucket.Build());
                    removed.ForEach(x => usrlogs.Infractions.Remove(x));
                    removed.Clear();

                    if (usrlogs.Infractions.Count == 0)
                    {
                        await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                        {
                            Title = $"Cleared {usrlogs.Username}'s Logs!",
                            Description = $"Cleared all of <@{usrlogs.Id}>'s infractions!",
                            Color = Color.Green,
                            Timestamp = DateTime.Now
                        }.Build());
                        return;
                    }
                    var pg = ModlogsPageHandler.BuildHelpPage(usrlogs.Infractions, 0, usrlogs.Id, Context.Guild.Id, Context.User.Id);
                    var emb = ModlogsPageHandler.BuildHelpPageEmbed(pg, 1);
                    var msg = await Context.Channel.SendMessageAsync($"Removed log numbers {string.Join(", ", args.Skip(1))}", false, emb.Build());
                    pg.MessageID = msg.Id;
                    ModlogsPageHandler.CurrentPages.Add(pg);
                    ModlogsPageHandler.SaveMLPages();
                    await msg.AddReactionsAsync(new IEmote[] { new Emoji("\U00002B05"), new Emoji("\U000027A1") });
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                {
                    Title = $"Modlogs for **{usr}**",
                    Description = "This user has no logs! :D",
                    Color = Color.Green,
                    Timestamp = DateTime.Now
                }.Build());
                return;
            }
        }
    }
}