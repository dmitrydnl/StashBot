using System.Collections.Generic;

namespace StashBot.Module.Database.Stash
{
    internal class DatabaseStashLocal : IDatabaseStash
    {
        internal DatabaseStashLocal()
        {

        }

        public List<string> GetMessagesFromStash(long chatId)
        {
            return null;
        }

        public void SaveMessageToStash(long chatId, string message)
        {
            
        }
    }
}
