using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Public_Bot.Modules.Handlers
{
    class StateHandler
    {
        public static string StateFolder = $"{ConfigLoader.DataDirectoryPath}{Path.DirectorySeparatorChar}State";
        public class StateBucket
        {
            private static List<StateSavingObject> QueuedSaves = new List<StateSavingObject>();
            private static List<StateLoadingObject> QueuedLoads = new List<StateLoadingObject>();

            private static Timer BucketSaveTimer = new Timer() { AutoReset = true, Interval = 20 };
            private static Timer BucketLoadTimer = new Timer() { AutoReset = true, Interval = 20 };

            private static List<string> Writing = new List<string>();
            private static List<string> Reading = new List<string>();

            private static bool saveInit = false;
            private static bool loadInit = false;
            private static bool CurrPush = false;
            private static bool CurrFetch = false;
            internal static async void Push(StateSavingObject obj)
            {
                if (!saveInit)
                    BucketSaveTimer.Elapsed += ClearSaveBucket;
                
                QueuedSaves.Add(obj);
                if (!BucketSaveTimer.Enabled && !CurrPush)
                    BucketSaveTimer.Start();
            }
            internal static async void ClearSaveBucket(object snd, ElapsedEventArgs e)
            {
                if (QueuedSaves.Count == 0)
                {
                    BucketSaveTimer.Stop();
                    return;
                }
                CurrPush = true;
                BucketSaveTimer.Stop();
                var s = QueuedSaves.First();
                await SaveStateFile(s.Name, s.Value);
                QueuedSaves.Remove(s);
                CurrPush = false;
                BucketSaveTimer.Start();
            }
            private static async Task SaveStateFile(string name, object value)
            {
                string path = $"{StateFolder}{Path.DirectorySeparatorChar}{name}.state";
                if (!File.Exists(path))
                    File.Create(path).Close();
                string json = JsonConvert.SerializeObject(value);
                Logger.Write($"Saved the object \"{name}\"!", Logger.Severity.Log);
                await File.WriteAllTextAsync(path, json);
            }

            private static Dictionary<StateLoadingObject, object> Returns = new Dictionary<StateLoadingObject, object>();
            internal static async Task<object> Fetch(StateLoadingObject obj)
            {
                if (!loadInit)
                    BucketLoadTimer.Elapsed += ClearLoadBucket;

                QueuedLoads.Add(obj);
                if (!BucketLoadTimer.Enabled && !CurrFetch)
                    BucketLoadTimer.Start();
                while(!Returns.ContainsKey(obj)) { await Task.Delay(10); }
                var rob = Returns[obj];
                Returns.Remove(obj);
                return rob;
            }
            private static async void ClearLoadBucket(object snd, ElapsedEventArgs e)
            {
                
                if (QueuedLoads.Count == 0)
                {
                    BucketLoadTimer.Stop();
                    return;
                }
                CurrFetch = true;
                BucketLoadTimer.Stop();
                var s = QueuedLoads.First();
                var val = await LoadStateFile(s.Name, s.Type);
                Returns.Add(s, val);
                QueuedLoads.Remove(s);
                CurrFetch = false;
                BucketLoadTimer.Start();
            }
            private static async Task<object> LoadStateFile(string name, Type t)
            {
                string path = $"{StateFolder}{Path.DirectorySeparatorChar}{name}.state";
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                    throw new Exception("State object didnt exists.. uhm shit..");
                }
                string cont = await File.ReadAllTextAsync(path);
                Logger.Write($"Loading {name}... Cont: {new string(cont.Take(20).ToArray())}..", Logger.Severity.Log);

                if (cont == "")
                    return null;
                var returnObject = JsonConvert.DeserializeObject(cont, t);
                if (returnObject == null)
                Logger.Write($"Loaded the object \"{name}\"!", Logger.Severity.Log);
                return returnObject;
            }
            internal class StateLoadingObject
            {
                internal Type Type;
                internal string Name;
                internal StateLoadingObject(string name, Type type)
                {
                    this.Type = type;
                    this.Name = name;
                }

            }
            internal class StateSavingObject
            {
                internal object Value;
                internal string Name;
                internal StateSavingObject(string name, object value)
                {
                    this.Value = value;
                    this.Name = name;
                }

            }
        }
        //public static void SaveObject<T>(string name, T value)
        //{
        //    StateBucket.Push(new StateBucket.StateSavingObject(name,  value));
        //}
        //public static Tpe LoadObject<Tpe>(string name)
        //{
        //    var s = new StateBucket.StateLoadingObject(name, typeof(Tpe));
        //    var res = (Tpe)StateBucket.Fetch(s).GetAwaiter().GetResult();
        //    if (res == null)
        //        throw new Exception("State object was null");
        //    return res;
        //}
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
    }
}
