using StashBot.Module.Database;
using StashBot.Module.Secure;
using StashBot.Module.ChatSession;

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
            ISecureManager secureManager =
                ModulesManager.GetModulesManager().GetSecureManager();
            ISessionsManager sessionsManager =
                ModulesManager.GetModulesManager().GetSessionsManager();

            IUser user = databaseManager.GetUser(chatId);
            if (user == null)
            {
                return false;
            }

            bool isEqual = secureManager.CompareWithHash(authCode, user.HashAuthCode());
            if (!isEqual)
            {
                return false;
            }

            sessionsManager.AuthorizeSession(chatId);
            return true;
        }
    }
}
