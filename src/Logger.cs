using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Public_Bot
{
    class Logger
    {
        public static string ErrorLogFile = $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}error.txt";
        public enum Severity
        {
            Log,
            Warn, 
            Error,
            Critical,
            DiscordAPI,
            Mongo,
            State
        }
        public static void Write(object msg, Severity s = Severity.Log, ICommandContext context = null)
        {
            if (!File.Exists(ErrorLogFile))
                File.Create(ErrorLogFile).Close();
            string message = $"[{DateTime.Now.ToString("o")} : {s}]".PadRight(50) + $" -> {msg}";
            switch (s)
            {
                case Severity.Critical:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    break;
                case Severity.DiscordAPI:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case Severity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case Severity.Log:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case Severity.Warn:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case Severity.State:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case Severity.Mongo:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;

            }
            if (s == Severity.Error || s == Severity.Critical)
            {
                string errormsg = $"<----------{DateTime.UtcNow.ToString("o")}---------->\n";
                
                if (msg.GetType() == typeof(Exception)) 
                {
                    Exception ex;
                    ex = (Exception)msg;
                    if(context != null)
                        errormsg += $"Command: {context.Message.Content}\n";
                    errormsg += $"Message: {ex.Message}\n";
                    errormsg += $"Stack: {ex.StackTrace}\n";
                    errormsg += $"InnerException?: {(ex.InnerException == null ? "None" : ex.InnerException.Message)}\n\n";
                    File.WriteAllText(ErrorLogFile, File.ReadAllText(ErrorLogFile) + errormsg);
                    Console.WriteLine(errormsg);
                }
                else
                {
                    Console.WriteLine(message);
                }
            }
            else
            {
                Console.WriteLine(message);
            }
        }
        public static void WriteError(string msg, Exception ex = null, Severity s = Severity.Log, ICommandContext context = null)
        {
            if (!File.Exists(ErrorLogFile))
                File.Create(ErrorLogFile).Close();
            if (ex != null)
            {
                switch (s)
                {
                    case Severity.Critical:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        break;
                    case Severity.Error:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.Black;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.Black;
                        break;
                }
                string errormsg = $"<----------{DateTime.UtcNow.ToString("o")}---------->\n";
                errormsg += $"{msg}\n";
                if (context != null)
                    errormsg += $"Command: {context.Message.Content}\n";
                errormsg += $"Message: {ex.Message}\n";
                errormsg += $"Stack: {ex.StackTrace}\n";
                errormsg += $"InnerException?: {(ex.InnerException == null ? "None" : ex.InnerException.Message)}\n\n";
                File.WriteAllText(ErrorLogFile, File.ReadAllText(ErrorLogFile) + errormsg);
                Console.WriteLine(errormsg);
            }
        }
    }
}
