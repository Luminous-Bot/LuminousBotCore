using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Timers;
using System.Linq;
using Public_Bot.Modules.Commands;
using Discord;
using Public_Bot.Modules.Handlers;

namespace Public_Bot.Modules.Auto_Moderation
{
    //[DiscordHandler]
    public class AntiSpamHandler
    {
    //    public class AuthorMessage
    //    {
    //        public ulong AuthorId { get; set; }
    //        public SocketTextChannel chan { get; set; }
    //        public Timer EvalTimer { get; set; }
    //        public List<SocketMessage> Contents { get; set; } = new List<SocketMessage>();

    //        public AuthorMessage(SocketMessage m)
    //        {
    //            AuthorId = m.Author.Id;
    //            chan = (SocketTextChannel)m.Channel;
    //            EvalTimer = new Timer() { AutoReset = false, Interval = 3000, Enabled = true };
    //            EvalTimer.Elapsed += (object s, ElapsedEventArgs e) =>
    //            {
    //                Bag.Remove(this);
    //                var isSpam = IsSpam();
    //                if (isSpam)
    //                    HandleIsSpam(this);
    //            };
    //        }
    //        public bool IsSpam()
    //        {
    //            string[] Contents = this.Contents.Select(x => x.Content).ToArray();

    //            //determin if spam looks like the following
    //            //    s
    //            //    f
    //            //    a
    //            //    gg
    //            //    ha
    //            //    s
    //            //    f
    //            //    g
    //            //    h
    //            //    t
    //            //    r
    //            //    ad
    //            //    g

    //            if (Contents.Length >= 10)
    //                return true;

    //            if(Contents.Max() - Contents.Min())
    //        }
    //    }
        
    //    public static DiscordShardedClient client { get; set; }
    //    static List<AuthorMessage> Bag { get; set; }
    //    public AntiSpamHandler(DiscordShardedClient c)
    //    {
    //        client = c;
    //        Bag = new List<AuthorMessage>();
    //        client.MessageReceived += HandleAntiSpam;
    //    }
    //    public static async Task HandleIsSpam(AuthorMessage m)
    //    {
    //        var gs = GuildSettings.Get(m.chan.Guild.Id);
    //        try
    //        {
    //            await m.chan.DeleteMessagesAsync(m.Contents);

