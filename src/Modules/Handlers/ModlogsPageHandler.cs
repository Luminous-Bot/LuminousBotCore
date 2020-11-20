using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Public_Bot.Modules.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot.Modules.Handlers
{
    [DiscordHandler]
    class ModlogsPageHandler
    {
        public static DiscordShardedClient client;
        public class ModlogHelpPage
        {
            public ulong UserID { get; set; }
            public ulong GuildID { get; set; }
            public ulong MessageID { get; set; }
            public int page { get; set; }
            public ulong pageOwner { get; set; }
            public List<Infraction> Modlogs { get; set; }
        }
        public static List<ModlogHelpPage> CurrentPages { get; set; }
           
        public ModlogsPageHandler(DiscordShardedClient c)
        {
            client = c;

            try
            {
                CurrentPages = StateHandler.LoadObject<List<ModlogHelpPage>>("MlPages");
            }
            catch
            {
                CurrentPages = new List<ModlogHelpPage>();
            }

            client.ReactionAdded += HandleModlogsPage;
        }
        public static ModlogHelpPage BuildHelpPage(List<Infraction> logs, ulong mID, ulong uID, ulong gID, ulong pOwn)
        {
            ModlogHelpPage page = new ModlogHelpPage()
            {
                MessageID = mID,
                Modlogs = logs,
                page = 1,
                UserID = uID,
                GuildID = gID,
                pageOwner = pOwn
            };
            return page;
            //CurrentPages.Add(page);
            //SaveMLPages();
        }
        public static EmbedBuilder BuildHelpPageEmbed(ModlogHelpPage page, int pagenum)
        {
            var pageTotals = (int)Math.Ceiling((double)page.Modlogs.Count / (double)25);
            if (pagenum > pageTotals)
                pagenum = pagenum - 1;
            //page.page = pagenum;
            var rs = page.Modlogs.Skip((pagenum - 1) * 25).Take(25).ToArray();
            var guild = client.GetGuild(page.GuildID);
            var user = guild.GetUser(page.UserID);
            var gs = GuildSettingsHelper.GetGuildSettings(page.GuildID);
            EmbedBuilder b = new EmbedBuilder()
            {
                Title = $"Modlogs for **{user}** ({user.Id})",
                Description = $"To remove a users logs use `{gs.Prefix}clearlogs <@user> <lognumber(s)>`",
                Color = Color.Green,
                Fields = new List<EmbedFieldBuilder>(),
                Footer = new EmbedFooterBuilder() { Text = $"Page {pagenum}/{pageTotals}"}
            };
            for (int i = 0; i != rs.Count(); i++)
            {
                var log = rs[i];
                var gu = GuildCache.GetGuild(log.GuildID);
                var md = gu.GuildMembers.GetGuildMember(log.ModeratorID, log.GuildID);
                b.Fields.Add(new EmbedFieldBuilder()
                {
                    IsInline = false,
                    Name = (((pagenum - 1) * 25) + (i + 1)).ToString() + $": {log.Action}",
                    Value =
                    $"Reason: {log.Reason}\n" +
                    $"Moderator: <@{log.ModeratorID}> ({(md == null ? "No username on record" : md.Username)})\n" +
                    $"Date: {log.Time}"
                });
            }
            if (page.Modlogs.Count == 0)
            {
                b.Description = "This user has not logs!";
            }
            return b;
        }
        public static void SaveMLPages()
        {
            StateHandler.SaveObject<List<ModlogHelpPage>>("MlPages", CurrentPages);
        }
        
        private async Task HandleModlogsPage(Discord.Cacheable<Discord.IUserMessage, ulong> arg1, ISocketMessageChannel arg2, SocketReaction arg3)
        {
            IUser User;
            if (arg3.User.IsSpecified)
                User = arg3.User.Value;
            else
                User = await arg2.GetUserAsync(arg3.UserId);

            if (User == null)
                User = client.GetUser(arg3.UserId);

            if (User == null)
                return;

            if (User.IsBot)
                return;

            if (CurrentPages.Any(x => x.MessageID == arg3.MessageId))
            {
                var page = CurrentPages.Find(x => x.MessageID == arg1.Id);
                var msg = client.GetGuild(page.GuildID).GetTextChannel(arg2.Id).GetMessageAsync(arg3.MessageId).Result;
                if (arg3.UserId != page.pageOwner)
                {
                    await msg.RemoveReactionAsync(arg3.Emote, arg3.User.Value);
                    return;
                }

                if (arg3.Emote.Name == "⬅")
                {
                    if (page.page == 1)
                    {
                        await msg.RemoveReactionAsync(arg3.Emote, arg3.User.Value);
                        return;
                    }
                    page.page--;
                    if (msg.GetType() == typeof(SocketUserMessage))
                        await (msg as SocketUserMessage).ModifyAsync(x => x.Embed = BuildHelpPageEmbed(page, page.page).Build());
                    else if(msg.GetType() == typeof(RestUserMessage))
                        await (msg as RestUserMessage).ModifyAsync(x => x.Embed = BuildHelpPageEmbed(page, page.page).Build());
                    else
                        Logger.Write($"Message was not socket nor rest, its type was {msg.GetType()}", Logger.Severity.Warn);
                    await msg.RemoveReactionAsync(arg3.Emote, arg3.User.Value);
                    SaveMLPages();

                }
                else if (arg3.Emote.Name == "➡")
                {
                    page.page++;
                    if (msg.GetType() == typeof(SocketUserMessage))
                        await (msg as SocketUserMessage).ModifyAsync(x => x.Embed = BuildHelpPageEmbed(page, page.page).Build());
                    else if (msg.GetType() == typeof(RestUserMessage))
                        await (msg as RestUserMessage).ModifyAsync(x => x.Embed = BuildHelpPageEmbed(page, page.page).Build());
                    else
                        Logger.Write($"Message was not socket nor rest, its type was {msg.GetType()}", Logger.Severity.Warn);
                    await msg.RemoveReactionAsync(arg3.Emote, arg3.User.Value);
                    SaveMLPages();
                }
                else
                {
                    await msg.RemoveReactionAsync(arg3.Emote, arg3.User.Value);
                }   
            }
        }
    }
}
