using StashBot.Module.Database;
using StashBot.Module.Session;

namespace StashBot.Module.User.Authorisation
{
    internal class UserAuthorisation : IUserAuthorisation
    {
        public bool LoginUser(long chatId, string authCode)
        {
            IDatabaseManager databaseManager =
                ModulesManager.GetModulesManager().GetDatabaseManager();
            ISessionManager sessionManager =
                ModulesManager.GetModulesManager().GetSessionManager();

            IUser user = databaseManager.GetUser(chatId);
            if (user == null)
            {
                return false;
            }

            if (!user.ValidateAuthCode(authCode))
            {
                return false;
            }

            user.Login(authCode);
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
