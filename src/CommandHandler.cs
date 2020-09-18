using Discord;
using Discord.Commands;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;
using Public_Bot.Modules.Handlers;
using Public_Bot.State.Types;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Public_Bot
{
    public class CommandHandler
    {
        static DiscordShardedClient client;
        static CustomCommandService service;
        static HandlerService handlerService;
        bool isReady = false;
        //public static List<GuildSettings> CurrentGuildSettings { get; set; } = new List<GuildSettings>();
        public CommandHandler(CustomCommandService _service, DiscordShardedClient _client, HandlerService handler)
        {
            client = _client;

            service = _service;

            handlerService = handler;

            client.MessageReceived += CheckCommandAsync;

            client.ShardReady += Ready;

            //client.ShardConnected += Client_ShardConnected;

            //client.ShardDisconnected += Client_ShardDisconnected;

            Logger.Write($"Command Handler Ready", Logger.Severity.Log);
        }


        private static string FormatJson(string json)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }
        public static async Task HandleFailedGql(string q, string method, string type, string StatusCode, string resp, string stack, int trys)
        {
            try
            {
                await client.GetGuild(724798166804725780).GetTextChannel(733154982249103431).SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Failed GraphQl!",
                    Description = $"Failed to {type} {method}! Server sent: {StatusCode}!",
                    Color = Color.Red
                }.WithCurrentTimestamp().Build());
                File.WriteAllText($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}fgql.txt", $"Tried this request {trys} times\n\n----------< Start Stack >----------\n\n{stack}\n\n----------< End Stack >----------\n\n----------< Start GraphQLQuery >----------\n\n{q}\n\n----------< End GraphQLQuery >----------\n\n----------< Start Server Response >----------\n\n{resp}\n\n----------< End Server Response >----------");
                await client.GetGuild(724798166804725780).GetTextChannel(733154982249103431).SendFileAsync($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}fgql.txt", "");
            }
            catch (Exception x)
            {
                Console.WriteLine($"----------< Start GraphQLQuery >----------\n\n{q}\n\n----------< End GraphQLQuery >----------\n\n----------< Start Server Response >----------\n\n{resp}\n\n----------< End Server Response >----------");
            }
        }

        //private async Task Client_ShardDisconnected(Exception arg1, DiscordSocketClient arg2)
        //    => isReady = false;


        private async Task Ready(DiscordSocketClient arg)
        {
            handlerService.CreateHandlers();

            isReady = true;
        }
        public static bool IsBotRole(IRole role)
        {
            var guild = client.GetGuild(role.Guild.Id);
            var users = guild.Users.Where(x => x.Roles.Contains(role));

            if (users.Count() == 1)
                if (users.First().IsBot)
                    return true;
            return false;
        }
        private async Task CheckCommandAsync(SocketMessage arg)
        {
            if (!isReady)
            {
                Logger.Write("Fired command before bot was ready!", Logger.Severity.Warn);
                return;
            }

            var msg = arg as SocketUserMessage;
            if (msg == null) return;
            ShardedCommandContext context = new ShardedCommandContext(client, msg);
            if (context.IsPrivate)
                return;

            var s = GuildSettingsHelper.GetGuildSettings(context.Guild.Id);
            if (arg.Content == ($"<@{client.CurrentUser.Id}>") || arg.Content == ($"<@!{client.CurrentUser.Id}>"))
            {
                await context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                {
                    Title = "Prefix",
                    Description = $"My current prefix is `{s.Prefix}`\nYou can also mention me instead of using a prefix.",
                    Color = Color.Green,
                }.WithCurrentTimestamp().Build());
                return;
            }
            if (arg.Content.StartsWith(s.Prefix) || arg.Content.StartsWith($"<@{client.CurrentUser.Id}>") || arg.Content.StartsWith($"<@!{client.CurrentUser.Id}>"))
            {
                Thread thread = new Thread(async () =>
                {
                    var resp = await service.ExecuteAsync(context, s);
                    Logger.Write($"Command Result: {resp.Result} - Command: {arg.Content}");
                    
                    if (resp.Result == CommandStatus.MissingGuildPermission)
                    {
                        await context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "**Missing Permissions**",
                            Color = Color.Red,
                            Description = $"{client.CurrentUser.Username} is missing these permissions:\n{resp.ResultMessage}This command will not work until you give {client.CurrentUser.Username} these permissions!"
                        }.WithCurrentTimestamp().Build());
                    }
                    else if (resp.Result == CommandStatus.Error)
                    {
                    
                        if (resp.Exception.InnerException != null && resp.Exception.InnerException.GetType() == typeof(Discord.Net.HttpException))
                        {
                            var e = resp.Exception.InnerException as HttpException;
                    
                            if (e.DiscordCode == 50013)
                            {
                                 // no permissions, tell the user
                                 try
                                {
                                    await context.User.SendMessageAsync("", false, new EmbedBuilder()
                                    {
                                        Title = "Hey! We're missing permissions!",
                                        Color = Color.Red,
                                        Description = $"The bot doesn't have permission to send messages in <#{context.Channel.Id}> on {context.Guild}!",
                                        Fields = new List<EmbedFieldBuilder>()
                                        {
                                             new EmbedFieldBuilder()
                                             {
                                                 Name = "If you're an Admin",
                                                 Value = $"Please check {client.CurrentUser.Username}'s permission in `#{context.Channel.Name}` to make sure the bot can **Send Messages** in said channel. If the bot still has this issue please join the [Support Server](https://discord.com/invite/w8EcwBy) and ask around for help.",
                                                 IsInline = true
                                             },
                                             new EmbedFieldBuilder()
                                             {
                                                 Name = "If you're a Member",
                                                 Value = $"Contact an Mod/Admin/Owner in {context.Guild.Name} and tell them that the bot doesn't have permission to send messages in `#{context.Channel}`. Thanks.",
                                                 IsInline = true
                                             }
                                        }
                                    }.WithCurrentTimestamp().Build());
                                }
                                catch { }
                                return;
                            }
                        }
                    
                        var c = client.GetGuild(724798166804725780).GetTextChannel(740141617566056489);
                        if (c == null)
                            return;
                        File.WriteAllText($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}cmderror.txt", $"----< Start Exception >----\n\n{resp.Exception.ToString()}\n\n----< End Exception >----");
                        await c.SendFileAsync($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}cmderror.txt", "", false, new EmbedBuilder()
                        {
                            Title = "Command Exception!",
                            Color = Color.Red,
                            Fields = new List<EmbedFieldBuilder>()
                            {
                                 new EmbedFieldBuilder()
                                 {
                                     Name = "Command",
                                     Value = msg.Content
                                 },
                                 new EmbedFieldBuilder()
                                 {
                                     Name = "Author",
                                     Value = msg.Author.ToString(),
                                 },
                                 new EmbedFieldBuilder()
                                 {
                                     Name = "Guild",
                                     Value = context.Guild.Name
                                 }
                            }
                        }.Build());
                    }
                    else if (resp.Result == CommandStatus.ChannelBlacklisted)
                    {
                        try
                        {
                            await context.User.SendMessageAsync($"Hey {context.User.Username}, Commands are turned off in <#{context.Channel.Id}> on {context.Guild.Name}. Sorry!");
                        }
                        catch { }
                    }
                    else if (resp.Result == CommandStatus.InvalidPermissions)
                    {
                        await context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "**You do not have permission**",
                            Description = @"Looks like you don't have permission for this command.",
                            Color = Color.Red,
                            Timestamp = DateTimeOffset.Now,
                        }.Build());
                    }
                    
                    else if (resp.Result == CommandStatus.Disabled)
                    {
                        await context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "**Sorry!**",
                            Description = @"That command is disabled!",
                            Color = Color.Red,
                            Timestamp = DateTimeOffset.Now,
                        }.Build());
                    }
                    
                    else if (resp.Result == CommandStatus.NotEnoughParams)
                    {
                        await context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "**You didn't provide enough parameters!**",
                            Description = @$"Here's how to use the command",
                            Fields = new List<EmbedFieldBuilder>()
                            {
                                 new EmbedFieldBuilder()
                                 {
                                     Name = "Your input:",
                                     Value = msg.Content,
                                     IsInline = true
                                 },
                                 new EmbedFieldBuilder()
                                 {
                                     Name = "Expected input:",
                                     Value = $"{resp.commandUsed.Replace("(PREFIX)", s.Prefix)}"
                                 }
                            },
                            Color = Color.Red,
                            Timestamp = DateTimeOffset.Now,
                        }.Build());
                    }
                    
                    else if (resp.Result == CommandStatus.InvalidParams)
                    {
                        await context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "**The parameters you provided were invalid!**",
                            Description = @$"Here's how to use the command",
                            Fields = new List<EmbedFieldBuilder>()
                            {
                                 new EmbedFieldBuilder()
                                 {
                                     Name = "Your input:",
                                     Value = msg.Content,
                                     IsInline = true
                                 },
                                 new EmbedFieldBuilder()
                                 {
                                     Name = "Expected input:",
                                     Value = $"{resp.commandUsed.Replace("(PREFIX)", s.Prefix)}"
                                 }
                            },
                            Color = Color.Red,
                            Timestamp = DateTimeOffset.Now,
                        }.Build());
                    }
                });
                thread.Start();
            }
        }
    }
}
