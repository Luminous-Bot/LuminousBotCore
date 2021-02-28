using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot
{
    public struct GQLReturn<T>
    {
        public T? Value;
        public bool HasValue { get; }

        public GQLReturn(bool hasValue, T val = default(T))
        {
            this.HasValue = hasValue;

            // This might set classes to their default props because of default(T)?
            this.Value = val;
        }
        public GQLReturn(T val)
        {
            this.HasValue = true;
            this.Value = val;
        }
    }

    public class StateService
    {
        private static HttpClient client = new HttpClient();
        public static string Url
            => ConfigLoader.StateUrl;

        public static GQLReturn<T> Query<T>(string q, bool HighPriority = false, int Try = 1)
            => QueryAsync<T>(q, HighPriority, Try).GetAwaiter().GetResult();
        public static async Task<GQLReturn<T>> QueryAsync<T>(string q, bool HighPriority = false, int Try = 1)
        {
            string _stack = Environment.StackTrace;
            string tName = typeof(T).Name;
            if (typeof(T).Name.Contains("List"))
                tName = typeof(T).GenericTypeArguments[0].Name + "[]";

            //Logger.Write($"Sending Gql Query for {tName}", Logger.Severity.State);
            var res = await client.PostAsync(Url, new StringContent(q, Encoding.UTF8, "application/json"));
            string cntnt = await res.Content.ReadAsStringAsync();
            if(res.StatusCode == System.Net.HttpStatusCode.BadGateway && HighPriority)
            {
                if(Try < 5)
                {
                    await Task.Delay(500);
                    return await QueryAsync<T>(q, HighPriority, Try + 1);
                }
            }
            if (res.IsSuccessStatusCode)
            {
                GqlBase<T> rtn;
                try
                {
                    rtn = JsonConvert.DeserializeObject<GqlBase<T>>(cntnt);
                }
                catch(Exception x)
                {
                    Logger.Write($"Got Exception on QueryAsync: {x}");
                    return new GQLReturn<T>(false);
                }
                //Logger.Write($"Got Status {res.StatusCode}!", Logger.Severity.State);
                if (rtn.data == null)
                    return default;
                return new GQLReturn<T>(true, rtn.data.First().Value);
            }
            else
            {
                Logger.Write($"Got Status {res.StatusCode}!", Logger.Severity.Error);
                await CommandHandler.HandleFailedGql(q, typeof(T).Name, "query", res.StatusCode.ToString(), cntnt, _stack, Try);
                return default;
            }
        }
        public static GQLReturn<T> Mutate<T>(string q, bool HighPriority = false, int Try = 1)
            => MutateAsync<T>(q, HighPriority, Try).GetAwaiter().GetResult();
        public static async Task<GQLReturn<T>> MutateAsync<T>(string q, bool HighPriority = false, int Try = 1)
        {
            string _stack = Environment.StackTrace;
            string tName = typeof(T).Name;
            if (typeof(T).Name.Contains("List"))
                tName = typeof(T).GenericTypeArguments[0].Name + "[]";

            //Logger.Write($"Sending Gql Mutation for {tName}", Logger.Severity.State);
            var res = await client.PostAsync(Url, new StringContent(q, Encoding.UTF8, "application/json"));
            var cont = await res.Content.ReadAsStringAsync();
            if (res.StatusCode == System.Net.HttpStatusCode.BadGateway && HighPriority)
            {
                if (Try < 5)
                {
                    await Task.Delay(500);
                    return await MutateAsync<T>(q, HighPriority, Try + 1); 
                }
            }
            if (res.IsSuccessStatusCode)
            {
                GqlBase<T> rtn;
                
                try
                {
                     rtn = JsonConvert.DeserializeObject<GqlBase<T>>(cont);
                }
                catch(Exception x)
                {
                    Logger.Write($"Got Exception on MutateAsync {x}", Logger.Severity.Warn);
                    return new GQLReturn<T>(false);
                }
                if(rtn.errors == null)
                {
                    //Logger.Write($"Got Status {res.StatusCode}!", Logger.Severity.State);
                    if (rtn.data == null)
                        return default;
                    return new GQLReturn<T>(rtn.data.First().Value);
                }
                else
                {
                    Logger.Write($"Got Status {res.StatusCode}!", Logger.Severity.Error);
                    await CommandHandler.HandleFailedGql(q, typeof(T).Name, "mutation", res.StatusCode.ToString(), cont, _stack, Try);
                    return default;
                }
            }
            else
            {
                Logger.Write($"Got Status {res.StatusCode}!", Logger.Severity.Error);
                await CommandHandler.HandleFailedGql(q, typeof(T).Name, "mutation", res.StatusCode.ToString(), cont, _stack, Try);
                return default;
            }
        }
        public static async Task ExecuteNoReturnAsync<T>(string q, bool HighPriority = false, int Try = 1)
        {
            string _stack = Environment.StackTrace;
            string tName = typeof(T).Name;
            if (typeof(T).Name.Contains("List"))
                tName = typeof(T).GenericTypeArguments[0].Name + "[]";

            //Logger.Write($"Sending Raw GQL for {tName}", Logger.Severity.State);
            var res = await client.PostAsync(Url, new StringContent(q, Encoding.UTF8, "application/json"));
            string cont = await res.Content.ReadAsStringAsync();
            if (res.StatusCode == System.Net.HttpStatusCode.BadGateway && HighPriority)
            {
                if (Try < 5)
                {
                    await Task.Delay(500);
                    await ExecuteNoReturnAsync<T>(q, HighPriority, Try + 1);
                }
            }
            try
            {
                var rtn = JsonConvert.DeserializeObject<GqlError>(cont);
                if (res.IsSuccessStatusCode && rtn.errors == null)
                {
                    //Logger.Write($"Got Status {res.StatusCode}!", Logger.Severity.State);
                }
                else
                {
                    Logger.Write($"Got Status {res.StatusCode}!", Logger.Severity.Error);
                    await CommandHandler.HandleFailedGql(q, typeof(T).Name, "mutation", res.StatusCode.ToString(), cont, _stack, Try);
                }
            }
            catch(Exception x)
            {
                Logger.Write($"Got Exception on ExecuteNoReturnAsync: {x}", Logger.Severity.Warn);
            }
        }
        public static bool Exists<T>(string q)
        {
            var res = Query<T>(q, true);
            return res.HasValue;
        }

        public static bool Exists(string q)
        {
            var res = Query<ExistBase>(q, true);
            return res.HasValue ? res.Value.result.First().Value : false;
        }
        private class ExistBase
        {
            public Dictionary<string, bool> result { get; set; }
        }
        public class ExistNullBase
        {
            public Dictionary<string, dynamic> result { get; set; }
        }
        private class GqlError
        {
            public List<object> errors { get; set; }
        }
        internal class GqlBase<T>
        {
            public Dictionary<string, T> data { get; set; }
            public List<object> errors { get; set; }
        }

    }
}
