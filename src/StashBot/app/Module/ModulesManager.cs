using StashBot.Module.Message;
using StashBot.Module.ChatSession;
using StashBot.WorkData;
using Telegram.Bot;

namespace StashBot.Module
{
    internal class ModulesManager : IModulesManager
    {
        private static IModulesManager modulesManager;

        private readonly ITelegramBotClient telegramBotClient;
        private readonly IMessageManager messageManager;
        private readonly ISessionsManager sessionsManager;

        private ModulesManager()
        {
            telegramBotClient = new TelegramBotClient(new BotToken().Get());
            messageManager = new MessageManager();
            sessionsManager = new SessionsManager();
        }

        internal static IModulesManager GetModulesManager()
        {
            if (modulesManager == null)
            {
                modulesManager = new ModulesManager();
            }

            return modulesManager;
        }

        public ITelegramBotClient GetTelegramBotClient()
        {
            return telegramBotClient;
        }

        public IMessageManager GetMessageManager()
        {
            return messageManager;
        }

        public ISessionsManager GetSessionsManager()
        {
            return sessionsManager;
        }
    }
}
