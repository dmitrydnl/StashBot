using System.Collections.Generic;

namespace StashBot.Module.Database.Stash
{
    internal interface IDatabaseStash
    {
        void SaveMessageToStash(long chatId, string message);
        List<string> GetMessagesFromStash(long chatId);
    }
}
