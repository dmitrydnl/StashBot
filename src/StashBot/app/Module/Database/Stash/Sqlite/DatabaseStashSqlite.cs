using System;
using System.Linq;
using System.Collections.Generic;

namespace StashBot.Module.Database.Stash.Sqlite
{
    internal class DatabaseStashSqlite : IDatabaseStash
    {
        private readonly IStashMessageFactory stashMessageFactory;

        internal DatabaseStashSqlite()
        {
            stashMessageFactory = new StashMessageSqliteFactory();
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

            StashMessageModel messageModel = ((IStashMessageDatabaseModelConverter)stashMessage).ToStashMessageModel();
            using (StashMessagesContext db = new StashMessagesContext())
            {
                db.StashMessages.Add(messageModel);
                db.SaveChanges();
            }
        }

        public List<IStashMessage> GetMessagesFromStash(long chatId)
        {
            List<IStashMessage> stashMessages = new List<IStashMessage>();

            if (!IsStashExist(chatId))
            {
                return stashMessages;
            }

            using (StashMessagesContext db = new StashMessagesContext())
            {
                IQueryable<StashMessageModel> stashMessageModel = db.StashMessages
                        .Where(message => message.ChatId == chatId);

                foreach (StashMessageModel messageModel in stashMessageModel)
                {
                    IStashMessage stashMessage = CreateStashMessage(null);
                    ((IStashMessageDatabaseModelConverter)stashMessage).FromStashMessageModel(messageModel);
                    stashMessages.Add(stashMessage);
                }
            }

            return stashMessages;
        }

        public void DeleteStashMessage(long chatId, long databaseMessageId)
        {
            using (StashMessagesContext db = new StashMessagesContext())
            {
                StashMessageModel messageModel = db.StashMessages
                    .Where(message => (message.ChatId == chatId) && (message.Id == databaseMessageId))
                    .FirstOrDefault();

                if (messageModel != null)
                {
                    db.Remove(messageModel);
                    db.SaveChanges();
                }
            }
        }

        public void ClearStash(long chatId)
        {
            using (StashMessagesContext db = new StashMessagesContext())
            {
                IQueryable<StashMessageModel> messageModels = db.StashMessages
                    .Where(message => message.ChatId == chatId);

                foreach (StashMessageModel messageModel in messageModels)
                {
                    db.Remove(messageModel);
                }

                db.SaveChanges();
            }
        }

        public bool IsStashExist(long chatId)
        {
            using (StashMessagesContext db = new StashMessagesContext())
            {
                StashMessageModel messageModel = db.StashMessages
                    .Where(message => message.ChatId == chatId)
                    .FirstOrDefault();

                return messageModel != null;
            }
        }
    }
}