    //            switch (gs.AutoModeration.SpamAction)
    //            {
    //                case SpamAction.Delete:
    //                    break;
    //                case SpamAction.DeleteAndWarn:
    //                    {
    //                        new Infraction(m.AuthorId, client.CurrentUser.Id, m.chan.Guild.Id, Action.Warned, "Anti-Spam", DateTime.UtcNow);
    //                        try 
    //                        {
    //                            await m.Contents[0].Author.SendMessageAsync("", false, new EmbedBuilder()
    //                            {
    //                                Title = $"**You have been Warned on {m.chan.Guild.Name}**",
    //                                Fields = new List<EmbedFieldBuilder>()
    //                            {
    //                                new EmbedFieldBuilder()
    //                                {
    //                                    Name = "Moderator",
    //                                    Value = $"<@{client.CurrentUser.Id}>",
    //                                    IsInline = true,
    //                                },
    //                                new EmbedFieldBuilder()
    //                                {
    //                                    Name = "Reason",
    //                                    Value = "Anti-Spam",
    //                                    IsInline = true
    //                                }
    //                            },
    //                                Color = Color.Orange
    //                            }.WithCurrentTimestamp().Build());
    //                        }
    //                        catch { }
    //                    }
    //                    break;
    //                case SpamAction.DeleteAndMute:
    //                    new Infraction(m.AuthorId, client.CurrentUser.Id, m.chan.Guild.Id, Action.Muted, "Anti-Spam", DateTime.UtcNow);
    //                    MuteHandler.AddNewMuted(m.AuthorId, DateTime.UtcNow.AddMinutes(gs.AutoModeration.SpamMuteMinutes), gs);
    //                    try
    //                    {
    //                        await m.Contents[0].Author.SendMessageAsync("", false, new EmbedBuilder()
    //                        {
    //                            Title = $"**You have been Muted on {m.chan.Guild.Name}**",
    //                            Fields = new List<EmbedFieldBuilder>()
    //                            {
    //                                new EmbedFieldBuilder()
    //                                {
    //                                    Name = "Moderator",
    //                                    Value = $"<@{client.CurrentUser.Id}>",
    //                                    IsInline = true,
    //                                },
    //                                new EmbedFieldBuilder()
    //                                {
    //                                    Name = "Reason",
    //                                    Value = "Anti-Spam",
    //                                    IsInline = true
    //                                }
    //                            },
    //                            Color = Color.Orange
    //                        }.WithCurrentTimestamp().Build());
    //                    }
    //                    catch { }
    //                    break;
    //                case SpamAction.DeleteAndKick:
    //                    new Infraction(m.AuthorId, client.CurrentUser.Id, m.chan.Guild.Id, Action.Kicked, "Anti-Spam", DateTime.UtcNow);
    //                    try
    //                    {
    //                        await m.Contents[0].Author.SendMessageAsync("", false, new EmbedBuilder()
    //                        {
    //                            Title = $"**You have been Kicked on {m.chan.Guild.Name}**",
    //                            Fields = new List<EmbedFieldBuilder>()
    //                            {
    //                                new EmbedFieldBuilder()
    //                                {
    //                                    Name = "Moderator",
    //                                    Value = $"<@{client.CurrentUser.Id}>",
    //                                    IsInline = true,
    //                                },
    //                                new EmbedFieldBuilder()
    //                                {
    //                                    Name = "Reason",
    //                                    Value = "Anti-Spam",
    //                                    IsInline = true
    //                                }
    //                            },
    //                            Color = Color.Orange
    //                        }.WithCurrentTimestamp().Build());
    //                    }
    //                    catch { }
    //                    try
    //                    {
    //                        await m.chan.Guild.GetUser(m.AuthorId).KickAsync("Anti-Spam");
    //                    }
    //                    catch { }
    //                    break;
    //                case SpamAction.DeleteAndBan:
    //                    new Infraction(m.AuthorId, client.CurrentUser.Id, m.chan.Guild.Id, Action.Banned, "Anti-Spam", DateTime.UtcNow);
    //                    try
    //                    {
    //                        await m.Contents[0].Author.SendMessageAsync("", false, new EmbedBuilder()
    //                        {
    //                            Title = $"**You have been Banned on {m.chan.Guild.Name}**",
    //                            Fields = new List<EmbedFieldBuilder>()
    //                            {
    //                                new EmbedFieldBuilder()
    //                                {
    //                                    Name = "Moderator",
    //                                    Value = $"<@{client.CurrentUser.Id}>",
    //                                    IsInline = true,
    //                                },
    //                                new EmbedFieldBuilder()
    //                                {
    //                                    Name = "Reason",
    //                                    Value = "Anti-Spam",
    //                                    IsInline = true
    //                                }
    //                            },
    //                            Color = Color.Orange
    //                        }.WithCurrentTimestamp().Build());
    //                    }
    //                    catch { }
    //                    try
    //                    {
    //                        await m.chan.Guild.GetUser(m.AuthorId).BanAsync(0, "Anti-Spam");
    //                    }
    //                    catch { }
    //                    break;
    //            }
    //        }
    //        catch
    //        {

    //        }
    //    }
    //    private async Task HandleAntiSpam(SocketMessage arg)
    //    {
    //        if (arg.Author.IsBot || arg.Author.IsWebhook)
    //            return;
    //        if (arg.Channel.GetType() != typeof(SocketTextChannel))
    //            return;
    //        if (!GuildSettings.Get((arg.Channel as SocketTextChannel).Guild.Id).AutoModeration.AntiSpamEnabled)
    //            return;
    //        if (Bag.Any(x => x.AuthorId == arg.Author.Id))
    //        {
    //            Bag.Find(x => x.AuthorId == arg.Author.Id).Contents.Add(arg);
    //        }
    //        else
    //            Bag.Add(new AuthorMessage(arg));
    //    }
    }
    public enum SpamAction
    {
        Delete,
        DeleteAndWarn,
        DeleteAndMute,
        DeleteAndKick,
        DeleteAndBan,
    }
}
