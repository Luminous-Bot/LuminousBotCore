using System;
using System.Collections.Generic;
using System.Text;

namespace Public_Bot
{
    public interface IDoubleEntityID
    {
        ulong GuildID { get; }
        ulong Id { get; }
    }
}
