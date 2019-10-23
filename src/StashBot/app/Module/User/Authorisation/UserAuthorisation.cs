using StashBot.Module.Database;
using StashBot.Module.Session;

namespace StashBot.Module.User.Authorisation
{
    internal class UserAuthorisation : IUserAuthorisation
    {
        public bool LoginUser(long chatId, string password)
        {
            IDatabaseManager databaseManager =
                ModulesManager.GetModulesManager().GetDatabaseManager();

            IUser user = databaseManager.GetUser(chatId);
            if (user == null)
            {
                return false;
            }

            if (!user.ValidatePassword(password))
            {
                return false;
            }

            user.Login(password);
            return true;
        }

        public void LogoutUser(long chatId)
        {
            IDatabaseManager databaseManager =
                ModulesManager.GetModulesManager().GetDatabaseManager();

            IUser user = databaseManager.GetUser(chatId);
            if (user != null)
            {
                user.Logout();
            }
        }
    }
}
