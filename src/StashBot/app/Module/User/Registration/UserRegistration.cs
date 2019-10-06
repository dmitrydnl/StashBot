using System.Security.Cryptography;
using StashBot.Module.Secure;
using StashBot.Module.Database;

namespace StashBot.Module.User.Registration
{
    internal class UserRegistration : IUserRegistration
    {
        internal UserRegistration()
        {
        }

        public string CreateNewUser(long chatId)
        {
            ISecureManager secureManager =
                ModulesManager.GetModulesManager().GetSecureManager();
            IDatabaseManager databaseManager =
                ModulesManager.GetModulesManager().GetDatabaseManager();

            RSACryptoServiceProvider csp = secureManager.CreateRsaCryptoService();
            string cspStr = secureManager.RsaCryptoServiceToXmlString(csp, true);
            byte[] cspEncrypted = secureManager.EncryptWithAes(cspStr);
            string authCode = secureManager.AesEncryptedDataToString(cspEncrypted);
            string hashAuthCode = secureManager.CalculateHash(authCode);
            databaseManager.CreateNewUser(chatId, hashAuthCode);
            return authCode;
        }
    }
}
