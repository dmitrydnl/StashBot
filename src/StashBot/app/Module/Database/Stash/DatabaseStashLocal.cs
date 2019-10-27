using System.Collections.Generic;

namespace StashBot.Module.Database.Stash
{
    internal class DatabaseStashLocal : IDatabaseStash
    {
        private readonly Dictionary<long, List<IStashMessage>> usersStashes;

        internal DatabaseStashLocal()
        {
            usersStashes = new Dictionary<long, List<IStashMessage>>();
        }

        public void SaveMessageToStash(IStashMessage stashMessage)
        {
            IDatabaseManager databaseManager =
                ModulesManager.GetModulesManager().GetDatabaseManager();

            if (!usersStashes.ContainsKey(stashMessage.ChatId))
            {
                usersStashes.Add(stashMessage.ChatId, new List<IStashMessage>());
            }

            IUser user = databaseManager.GetUser(stashMessage.ChatId);
            if (user != null)
            {
                stashMessage.Encrypt(user);
                usersStashes[stashMessage.ChatId].Add(stashMessage);
            }
        }

        public List<IStashMessage> GetMessagesFromStash(long chatId)
        {
            IDatabaseManager databaseManager =
                ModulesManager.GetModulesManager().GetDatabaseManager();

            if (!usersStashes.ContainsKey(chatId))
            {
                return new List<IStashMessage>();
            }

            IUser user = databaseManager.GetUser(chatId);
            if (user == null)
            {
                return new List<IStashMessage>();
            }

            List<IStashMessage> decryptedMessages = new List<IStashMessage>();
            foreach (IStashMessage encryptedMessage in usersStashes[chatId])
            {
                encryptedMessage.Decrypt(user);
                decryptedMessages.Add(encryptedMessage);
            }
            return decryptedMessages;
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
