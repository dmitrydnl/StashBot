using System.Collections.Generic;
using System.Security.Cryptography;
using StashBot.Module.Secure;

namespace StashBot.Module.Database.Stash
{
    internal class DatabaseStashLocal : IDatabaseStash
    {
        private readonly Dictionary<long, List<string>> usersStashes;

        internal DatabaseStashLocal()
        {
            usersStashes = new Dictionary<long, List<string>>();
        }

        public List<string> GetMessagesFromStash(long chatId)
        {
            IDatabaseManager databaseManager =
                ModulesManager.GetModulesManager().GetDatabaseManager();
            ISecureManager secureManager =
                ModulesManager.GetModulesManager().GetSecureManager();

            if (!usersStashes.ContainsKey(chatId))
            {
                return new List<string>();
            }

            IUser user = databaseManager.GetUser(chatId);
            RSACryptoServiceProvider csp = user.RsaCryptoServiceProvider();
            List<string> decryptedMessages = new List<string>();
            foreach (string encrypted in usersStashes[chatId])
            {
                decryptedMessages.Add(secureManager.DecryptWithRsa(csp, encrypted));
            }

            return decryptedMessages;
        }

        public void SaveMessageToStash(long chatId, string message)
        {
            IDatabaseManager databaseManager =
                ModulesManager.GetModulesManager().GetDatabaseManager();
            ISecureManager secureManager =
                ModulesManager.GetModulesManager().GetSecureManager();

            if (!usersStashes.ContainsKey(chatId))
            {
                usersStashes.Add(chatId, new List<string>());
            }

            IUser user = databaseManager.GetUser(chatId);
            RSACryptoServiceProvider csp = user.RsaCryptoServiceProvider();
            string encryptedMessage = secureManager.EncryptWithRsa(csp, message);
            usersStashes[chatId].Add(encryptedMessage);
        }

        public void ClearStash(long chatId)
        {
            if (usersStashes.ContainsKey(chatId))
            {
                usersStashes[chatId].Clear();
            }
        }
    }
}
