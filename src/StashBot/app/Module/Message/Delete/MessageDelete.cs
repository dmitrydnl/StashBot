using System.Collections.Generic;
using Telegram.Bot;
using StashBot.Module.Session;

namespace StashBot.Module.Message.Delete
{
    internal class MessageDelete : IMessageDelete
    {
        public void DeleteMessage(long chatId, int messageId)
        {
            ITelegramBotClient telegramBotClient = ModulesManager.GetTelegramBotClient();
            ISessionManager sessionManager = ModulesManager.GetSessionManager();

            sessionManager.GetChatSession(chatId).MessageDeleted(messageId);
            telegramBotClient.DeleteMessageAsync(chatId, messageId);
        }

        public void DeleteListMessages(long chatId, List<int> messagesId)
        {
            foreach (int id in messagesId)
            {
                DeleteMessage(chatId, id);
            }
        }
    }
}
