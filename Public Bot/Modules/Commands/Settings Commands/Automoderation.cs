using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
namespace Public_Bot.Modules.Commands.Settings_Commands
{
    [DiscordCommandClass("⚙️ Settings ⚙️", "Change how this bot works in your server!")]
    public class Automoderation : CommandModuleBase
    {
        //[DiscordCommand("automoderation",
        //    description = "Gives the AutoMod settings!",
        //    commandHelp = "`(PREFIX)automod`\n" +
        //    "`(PREFIX)automod enable/disable`\n" +
        //    "`(PREFIX)automod admins enable/disable` \n" +
        //    "`(PREFIX)automod bots enable/disable` \n" +
        //    "`(PREFIX)automod maxchars <maximum_characters_allowed_as_not_spam>`\n" +
        //    "`(PREFIX)automod dm enable/disable`\n"

        //    )]
        //public async Task AutoModSet(params string[] args)
        //{
        //    if (args.Length == 0)
        //    {
        //        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
        //        {
        //            Title = "Auto-Moderation Settings",
        //            Color = Blurple
        //        }
        //        .AddField("Enabled?",GuildSettings.autoMod.Enabled)
        //        .AddField("Moderate admins?",GuildSettings.autoMod.ApplyOnAdmins)
        //        .AddField("Moderate Bots?",GuildSettings.autoMod.ApplyOnBots)
        //        .AddField("DMs user?",GuildSettings.autoMod.DMUser)
        //        .AddField("Anti-Spam",
        //        $"Maximum Characters: {GuildSettings.autoMod.Antispam.MaxChars}\n"
        //        )
        //        .AddField("Anti Mass-Caps Spam",
        //        $"Enabled?: {GuildSettings.autoMod.AntiMCS.Enabled}\n" +
        //        $"Percentage: {GuildSettings.autoMod.AntiMCS.Percentage}\n"
        //        )
        //        .WithCurrentTimestamp()
        //        .Build());
        //    } else
        //    {
        //        GuildSettings.autoMod.Enabled =
        //            args[0] switch
        //            {
        //                //The Enabler/Disablers
        //                "on" => true,
        //                "enable" => true,
        //                "true" => true,
        //                "off" => false,
        //                "disable" => false,
        //                "false" => false,
        //                _ => GuildSettings.autoMod.Enabled
        //            };
        //        if (args.Length > 1)
        //        {
        //            if (args[0].ToLower().Contains("caps"))
        //            {
        //                GuildSettings.autoMod.AntiMCS.Enabled =
        //                    args[1] switch
        //                    {
        //                        "on" => true,
        //                        "enable" => true,
        //                        "true" => true,
        //                        "off" => false,
        //                        "disable" => false,
        //                        "false" => false,
        //                        _ => GuildSettings.autoMod.AntiMCS.Enabled
        //                    };
        //                if (ushort.TryParse(args[1], out ushort x12))
        //                {
        //                    GuildSettings.autoMod.AntiMCS.Percentage = x12;
        //                }
        //            }
        //            if (args[0].ToLower().Contains("maxchar"))
        //            {
        //                if (uint.TryParse(args[1], out uint res))
        //                {
        //                    GuildSettings.autoMod.Antispam.MaxChars = res;
        //                }
        //            }
        //            if (args[0].ToLower().Contains("dm"))
        //            {
        //                GuildSettings.autoMod.SetDMUser(args[1] switch
        //                    {
        //                        "on" => true,
        //                        "enable" => true,
        //                        "true" => true,
        //                        "off" => false,
        //                        "disable" => false,
        //                        "false" => false,
        //                        _ => GuildSettings.autoMod.DMUser
        //                    });
        //            }
        //            if (args[0].ToLower().Contains("admin"))
        //            {
        //                GuildSettings.autoMod.ApplyOnAdmins =
        //                    args[1] switch
        //                    {
        //                        "on" => true,
        //                        "enable" => true,
        //                        "true" => true,
        //                        "off" => false,
        //                        "disable" => false,
        //                        "false" => false,
        //                        _ => GuildSettings.autoMod.ApplyOnAdmins
        //                    };
        //            }
        //            if (args[0].ToLower().Contains("bots"))
        //            {
        //                GuildSettings.autoMod.ApplyOnBots =
        //                    args[1] switch
        //                    {
        //                        "on" => true,
        //                        "enable" => true,
        //                        "true" => true,
        //                        "off" => false,
        //                        "disable" => false,
        //                        "false" => false,
        //                        _ => GuildSettings.autoMod.ApplyOnBots
        //                    };
        //            }
        //        }
        //        GuildSettings.SaveGuildSettings();
        //        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder
        //        {
        //            Title = "UPDATED Auto-Moderation Settings",
        //            Color = Color.Red
        //        }
        //        .AddField("Enabled?", GuildSettings.autoMod.Enabled)
        //        .AddField("Moderate admins?", GuildSettings.autoMod.ApplyOnAdmins)
        //        .AddField("Moderate Bots?", GuildSettings.autoMod.ApplyOnBots)
        //        .AddField("DMs user?",GuildSettings.autoMod.DMUser)
        //        .AddField("Anti-Spam",
        //        $"Maximum Characters: {GuildSettings.autoMod.Antispam.MaxChars}\n"
        //        )
        //        .AddField("Anti Mass-Caps Spam",
        //        $"Enabled?: {GuildSettings.autoMod.AntiMCS.Enabled}\n" +
        //        $"Percentage: {GuildSettings.autoMod.AntiMCS.Percentage}\n"

        //        )
        //        .WithCurrentTimestamp()
        //        .Build());
        //    }
        //}
        
    }
}