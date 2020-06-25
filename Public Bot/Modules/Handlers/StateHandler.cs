using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
    }
}
