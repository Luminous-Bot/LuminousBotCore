using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Public_Bot;
using Public_Bot.Modules.Handlers;

namespace Public_Bot
{
    class Program
    {
        public static DateTime StartTime;
        static void Main(string[] args)
        {
            StartTime = DateTime.UtcNow;
            while (true)
            {
                try
                {
                    new Program().Startup().GetAwaiter().GetResult();
                }
                catch(Exception ex)
                {
                    Logger.WriteError("Error on Entrypoint!", ex, Logger.Severity.Critical);
                }
                finally
                {
                    Logger.Write("Retrying in 5", Logger.Severity.Warn);
                    Task.Delay(5000).GetAwaiter().GetResult();
                }
            }
        }
        CustomCommandService _service;
        HandlerService handlerService;
        DiscordShardedClient _client;
        CommandHandler _handler;
        public async Task Startup()
        {
            try
            {
                Logger.Write("Starting to load config..", Logger.Severity.Log);
                ConfigLoader.LoadConfig();
                Logger.Write("Loaded config!", Logger.Severity.Log);

                _client = new DiscordShardedClient(new DiscordSocketConfig()
                {
                    LogLevel = LogSeverity.Debug,
                    DefaultRetryMode = RetryMode.RetryRatelimit,
                    TotalShards = 1,
                    MessageCacheSize = 100,
                    ExclusiveBulkDelete = true,
                    AlwaysDownloadUsers = true,
                    //ShardId = 0,
                });

                _client.Log += _client_Log;

                _service = new CustomCommandService(new Settings()
                {
                    DefaultPrefix = '!',
                    AllowCommandExecutionOnInvalidPermissions = false,
                    DMCommands = false,
                    
                });

                handlerService = new HandlerService(_client);

                ReactionService.Create(_client);

                await _client.LoginAsync(TokenType.Bot, ConfigLoader.Token);

                await _client.StartAsync();

                _handler = new CommandHandler(_service, _client, handlerService);
                Logger.Write($"Starter Ready", Logger.Severity.Log);
                await Task.Delay(-1);
            }
            catch (Exception ex)
            {
                Logger.WriteError("Error on startup", ex, Logger.Severity.Critical);
            }
        }

        private async Task _client_Log(LogMessage msg)
        {
            if (msg.Message == null)
                return;
            if(!msg.Message.StartsWith("Received Dispatch"))
                Logger.Write($"[Svt: {msg.Severity}".PadRight(14) + $" Src: {msg.Source}".PadRight(16) + $" Ex: {msg.Exception}]" + " - " + msg.Message, Logger.Severity.DiscordAPI);
        }
    }
}
