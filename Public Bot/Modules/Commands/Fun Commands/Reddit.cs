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
    public class Reddit : CommandModuleBase
    {
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
                //Console.WriteLine(response);
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
                //Console.WriteLine($"I: {indx} C: {childs.Count}");
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
    }
}