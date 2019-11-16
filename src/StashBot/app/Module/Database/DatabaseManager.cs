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
            switch (DatabaseSettings.AccountDatabaseType)
            {
                case DatabaseType.Local:
                    databaseAccount = new DatabaseAccountLocal();
                    break;
                case DatabaseType.Sqlite:
                    databaseAccount = new DatabaseAccountSqlite();
                    break;
                default:
                    databaseAccount = new DatabaseAccountLocal();
                    break;
            }

            switch (DatabaseSettings.StashDatabaseType)
            {
                case DatabaseType.Local:
                    databaseStash = new DatabaseStashLocal();
                    break;
                case DatabaseType.Sqlite:
                    databaseStash = new DatabaseStashSqlite();
                    break;
                default:
                    databaseStash = new DatabaseStashLocal();
                    break;
            }
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

        public List<IStashMessage> GetMessagesFromStash(long chatId)
        {
            return databaseStash.GetMessagesFromStash(chatId);
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
