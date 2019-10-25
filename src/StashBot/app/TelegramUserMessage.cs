using System;
using Telegram.Bot.Types;

namespace StashBot
{
    internal class TelegramUserMessage : ITelegramUserMessage
    {
        public long ChatId
        {
            get;
        }

        public int MessageId
        {
            get;
        }

        public DateTime DateSent
        {
            get;
        }

        public string Message
        {
            get;
        }

        public string PhotoId
        {
            get;
        }

        internal TelegramUserMessage(Message message)
        {
            ChatId = message.Chat.Id;
            MessageId = message.MessageId;
            DateSent = message.Date;
            Message = message.Text;
            if (message.Photo != null && message.Photo.Length > 0)
            {
                PhotoId = message.Photo[message.Photo.Length - 1].FileId;
            }
            else
            {
                PhotoId = null;
            }
        }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(Message) && string.IsNullOrEmpty(PhotoId);
        }
    }
}
