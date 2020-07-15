using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Public_Bot.Modules.Handlers
{
    class StateHandler
    {
        public static string StateFolder = $"{ConfigLoader.DataDirectoryPath}{Path.DirectorySeparatorChar}State";

        public static void SaveObject<T>(string name, T value)
        {
            string path = $"{StateFolder}{Path.DirectorySeparatorChar}{name}.state";
            if (!File.Exists(path))
                File.Create(path).Close();
            string json = JsonConvert.SerializeObject(value);
            Logger.Write($"Saved the object \"{name}\"!", Logger.Severity.Log);
            File.WriteAllText(path, json);
        }
        public static T LoadObject<T>(string name)
        {
            string path = $"{StateFolder}{Path.DirectorySeparatorChar}{name}.state";
            if (!File.Exists(path))
            {
                File.Create(path).Close();
                throw new Exception("State object didnt exists.. uhm shit..");
            }
            string cont = File.ReadAllText(path);
            if (cont == "")
                throw new Exception("State object was empty.");
            T returnObject = JsonConvert.DeserializeObject<T>(cont);
            if (returnObject == null)
                throw new Exception("State object was null.");
            Logger.Write($"Loaded the object \"{name}\"!", Logger.Severity.Log);
            return returnObject;
        }
        public class StateWrapperClass<Type>
        {
            public StateWrapperClass(string name, string method, Type data)
            {
                this.Method = method;
                this.Name = name;
                this.Data = data;
            }
            public string Method { get; set; }
            public string Name { get; set; }
            public Type Data { get; set; }
        }
        private static HttpClient client = new HttpClient();
        private static string url = "";
        public abstract class SaveableObject<Type>
        {
            public bool Save(Type data)
                => SaveAsync(data).GetAwaiter().GetResult();
            public async Task<bool> SaveAsync(Type data)
            {
                var wrp = new StateWrapperClass<Type>(data.GetType().Name, "Save", data);
                string Content = JsonConvert.SerializeObject(wrp);
                var res = await client.PostAsync(url, new StringContent(Content, Encoding.UTF8, "application/json"));
                if (res.IsSuccessStatusCode)
                    return true;
                return false;
            }
        }
        public abstract class LoadableObject<Type>
        {
            public Type Load()
                => LoadAsync().GetAwaiter().GetResult();
            public async Task<Type> LoadAsync()
            {
                var wrp = new StateWrapperClass<Type>(typeof(Type).Name, "Load", default);
                string Content = JsonConvert.SerializeObject(wrp);
                var res = await client.PostAsync(url, new StringContent(Content, Encoding.UTF8, "application/json"));
                if (res.IsSuccessStatusCode)
                {
                    string content = await res.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Type>(content);
                }
                return default;
            }
        }
    }
}