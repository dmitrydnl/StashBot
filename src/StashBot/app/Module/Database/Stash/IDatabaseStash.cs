using System.Collections.Generic;

namespace StashBot.Module.Database.Stash
{
    internal interface IDatabaseStash
    {
        IStashMessage CreateStashMessage(ITelegramUserMessage telegramMessage);
        void SaveMessageToStash(IStashMessage stashMessage);
        List<IStashMessage> GetMessagesFromStash(long chatId);
        void DeleteStashMessage(long chatId, long databaseMessageId);
        void ClearStash(long chatId);
        bool IsStashExist(long chatId);
    }
}
