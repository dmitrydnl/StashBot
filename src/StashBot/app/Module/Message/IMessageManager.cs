using System.Collections.Generic;

namespace StashBot.Module.Message
{
    internal interface IMessageManager
    {
        void UserSentTextMessage(long chatId, int messageId, string textMessage);
        void SendTextMessage(long chatId, string textMessage);
        void SendWelcomeMessage(long chatId);
        void DeleteBotMessage(long chatId, int messageId);
        void DeleteListBotMessages(long chatId, List<int> messagesId);
    }
}
