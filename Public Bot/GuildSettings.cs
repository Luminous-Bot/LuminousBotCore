using Discord;
using Discord.WebSocket;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Public_Bot.Modules.Handlers.LevelHandler;

namespace Public_Bot
{
    public class GuildSettings
    {
        public ulong GuildID { get; set; }
        public string Prefix { get; set; } = "*";
        public List<ulong> PermissionRoles { get; set; } = new List<ulong>();
        public Dictionary<string, bool> ModulesSettings { get; set; } = new Dictionary<string, bool>();
        public ulong MutedRoleID { get; set; } = 0;
        public bool Leveling { get; set; } = true;
        public GuildSettings() { }

        public static void SaveGuildSettings()
        {
            StateHandler.SaveObject<List<GuildSettings>>("guildsettings", CommandHandler.CurrentGuildSettings);
        }
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
        public GuildSettings(IGuild guild)
        {
            if (guild == null)
                return;
            this.GuildID = guild.Id;
            var curUser = guild.GetCurrentUserAsync().Result;
            if (curUser.GuildPermissions.Administrator)
                ModulesSettings.Add("🔨 Mod Commands 🔨", true);
            else
                ModulesSettings.Add("🔨 Mod Commands 🔨", false);
            foreach (var itm in CustomCommandService.Modules)
                if(!ModulesSettings.ContainsKey(itm.Key))
                    ModulesSettings.Add(itm.Key, true);
            PermissionRoles.AddRange(guild.Roles.Where(x => x.Permissions.Administrator).Select(x => x.Id));

            if (Leveling)
                new GuildLeaderboards(guild);

            CommandHandler.CurrentGuildSettings.Add(this);
            StateHandler.SaveObject<List<GuildSettings>>("guildsettings", CommandHandler.CurrentGuildSettings);
        }
    }
}
