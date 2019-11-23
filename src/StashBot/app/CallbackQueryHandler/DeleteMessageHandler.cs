using StashBot.Module;
using StashBot.Module.Database;
using StashBot.Module.Message;

namespace StashBot.CallbackQueryHandler
{
    internal class DeleteMessageHandler : ICallbackQueryHandler
    {
        private const int MIN_PARAMETERS_COUNT = 3;

        public void Handle(string[] queryArray, int messageId)
        {
            IDatabaseManager databaseManager = ModulesManager.GetDatabaseManager();
            IMessageManager messageManager = ModulesManager.GetMessageManager();

            if (queryArray.Length < MIN_PARAMETERS_COUNT)
            {
                return;
            }

            bool isChatIdParsed = long.TryParse(queryArray[1], out long chatId);
            bool isDatabaseMessageIdParsed = long.TryParse(queryArray[2], out long databaseMessageId);
            if (!isChatIdParsed || !isDatabaseMessageIdParsed)
            {
                return;
            }

            databaseManager.DeleteStashMessage(chatId, databaseMessageId);
            messageManager.DeleteMessage(chatId, messageId);
        }
    }
}
