using System.Collections.Generic;

namespace StashBot.Module.Message.Delete
{
    internal interface IMessageDelete
    {
        void DeleteMessage(long chatId, int messageId);
        void DeleteListMessages(long chatId, List<int> messagesId);
    }
}
