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
    public class Mute : CommandModuleBase
    {
        [GuildPermissions(GuildPermission.ManageRoles)]
        [DiscordCommand("mute", RequiredPermission = true, description = "Mutes a user", commandHelp = "Usage - `(PREFIX)mute <@user> <timespan> <reason>`\nTimespans:\n`10m` - Ten minutes\n`1h` - One hour\n`45s` - Forty five seconds\n`2d` - Two days\n`1y` - One year (dont recommend)")]
        public async Task mute(params string[] args)
        {
            if (GuildSettings.MutedRoleID == 0)
            {
                await Context.Channel.SendMessageAsync($"This command requires a \"Muted Role\", To set one up do `{GuildSettings.Prefix}createmutedrole`");
                return;
            }

            if (args.Length == 0)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "**Who? How long? and Why?**",
                    Description = "Please provide a user, time, and reason",
                    Timestamp = DateTime.Now,
                    Color = Color.Orange
                }.Build());
                return;
            }
            if (args.Length == 1)
            {
                await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                {
                    Title = "Give me a time!",
                    Description = $"if you wanted to mute for 10 minutes use `{GuildSettings.Prefix}mute <user> 10m <reason>`",
                    Color = Color.Orange,
                    Timestamp = DateTime.Now
                }.Build());
                return;
            }
            if (args.Length == 2)
            {
                await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                {
                    Title = "Give me a Reason!",
                    Description = $"You need to provide a reason",
                    Color = Color.Orange,
                    Timestamp = DateTime.Now
                }.Build());
                return;
            }
            var user = GetUser(args[0]);
            if (user == null)
            {
                await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                {
                    Title = "Who?",
                    Description = $"That user is invalid!",
                    Color = Color.Red,
                    Timestamp = DateTime.Now
                }.Build());
                return;
            }
            if (user.Roles.Any(x => x.Id == GuildSettings.MutedRoleID))
            {
                await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                {
                    Title = "That user is already muted!",
                    Description = "We cant mute someone whos already muted :/",
                    Color = Color.Red,
                    Timestamp = DateTime.Now
                }.Build());
                return;
            }
            var regex = new Regex(@"^(\d*)([a-z])$");
            var datetime = DateTime.UtcNow;
            //TimeSpan s = new TimeSpan();
            if (regex.IsMatch(args[1].ToLower()))
            {
                var r = regex.Match(args[1].ToLower());
                switch (r.Groups[2].Value)
                {
                    case "s":
                        datetime = datetime.AddSeconds(double.Parse(r.Groups[1].Value));
                        break;
                    case "m":
                        datetime = datetime.AddMinutes(double.Parse(r.Groups[1].Value));
                        break;
                    case "h":
                        datetime = datetime.AddHours(double.Parse(r.Groups[1].Value));
                        break;
                    case "d":
                        datetime = datetime.AddDays(double.Parse(r.Groups[1].Value));
                        break;
                    case "y":
                        datetime = datetime.AddYears(int.Parse(r.Groups[1].Value));
                        break;
                    default:
                        await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                        {
                            Title = "**How long?**",
                            Description = $"Please provide a valid time span, here are some examples\n" +
                            $"`10m` - Ten minutes\n" +
                            $"`1h` - One hour\n" +
                            $"`45s` - Forty five seconds\n" +
                            $"`2d` - Two days\n" +
                            $"`1y` - One year (dont recommend)",
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
                    Title = "**How long?**",
                    Description = $"Please provide a valid time span, here are some examples\n" +
                    $"`10m` - Ten minutes\n" +
                    $"`1h` - One hour\n" +
                    $"`45s` - Forty five seconds\n" +
                    $"`2d` - Two days\n" +
                    $"`1y` - One year (dont recommend)",
                    Color = Color.Red,
                    Timestamp = DateTime.Now
                }.Build());
                return;
            }
            if ((datetime - DateTime.UtcNow).TotalMilliseconds > int.MaxValue)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Error",
                    Description = $"The time you entered is wayy too long, The max time is {TimeSpan.FromMilliseconds(int.MaxValue).ToString("dd'd 'hh'h 'mm'm'")}",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            string reason = string.Join(' ', args.Skip(2));
            try
            {
                await user.AddRoleAsync(Context.Guild.GetRole(GuildSettings.MutedRoleID));
            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync("", false, new Discord.EmbedBuilder()
                {
                    Title = "**Welp, that didn't work!**",
                    Description = $"We couldn't add the muted role to {user}, Here's the reason: {ex.Message}",
                    Color = Color.Red,
                    Timestamp = DateTime.Now
                }.Build());
                return;
            }

            Embed b = new EmbedBuilder()
            {
                Title = $"**You have been Muted on {Context.Guild.Name}**",
                Fields = new List<EmbedFieldBuilder>()
                    {
                        { new EmbedFieldBuilder(){
                            Name = "Moderator",
                            Value = Context.Message.Author.ToString(),
                            IsInline = true
                        } },
                        {new EmbedFieldBuilder()
                        {
                            Name = "Reason",
                            Value = reason,
                            IsInline = true
                        } }
                    },
                Timestamp = DateTime.Now,
                Color = Color.Orange
            }.Build();
            bool dmed = true;
            try
            {
                await user.SendMessageAsync("", false, b);
            }
            catch
            { dmed = false; }
            Embed b2 = new EmbedBuilder()
            {
                Title = $"Successfully **Muted** user **{user}**",
                Fields = new List<EmbedFieldBuilder>()
                    {
                        { new EmbedFieldBuilder(){
                            Name = "Moderator",
                            Value = Context.Message.Author.ToString(),
                            IsInline = true
                        } },
                        {new EmbedFieldBuilder()
                        {
                            Name = "Reason",
                            Value = reason,
                            IsInline = true
                        } },
                        {new EmbedFieldBuilder()
                        {
                            Name = "Notified in DM?",
                            Value = dmed,
                            IsInline = true
                        } }
                    },
                Timestamp = DateTime.Now,
                Color = Color.Green
            }.Build();
            await Context.Channel.SendMessageAsync("", false, b2);
            Infraction infrac = new Infraction(user.Id, Context.User.Id, Context.Guild.Id, Action.Muted, reason, DateTime.UtcNow);
            Handlers.MuteHandler.AddNewMuted(user.Id, datetime, GuildSettings);
        }
    }
}