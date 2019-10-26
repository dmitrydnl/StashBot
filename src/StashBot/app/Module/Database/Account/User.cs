using StashBot.Module.Secure;

namespace StashBot.Module.Database
{
    internal class User : IUser
    {
        private readonly long chatId;
        private readonly string hashPassword;
        private string encryptedPassword;

        internal User(long chatId, string password)
        {
            ISecureManager secureManager =
                ModulesManager.GetModulesManager().GetSecureManager();

            this.chatId = chatId;
            hashPassword = secureManager.CalculateHash(password);
            encryptedPassword = null;
        }

        public void Login(string password)
        {
            ISecureManager secureManager =
                ModulesManager.GetModulesManager().GetSecureManager();

            encryptedPassword = secureManager.EncryptWithAes(password);
        }

        public void Logout()
        {
            encryptedPassword = null;
        }

        public bool ValidatePassword(string password)
        {
            ISecureManager secureManager =
                ModulesManager.GetModulesManager().GetSecureManager();

            return secureManager.CompareWithHash(password, hashPassword);
        }

        public string EncryptMessage(string secretMessage)
        {
            ISecureManager secureManager =
                ModulesManager.GetModulesManager().GetSecureManager();

            string password = secureManager.DecryptWithAes(encryptedPassword);
            return secureManager.EncryptWithAesHmac(secretMessage, password);
        }

        public string DecryptMessage(string encryptedMessage)
        {
            ISecureManager secureManager =
                ModulesManager.GetModulesManager().GetSecureManager();

            string password = secureManager.DecryptWithAes(encryptedPassword);
            return secureManager.DecryptWithAesHmac(encryptedMessage, password);
        }
    }
}
