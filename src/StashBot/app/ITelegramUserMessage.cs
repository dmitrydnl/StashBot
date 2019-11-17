using System;

namespace StashBot
{
    public interface ITelegramUserMessage
    {
        long ChatId
        {
            get;
        }

        DateTime DateSent
        {
            get;
        }

        int MessageId
        {
            get;
        }

        string Message
        {
            get;
        }

        string PhotoId
        {
            get;
        }

        bool IsEmpty();
    }
}
