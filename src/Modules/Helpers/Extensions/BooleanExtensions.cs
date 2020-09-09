using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot.Modules
{
    public static class BooleanExtensions
    {
        public static string CheckOrX(this bool value)
            => value ? "✅" : "❌";
    }
}
