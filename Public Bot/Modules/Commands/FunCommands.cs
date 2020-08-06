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

namespace Public_Bot.Modules.Commands
{

    [DiscordCommandClass("🤽🏼 Fun 🤽🏼", "Commands to use to spice up your server.")]
    public class FunCommands : CommandModuleBase
    {
        // Variables to use later on
        static string[] puns = File.ReadAllLines($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}puns.txt");
        int rot = 0;
        private readonly Random _random = new Random();
        [Alt("meme")]
        [DiscordCommand("reddit", commandHelp = "Usage: `(PREFIX)reddit \n(PREFIX)reddit subs \n(PREFIX)reddit subs add <r/sub> \n(PREFIX)reddit subs remove <r/sub>`", description = "Grabs a random post from reddit")]
        public async Task meme(params string[] args)
        {
            if (args.Length == 0)
            {
                HttpClient client = new HttpClient();
                var sb = new Random().Next(0, GuildSettings.MemeSubreddits.Count);
                var sbrt2 = GuildSettings.MemeSubreddits.ToList();
                var sbrt = sbrt2[sb] + ".json";
                var request = await client.GetAsync(sbrt);
                string response = await request.Content.ReadAsStringAsync();
                Console.WriteLine(response);
                var data = JsonConvert.DeserializeObject<Public_Bot.Modules.Handlers.RedditHandler>(response);
                if (data.Data.Children.Length == 0)
                {
                    GuildSettings.MemeSubreddits.Remove(sbrt.Replace(".json", ""));
                    if (GuildSettings.MemeSubreddits.Count == 0)
                        GuildSettings.MemeSubreddits.Add("https://reddit.com/r/dankmemes");
                    GuildSettings.SaveGuildSettings();
                    await meme(args).ConfigureAwait(false);
                }
                Regex r = new Regex(@"https:\/\/i.redd.it\/(.*?)\.");
                var childs = data.Data.Children.Where(x => r.IsMatch(x.Data.Url.ToString())).ToList();
                if(childs.Count == 0)
                {
                    GuildSettings.MemeSubreddits.Remove(sbrt.Replace(".json", ""));
                    if (GuildSettings.MemeSubreddits.Count == 0)
                        GuildSettings.MemeSubreddits.Add("https://reddit.com/r/dankmemes");
                    GuildSettings.SaveGuildSettings();
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Error",
                        Description = $"Unable to find good posts on [r/{data.Data.Children.First().Data.Subreddit}]({sbrt.Replace(".json", "")}). Because of this we are going to remove it from the list!",
                        Color = Color.Orange,
                        
                    }.WithCurrentTimestamp().Build());
                    return;
                }
                Random rnd = new Random();
                var indx = rnd.Next(0, childs.Count);
                Console.WriteLine($"I: {indx} C: {childs.Count}");
                var post = childs[indx];
                EmbedBuilder b = new EmbedBuilder()
                {
                    Title = $"r/{data.Data.Children.First().Data.Subreddit}",
                    Url = $"{sbrt.Replace(".json", "")}",
                    Description = $"{post.Data.Title.ToString()}",
                    ImageUrl = post.Data.Url.ToString(),
                    Footer = new EmbedFooterBuilder()
                    {
                        Text = $"u/{post.Data.Author}",
                    }
                };

                b.WithCurrentTimestamp();
                b.WithColor(Blurple);

