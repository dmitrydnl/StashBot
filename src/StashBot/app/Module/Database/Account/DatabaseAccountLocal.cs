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

        public void CreateNewUser(long chatId, string hashAuthCode)
        {
            User newUser = new User(chatId, hashAuthCode);
            if (usersDatabase.ContainsKey(chatId))
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
            if (!usersDatabase.ContainsKey(chatId))
            {
                return null;
            }

            return usersDatabase[chatId];
        }
    }
}
