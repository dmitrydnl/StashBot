using System.Collections.Generic;

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

            if (!usersStashes.ContainsKey(chatId))
            {
                return new List<string>();
            }

            IUser user = databaseManager.GetUser(chatId);
            if (user == null)
            {
                return new List<string>();
            }

            List<string> decryptedMessages = new List<string>();
            foreach (string encryptedMessage in usersStashes[chatId])
            {
                decryptedMessages.Add(user.DecryptMessage(encryptedMessage));
            }
            return decryptedMessages;
        }

        public void SaveMessageToStash(long chatId, string message)
        {
            IDatabaseManager databaseManager =
                ModulesManager.GetModulesManager().GetDatabaseManager();

            if (!usersStashes.ContainsKey(chatId))
            {
                usersStashes.Add(chatId, new List<string>());
            }

            IUser user = databaseManager.GetUser(chatId);
            if (user != null)
            {
                string encryptedMessage = user.EncryptMessage(message);
                usersStashes[chatId].Add(encryptedMessage);
            }
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
