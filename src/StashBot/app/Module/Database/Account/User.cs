using StashBot.Module.Secure;

namespace StashBot.Module.Database
{
    internal class User : IUser
    {
        private readonly long chatId;
        private readonly string hashPassword;

        public string EncryptedPassword
        {
            get;
            private set;
        }

        internal User(long chatId, string password)
        {
            ISecureManager secureManager =
                ModulesManager.GetModulesManager().GetSecureManager();

            this.chatId = chatId;
            hashPassword = secureManager.CalculateHash(password);
            EncryptedPassword = null;
        }

        public void Login(string password)
        {
            ISecureManager secureManager =
                ModulesManager.GetModulesManager().GetSecureManager();

            EncryptedPassword = secureManager.EncryptWithAes(password);
        }

        public void Logout()
        {
            EncryptedPassword = null;
        }

        public bool ValidatePassword(string password)
        {
            ISecureManager secureManager =
                ModulesManager.GetModulesManager().GetSecureManager();

            return secureManager.CompareWithHash(password, hashPassword);
        }
    }
}
