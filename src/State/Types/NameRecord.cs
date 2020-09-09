using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot.State.Types
{
    public class NameRecord
    {
        [GraphQLProp]
        public string Name { get; set; }
        [GraphQLProp]
        public DateTime Time { get; set; }
    }
}
