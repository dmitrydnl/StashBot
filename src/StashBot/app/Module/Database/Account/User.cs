using System;
using StashBot.Module.Secure;

namespace StashBot.Module.Database
{
    internal class User : IUser
    {
        public long ChatId
        {
            get;
            private set;
        }

        public bool IsAuthorized
        {
            get;
            private set;
        }

        public string EncryptedPassword
        {
            get;
            private set;
        }

        private readonly string hashPassword;

        internal User(long chatId, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be null");
            }

            ISecureManager secureManager =
                ModulesManager.GetModulesManager().GetSecureManager();

            ChatId = chatId;
            IsAuthorized = false;
            hashPassword = secureManager.CalculateHash(password);
            EncryptedPassword = null;
        }

        public void Login(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be null");
            }

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
