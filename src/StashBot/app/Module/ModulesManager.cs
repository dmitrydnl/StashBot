using StashBot.Module.Message;
using StashBot.Module.Session;
using StashBot.Module.User;
using StashBot.Module.Secure;
using StashBot.Module.Database;
using StashBot.AppSetting;
using Telegram.Bot;

namespace StashBot.Module
{
    internal class ModulesManager
    {
        private static ITelegramBotClient telegramBotClient;
        private static IMessageManager messageManager;
        private static ISessionManager sessionManager;
        private static IUserManager userManager;
        private static ISecureManager secureManager;
        private static IDatabaseManager databaseManager;

        internal static ITelegramBotClient GetTelegramBotClient()
        {
            if (telegramBotClient == null)
            {
                telegramBotClient = new TelegramBotClient(BotToken.Get());
            }

            return telegramBotClient;
        }

        internal static IMessageManager GetMessageManager()
        {
            if (messageManager == null)
            {
                messageManager = new MessageManager();
            }

            return messageManager;
        }

        internal static ISessionManager GetSessionManager()
        {
            if (sessionManager == null)
            {
                sessionManager = new SessionManager();
            }

            return sessionManager;
        }

        internal static IUserManager GetUserManager()
        {
            if (userManager == null)
            {
                userManager = new UserManager();
            }

            return userManager;
        }

        internal static ISecureManager GetSecureManager()
        {
            if (secureManager == null)
            {
                secureManager = new SecureManager();
            }

            return secureManager;
        }

        internal static IDatabaseManager GetDatabaseManager()
        {
            if (databaseManager == null)
            {
                databaseManager = new DatabaseManager();
            }

            return databaseManager;
        }
    }
}
