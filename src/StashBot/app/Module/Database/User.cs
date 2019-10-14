using System.Security.Cryptography;
using StashBot.Module.Secure;

namespace StashBot.Module.Database
{
    internal class User : IUser
    {
        private readonly long chatId;
        private readonly string hashAuthCode;
        private RSACryptoServiceProvider rsaCryptoServiceProvider;

        internal User(long chatId, string hashAuthCode)
        {
            this.chatId = chatId;
            this.hashAuthCode = hashAuthCode;
            rsaCryptoServiceProvider = null;
        }

        public void Authorize(string rsaXmlString)
        {
            ISecureManager secureManager =
                ModulesManager.GetModulesManager().GetSecureManager();

            rsaCryptoServiceProvider = secureManager.RsaCryptoServiceFromXmlString(rsaXmlString);
        }

        public string HashAuthCode()
        {
            return hashAuthCode;
        }

        public RSACryptoServiceProvider RsaCryptoServiceProvider()
        {
            return rsaCryptoServiceProvider;
        }
    }
}
