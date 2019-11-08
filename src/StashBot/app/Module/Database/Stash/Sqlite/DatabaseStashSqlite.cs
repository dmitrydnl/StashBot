using System;
using System.Collections.Generic;

namespace StashBot.Module.Database.Stash.Sqlite
{
    internal class DatabaseStashSqlite : IDatabaseStash
    {
        private readonly IStashMessageFactory stashMessageFactory;

        internal DatabaseStashSqlite()
        {
            stashMessageFactory = new StashMessageSqliteFactory();
        }

        public IStashMessage CreateStashMessage(ITelegramUserMessage telegramMessage)
        {
            return stashMessageFactory.Create(telegramMessage);
        }

        public void SaveMessageToStash(IStashMessage stashMessage)
        {
            throw new NotImplementedException();
        }

        public List<IStashMessage> GetMessagesFromStash(long chatId)
        {
            throw new NotImplementedException();
        }

        public void ClearStash(long chatId)
        {
            throw new NotImplementedException();
        }

        public bool IsStashExist(long chatId)
        {
            throw new NotImplementedException();
        }
    }
}
