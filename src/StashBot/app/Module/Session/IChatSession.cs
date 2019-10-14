using System;
using System.Collections.Generic;

namespace StashBot.Module.Session
{
    internal interface IChatSession
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
