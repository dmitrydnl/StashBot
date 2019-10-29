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
                    "An unencrypted message cannot be stored in a stash",
                    nameof(stashMessage));
            }

            if (!usersStashes.ContainsKey(stashMessage.ChatId))
            {
                usersStashes.Add(stashMessage.ChatId, new List<IStashMessage>());
            }

            usersStashes[stashMessage.ChatId].Add(stashMessage);
        }

        public List<IStashMessage> GetMessagesFromStash(long chatId)
        {
            if (!usersStashes.ContainsKey(chatId))
            {
                return new List<IStashMessage>();
            }

            return usersStashes[chatId];
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
