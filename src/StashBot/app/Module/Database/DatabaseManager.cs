using StashBot.Module.Database.Account;

namespace StashBot.Module.Database
{
    internal class DatabaseManager : IDatabaseManager
    {
        private IDatabaseAccount databaseAccount;

        internal DatabaseManager()
        {
            databaseAccount = new DatabaseAccountLocal();
        }

        public void CreateNewUser(long chatId, string hashAuthCode)
        {
            databaseAccount.CreateNewUser(chatId, hashAuthCode);
        }
    }
}
