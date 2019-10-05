using System;
using System.Collections.Generic;

namespace StashBot.ChatSession
{
    internal class Session
    {
        private readonly long chatId;
        private DateTime lastUserMessage;
        private readonly List<int> botMessagesId;

        internal Session(long chatId)
        {
            this.chatId = chatId;
            lastUserMessage = DateTime.UtcNow;
            botMessagesId = new List<int>();
        }

        internal void UserMessageReceived()
        {
            lastUserMessage = DateTime.UtcNow;
        }

        internal void BotMessageSent(int messageId)
        {
            botMessagesId.Add(messageId);
        }

        internal long ChatId()
        {
            return chatId;
        }

        internal DateTime LastUserMessage()
        {
            return lastUserMessage;
        }

        internal List<int> BotMessagesId()
        {
            return botMessagesId;
        }
    }
}
