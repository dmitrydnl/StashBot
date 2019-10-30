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

            if (databaseManager.IsStashExist(chatId))
            {
                databaseManager.ClearStash(chatId);
            }

            User newUser = new User(chatId, password);
            if (IsUserExist(chatId))
            {
                usersDatabase[chatId] = newUser;
            }
            else
            {
                usersDatabase.Add(chatId, newUser);
            }
        }

        public IUser GetUser(long chatId)
        {
            if (IsUserExist(chatId))
            {
                return usersDatabase[chatId];
            }

            return null;
        }

        public bool IsUserExist(long chatId)
        {
            return usersDatabase.ContainsKey(chatId);
        }
    }
}
