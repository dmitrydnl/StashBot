using System.Linq;
using System.Collections.Generic;
using StashBot.Module.Secure;

namespace StashBot.Module.Database.Account.Sqlite
{
    public class DatabaseAccountSqlite : IDatabaseAccount
    {
        private readonly Dictionary<long, IUser> authorizedUsers;
        private readonly IUserFactory userFactory;

        public DatabaseAccountSqlite()
        {
            authorizedUsers = new Dictionary<long, IUser>();
            userFactory = new UserSqliteFactory();
        }

        public void CreateUser(long chatId, string password)
        {
            ISecureManager secureManager = ModulesManager.GetModulesManager().GetSecureManager();

            if (IsUserExist(chatId))
            {
                LogoutUser(chatId);

                using (UsersContext db = new UsersContext())
                {
                    UserModel userModel = db.Users
                        .Where(user => user.ChatId == chatId)
                        .First();

                    userModel.HashPassword = secureManager.CalculateHash(password);
                    db.SaveChanges();
                }
            }
            else
            {
                using (UsersContext db = new UsersContext())
                {
                    UserModel userModel = new UserModel
                    {
                        ChatId = chatId,
                        HashPassword = secureManager.CalculateHash(password)
                    };

                    db.Users.Add(userModel);
                    db.SaveChanges();
                }
            }
        }

        public IUser GetUser(long chatId)
        {
            if (IsUserAuthorized(chatId))
            {
                return authorizedUsers[chatId];
            }

            if (IsUserExist(chatId))
            {
                using (UsersContext db = new UsersContext())
                {
                    UserModel userModel = db.Users
                        .Where(user => user.ChatId == chatId)
                        .First();

                    IUser user = userFactory.Create(userModel.ChatId, userModel.HashPassword);
                    return user;
                }
            }

            return null;
        }

        public bool IsUserExist(long chatId)
        {
            using (UsersContext db = new UsersContext())
            {
                UserModel userModel = db.Users
                    .Where(user => user.ChatId == chatId)
                    .FirstOrDefault();

                return userModel != null;
            }
        }

        public bool LoginUser(long chatId, string password)
        {
            IUser user = GetUser(chatId);
            if (user == null || !user.ValidatePassword(password))
            {
                return false;
            }

            user.Login(password);
            if (IsUserAuthorized(chatId))
            {
                authorizedUsers[chatId] = user;
            }
            else
            {
                authorizedUsers.Add(chatId, user);
            }

            return true;
        }

        public void LogoutUser(long chatId)
        {
            if (IsUserAuthorized(chatId))
            {
                IUser user = authorizedUsers[chatId];
                user.Logout();
                authorizedUsers.Remove(chatId);
            }
        }

        private bool IsUserAuthorized(long chatId)
        {
            return authorizedUsers.ContainsKey(chatId);
        }
    }
}
