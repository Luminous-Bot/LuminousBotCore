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
        [DiscordCommand("purge", RequiredPermission = true)]
        public async Task purge(string usr, uint ammount)
        {

            var user = GetUser(usr);
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
            var m = await Context.Channel.SendMessageAsync($"Purge completed!");
            await Task.Delay(delay);
            await m.DeleteAsync();

        }
    }
}