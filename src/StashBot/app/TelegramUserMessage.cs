﻿using System;
using Telegram.Bot.Types;

namespace StashBot
{
    public class TelegramUserMessage : ITelegramUserMessage
    {
        public long ChatId
        {
            get;
        }

        public DateTime DateSent
        {
            get;
        }

        public int MessageId
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

        public TelegramUserMessage(Message telegramMessage)
        {
            if (telegramMessage == null)
            {
                throw new ArgumentException("Telegram message cannot be null");
            }

            ChatId = telegramMessage.Chat.Id;
            DateSent = telegramMessage.Date;
            MessageId = telegramMessage.MessageId;
            Message = telegramMessage.Text;
            if (telegramMessage.Photo != null && telegramMessage.Photo.Length > 0)
            {
                PhotoId = telegramMessage.Photo[telegramMessage.Photo.Length - 1].FileId;
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
