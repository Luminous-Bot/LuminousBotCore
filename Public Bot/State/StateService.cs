using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot
{
    public class StateService
    {
        private static HttpClient client = new HttpClient();
        public static string Url
            => ConfigLoader.StateUrl;
        public static T Query<T>(string q)
            => QueryAsync<T>(q).GetAwaiter().GetResult();
        public static async Task<T> QueryAsync<T>(string q)
        {
            string tName = typeof(T).Name;
            if (typeof(T).Name.Contains("List"))
                tName = typeof(T).GenericTypeArguments[0].Name + "[]";

            Logger.Write($"Sending Gql Query for {tName}", Logger.Severity.State);
            var res = await client.PostAsync(Url, new StringContent(q, Encoding.UTF8, "application/json"));
            if (res.IsSuccessStatusCode)
            {
                string cntnt = await res.Content.ReadAsStringAsync();
                var rtn = JsonConvert.DeserializeObject<GqlBase<T>>(cntnt);
                Logger.Write($"Got Status {res.StatusCode}!", Logger.Severity.State);
                if (rtn.data == null)
                    return default;
                return rtn.data.First().Value;
            }
            else
            {
                Logger.Write($"Got Status {res.StatusCode}!", Logger.Severity.Error);
                await CommandHandler.HandleFailedGql(q, typeof(T).Name, "query", res.StatusCode.ToString());
                return default;
            }
        }
        public static T Mutate<T>(string q)
            => MutateAsync<T>(q).GetAwaiter().GetResult();
        public static async Task<T> MutateAsync<T>(string q)
        {
            string tName = typeof(T).Name;
            if (typeof(T).Name.Contains("List"))
                tName = typeof(T).GenericTypeArguments[0].Name + "[]";

            Logger.Write($"Sending Gql Mutation for {tName}", Logger.Severity.State);
            var res = await client.PostAsync(Url, new StringContent(q, Encoding.UTF8, "application/json"));
            var rtn = JsonConvert.DeserializeObject<GqlBase<T>>(await res.Content.ReadAsStringAsync());
            if (res.IsSuccessStatusCode && rtn.errors == null)
            {
                Logger.Write($"Got Status {res.StatusCode}!", Logger.Severity.State);
                if (rtn.data == null)
                    return default;
                return rtn.data.First().Value;
            }
            else
            {
                Logger.Write($"Got Status {res.StatusCode}!", Logger.Severity.Error);
                await CommandHandler.HandleFailedGql(q, typeof(T).Name, "mutation", res.StatusCode.ToString(), rtn.errors);
                return default;
            }
        }
        public static async Task ExecuteNoReturnAsync<T>(string q)
        {
            string tName = typeof(T).Name;
            if (typeof(T).Name.Contains("List"))
                tName = typeof(T).GenericTypeArguments[0].Name + "[]";

            Logger.Write($"Sending Raw GQL for {tName}", Logger.Severity.State);
            var res = await client.PostAsync(Url, new StringContent(q, Encoding.UTF8, "application/json"));
            var rtn = JsonConvert.DeserializeObject<GqlError>(await res.Content.ReadAsStringAsync());
            if (res.IsSuccessStatusCode && rtn.errors == null)
            {
                Logger.Write($"Got Status {res.StatusCode}!", Logger.Severity.State);
            }
            else
            {
                Logger.Write($"Got Status {res.StatusCode}!", Logger.Severity.Error);
                await CommandHandler.HandleFailedGql(q, typeof(T).Name, "mutation", res.StatusCode.ToString(), rtn.errors);
            }
        }
        public static bool Exists<T>(string q)
        {
            var res = Query<T>(q);
            if (res == null)
                return false;
            return true;
        }
        public static bool Exists(string q)
        {
            var res = Query<ExistBase>(q);
            return res.result.Values.First();
        }
        private class ExistBase
        {
            public Dictionary<string, bool> result { get; set; }
        }
        private class GqlError
        {
            public List<object> errors { get; set; }
        }
        private class GqlBase<T>
        {
            public Dictionary<string, T> data { get; set; }
            public List<object> errors { get; set; }
        }

    }
}