                await Context.Channel.SendMessageAsync("", false, b.Build());
            }
            else
            {
                if (HasExecutePermission)
                {
                    if (args[0].ToLower() == "subs" || args[0].ToLower() == "subreddits")
                    {
                        if (args.Length == 1)
                        {
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Current Subreddits",
                                Color = Blurple,
                                Description = $"Here's the current subreddits for this command:\n```\n{string.Join("\n", GuildSettings.MemeSubreddits)}```\nTo add one do `{GuildSettings.Prefix}reddit {args[0]} add <r/sub>`\n To remove one do `{GuildSettings.Prefix}reddit {args[0]} remove <r/sub>`"
                            }.WithCurrentTimestamp().Build());
                            return;
                        }

                        switch (args[1])
                        {
                            case "add":
                                if (args.Length == 2)
                                {
                                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                    {
                                        Title = "No Parameters.",
                                        Color = Color.Red,
                                        Description = $"Please add some parameters.\nExample: `{GuildSettings.Prefix}reddit {args[0]} add r/yoursub`"
                                    }.WithCurrentTimestamp().Build());
                                    return;
                                }
                                string sb = args[2];
                                if (Regex.IsMatch(sb, "r\\/(\\w*?)$"))
                                {
                                    var request = await new HttpClient().GetAsync($"https://www.reddit.com/{sb}.json");
                                    string response = await request.Content.ReadAsStringAsync();
                                    var data = JsonConvert.DeserializeObject<Public_Bot.Modules.Handlers.RedditHandler>(response);
                                    if (!data.Data.Children.Any())
                                    {
                                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                        {
                                            Title = "Invalid Subreddit!",
                                            Color = Color.Red,
                                            Description = $"The subreddit `{sb}` doesn't exist!"
                                        }.WithCurrentTimestamp().Build());
                                        return;
                                    }
                                    if (GuildSettings.MemeSubreddits.Contains($"https://www.reddit.com/{sb}"))
                                    {
                                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                        {
                                            Title = "Subreddit already added!",
                                            Color = Color.Red,
                                            Description = $"The subreddit `{sb}` is already added"
                                        }.WithCurrentTimestamp().Build());
                                        return;
                                    }
                                    GuildSettings.MemeSubreddits.Add($"https://www.reddit.com/{sb}");
                                    GuildSettings.SaveGuildSettings();
                                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                    {
                                        Title = "Success!",
                                        Color = Color.Green,
                                        Description = $"Added [{sb}](https://www.reddit.com/{sb}) to the list of subreddits"
                                    }.WithCurrentTimestamp().Build());
                                    return;
                                }
                                else
                                {
                                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                    {
                                        Title = "Invalid format!",
                                        Color = Color.Red,
                                        Description = $"Please use the format `r/yourSubreddit` without spaces!\nExample: `{GuildSettings.Prefix}reddit {args[0]} add r/yoursub`"
                                    }.WithCurrentTimestamp().Build());
                                    return;
                                }
                            case "remove":
                                if (args.Length == 2)
                                {
                                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                    {
                                        Title = "No Parameters.",
                                        Color = Color.Red,
                                        Description = $"Please add some parameters.\nExample: `{GuildSettings.Prefix}reddit {args[0]} remove r/yoursub`"
                                    }.WithCurrentTimestamp().Build());
                                    return;
                                }
                                string sub = args[2];
                                if (Regex.IsMatch(sub, "r\\/(\\w*?)$"))
                                {
                                    if (GuildSettings.MemeSubreddits.Count == 1)
                                    {
                                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                        {
                                            Title = "Can't have empty list!",
                                            Color = Color.Red,
                                            Description = $"You can't remove the last subreddit in the list!"
                                        }.WithCurrentTimestamp().Build());
                                        return;
                                    }
                                    if (!GuildSettings.MemeSubreddits.Contains($"https://www.reddit.com/{sub}"))
                                    {
                                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                        {
                                            Title = "Subreddit not added!",
                                            Color = Color.Red,
                                            Description = $"The Subreddit `{sub}` isn't in the list!"
                                        }.WithCurrentTimestamp().Build());
                                        return;
                                    }
                                    GuildSettings.MemeSubreddits.Remove($"https://www.reddit.com/{sub}");
                                    GuildSettings.SaveGuildSettings();
                                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                    {
                                        Title = "Success!",
                                        Color = Color.Green,
                                        Description = $"Removed [{sub}](https://www.reddit.com/{sub}) from the list of meme subreddits"
                                    }.WithCurrentTimestamp().Build());
                                    return;
                                }
                                else
                                {
                                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                    {
                                        Title = "Invalid format!",
                                        Color = Color.Red,
                                        Description = $"Please use the format `r/yourSubreddit` without spaces!\nExample: `{GuildSettings.Prefix}reddit {args[0]} remove r/yoursub`"
                                    }.WithCurrentTimestamp().Build());
                                    return;
                                }
                            case "list":
                                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                {
                                    Title = "Current Subreddits",
                                    Color = Blurple,
                                    Description = $"Here's the current subreddits for this command:\n```\n{string.Join("\n", GuildSettings.MemeSubreddits)}```\nTo add one do `{GuildSettings.Prefix}reddit {args[0]} add <r/sub>`"
                                }.WithCurrentTimestamp().Build());
                                return;

                        }
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Invalid Permission",
                        Description = "You do not have permission to change the subreddits!",
                        Color = Color.Red
                    }.WithCurrentTimestamp().Build());
                }
            }
        }

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
                        coin.WithImageUrl("https://cdn.hapsy.net/ee8f064c-e010-4699-8ccd-f68a30823da7");
                        break;
                    }

                    coin.WithImageUrl("https://realflipacoin.net/media/assets/coin-mid-0.png?a");
                    break;
                case 0:
                    coin.WithDescription("Tails!");
                    if (chancheck)
                    {
                        coin.WithImageUrl("https://cdn.hapsy.net/99420dee-1c24-41cd-a7af-b13c7a6f9528");
                        break;
                    }
                    coin.WithImageUrl("https://realflipacoin.net/media/assets/coin-mid-1.png?a");
                    break;
            }

            await Context.Channel.SendMessageAsync("", false, coin.Build());
        }

        [DiscordCommand("8ball", commandHelp = "`(PREFIX)8ball`", description = "Ask the all knowing 8-ball")]

        public async Task eightball(params string[] args)
        {

            var sb = new StringBuilder();

            var embed = new EmbedBuilder();

            var replies = new List<string>();

            //Possible Replies
            replies.Add("Yes");
            replies.Add("No");
            replies.Add("Lol idk");
            replies.Add("69% Chance");

            embed.WithColor(Blurple);
            embed.WithTitle("8-Ball");

            sb.AppendLine($"{Context.User.Username}");
            sb.AppendLine();

            if (args == null)
            {
                sb.AppendLine("Sorry, can't answer air");
            }
            else
            {
                var ans = replies[new Random().Next(replies.Count - 1)];

                sb.AppendLine($"Your question is **{string.Join(' ', args)}**");
                sb.AppendLine();
                sb.AppendLine($"The answer is **{ans}**");
            }

            embed.Description = sb.ToString();

            await Context.Channel.SendMessageAsync("", false, embed.Build());

        }

        [DiscordCommand("pun", commandHelp = "(PREFIX)pun", description = "Sends a random pun from our collection!")]

        public async Task Pun()
        {
            try
            {
                Random rand = new Random();
                int index = rand.Next(puns.Length);

                EmbedBuilder b = new EmbedBuilder()
                    .WithTitle($"{puns[index]}")
                    .WithColor(Blurple);

                await Context.Channel.SendMessageAsync("", false, b.Build());
            } catch
            {
                EmbedBuilder e = new EmbedBuilder()
                {
                    Title = "puns.txt doesn't exist, please contact quin#3017"
                };
                await Context.Channel.SendMessageAsync("", false, e.Build());
            }
        }

        [DiscordCommand("wiki", commandHelp = "(PREFIX)wiki <search>", description = "Searches wikipedia!")]

        public async Task SearchWikipedia(params string[] arg1)
        {
            if(arg1.Length == 0)
            {
                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder() 
                {
                    Title = "No Arguments",
                    Description = $"What wiki do you want to search for? Please provide some arguments: `{GuildSettings.Prefix}wiki <wiki>`",
                    Color = Color.Orange
                }.Build());
                return;
            }

            var args = String.Join(' ', arg1);

            EmbedBuilder b = new EmbedBuilder()
                .WithFooter("Not what you wanted? Try searching without spaces.")
                .WithColor(Blurple);
            

            StringBuilder sb = new StringBuilder();

            WikiSearchResponse response = WikiSearcher.Search(args, new WikiSearchSettings
            {
                ResultLimit = 1
            });

            if (response.Query.SearchResults == null)
            {
                b.WithDescription("No results could be found :(");
                await Context.Message.Channel.SendMessageAsync("", false, b.Build());
                return;
            }
            //Extra protection I guess
            if (response.Query.SearchResults.Length == 0)
            {
                b.WithDescription("No results could be found :(");
                await Context.Message.Channel.SendMessageAsync("", false, b.Build());
                return;
            }
            foreach (WikiSearchResult result in response.Query.SearchResults)
            {
                string link =
                    $"{result.Preview}...\n\n";

                string title = $"{result.Title}";

                b.WithTitle(title);

                //There is a character limit of 2048, so lets make sure we don't hit that
                if (sb.Length >= 2048) continue;

                if (sb.Length + link.Length >= 2048) continue;

                sb.Append(link);

                b.WithUrl(result.ConstantUrl("en"));

            }

            b.WithDescription(sb.ToString());
            b.WithCurrentTimestamp();

            await Context.Channel.SendMessageAsync("", false, b.Build());
        }

        //[DiscordCommand("hangman", commandHelp = "(PREFIX)hangman", description = "Starts a game of hangman")]

        //public async Task HangmanAsync(SocketMessage arg)
        //{
        //    await Context.Message.Author.SendMessageAsync("Please respond with the word or phrase you want to use for hangman!");
        //    await Context.Message.Channel.SendMessageAsync("Sent you a dm <3");



        //}

    }
}