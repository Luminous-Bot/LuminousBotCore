using System;
using System.Collections.Generic;
using System.Linq;
using Discord;
using System.Threading.Tasks;
using Discord.WebSocket;
using System.Data;
using Public_Bot.Modules.Handlers;

namespace Public_Bot.Modules.Commands.Utility_Commands
{
    [DiscordCommandClass("🎚 Utilities 🎚", "Enable some vital features to improve your overall server fuctionality!")]
    public class Rolecard : CommandModuleBase
    {
        [DiscordCommand("rolecard",
            RequiredPermission = true,
            description = "Create, Edit, and Delete your rolecards!",
            // TODO: Change help message if need be
            commandHelp = "`(PREFIX)rolecard`"
        )]
        // create <#channel> <messageId> [<Emote> <Role>]
        // add
        // remove
        // delete
        // list
        public async Task Rolecard_Command(params string[] args)
        {
            if(args.Length == 0)
            {
                // List the guilds role cards
            }

            switch (args[0].ToLower())
            {
                case "create":

                    if(args.Length == 1)
                    {
                        // Show params

                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {

                        }.WithCurrentTimestamp().Build());
                        return;
                    }

                    switch (args.Length)
                    {
                        case 2:
                            // No message id
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "No Message ID",
                                Description = "You didn't provide a Message ID for the role menu!",
                                Color = Color.Red
                            }.WithCurrentTimestamp().Build());
                            return;

