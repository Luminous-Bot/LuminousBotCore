using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            Logger.Write("Sending Gql Query", Logger.Severity.State);
            var res = await client.PostAsync(Url, new StringContent(q, Encoding.UTF8, "application/json"));
            if (res.IsSuccessStatusCode)
            {
                var rtn = JsonConvert.DeserializeObject<GqlBase<T>>(await res.Content.ReadAsStringAsync());
                Logger.Write($"Got Status {res.StatusCode}!", Logger.Severity.State);
                return rtn.Data;
            }
            else
            {
                Logger.Write($"Got Status {res.StatusCode}!", Logger.Severity.Error);
                return default;
            }
        }
        public static T Mutate<T>(string q)
            => MutateAsync<T>(q).GetAwaiter().GetResult();
        public static async Task<T> MutateAsync<T>(string q)
        {
            Logger.Write("Sending Gql Mutation", Logger.Severity.State);
            var res = await client.PostAsync(Url, new StringContent(q, Encoding.UTF8, "application/json"));
            if (res.IsSuccessStatusCode)
            {
                var rtn = JsonConvert.DeserializeObject<GqlBase<T>>(await res.Content.ReadAsStringAsync());
                Logger.Write($"Got Status {res.StatusCode}!", Logger.Severity.State);
                return rtn.Data;
            }
            else
            {
                Logger.Write($"Got Status {res.StatusCode}!", Logger.Severity.Error);
                return default;
            }
        }
        public static bool Exists<T>(string q)
        {
            var res = Query<T>(q);
            if (res == null)
                return false;
            return true;
        }
        private class GqlBase<T>
        {
            public T Data { get; set; }
        }
    }
}
