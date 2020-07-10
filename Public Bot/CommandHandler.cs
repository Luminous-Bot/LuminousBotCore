using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Public_Bot.Modules.Handlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

            client.MessageReceived += CheckCommandAsync;

            client.ShardReady += Ready;
            client.ShardDisconnected += Client_ShardDisconnected;
            LoadGuildSettings();

            Logger.Write($"Command Handler Ready", Logger.Severity.Log);
        }

        private async Task Client_ShardDisconnected(Exception arg1, DiscordSocketClient arg2)
        {
            isReady = false;
        }

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
                return;

            var msg = arg as SocketUserMessage;
            if (msg == null) return;
            ShardedCommandContext context = new ShardedCommandContext(client, msg);
            if (context.IsPrivate)
                return;

            var s = CurrentGuildSettings.Find(x => x.GuildID == context.Guild.Id);

            if (arg.Content.StartsWith(s.Prefix) || arg.Content.StartsWith($"<@{client.CurrentUser.Id}>") || arg.Content.StartsWith($"<@!{client.CurrentUser.Id}>"))
            {
                var resp = await service.ExecuteAsync(context, s);
                Logger.Write($"Command Result: {resp.Result} - Command: {arg.Content}");

                if (resp.Result == CommandStatus.InvalidPermissions)
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
            }
        }
    }
}
