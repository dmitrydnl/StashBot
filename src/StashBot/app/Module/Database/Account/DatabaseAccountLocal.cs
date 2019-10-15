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

        public void CreateNewUser(long chatId, string authCode)
        {
            IDatabaseManager databaseManager =
                ModulesManager.GetModulesManager().GetDatabaseManager();

            User newUser = new User(chatId, authCode);
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

        public bool ValidateUserAuthCode(long chatId, string authCode)
        {
            IUser user = GetUser(chatId);
            if (user != null)
            {
                return user.ValidateAuthCode(authCode);
            }

            return false;
        }

        public IUser GetUser(long chatId)
        {
            if (!IsUserExist(chatId))
            {
                return null;
            }

            return usersDatabase[chatId];
        }

        public void LoginUser(long chatId, string authCode)
        {
            IUser user = GetUser(chatId);
            if (user != null)
            {
                user.Login(authCode);
            }
        }

        public void LogoutUser(long chatId)
        {
            IUser user = GetUser(chatId);
            if (user != null)
            {
                user.Logout();
            }
        }
    }
}
