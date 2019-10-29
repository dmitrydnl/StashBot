using System;
using StashBot.Module.Message;
using StashBot.Module.Secure;

namespace StashBot.Module.Database.Stash
{
    internal class StashMessage : IStashMessage
    {
        public long ChatId
        {
            get;
        }

        public bool IsEncrypt
        {
            get;
            private set;
        }

        public string Message
        {
            get;
            private set;
        }

        internal StashMessage(ITelegramUserMessage telegramMessage)
        {
            ChatId = telegramMessage.ChatId;
            IsEncrypt = false;
            Message = telegramMessage.Message;
        }

        public void Encrypt(IUser user)
        {
            if (IsEncrypt)
            {
                return;
            }

            ISecureManager secureManager =
                ModulesManager.GetModulesManager().GetSecureManager();

            string password = secureManager.DecryptWithAes(user.EncryptedPassword);
            Message = secureManager.EncryptWithAesHmac(Message, password);
            IsEncrypt = true;
        }

        public void Decrypt(IUser user)
        {
            if (!IsEncrypt)
            {
                return;
            }

            ISecureManager secureManager =
                ModulesManager.GetModulesManager().GetSecureManager();

            string password = secureManager.DecryptWithAes(user.EncryptedPassword);
            Message = secureManager.DecryptWithAesHmac(Message, password);
            IsEncrypt = false;
        }

        public void Send()
        {
            if (IsEncrypt)
            {
                throw new ArgumentException("An encrypted message cannot send");
            }

            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            messageManager.SendMessage(ChatId, Message);
        }
    }
}
