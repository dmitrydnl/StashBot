using StashBot.Module.Message;
using StashBot.Module.ChatSession;
using StashBot.Module.User;
using StashBot.Module.Secure;
using StashBot.Module.Database;
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
        private readonly IUserManager userManager;
        private readonly ISecureManager secureManager;
        private readonly IDatabaseManager databaseManager;

        private ModulesManager()
        {
            telegramBotClient = new TelegramBotClient(new BotToken().Get());
            messageManager = new MessageManager();
            sessionsManager = new SessionsManager();
            userManager = new UserManager();
            secureManager = new SecureManager();
            databaseManager = new DatabaseManager();
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

        public IUserManager GetUserManager()
        {
            return userManager;
        }

        public ISecureManager GetSecureManager()
        {
            return secureManager;
        }

        public IDatabaseManager GetDatabaseManager()
        {
            return databaseManager;
        }
    }
}
