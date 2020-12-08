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
    public class Unmute : CommandModuleBase
    {
        [DiscordCommand("unmute", RequiredPermission = true, description = "Unmutes a muted user", commandHelp = "Usage - `(PREFIX)unmute <@user>`")]
        public async Task unmute(params string[] args)
        {
            if (args.Length == 0)
            {
                await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                {
                    Title = "Who..?",
                    Description = "Who do you want me to unmute",
                    Color = Color.Red,
                    Timestamp = DateTime.Now
                }.Build());
                return;
            }
            var user = await GetUser(args[0]);
            if (user == null)
            {
                await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                {
                    Title = "**Who..?**",
                    Description = "That user is invalid!",
                    Color = Color.Red,
                    Timestamp = DateTime.Now
                }.Build());
                return;
            }
            if (user.Roles.Any(x => x.Id == GuildSettings.MutedRoleID))
            {
                var role = Context.Guild.GetRole(GuildSettings.MutedRoleID);
                try
                {
                    await user.RemoveRoleAsync(role);
                    MuteHandler.CurrentMuted.Remove(MuteHandler.CurrentMuted.Find(x => x.UserID == user.Id));
                    MuteHandler.SaveMuted();
                    Embed b2 = new EmbedBuilder()
                    {
                        Title = $"**Successfully Unmuted user {user.ToString()}**",
                        Fields = new List<EmbedFieldBuilder>()
                            {
                                { new EmbedFieldBuilder(){
                                    Name = "Moderator",
                                    Value = Context.Message.Author.ToString(),
                                    IsInline = true
                                } }
                            },
                        Timestamp = DateTime.Now,
                        Color = Color.Green
                    }.Build();
                    await Context.Channel.SendMessageAsync("", false, b2);
                }
                catch (Exception ex)
                {
                    await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                    {
                        Title = "**There was an Error**",
                        Description = $"Looks like we faild trying to remove the muted role, Here's the error message: {ex.Message}",
                        Color = Color.Red,
                        Timestamp = DateTime.Now
                    }.Build());
                    return;
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                {
                    Title = "**That user is not Muted**",
                    Description = "They are not muted lol. dont know what else you want me to say",
                    Color = Color.Red,
                    Timestamp = DateTime.Now
                }.Build());
                return;
            }

        }
    }
}