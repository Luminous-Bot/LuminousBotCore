using Discord.WebSocket;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Public_Bot
{
    [DiscordHandler]
    public class GuildSettingsHelper
    {
        //
#if DEBUG
        private static MongoClient _client = new MongoClient("mongodb://localhost:27017");
#else
        private static MongoClient _client = new MongoClient("mongodb://root:mongodbAdmin1@localhost:27017/?authSource=admin&readPreference=primary");
#endif
        private static IMongoDatabase _database = _client.GetDatabase("luminous-guildsettings");
        private static IMongoCollection<GuildSettings> _collection = _database.GetCollection<GuildSettings>("guildsettings");

        private static FindOptions opt = new FindOptions() { ShowRecordId = false };
        public static DiscordShardedClient client { get; set; }
        private static SingleGuildIDEntityCache<GuildSettings> _cache
             = new SingleGuildIDEntityCache<GuildSettings>();
        public GuildSettingsHelper(DiscordShardedClient c)
        {
            client = c;
        }

        public static bool GuildSettingsExists(ulong GuildId)
        {
            var count = _collection.CountDocuments<GuildSettings>(x => x.GuildID == GuildId);
            return count > 0;
        }

        public static GuildSettings GetGuildSettings(ulong GuildID)
        {
            if (_cache.Any(x => x.GuildID == GuildID))
                return _cache[GuildID];
            
            var result = _collection.Find<GuildSettings>(x => x.GuildID == GuildID, opt);

            if (!result.Any())
                return null;
            _cache.Add(result.First());
            Logger.Write($"Guildsettings {GuildID} Fetched", Logger.Severity.Mongo);

            return result.First();
        }
        public static void SaveGuildSettings(GuildSettings gs)
        {
            var count = _collection.CountDocuments<GuildSettings>(x => x.GuildID == gs.GuildID);

            if (count == 0)
            {
                _collection.InsertOne(gs);
                _cache.AddOrReplace(gs);
                Logger.Write($"Guildsettings {gs.GuildID} Created", Logger.Severity.Mongo);
            }
            else
            {
                Logger.Write($"Guildsettings {gs.GuildID} Updated", Logger.Severity.Mongo);
                _collection.ReplaceOne<GuildSettings>(x => x.GuildID == gs.GuildID, gs);
                _cache.AddOrReplace(gs);
            }
        }
    }
}
