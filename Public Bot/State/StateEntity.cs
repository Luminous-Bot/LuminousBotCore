using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Public_Bot.Modules.Handlers;

namespace Public_Bot.State
{
    public abstract class StateEntity<EntityType> 
    {
        public EntityType Execute(string schema)
            => ExecuteAsync(schema).GetAwaiter().GetResult();
        public async Task<EntityType> ExecuteAsync(string schema)
        {
            return await StateHandler.Postgql<EntityType>(schema);
        }
    }
}
