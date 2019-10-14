using StashBot.Module.Database;
using StashBot.Module.Secure;
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
            ISecureManager secureManager =
                ModulesManager.GetModulesManager().GetSecureManager();
            ISessionManager sessionManager =
                ModulesManager.GetModulesManager().GetSessionManager();

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

            byte[] usersEncryptedRsa = secureManager.AesStringToEncryptedData(authCode);
            string usersRsaXmlString = secureManager.DecryptWithAes(usersEncryptedRsa);
            user.Authorize(usersRsaXmlString);
            sessionManager.AuthorizeChatSession(chatId);
            return true;
        }
    }
}
