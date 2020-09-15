using System;
using System.Collections.Generic;
using System.Linq;
using Discord;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Public_Bot.Modules.Handlers
{
    [DiscordHandler]
    public class RolecardHandler
    {
        private DiscordShardedClient _client;

        public RolecardHandler(DiscordShardedClient client)
        {
            this._client = client;

            GuildCache.ItemAdded += (object sender, Guild e) 
                => SafeloadGuild(e);
        }

        public void SafeloadGuild(Guild g)
        {
            if (g.ReactionRoleCards == null)
                return;

            if (g.ReactionRoleCards.Count == 0)
                return;

            foreach(var item in g.ReactionRoleCards)
            {
                ReactionService.AddReactionHandler(item.MessageID, ReactionCardAddEvent, ReactionCardRemoveEvent);
            }
        }

        public void AddRankCard(ReactionRoleCard card)
        {
            ReactionService.AddReactionHandler(card.MessageID, ReactionCardAddEvent, ReactionCardRemoveEvent);
        }

        public void DeloadGuild(Guild g)
        {
            if (g.ReactionRoleCards == null)
                return;

            if (g.ReactionRoleCards.Count == 0)
                return;

            foreach (var item in g.ReactionRoleCards)
            {
                ReactionService.RemoveReactionHandler(item.MessageID);
            }
        }

        private async Task ReactionCardAddEvent(IMessage arg1, SocketTextChannel arg2, SocketReaction arg3)
        {
            var g = arg2.Guild;
            // Return if its a bot
            var user = g.GetUser(arg3.UserId);
            if (user.IsBot)
                return;

            // Get our role card
            var guild = GuildCache.GetGuild(g.Id);
            var card = guild.ReactionRoleCards.Find(x => x.MessageID == arg1.Id);

            // Get the emote role pair
            var rolecardItem = card.Roles.Find(x => x.EmoteID == arg3.Emote.Name);

            if(rolecardItem == null)
            {
                // Oh shit, somthings wrong i can feel it
            }

            // Get the role the give the user it
            
            var role = g.GetRole(rolecardItem.RoleID);

            if(role == null)
            {
                //remove from card and cry
            }
            if (!user.Roles.Contains(role))
            {
                await user.AddRoleAsync(role);

                try
                {
                    await user.SendMessageAsync($"{arg3.Emote.Name}: You got the role **{role.Name}** on {g.Name}!");
                }
                catch { }
            }
        }

        private async Task ReactionCardRemoveEvent(IMessage arg1, SocketTextChannel arg2, SocketReaction arg3)
        {
            var g = arg2.Guild;
            // Return if its a bot
            var user = g.GetUser(arg3.UserId);
            if (user.IsBot)
                return;

            // Get our role card
            var guild = GuildCache.GetGuild(g.Id);
            var card = guild.ReactionRoleCards.Find(x => x.MessageID == arg1.Id);

            // Get the emote role pair
            var rolecardItem = card.Roles.Find(x => x.EmoteID == arg3.Emote.Name);

            if (rolecardItem == null)
            {
                // Oh shit, somthings wrong i can feel it
            }

            // Get the role the give the user it
            var role = g.GetRole(rolecardItem.RoleID);

            if (role == null)
            {
                //remove from card and cry
            }
            if (user.Roles.Contains(role))
            {
                await user.RemoveRoleAsync(role);

                try
                {
                    await user.SendMessageAsync($"{arg3.Emote.Name}: You lost the role **{role.Name}** on {g.Name}!");
                }
                catch { }
            }
        }
    }
}
