using System;
using System.IO;
using System.Threading.Tasks;
using StashBot.Module.Message;
using StashBot.Module.Secure;
using Telegram.Bot;

namespace StashBot.Module.Database.Stash.Sqlite
{
    internal class StashMessageSqlite : IStashMessage, IStashMessageDatabaseModelConverter
    {
        public long? DatabaseMessageId
        {
            get;
            private set;
        }

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

        private StashMessageType type;
        private string content;
        private string photoId;

        private readonly IKeyboardForStashMessage keyboardForStashMessage;

        internal StashMessageSqlite(ITelegramUserMessage telegramMessage)
        {
            DatabaseMessageId = null;
            FromITelegramUserMessage(telegramMessage);
            keyboardForStashMessage = new KeyboardForStashMessage(this);
        }

        private void FromITelegramUserMessage(ITelegramUserMessage telegramMessage)
        {
            if (telegramMessage == null)
            {
                return;
            }

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

        public Task DownloadAsync()
        {
            if (IsDownloaded)
            {
                return Task.CompletedTask;
            }

            if (IsEncrypt)
            {
                throw new ArgumentException("An encrypted message cannot download");
            }

            return DownloadInternalAsync();
        }

        public async Task DownloadInternalAsync()
        {
            ITelegramBotClient telegramBotClient = ModulesManager.GetTelegramBotClient();

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

            ISecureManager secureManager = ModulesManager.GetSecureManager();

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

            ISecureManager secureManager = ModulesManager.GetSecureManager();

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

            IMessageManager messageManager = ModulesManager.GetMessageManager();

            switch (type)
            {
                case StashMessageType.Text:
                    _ = messageManager.SendTextMessageAsync(ChatId, content, keyboardForStashMessage.ForTextMessage());
                    break;
                case StashMessageType.Photo:
                    byte[] imageBytes = Convert.FromBase64String(content);
                    _ = messageManager.SendPhotoMessageAsync(ChatId, imageBytes, keyboardForStashMessage.ForPhotoMessage());
                    break;
                case StashMessageType.Empty:
                    break;
                default:
                    break;
            }
        }

        public StashMessageModel ToStashMessageModel()
        {
            StashMessageModel messageModel = new StashMessageModel
            {
                ChatId = ChatId,
                Type = type,
                Content = content
            };

            return messageModel;
        }

        public void FromStashMessageModel(StashMessageModel messageModel)
        {
            DatabaseMessageId = messageModel.Id;
            ChatId = messageModel.ChatId;
            type = messageModel.Type;
            content = messageModel.Content;
            IsEncrypt = true;
            IsDownloaded = true;
        }
    }
}
