using StashBot.Module.Message;
using StashBot.Module.Session;
using StashBot.Module.User;
using StashBot.Module.Secure;
using StashBot.Module.Database;
using StashBot.AppSetting;
using Telegram.Bot;

namespace StashBot.Module
{
    internal class ModulesManager : IModulesManager
    {
        private static IModulesManager modulesManager;

        private readonly ITelegramBotClient telegramBotClient;
        private readonly IMessageManager messageManager;
        private readonly ISessionManager sessionManager;
        private readonly IUserManager userManager;
        private readonly ISecureManager secureManager;
        private readonly IDatabaseManager databaseManager;

        private ModulesManager()
        {
            telegramBotClient = new TelegramBotClient(new BotToken().Get());
            messageManager = new MessageManager();
            sessionManager = new SessionManager();
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

        public ISessionManager GetSessionManager()
        {
            return sessionManager;
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
