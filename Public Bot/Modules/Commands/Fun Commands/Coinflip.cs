using Discord;
using Discord.WebSocket;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Linq;
using Discord.Commands;
using WikiDotNet;
namespace Public_Bot.Modules.Commands.Fun_Commands
{
    [DiscordCommandClass("🤽🏼 Fun 🤽🏼", "Commands to use to spice up your server.")]
    public class Coinflip : CommandModuleBase
    {
        private readonly Random _random = new Random();
        [DiscordCommand("coinflip", commandHelp = "`(PREFIX)coinflip`", description = "Flips a coin")]
        public async Task CoinFlip()
        {
            bool chancheck = Context.Guild.GetTextChannel(Context.Channel.Id).IsNsfw;
            var value = _random.Next(0, 2);
            EmbedBuilder coin = new EmbedBuilder()
                .WithColor(Blurple)
                .WithTitle("Coin Flip!");

            switch (value)
            {
                case 1:

                    coin.WithDescription("Heads!");

                    if (chancheck)
                    {
                        coin.WithImageUrl("https://images-ext-2.discordapp.net/external/yotLGOWZxrYwnzEO-RnVKt_E0l7p-vDCiVuOEcJpsP8/https/cdn.hapsy.net/e16ef7e1-3252-4cc7-b3f4-e8a717628634?width=1133&height=1133");
                        break;
                    }

                    coin.WithImageUrl("https://realflipacoin.net/media/assets/coin-mid-0.png?a");
                    break;
                case 0:
                    coin.WithDescription("Tails!");
                    if (chancheck)
                    {
                        coin.WithImageUrl("https://images-ext-1.discordapp.net/external/AtQjFLJNpW2o8EqdSK0sVpKGhWIE0DG9IqQ3cB1msKM/https/cdn.hapsy.net/66e73970-8bb8-4ae9-b2c3-b71c60ce19d5?width=1133&height=1133");
                        break;
                    }
                    coin.WithImageUrl("https://realflipacoin.net/media/assets/coin-mid-1.png?a");
                    break;
            }

            await Context.Channel.SendMessageAsync("", false, coin.Build());
        }
    }
}