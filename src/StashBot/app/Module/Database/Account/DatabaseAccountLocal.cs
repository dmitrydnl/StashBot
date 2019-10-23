using System.Collections.Generic;

namespace StashBot.Module.Database.Account
{
    internal class DatabaseAccountLocal : IDatabaseAccount
    {
        private readonly Dictionary<long, IUser> usersDatabase;

        internal DatabaseAccountLocal()
        {
            usersDatabase = new Dictionary<long, IUser>();
        }

        public void CreateNewUser(long chatId, string password)
        {
            IDatabaseManager databaseManager =
                ModulesManager.GetModulesManager().GetDatabaseManager();

            User newUser = new User(chatId, password);
            if (IsUserExist(chatId))
            {
                databaseManager.ClearStash(chatId);
                usersDatabase[chatId] = newUser;
            }
            else
            {
                usersDatabase.Add(chatId, newUser);
            }
        }

        public bool IsUserExist(long chatId)
        {
            return usersDatabase.ContainsKey(chatId);
        }

        public IUser GetUser(long chatId)
        {
            if (IsUserExist(chatId))
            {
                return usersDatabase[chatId];
            }

            return null;
        }
    }
}
