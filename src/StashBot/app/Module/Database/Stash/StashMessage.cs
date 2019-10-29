using System;
using System.IO;
using System.Threading.Tasks;
using StashBot.Module.Message;
using StashBot.Module.Secure;
using Telegram.Bot;

namespace StashBot.Module.Database.Stash
{
    internal class StashMessage : IStashMessage
    {
        public long ChatId
        {
            get;
            private set;
        }

        public bool IsEncrypt
        {
            get;
            private set;
        }

        public bool IsDownloaded
        {
            get;
            private set;
        }

        public string Message
        {
            get;
            private set;
        }

        public string PhotoId
        {
            get;
            private set;
        }

        internal StashMessage(ITelegramUserMessage telegramMessage)
        {
            ChatId = telegramMessage.ChatId;
            IsEncrypt = false;
            Message = telegramMessage.Message;
            PhotoId = telegramMessage.PhotoId;
            IsDownloaded = string.IsNullOrEmpty(PhotoId);
        }

        public async Task Download()
        {
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
