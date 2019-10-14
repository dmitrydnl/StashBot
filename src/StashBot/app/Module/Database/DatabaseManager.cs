using System.Collections.Generic;
using StashBot.Module.Database.Account;
using StashBot.Module.Database.Stash;

namespace StashBot.Module.Database
{
    internal class DatabaseManager : IDatabaseManager
    {
        private IDatabaseAccount databaseAccount;
        private IDatabaseStash databaseStash;

        internal DatabaseManager()
        {
            databaseAccount = new DatabaseAccountLocal();
            databaseStash = new DatabaseStashLocal();
        }

        public void CreateNewUser(long chatId, string hashAuthCode)
        {
            databaseAccount.CreateNewUser(chatId, hashAuthCode);
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
    }
}
