using System;
using System.IO;
using System.Threading.Tasks;
using StashBot.Module.Message;
using StashBot.Module.Secure;
using Telegram.Bot;

namespace StashBot.Module.Database.Stash.Local
{
    internal class StashMessageLocal : IStashMessage
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

        private readonly StashMessageType type;
        private string content;
        private string photoId;

        internal StashMessageLocal(ITelegramUserMessage telegramMessage)
        {
            ChatId = telegramMessage.ChatId;
            IsEncrypt = false;
            if (!string.IsNullOrEmpty(telegramMessage.Message))
            {
                content = telegramMessage.Message;
                IsDownloaded = true;
                type = StashMessageType.Text;
            }
            else if (!string.IsNullOrEmpty(telegramMessage.PhotoId))
            {
                content = null;
                photoId = telegramMessage.PhotoId;
                IsDownloaded = false;
                type = StashMessageType.Photo;
            }
            else
            {
                content = null;
                IsDownloaded = true;
                type = StashMessageType.Empty;
            }
        }

        public async Task Download()
        {
            if (IsDownloaded)
            {
                return;
            }

            if (IsEncrypt)
            {
                throw new ArgumentException("An encrypted message cannot download");
            }

            ITelegramBotClient telegramBotClient = ModulesManager.GetModulesManager().GetTelegramBotClient();

            using (MemoryStream stream = new MemoryStream())
            {
                await telegramBotClient.GetInfoAndDownloadFileAsync(photoId, stream);
                byte[] imageBytes = stream.ToArray();
                content = Convert.ToBase64String(imageBytes);
            }

            photoId = null;
            IsDownloaded = true;
        }

        public void Encrypt(IUser user)
        {
            if (IsEncrypt)
            {
                return;
            }

            if (!IsDownloaded)
            {
                throw new ArgumentException("An undownloaded message cannot encrypt");
            }

            if (!user.IsAuthorized)
            {
                throw new ArgumentException("User is unauthorized, message cannot encrypt");
            }

            ISecureManager secureManager = ModulesManager.GetModulesManager().GetSecureManager();

            string password = secureManager.DecryptWithAes(user.EncryptedPassword);
            if (type != StashMessageType.Empty)
            {
                content = secureManager.EncryptWithAesHmac(content, password);
            }

            IsEncrypt = true;
        }

        public void Decrypt(IUser user)
        {
            if (!IsEncrypt)
            {
                return;
            }

            if (!IsDownloaded)
            {
                throw new ArgumentException("An undownloaded message cannot decrypt");
            }

            if (!user.IsAuthorized)
            {
                throw new ArgumentException("User is unauthorized, message cannot decrypt");
            }

            ISecureManager secureManager = ModulesManager.GetModulesManager().GetSecureManager();

            string password = secureManager.DecryptWithAes(user.EncryptedPassword);
            if (type != StashMessageType.Empty)
            {
                content = secureManager.DecryptWithAesHmac(content, password);
            }

            IsEncrypt = false;
        }

        public void Send()
        {
            if (IsEncrypt)
            {
                throw new ArgumentException("An encrypted message cannot send");
            }

            if (!IsDownloaded)
            {
                throw new ArgumentException("An undownloaded message cannot send");
            }

            IMessageManager messageManager = ModulesManager.GetModulesManager().GetMessageManager();

            switch (type)
            {
                case StashMessageType.Text:
                    _ = messageManager.SendTextMessage(ChatId, content);
                    break;
                case StashMessageType.Photo:
                    byte[] imageBytes = Convert.FromBase64String(content);
                    _ = messageManager.SendPhotoMessage(ChatId, imageBytes);
                    break;
                case StashMessageType.Empty:
                    break;
            }
        }
    }
}
