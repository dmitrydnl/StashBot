using StashBot.Module.Database;
using StashBot.Module.Session;

namespace StashBot.Module.User.Authorisation
{
    internal class UserAuthorisation : IUserAuthorisation
    {
        internal UserAuthorisation()
        {

        }

        public bool AuthorisationUser(long chatId, string authCode)
        {
            IDatabaseManager databaseManager =
                ModulesManager.GetModulesManager().GetDatabaseManager();
            ISessionManager sessionManager =
                ModulesManager.GetModulesManager().GetSessionManager();

            if (!databaseManager.IsUserExist(chatId))
            {
                return false;
            }

            if (!databaseManager.ValidateUserAuthCode(chatId, authCode))
            {
                return false;
            }

            databaseManager.AuthorizeUser(chatId, authCode);
            sessionManager.AuthorizeChatSession(chatId);
            return true;
        }
    }
}
