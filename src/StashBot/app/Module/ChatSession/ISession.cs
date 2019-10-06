using System;
using System.Collections.Generic;

namespace StashBot.Module.ChatSession
{
    internal interface ISession
    {
        void UserSentMessage(int messageId);
        void BotSentMessage(int messageId);
        void Authorize();
        long ChatId();
        bool IsAuthorized();
        DateTime LastUserMessage();
        List<int> BotMessagesId();
    }
}
