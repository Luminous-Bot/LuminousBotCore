﻿using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Public_Bot
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DiscordHandler : Attribute { }

    public class HandlerService
    {
        private bool Loaded = false;
        public static List<Type> CurrentLoadedHandlers = new List<Type>();
        public DiscordShardedClient client { get; set; }
        
        public HandlerService(DiscordShardedClient _client)
        {
            this.client = _client;

            //CreateHandlers();
        }
        public void CreateHandlers()
        {
            if(!Loaded)
            {
                List<Type> typs = new List<Type>();
                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    foreach (Type type in assembly.GetTypes())
                    {
                        var attribs = type.GetCustomAttributes(typeof(DiscordHandler), false);
                        if (attribs != null && attribs.Length > 0)
                        {
                            // add to a cache.
                            typs.Add(type);
                        }
                    }
                }
                foreach (var handler in typs)
                {
                    var inst = Activator.CreateInstance(handler, new object[] { client });
                    Logger.Write($"Handler {handler.Name} Loaded!");
                    CurrentLoadedHandlers.Add(inst.GetType());
                }
                Loaded = true;
            }
        }
    }
}
