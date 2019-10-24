using System;

namespace StashBot
{
    internal interface ITelegramUserMessage
    {
        long ChatId
        {
            get;
        }

        int MessageId
        {
            get;
        }

        DateTime DateSent
        {
            get;
        }

        string Message
        {
            get;
        }
    }
}
