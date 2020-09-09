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
    public class Unlock : CommandModuleBase
    {
        [GuildPermissions(GuildPermission.ManageChannels)]
        [DiscordCommand("unlock", commandHelp = "`(PREFIX)unlock #channel`", description = "Unlocks the mentioned channel", RequiredPermission = true)]
        public async Task unlock(params string[] args)
        {
            SocketGuildChannel lockchnl;
            if (!Context.Message.MentionedChannels.Any())
            {
                //Assuming they want to lock the current channel.
                lockchnl = Context.Channel as SocketGuildChannel;
            }
            else
            {
                lockchnl = Context.Message.MentionedChannels.First();
            }
            var lockMSGchnl = lockchnl as SocketTextChannel;
            EmbedBuilder alfa = new EmbedBuilder();
            try
            {
                if (lockMSGchnl == null)
                {
                    var lockVOICE = lockchnl as SocketVoiceChannel;
                    var xyz = new OverwritePermissions(connect: PermValue.Inherit);
                    await lockVOICE.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, xyz);
                    alfa.Title = $"Unlocked Voice Channel {lockVOICE.Name}";
                    alfa.Description = "The aforementioned voice channel has been unlocked.";
                    alfa.WithCurrentTimestamp();
                }
                else
                {
                    var sry = new OverwritePermissions(sendMessages: PermValue.Inherit);
                    await lockchnl.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, sry);
                    alfa.Title = $"Unlocked Text Channel {lockMSGchnl.Name}";
                    alfa.Description = $"{lockMSGchnl.Mention} has been unlocked";
                    alfa.WithCurrentTimestamp();
                }
                await Context.Channel.SendMessageAsync("", false, alfa.Build());
            }
            catch
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
                {
                    Title = "Missing Permissions",
                    Description = "I do not have perms :sob:"
                }.WithCurrentTimestamp().Build());
            }
        }
    }
}