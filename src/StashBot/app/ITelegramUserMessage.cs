using System;

namespace StashBot
{
    internal interface ITelegramUserMessage
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
