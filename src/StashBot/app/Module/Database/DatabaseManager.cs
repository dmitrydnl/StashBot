using System.Collections.Generic;
using StashBot.Module.Database.Account;
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

        public bool IsUserExist(long chatId)
        {
            return databaseAccount.IsUserExist(chatId);
        }

        public IUser GetUser(long chatId)
        {
            return databaseAccount.GetUser(chatId);
        }

        public void SaveMessageToStash(long chatId, string message)
        {
            databaseStash.SaveMessageToStash(chatId, message);
        }

        public List<string> GetMessagesFromStash(long chatId)
        {
            return databaseStash.GetMessagesFromStash(chatId);
        }

        public void ClearStash(long chatId)
        {
            databaseStash.ClearStash(chatId);
        }
    }
}
