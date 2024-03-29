﻿using Discord;
using Discord.WebSocket;
using MongoDB.Bson.Serialization.Attributes;
using Public_Bot.Modules.Auto_Moderation;
using Public_Bot.Modules.Handlers;
using Public_Bot.State.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Public_Bot.Modules.Handlers.LevelHandler;

namespace Public_Bot
{
    [BsonIgnoreExtraElements]
    public class GuildSettings : IGuildIDEntity
    {
        public ulong GuildID { get; set; }
        public string Prefix { get; set; } = "*";
        public List<ulong> PermissionRoles { get; set; } = new List<ulong>();
        public Dictionary<string, bool> ModulesSettings { get; set; } = new Dictionary<string, bool>();
        public List<string> DisabledCommands { get; set; } = new List<string>();
        public ulong MutedRoleID { get; set; } = 0;
        public ulong LogChannel { get; set; } = 0;
        public ulong NewMemberRole { get; set; } = 0;
        public bool Logging { get; set; } = false;
        public HashSet<string> MemeSubreddits = new HashSet<string>() { "https://www.reddit.com/r/dankmemes" };
        public WelcomeCard WelcomeCard { get; set; }
        public LeaveMessage leaveMessage { get; set; }
        public List<ulong> BlacklistedChannels { get; set; } = new List<ulong>();
        public GuildSettings() { }

        public void SaveGuildSettings()
            => GuildSettingsHelper.SaveGuildSettings(this);

        public GuildSettings AddRole(SocketRole role)
        {
            if (!this.PermissionRoles.Contains(role.Id))
                this.PermissionRoles.Add(role.Id);
            else throw new Exception();
            return this;
        }
        public GuildSettings RemoveRole(SocketRole role)
        {
            if (this.PermissionRoles.Contains(role.Id))
                this.PermissionRoles.Remove(role.Id);
            else throw new Exception();
            return this;
        }
        public static GuildSettings Get(ulong id)
            => GuildSettingsHelper.GetGuildSettings(id);
        public GuildSettings(IGuild guild)
        {
            if (guild == null)
                return;
            this.GuildID = guild.Id;
            var curUser = guild.GetCurrentUserAsync().Result;
            if (curUser.GuildPermissions.Administrator)
                ModulesSettings.Add("🔨 Mod Commands 🔨", true);
            else
                if(curUser.GuildPermissions.KickMembers && curUser.GuildPermissions.BanMembers && curUser.GuildPermissions.ManageRoles && curUser.GuildPermissions.ManageMessages)
                    ModulesSettings.Add("🔨 Mod Commands 🔨", true);
                else
                    ModulesSettings.Add("🔨 Mod Commands 🔨", false);
            if (curUser.GuildPermissions.ManageRoles || curUser.GuildPermissions.Administrator)
                ModulesSettings.Add("🧪 Levels 🧪", true);
            else
                ModulesSettings.Add("🧪 Levels 🧪", false);

            foreach (var itm in CustomCommandService.Modules)
                    if (!ModulesSettings.ContainsKey(itm.Key))
                        ModulesSettings.Add(itm.Key, true);

            PermissionRoles.AddRange(guild.Roles.Where(x => x.Permissions.Administrator && !CommandHandler.IsBotRole(x)).Select(x => x.Id));

            if (!Guild.Exists(guild.Id))
            {
                _ = new Guild(guild);
                _ = new GuildLeaderboards(guild);
            }

            if (this == null)
                Console.WriteLine("UH OH: THIS IS NULL ");
            WelcomeCard = new WelcomeCard(this, guild);
            leaveMessage = new LeaveMessage(this,guild);
            GuildSettingsHelper.SaveGuildSettings(this);
        }
    }
}
