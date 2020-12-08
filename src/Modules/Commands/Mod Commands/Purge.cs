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
    public class Purge : CommandModuleBase
    {
        [GuildPermissions(GuildPermission.ManageMessages)]
        [DiscordCommand("purge", 
            RequiredPermission = true, 
            commandHelp = "`(PREFIX)purge <amount>\n(PREFIX)purge <@user> <amount>",
            description = "Deletes X amount of messages"
            )]
        public async Task purge(params string[] args)
        {
            if(args.Length == 1)
            {
                uint ammount = 0;

                if (uint.TryParse(args[0], out var res))
                    ammount = res;
                else
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Invalid Ammount",
                        Color = Color.Red,
                        Description = "The ammount of messages you entered was not a number or far exceeds our parseable range",
                    }.WithCurrentTimestamp().Build());
                    return;
                }

                var tmp = await Context.Channel.GetMessagesAsync((int)ammount).FlattenAsync();
                var msgs = tmp.Where(x => (DateTime.UtcNow - x.Timestamp.UtcDateTime).TotalDays < 14);
                await ((ITextChannel)Context.Channel).DeleteMessagesAsync(msgs);
                const int delay = 3000;
                try
                {
                    var m = await Context.Channel.SendMessageAsync($"Purge completed!{(tmp.Count() != msgs.Count() ? " (Some messages couldn't be deleted because there older than two weeks!)" : "")}");
                    await Task.Delay(delay);
                    await m.DeleteAsync();
                }
                catch(AggregateException x)
                {
                    await Context.User.SendMessageAsync($"Purge completed!{(tmp.Count() != msgs.Count() ? " (Some messages couldn't be deleted because there older than two weeks!)" : "")} I've dm'd you the results of the purge because Luminous doesn't have permission to send messages in {Context.Channel.Name}!");
                }
            }
            else if(args.Length == 2)
            {
                string usr = args[0];
                uint ammount = 0;

                if (uint.TryParse(args[1], out var res))
                    ammount = res + 1;
                else
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Invalid Ammount",
                        Color = Color.Red,
                        Description = "The ammount of messages you entered was not a number or far exceeds our parseable range",
                    }.WithCurrentTimestamp().Build());
                    return;
                }
                
                var user = await GetUser(usr);
                if (user == null)
                {
                    await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                    {
                        Title = "Invalid ID",
                        Description = "The user is not in the server or the ID is invalid!",
                        Color = Color.Red
                    }.Build());
                    return;
                }
                var tmp = await Context.Channel.GetMessagesAsync(100).FlattenAsync();
                if (!tmp.Any(x => x.Author.Id == user.Id))
                {
                    await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                    {
                        Title = "Unable to find messages",
                        Description = $"we cant find any messages from <@{user.Id}>!",
                        Color = Color.Red
                    }.Build());
                    return;
                }
                var messages = tmp.Where(x => x.Author.Id == user.Id).Take((int)ammount);
                messages = messages.Where(x => (DateTime.UtcNow - x.CreatedAt.UtcDateTime).TotalDays < 14);
                await ((ITextChannel)Context.Channel).DeleteMessagesAsync(messages);
                const int delay = 2000;
                try
                {
                    var m = await Context.Channel.SendMessageAsync($"Purge completed!");
                    await Task.Delay(delay);
                    await m.DeleteAsync();
                }
                catch (AggregateException x)
                {
                    await Context.User.SendMessageAsync($"Purge completed! I've dm'd you the results of the purge because Luminous doesn't have permission to send messages in {Context.Channel.Name}!");
                }
            }
        }
    }
}