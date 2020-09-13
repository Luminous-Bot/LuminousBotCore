using Discord;
using System.Threading.Tasks;

namespace Public_Bot.Modules.Commands.Settings_Commands
{
    [DiscordCommandClass("🎚 Utilities 🎚", "Enable some vital features to improve your overall fuctionality!")]
    public class Joinrole : CommandModuleBase
    {
        [GuildPermissions(GuildPermission.ManageRoles)]
        [DiscordCommand("joinrole", description = "Gives new users a role", commandHelp = "Usage - `(PREFIX)joinrole <role>", RequiredPermission = true)]
        public async Task joinrole(string r)
        {
            if(r == "remove")
            {
                GuildSettings.NewMemberRole = 0;
                GuildSettings.SaveGuildSettings();
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Success!",
                    Description = $"Removed the join role",
                    Color = Color.Green
                }.WithCurrentTimestamp().Build());
                return;
            }
            var role = GetRole(r);
            if(role == null)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Invalid Role!",
                    Description = "The role you provided was invalid!",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                return;
            }
            GuildSettings.NewMemberRole = role.Id;
            GuildSettings.SaveGuildSettings();
            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
            {
                Title = "Success!",
                Description = $"New members will now get the role {role.Mention}",
                Color = Color.Green
            }.WithCurrentTimestamp().Build());
        }
    }
}