                        case 3:
                            // No emoji role pair
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "No Emoji/Roles Provided!",
                                Description = "You didn't provide any emojies and roles for the menu!",
                                Color = Color.Red
                            }.WithCurrentTimestamp().Build());
                            return;

                        case 4:
                            // No role for emote
                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Almost there!",
                                Description = "You need to provide a role to give for an emoji",
                                Color = Color.Red
                            }.WithCurrentTimestamp().Build());
                            return;
                    }
                    
                    var chan = GetChannel(args[1]);
                    
                    if(chan == null)
                    {
                        // Bad channel
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Invalid Channel",
                            Description = "The channel you provided was invalid! Please make sure you spelled it correctly or you provided the correct ID!",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }

                    if(chan.GetType() == typeof(SocketVoiceChannel))
                    {
                        // Bad channel (voice)
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Uhm..",
                            ThumbnailUrl = "https://i.redd.it/yo7gqdmlny341.jpg",
                            Description = "Did you really try to create a reaction card in a voice channel? I can't fathem how that could work.",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }

                    if(!ulong.TryParse(args[2], out var res))
                    {
                        // Bad message id

                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Invalid Message",
                            Description = "The Message ID you provided was invalid! if you cant find the Message Id please read [This Article](https://support.discord.com/hc/en-us/articles/206346498-Where-can-I-find-my-User-Server-Message-ID-) on how to get ID's",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }
                    
                    var textchan = (SocketTextChannel)chan;

                    if(textchan == null)
                    {
                        // Bad channel

                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Invalid Channel",
                            Description = "Don't know what to say here.. somthing about that channel isn't right.",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }


                    var message = await textchan.GetMessageAsync(res);

                    if(message == null)
                    {
                        // Bad message

                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Invalid Message",
                            Description = $"Either the message doesn't exist or {Context.Client.CurrentUser.Username} can't see the message, make sure the bot can see the channel and has Read Message History permission!",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }

                    var emoteRoles = args.Skip(3).ToArray();

                    if(emoteRoles.Length % 2 != 0)
                    {
                        // Bad emote roles pairs
                        await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                        {
                            Title = "Invalid Emote/Role Pairs!",
                            Description = $"\"{emoteRoles.Last()}\" is missing a role to go with it! please make sure each emote is followed by a role!",
                            Color = Color.Red
                        }.WithCurrentTimestamp().Build());
                        return;
                    }

                    RoleCardItem[] items = new RoleCardItem[emoteRoles.Length / 2];
                    var emotes = new List<IEmote>();
                    var testmsg = await Context.Channel.SendMessageAsync("This message is just to test your provided emojis, it will be deleted after the test.");
                    
                    for (int i = 0; i != emoteRoles.Length / 2; i++)
                    {
                        // Try our emotes out
                        IEmote final;
                        if(Emote.TryParse(emoteRoles[i], out var emote))
                        {
                            final = emote;
                        }
                        else
                        {
                            var emoji = new Emoji(emoteRoles[i]);
                            final = emoji;
                        }

                        try
                        {
                            await testmsg.AddReactionAsync(final);
                        }
                        catch
                        {
                            // Bad emote
                            await testmsg.DeleteAsync();

                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Invalid Emote",
                                Description = $"The emote \"{final.Name}\" could not be added! If its a guild specific emote make sure {Context.Client.CurrentUser.Username} is in that server!",
                                Color = Color.Red
                            }.WithCurrentTimestamp().Build());
                            return;
                        }

                        emotes.Add(final);

                        // Get the role
                        var role = GetRole(emoteRoles[i + 1]);

                        if(role == null)
                        {
                            // Bad role

                            await testmsg.DeleteAsync();

                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Invalid Role!",
                                Description = $"The role \"{emoteRoles[i+1]}\" doesn't exist! Please make sure the spelling/Id is correct!",
                                Color = Color.Red
                            }.WithCurrentTimestamp().Build());
                            return;
                        }

                        if (items.Any(x => x != null && x.EmoteID == final.Name))
                        {
                            // Duplicate emoji

                            await testmsg.DeleteAsync();

                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Duplicate Emote!",
                                Description = $"Looks like you put {final} twice, that will cause some issues. Please only use unique emotes/roles",
                                Color = Color.Red
                            }.WithCurrentTimestamp().Build());
                            return;
                        }
                        if(items.Any(x => x != null && x.RoleID == role.Id))
                        {
                            // Duplicate Role

                            await testmsg.DeleteAsync();

                            await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                            {
                                Title = "Duplicate Role!",
                                Description = $"Looks like you put <@{role.Id}> twice, that will cause some issues. Please only use unique emotes/roles",
                                Color = Color.Red
                            }.WithCurrentTimestamp().Build());
                            return;
                        }

                        items[i] = new RoleCardItem()
                        {
                            EmoteID = final.Name,
                            RoleID = role.Id
                        };
                    }

                    await testmsg.DeleteAsync();

                    foreach (var emote in emotes)
                    {
                        if (!message.Reactions.ContainsKey(emote))
                        {
                            try
                            {
                                await message.AddReactionAsync(emote);
                            }
                            catch
                            {
                                // Emote adding error

                                await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                                {
                                    Title = "Failed to add Emote",
                                    Description = $"The bot couldn't add {emote} to the specified message! Make sure the bot has permission to add reactions in {textchan.Mention}",
                                    Color = Color.Red
                                }.WithCurrentTimestamp().Build());
                                return;
                            }
                        }
                    }

                    ReactionRoleCard card = await new ReactionRoleCard()
                    {
                        GuildID = Context.Guild.Id,
                        MessageID = message.Id,
                        Roles = new List<RoleCardItem>(items)
                    }.Create();

                    // Register the newly created card with the Rolecard Handler
                    var _handler = HandlerService.GetHandlerInstance<RolecardHandler>();
                    _handler.AddRankCard(card);
                    
                    await Context.Channel.SendMessageAsync("", false, new EmbedBuilder()
                    {
                        Title = "Success!",
                        Color = Color.Green,
                        Description = $"Rolecard has been created! Here are all the roles:\n\n{string.Join('\n', items.Select(x => $"{x.EmoteID} - <@&{x.RoleID}>"))}",
                        Fields = new List<EmbedFieldBuilder>()
                    }.WithCurrentTimestamp().Build());
                   
                    return;

                case "delete":

                    return;

                case "add":
                    await AddTo(args);
                    return;
                case "addto":
                    await AddTo(args);
                    return;

                case "removefrom":
                    await RemoveFrom(args);
                    return;
                case "remove":
                    await RemoveFrom(args);
                    return;

            }
        }
        public async Task AddTo(string[] args)
        {

        }
        public async Task RemoveFrom(string[] args)
        {

        }
    }
}
