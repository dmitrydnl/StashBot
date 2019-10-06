using System.Collections.Generic;

namespace StashBot.Module.Message.Delete
{
    internal interface IMessageDelete
    {
        void DeleteBotMessage(long chatId, int messageId);
        void DeleteListBotMessages(long chatId, List<int> messagesId);
    }
}
