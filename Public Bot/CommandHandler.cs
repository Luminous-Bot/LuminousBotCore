using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using Public_Bot.Modules.Handlers;
using Public_Bot.State.Types;
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
        public static List<GuildSettings> CurrentGuildSettings { get; set; } = new List<GuildSettings>();
        public CommandHandler(CustomCommandService _service, DiscordShardedClient _client, HandlerService handler)
        {
            client = _client;

            service = _service;

            handlerService = handler;

            LoadGuildSettings();

            client.MessageReceived += CheckCommandAsync;

            client.ShardReady += Ready;

            client.ShardConnected += Client_ShardConnected;

            //client.ShardDisconnected += Client_ShardDisconnected;

            Logger.Write($"Command Handler Ready", Logger.Severity.Log);
        }

        private async Task Client_ShardConnected(DiscordSocketClient arg)
        {
            CurrentGuildSettings.Where(x => x.WelcomeCard == null && arg.GetGuild(x.GuildID) != null).ToList().ForEach(x => x.WelcomeCard = new WelcomeCard(x, client.GetGuild(x.GuildID)));
            CurrentGuildSettings.Where(x => arg.GetGuild(x.GuildID) != null && x.WelcomeCard.BackgroundUrl == null ).ToList().ForEach(x => x.WelcomeCard = new WelcomeCard(x, client.GetGuild(x.GuildID)));
            CurrentGuildSettings.Where(x => x.leaveMessage == null && arg.GetGuild(x.GuildID) != null).ToList().ForEach(x => x.leaveMessage = new LeaveMessage(x, client.GetGuild(x.GuildID)));


            foreach(var gs in CurrentGuildSettings)
                foreach (var itm in CustomCommandService.Modules)
                    if (!gs.ModulesSettings.ContainsKey(itm.Key))
                        gs.ModulesSettings.Add(itm.Key, true);

            StateHandler.SaveObject<List<GuildSettings>>("guildsettings", CurrentGuildSettings);
        }
        private static string FormatJson(string json)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }
        public static async Task HandleFailedGql(string q, string method, string type, string StatusCode, List<object> error = null)
        {
            await client.GetGuild(724798166804725780).GetTextChannel(733154982249103431).SendMessageAsync("", false, new EmbedBuilder()
            {
                Title = "Failed GraphQl!",
                Description = $"Failed to {type} {method}! Server sent: {StatusCode}\n\n**Json**```json\n{string.Join("", FormatJson(q).Take(1800))}...```" +
                $"{(error == null ? "" : $"\n**Error**\n```json\n{string.Join("\n\n", error)}```")}",
                Color = Color.Red
            }.WithCurrentTimestamp().Build());
        }

        //private async Task Client_ShardDisconnected(Exception arg1, DiscordSocketClient arg2)
        //    => isReady = false;

        public static GuildSettings GetGuildSettings(ulong guildID)
        {
            if (CurrentGuildSettings.Any(x => x.GuildID == guildID))
                return CurrentGuildSettings.Find(x => x.GuildID == guildID);
            else
                return null;
        }
        public void LoadGuildSettings()
        {
            try
            {
                CurrentGuildSettings = StateHandler.LoadObject<List<GuildSettings>>("guildsettings");
            }
            catch
            {
                CurrentGuildSettings = new List<GuildSettings>();
            }
        }
        private async Task Ready(DiscordSocketClient arg)
        {
            handlerService.CreateHandlers();

            foreach (var guild in arg.Guilds)
                if (!CurrentGuildSettings.Any(x => x.GuildID == guild.Id))
                    new GuildSettings(guild);
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
                Logger.Write("Fired command before bot was ready!", Logger.Severity.Error);
                return;
            }

            var msg = arg as SocketUserMessage;
            if (msg == null) return;
            ShardedCommandContext context = new ShardedCommandContext(client, msg);
            if (context.IsPrivate)
                return;

            var s = CurrentGuildSettings.Find(x => x.GuildID == context.Guild.Id);
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
                new Thread( async () =>
                {
                   var resp = await service.ExecuteAsync(context, s);
                   Logger.Write($"Command Result: {resp.Result} - Command: {arg.Content}");

                   if(resp.Result == CommandStatus.MissingGuildPermission)
                   {
                        await context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "**Missing Permissions**",
                            Color = Color.Red,
                            Description = $"{client.CurrentUser.Username} is missing these permissions:\n{resp.ResultMessage}This command will not work until you give {client.CurrentUser.Username} these permissions!"
                        }.WithCurrentTimestamp().Build());
                   }
                   else if (resp.Result == CommandStatus.InvalidPermissions)
                       await context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                       {
                           Title = "**You do not have permission**",
                           Description = @"Looks like you don't have permission for this command.",
                           Color = Color.Red,
                           Timestamp = DateTimeOffset.Now,
                       }.Build());

                   else if (resp.Result == CommandStatus.Disabled)
                   {
                       await context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                       {
                           Title = "**Sorry!**",
                           Description = @"That module is disabled!",
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
                                   Value = $"{CommandModuleBase.ReadCurrentCommands(s.Prefix).Find(x => x.CommandName == msg.Content.Split(' ')[0].Remove(0,1)).CommandHelpMessage}"
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
                                Value = $"{CommandModuleBase.ReadCurrentCommands(s.Prefix).Find(x => x.CommandName == msg.Content.Split(' ')[0].Remove(0,1)).CommandHelpMessage}"
                            }
                        },
                           Color = Color.Red,
                           Timestamp = DateTimeOffset.Now,
                       }.Build());
                   }
                }).Start();
            }
        }
    }
}
