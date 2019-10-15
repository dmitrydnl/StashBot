﻿using System.Collections.Generic;
using StashBot.Module.Database.Account;
using StashBot.Module.Database.Stash;

namespace StashBot.Module.Database
{
    internal class DatabaseManager : IDatabaseManager
    {
        private IDatabaseAccount databaseAccount;
        private IDatabaseStash databaseStash;

        internal DatabaseManager()
        {
            databaseAccount = new DatabaseAccountLocal();
            databaseStash = new DatabaseStashLocal();
        }

        public void CreateNewUser(long chatId, string authCode)
        {
            databaseAccount.CreateNewUser(chatId, authCode);
        }

        public bool IsUserExist(long chatId)
        {
            return databaseAccount.IsUserExist(chatId);
        }

        public bool ValidateUserAuthCode(long chatId, string authCode)
        {
            return databaseAccount.ValidateUserAuthCode(chatId, authCode);
        }

        public IUser GetUser(long chatId)
        {
            return databaseAccount.GetUser(chatId);
        }

        public void AuthorizeUser(long chatId, string authCode)
        {
            databaseAccount.AuthorizeUser(chatId, authCode);
        }

        public void SaveMessageToStash(long chatId, string message)
        {
            databaseStash.SaveMessageToStash(chatId, message);
        }

        public List<string> GetMessagesFromStash(long chatId)
        {
            return databaseStash.GetMessagesFromStash(chatId);
        }
    }
}
