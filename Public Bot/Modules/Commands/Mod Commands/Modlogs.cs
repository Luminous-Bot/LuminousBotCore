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
    public class Modlogs : CommandModuleBase
    {
        [DiscordCommand("modlogs", RequiredPermission = true, description = "View a users modlogs", commandHelp = "Usage `(PREFIX)modlogs <@user>`")]
        public async Task modlogs(params string[] args)
        {
            if (args.Length == 0)
            {
                await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                {
                    Title = "**Who's logs do you want to view?**",
                    Description = "Please provide a user",
                    Color = Color.Red,
                    Timestamp = DateTime.Now
                }.Build());
                return;
            }
            var user = GetUser(args[0]);
            if (user == null)
            {
                await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                {
                    Title = "**Who's that?**",
                    Description = "Please provide a valid user",
                    Color = Color.Red,
                    Timestamp = DateTime.Now
                }.Build());
                return;
            }
            var guild = GuildHandler.GetGuild(Context.Guild.Id);
            var gm = guild.GuildMembers.GetGuildMember(user.Id);
            if (gm.Infractions.Count > 0)
            {
                var userlog = gm;
                var pg = ModlogsPageHandler.BuildHelpPage(userlog.Infractions, 0, user.Id, Context.Guild.Id, Context.User.Id);
                var emb = ModlogsPageHandler.BuildHelpPageEmbed(pg, 1);
                var msg = await Context.Channel.SendMessageAsync("", false, emb.Build());
                pg.MessageID = msg.Id;
                ModlogsPageHandler.CurrentPages.Add(pg);
                ModlogsPageHandler.SaveMLPages();
                await msg.AddReactionsAsync(new IEmote[] { new Emoji("\U00002B05"), new Emoji("\U000027A1") });
            }
            else
            {
                await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                {
                    Title = $"Modlogs for **{user}**",
                    Description = "This user has no logs! :D",
                    Color = Color.Green,
                    Timestamp = DateTime.Now
                }.Build());
                return;
            }
        }
    }
}