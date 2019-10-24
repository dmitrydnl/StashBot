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

        internal TelegramUserMessage(Message message)
        {
            ChatId = message.Chat.Id;
            MessageId = message.MessageId;
            DateSent = message.Date;
            Message = message.Text;
        }
    }
}
