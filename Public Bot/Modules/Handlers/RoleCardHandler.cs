using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using Discord.Rest;
using System.Linq;
using Discord;

namespace Public_Bot.Modules.Handlers
{
    class RoleCardHandler
    {
        public static DiscordShardedClient client;
        public static List<RoleCard> Rolecards;
        public RoleCardHandler(DiscordShardedClient c)
        {
            client = c;

            try
            {
                Rolecards = StateHandler.LoadObject<List<RoleCard>>("rolecards");
            }
            catch 
            {
                Rolecards = new List<RoleCard>(); 
            }

            client.ReactionAdded += CheckReactAdd;

            client.ReactionRemoved += CheckReactRemove;
        }
        public static void SaveRolecards()
        {
            StateHandler.SaveObject<List<RoleCard>>("rolecards", Rolecards);
        }
        private async Task CheckReactRemove(Discord.Cacheable<Discord.IUserMessage, ulong> arg1, ISocketMessageChannel arg2, SocketReaction arg3)
        {
            if (!Rolecards.Any(x => x.ChannelID == arg2.Id))
                return;
            var Rolecard = Rolecards.Find(x => x.ChannelID == arg2.Id);
            var usr = client.GetGuild(Rolecard.ServerID).GetUser(arg3.UserId);
            var guild = client.GetGuild(Rolecard.ServerID);

            var msg = await guild.GetTextChannel(Rolecard.ChannelID).GetMessageAsync(Rolecard.MessageID);
            if (arg3.MessageId != msg.Id)
                return;
            if (arg3.User.Value.IsBot)
                return;

            //doesnt contain "a" tag
            RoleCard.RoleEmoteDesc redVal = null;
            var sem = arg3.Emote.ToString();
            redVal = Rolecard.RoleEmojiIDs.Any(x => x.Emote == sem) ? Rolecard.RoleEmojiIDs.First(x => x.Emote == sem) : null;

            if (redVal == null)
            {
                await msg.RemoveReactionAsync(arg3.Emote, usr);
                return;
            }
            var role = guild.GetRole(redVal.RoleID);
            if (!usr.Roles.Contains(role))
                return;
            await usr.RemoveRoleAsync(role, new RequestOptions() { AuditLogReason = "Self-Assigned role" });
            try
            {
                await usr.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = $"**{guild.Name}**",
                    Description = $"You were removed from the role **\"{role.Name}\"**",
                    Color = Color.Green
                }.Build());
            }
            catch { }
        }

        private async Task CheckReactAdd(Discord.Cacheable<Discord.IUserMessage, ulong> arg1, ISocketMessageChannel arg2, SocketReaction arg3)
        {
            if (!Rolecards.Any(x => x.ChannelID == arg2.Id))
                return;
            var Rolecard = Rolecards.Find(x => x.ChannelID == arg2.Id);
            var usr = client.GetGuild(Rolecard.ServerID).GetUser(arg3.UserId);
            var guild = client.GetGuild(Rolecard.ServerID);
            var msg = await guild.GetTextChannel(Rolecard.ChannelID).GetMessageAsync(Rolecard.MessageID);
            if (arg3.MessageId != msg.Id)
                return;
            if (usr.IsBot)
                return;

            RoleCard.RoleEmoteDesc redVal = null;

            redVal = Rolecard.RoleEmojiIDs.Any(x => x.Emote == arg3.Emote.ToString()) ? Rolecard.RoleEmojiIDs.First(x => x.Emote == arg3.Emote.ToString()) : null;

            if (redVal == null)
            {
                await msg.RemoveReactionAsync(arg3.Emote, usr);
                return;
            }

            var role = guild.GetRole(redVal.RoleID);
            await usr.AddRoleAsync(role, new RequestOptions() { AuditLogReason = "Self-Assigned role" });
            try
            {
                await usr.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = $"**{guild.Name}**",
                    Description = $"You were assigned the role **\"{role.Name}\"**",
                    Color = Color.Green
                }.Build());
            }
            catch { }
        }

        public async Task ChangeRoles(SocketGuildUser usr, IRole role, RoleAction act)
        {
            if (act == RoleAction.Add)
                await usr.AddRoleAsync(role);
            if (act == RoleAction.Remove)
                await usr.RemoveRoleAsync(role);

        }
        public enum RoleAction
        {
            Add,
            Remove
        }

        public class RoleCard
        {
            public ulong ServerID { get; set; }
            public List<RoleEmoteDesc> RoleEmojiIDs { get; set; }
            public ulong MessageID { get; set; }
            public ulong ChannelID { get; set; }
            public static RoleCard Get(ulong id)
                => Rolecards.Any(x => x.ServerID == id) ? Rolecards.First(x => x.ServerID == id) : null;
            public class RoleEmoteDesc
            {
                public ulong RoleID { get; set; }
                public string Emote { get; set; }
                public string Description { get; set; }
            }
        }
    }
}
