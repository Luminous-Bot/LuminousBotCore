using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot.State.Types
{
    public class Users : StateEntity<Users>
    {
        public string Id { get; set; }
        public List<NameRecord> Usernames { get; set; }
        public string BarColor { get; set; }
        public string BackgroundColor { get; set; }
        public bool Donator { get; set; }
        public string BackgoundUrl { get; set; }
        public bool MentionOnLevelup { get; set; }

    }
}
