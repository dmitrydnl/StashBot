using System;
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
            if (!stashMessage.IsEncrypt)
            {
                throw new ArgumentException(
                    "An unencrypted message cannot be stored in a stash");
            }

            if (!stashMessage.IsDownloaded)
            {
                throw new ArgumentException(
                    "An undownloaded message cannot be stored in a stash");
            }

            if (!IsStashExist(stashMessage.ChatId))
            {
                usersStashes
                    .Add(stashMessage.ChatId, new List<IStashMessage>());
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
