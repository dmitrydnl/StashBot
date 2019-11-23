using System.Collections.Generic;
using StashBot.Module.Database.Account;
using StashBot.Module.Database.Account.Local;
using StashBot.Module.Database.Account.Sqlite;
using StashBot.Module.Database.Stash;
using StashBot.Module.Database.Stash.Local;
using StashBot.Module.Database.Stash.Sqlite;
using StashBot.BotSettings;

namespace StashBot.Module.Database
{
    internal class DatabaseManager : IDatabaseManager
    {
        private readonly IDatabaseAccount databaseAccount;
        private readonly IDatabaseStash databaseStash;

        internal DatabaseManager()
        {
            databaseAccount = DatabaseSettings.AccountDatabaseType switch
            {
                DatabaseType.Local => new DatabaseAccountLocal(),
                DatabaseType.Sqlite => new DatabaseAccountSqlite(),
                _ => new DatabaseAccountLocal(),
            };

            databaseStash = DatabaseSettings.StashDatabaseType switch
            {
                DatabaseType.Local => new DatabaseStashLocal(),
                DatabaseType.Sqlite => new DatabaseStashSqlite(),
                _ => new DatabaseStashLocal(),
            };
        }

        public void CreateUser(long chatId, string password)
        {
            databaseAccount.CreateUser(chatId, password);
        }

        public IUser GetUser(long chatId)
        {
            return databaseAccount.GetUser(chatId);
        }

        public bool IsUserExist(long chatId)
        {
            return databaseAccount.IsUserExist(chatId);
        }

        public bool LoginUser(long chatId, string password)
        {
            return databaseAccount.LoginUser(chatId, password);
        }

        public void LogoutUser(long chatId)
        {
            databaseAccount.LogoutUser(chatId);
        }

        public IStashMessage CreateStashMessage(ITelegramUserMessage telegramMessage)
        {
            return databaseStash.CreateStashMessage(telegramMessage);
        }

        public void SaveMessageToStash(IStashMessage stashMessage)
        {
            databaseStash.SaveMessageToStash(stashMessage);
        }

        public ICollection<IStashMessage> GetMessagesFromStash(long chatId)
        {
            return databaseStash.GetMessagesFromStash(chatId);
        }

        public void DeleteStashMessage(long chatId, long databaseMessageId)
        {
            databaseStash.DeleteStashMessage(chatId, databaseMessageId);
        }

        public void ClearStash(long chatId)
        {
            databaseStash.ClearStash(chatId);
        }

        public bool IsStashExist(long chatId)
        {
            return databaseStash.IsStashExist(chatId);
        }
    }
}
