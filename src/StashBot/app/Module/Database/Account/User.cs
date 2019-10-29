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

        public bool IsAuthorized
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
            IsAuthorized = false;
        }

        public void Login(string password)
        {
            ISecureManager secureManager =
                ModulesManager.GetModulesManager().GetSecureManager();

            EncryptedPassword = secureManager.EncryptWithAes(password);
            IsAuthorized = true;
        }

        public void Logout()
        {
            EncryptedPassword = null;
            IsAuthorized = false;
        }

        public bool ValidatePassword(string password)
        {
            ISecureManager secureManager =
                ModulesManager.GetModulesManager().GetSecureManager();

            return secureManager.CompareWithHash(password, hashPassword);
        }
    }
}
