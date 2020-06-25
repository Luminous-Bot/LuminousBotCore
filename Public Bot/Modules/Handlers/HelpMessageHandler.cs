using Discord;
using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Public_Bot.Modules.Handlers
{
    [DiscordHandler]
    public class HelpMessageHandler
    {
        public class HelpPage
        {
            public ulong GuildID { get; set; }
            public ulong MessageID { get; set; }
            public int page { get; set; }
            public ulong pageOwner { get; set; }
           // public List<ModCommands.ModLog> Modlogs { get; set; }
        }

        public static int HelpmsgPerPage = 8;
        public static Dictionary<string, string> HelpPagesPublic = new Dictionary<string, string>();
        public static Dictionary<string, string> HelpPagesStaff = new Dictionary<string, string>();

        public static int HelpPagesPublicCount = 0;
        public static int HelpPagesStaffCount = 0;

        public static List<HelpPage> CurrentHelpMessages;
        private DiscordShardedClient client;
        public HelpMessageHandler(DiscordShardedClient client)
        {
            this.client = client;
            try { CurrentHelpMessages = StateHandler.LoadObject<List<HelpPage>>("HelpMessages"); } catch { CurrentHelpMessages = new List<HelpPage>(); }
            //BuildHelpPages();
            client.ReactionAdded += HandleHelpMessage;
           
        }
        public static void SaveHelpMessages()
        {
            StateHandler.SaveObject<List<HelpPage>>("HelpMessages", CurrentHelpMessages);
        }
        public async Task HandleHelpMessage(Cacheable<Discord.IUserMessage, ulong> arg1, ISocketMessageChannel arg2, SocketReaction arg3)
        {
            if (arg3.User.Value.IsBot)
                return;
            if (!CurrentHelpMessages.Any(x => x.MessageID == arg3.MessageId))
                return;
            var hm = CurrentHelpMessages.Find(x => x.MessageID == arg3.MessageId);
            var gs = CommandHandler.GetGuildSettings(hm.GuildID);
            var msg = (RestUserMessage)client.GetGuild(hm.GuildID).GetTextChannel(arg3.Channel.Id).GetMessageAsync(arg3.MessageId).Result;
            if (CurrentHelpMessages.Any(x => x.MessageID == arg3.MessageId) && msg != null)
            {
                if (arg3.UserId != hm.pageOwner)
                {
                    await msg.RemoveReactionAsync(arg3.Emote, arg3.User.Value);
                    return;
                }
                //is a valid card, lets check what page were on
                var s = msg.Embeds.First().Title;

                Regex r = new Regex(@"\*\*Help \((\d)\/(\d)\)");
                var mtc = r.Match(s);
                var curpage = int.Parse(mtc.Groups[1].Value);

                if (arg3.Emote.Name == "⬅")
                {
                    //check if the message is > 2 weeks old or exists in swiss server
                    if (curpage == 1)
                    {
                        await msg.RemoveReactionAsync(arg3.Emote, arg3.User.Value);
                        return;
                    }

                    await msg.ModifyAsync(x => x.Embed = HelpEmbedBuilder(curpage - 1, CalcHelpPage(client.GetGuild(hm.GuildID).GetUser(arg3.User.Value.Id), gs)));
                    await msg.RemoveReactionAsync(arg3.Emote, arg3.User.Value);

                }
                else if (arg3.Emote.Name == "➡")
                {
                    await msg.ModifyAsync(x => x.Embed = HelpEmbedBuilder(curpage + 1, CalcHelpPage(client.GetGuild(hm.GuildID).GetUser(arg3.User.Value.Id), gs)));
                    await msg.RemoveReactionAsync(arg3.Emote, arg3.User.Value);
                }
                else
                {
                    await msg.RemoveReactionAsync(arg3.Emote, arg3.User.Value);
                }

            }
        }
        public static void BuildHelpPages(GuildSettings s)
        {
            HelpPagesPublic.Clear();
            HelpPagesStaff.Clear();
            foreach (var item in CommandModuleBase.ReadCurrentCommands(s.Prefix).Where(x => x.RequiresPermission == false))
            {
                if (item.CommandHelpMessage == null && item.CommandDescription == null)
                    continue;
                if (!HelpPagesPublic.ContainsKey(item.ModuleName))
                    HelpPagesPublic.Add(item.ModuleName, item.CommandName/*.PadRight(item.ModuleName.Length)*/);
                else
                    HelpPagesPublic[item.ModuleName] += $"\n{item.CommandName}"/*.PadRight(item.ModuleName.Length)*/;
            }
            foreach (var item in CommandModuleBase.ReadCurrentCommands(s.Prefix))
            {
                if (item.CommandHelpMessage == null && item.CommandDescription == null)
                    continue;
                if (!HelpPagesStaff.ContainsKey(item.ModuleName))
                    HelpPagesStaff.Add(item.ModuleName, item.CommandName/*.PadRight(item.ModuleName.Length)*/);
                else
                    HelpPagesStaff[item.ModuleName] += $"\n{item.CommandName}"/*.PadRight(item.ModuleName.Length)*/;
            }
            HelpPagesPublicCount = (int)Math.Ceiling((double)HelpPagesPublic.Count / (double)HelpmsgPerPage);
            HelpPagesStaffCount = (int)Math.Ceiling((double)HelpPagesStaff.Count / (double)HelpmsgPerPage);
        }
        public static Embed HelpEmbedBuilder(int page, HelpPages p)
        {
            if (p == HelpPages.Public)
            {
                //if (curpage == HelpPagesPublicCount)
                //{
                //    await msg.RemoveReactionAsync(arg3.Emote, arg3.User.Value);
                //    return;
                //}
                if (page > HelpPagesPublicCount)
                    page = page - 1;
                //var rs = HelpPagesPublic.Skip((page - 1) * HelpmsgPerPage).Take(HelpmsgPerPage);
                var em = new EmbedBuilder()
                {
                    Title = $"**Help ({page}/{HelpPagesPublicCount})**",
                    Color = Color.Green,
                    Description = "Here are the commands!",
                    Footer = new EmbedFooterBuilder()
                    {
                        Text = "Public help message"
                    },
                    Fields = new List<EmbedFieldBuilder>()
                };
                foreach(var h in HelpPagesPublic.OrderBy(x => x.Value.Split('\n').Length * -1))
                {
                    em.Fields.Add(new EmbedFieldBuilder() { Name = h.Key, Value = $"```{h.Value}```", IsInline = true });
                }
                return em.Build();
            }
            else if (p == HelpPages.Staff)
            {

                if (page > HelpPagesStaffCount)
                    page = page - 1;
                var em = new EmbedBuilder()
                {
                    Title = $"**Help ({page}/{HelpPagesStaffCount})**",
                    Color = Color.Green,
                    Description = "Here are the commands!",
                    Fields = new List<EmbedFieldBuilder>(),
                    Footer = new EmbedFooterBuilder()
                    {
                        Text = "Staff help message"
                    }
                };
                foreach (var h in HelpPagesStaff.OrderBy(x => x.Value.Split('\n').Length * -1))
                {
                    em.Fields.Add(new EmbedFieldBuilder() { Name = h.Key, Value = $"```{h.Value}```", IsInline = true });
                }
                return em.Build();
            }
            else
                return null;
        }
        public static HelpPages CalcHelpPage(SocketGuildUser usr, GuildSettings s)
        {
            if (s.PermissionRoles.Any(x => usr.Roles.Any(y => y.Id == x)))
                return HelpPages.Staff;
            else
                return HelpPages.Public;
        }
        public enum HelpPages
        {
            Public,
            Staff,
        }
        
    }
}
