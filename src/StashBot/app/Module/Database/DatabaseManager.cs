using System.Collections.Generic;
using StashBot.Module.Database.Account;
using StashBot.Module.Database.Account.Local;
using StashBot.Module.Database.Stash;

namespace StashBot.Module.Database
{
    internal class DatabaseManager : IDatabaseManager
    {
        private readonly IDatabaseAccount databaseAccount;
        private readonly IDatabaseStash databaseStash;

        internal DatabaseManager()
        {
            databaseAccount = new DatabaseAccountLocal();
            databaseStash = new DatabaseStashLocal();
        }

        public void CreateNewUser(long chatId, string password)
        {
            databaseAccount.CreateNewUser(chatId, password);
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
