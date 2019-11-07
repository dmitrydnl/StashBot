using System;
using StashBot.Module.Secure;

namespace StashBot.Module.Database.Account.Sqlite
{
    public class UserSqlite : IUser
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

        internal UserSqlite(long chatId, string hashPassword)
        {
            if (string.IsNullOrEmpty(hashPassword))
            {
                throw new ArgumentException("Hash password cannot be null");
            }

            ChatId = chatId;
            IsAuthorized = false;
            this.hashPassword = hashPassword;
            EncryptedPassword = null;
        }

        public void Login(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be null");
            }

            ISecureManager secureManager = ModulesManager.GetModulesManager().GetSecureManager();

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
            ISecureManager secureManager = ModulesManager.GetModulesManager().GetSecureManager();

            return secureManager.CompareWithHash(password, hashPassword);
        }
    }
}
