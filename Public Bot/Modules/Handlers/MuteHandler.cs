using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using System.Threading.Tasks;
using System.Timers;
using Discord.WebSocket;
using System.Linq;

namespace Public_Bot.Modules.Handlers
{
    [DiscordHandler]
    class MuteHandler
    {
        public class MutedUser
        {
            public ulong UserID { get; set; }
            public ulong GuildID { get; set; }
            public DateTime Time { get; set; }
            public ulong MutedRoleID { get; set; }
        }
        public static List<MutedUser> CurrentMuted { get; set; }
        private Dictionary<ulong, Timer> MuteTimers = new Dictionary<ulong, Timer>();
        public static DiscordShardedClient client { get; set; }
        public MuteHandler(DiscordShardedClient c)
        {
            client = c;
            //load init
            try
            {
                CurrentMuted = StateHandler.LoadObject<List<MutedUser>>("Muted");
            }
            catch
            {
                CurrentMuted = new List<MutedUser>();
            }
            Logger.Write($"Starting Muted init with {CurrentMuted.Count}", Logger.Severity.Log);

            LoadMuted();
        }
        public static void SaveMuted()
        {
            StateHandler.SaveObject<List<MutedUser>>("Muted", CurrentMuted);
        }
        public void LoadMuted()
        {
            foreach (var mt in CurrentMuted.ToArray())
            {
                //get dt
                var desttime = mt.Time - DateTime.UtcNow;
                if (desttime.TotalMilliseconds <= 0)
                {
                    //unmute
                    Unmute(mt).GetAwaiter().GetResult();
                }
                else
                {
                    Timer t = new Timer()
                    {
                        AutoReset = false,
                        Interval = desttime.TotalMilliseconds
                    };
                    var usr = mt.UserID;

                    t.Elapsed += (object sender, ElapsedEventArgs ag) =>
                    {
                        Unmute(mt).GetAwaiter().GetResult();
                        t.Dispose();
                    };
                    t.Start();

                }
            }
        }
        public static async Task SetupMutedRole(IGuild g, ulong completeChan, ulong User)
        {
            //create role
            var roles = g.Roles.Where(x => x.Permissions.Administrator || x.Permissions.ManageChannels);
            //roles.OrderByDescending(x => x.Position);
            var num = roles.OrderByDescending(x => x.Position);
            var pos = num.Last().Position;
            var role = await g.CreateRoleAsync($"Muted by {client.CurrentUser.Username}", new Discord.GuildPermissions(3212288), Color.DarkerGrey, false, false);
            await role.ModifyAsync(x => x.Position = pos);
            //apply to channels
            var chn = await g.GetTextChannelsAsync();
            foreach(var channel in chn)
            {
                await channel.AddPermissionOverwriteAsync(role, new OverwritePermissions(66560, 2048));
                await Task.Delay(3000);
            }

            CommandHandler.CurrentGuildSettings[CommandHandler.CurrentGuildSettings.IndexOf(CommandHandler.CurrentGuildSettings.Find(x => x.GuildID == g.Id))].MutedRoleID = role.Id;
            StateHandler.SaveObject<List<GuildSettings>>("guildsettings", CommandHandler.CurrentGuildSettings);

            var c = await g.GetTextChannelAsync(completeChan);
            await c.SendMessageAsync($"<@{User}>", false, new EmbedBuilder()
            {
                Title = "Complete!",
                Description = $"Ive set up the {role.Mention} in {chn.Count} Text channels!"
            }.Build());
        }
        public static async Task Unmute(MutedUser user)
        {
            CurrentMuted.Remove(user);
            SaveMuted();
            Logger.Write("Saved Muted State");
            var usr = client.GetGuild(user.GuildID).GetUser(user.UserID);
            try
            {
                if (usr.Roles.Any(x => x.Id == user.MutedRoleID))
                    await usr.RemoveRoleAsync(client.GetGuild(user.GuildID).GetRole(user.MutedRoleID));
                else
                    return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }
            try
            {
                await usr.SendMessageAsync($"**You have been Unmuted on {client.GetGuild(user.GuildID).Name}**");
            }
            catch { }
        }
        public static void AddNewMuted(ulong id, DateTime unmutetime, GuildSettings settings)
        {
            var desttime =  unmutetime - DateTime.UtcNow;
            var mu = new MutedUser()
            {
                GuildID = settings.GuildID,
                MutedRoleID = settings.MutedRoleID,
                Time = unmutetime,
                UserID = id
            };
            CurrentMuted.Add(mu);
            SaveMuted();
            if (desttime.TotalMilliseconds <= 0)
            {
                //unmute
                Unmute(mu).GetAwaiter().GetResult();
            }
            else
            {
                Timer t = new Timer()
                {
                    AutoReset = false,
                    Interval = desttime.TotalMilliseconds
                };
                var usr = id;

                t.Elapsed += (object sender, ElapsedEventArgs ag) =>
                {
                    Unmute(mu).GetAwaiter().GetResult();
                    t.Dispose();
                };
                t.Start();
            }
        }
    }
}
