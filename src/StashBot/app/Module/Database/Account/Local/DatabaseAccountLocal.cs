using System.Collections.Generic;

namespace StashBot.Module.Database.Account.Local
{
    public class DatabaseAccountLocal : IDatabaseAccount
    {
        private readonly Dictionary<long, IUser> usersDatabase;
        private readonly IUserFactory userFactory;

        public DatabaseAccountLocal()
        {
            usersDatabase = new Dictionary<long, IUser>();
            userFactory = new UserLocalFactory();
        }

        public void CreateUser(long chatId, string password)
        {
            IUser newUser = userFactory.Create(chatId, password);
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

        public bool LoginUser(long chatId, string password)
        {
            IUser user = GetUser(chatId);
            if (user == null || !user.ValidatePassword(password))
            {
                return false;
            }

            user.Login(password);
            return true;
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
