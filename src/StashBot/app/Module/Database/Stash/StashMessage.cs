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

        public string Photo
        {
            get;
            private set;
        }

        private readonly string photoId;

        internal StashMessage(ITelegramUserMessage telegramMessage)
        {
            ChatId = telegramMessage.ChatId;
            IsEncrypt = false;
            Message = telegramMessage.Message;
            photoId = telegramMessage.PhotoId;
            IsDownloaded = string.IsNullOrEmpty(photoId);
        }

        public async Task Download()
        {
            ITelegramBotClient telegramBotClient =
                ModulesManager.GetModulesManager().GetTelegramBotClient();

            using (MemoryStream stream = new MemoryStream())
            {
                await telegramBotClient.GetInfoAndDownloadFileAsync(photoId, stream);
                byte[] imageBytes = stream.ToArray();
                Photo = Convert.ToBase64String(imageBytes);
            }

            IsDownloaded = true;
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
            if (!string.IsNullOrEmpty(Message))
            {
                Message = secureManager.EncryptWithAesHmac(Message, password);
            }
            if (!string.IsNullOrEmpty(Photo))
            {
                Photo = secureManager.EncryptWithAesHmac(Photo, password);
            }

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
            if (!string.IsNullOrEmpty(Message))
            {
                Message = secureManager.DecryptWithAesHmac(Message, password);
            }
            if (!string.IsNullOrEmpty(Photo))
            {
                Photo = secureManager.DecryptWithAesHmac(Photo, password);

            }

            IsEncrypt = false;
        }

        public void Send()
        {
            if (!IsDownloaded)
            {
                throw new ArgumentException("An undownloaded message cannot send");
            }

            if (IsEncrypt)
            {
                throw new ArgumentException("An encrypted message cannot send");
            }

            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            if (!string.IsNullOrEmpty(Message))
            {
                _ = messageManager.SendTextMessage(ChatId, Message);
            }
            if (!string.IsNullOrEmpty(Photo))
            {
                byte[] imageBytes = Convert.FromBase64String(Photo);
                _ = messageManager.SendPhotoMessage(ChatId, imageBytes);
            }
        }
    }
}
