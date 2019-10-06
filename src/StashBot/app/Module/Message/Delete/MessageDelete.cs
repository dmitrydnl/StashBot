using System.Collections.Generic;
using Telegram.Bot;

namespace StashBot.Module.Message.Delete
{
    internal class MessageDelete : IMessageDelete
    {
        internal MessageDelete()
        {
        }

        public void DeleteBotMessage(long chatId, int messageId)
        {
            ITelegramBotClient telegramBotClient =
                ModulesManager.GetModulesManager().GetTelegramBotClient();

            telegramBotClient.DeleteMessageAsync(chatId, messageId);
        }

        public void DeleteListBotMessages(long chatId, List<int> messagesId)
        {
            foreach (int id in messagesId)
            {
                DeleteBotMessage(chatId, id);
            }
        }
    }
}
