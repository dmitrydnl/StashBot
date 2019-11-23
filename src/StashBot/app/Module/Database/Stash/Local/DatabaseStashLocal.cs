using System;
using System.Linq;
using System.Collections.Generic;

namespace StashBot.Module.Database.Stash.Local
{
    public class DatabaseStashLocal : IDatabaseStash
    {
        private readonly Dictionary<long, List<IStashMessage>> usersStashes;
        private readonly IStashMessageFactory stashMessageFactory;

        public DatabaseStashLocal()
        {
            usersStashes = new Dictionary<long, List<IStashMessage>>();
            stashMessageFactory = new StashMessageLocalFactory();
        }

        public IStashMessage CreateStashMessage(ITelegramUserMessage telegramMessage)
        {
            return stashMessageFactory.Create(telegramMessage);
        }

        public void SaveMessageToStash(IStashMessage stashMessage)
        {
            if (!stashMessage.IsEncrypt)
            {
                throw new ArgumentException("An unencrypted message cannot be stored in a stash");
            }

            if (!stashMessage.IsDownloaded)
            {
                throw new ArgumentException("An undownloaded message cannot be stored in a stash");
            }

            if (!IsStashExist(stashMessage.ChatId))
            {
                usersStashes.Add(stashMessage.ChatId, new List<IStashMessage>());
            }

            usersStashes[stashMessage.ChatId].Add(stashMessage);
        }

        public List<IStashMessage> GetMessagesFromStash(long chatId)
        {
            if (!IsStashExist(chatId))
            {
                usersStashes.Add(chatId, new List<IStashMessage>());
            }

            return usersStashes[chatId];
        }

        public void DeleteStashMessage(long chatId, long databaseMessageId)
        {
            if (!IsStashExist(chatId))
            {
                return;
            }

            IStashMessage stashMessage = usersStashes[chatId]
                .Where(message => message.DatabaseMessageId == databaseMessageId)
                .FirstOrDefault();

            if (stashMessage != null)
            {
                usersStashes[chatId].Remove(stashMessage);
            }
        }

        public void ClearStash(long chatId)
        {
            if (IsStashExist(chatId))
            {
                usersStashes[chatId].Clear();
            }
        }

        public bool IsStashExist(long chatId)
        {
            return usersStashes.ContainsKey(chatId);
        }
    }
}
