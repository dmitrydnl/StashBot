using StashBot.Module.Message;
using StashBot.Module.Session;
using StashBot.Module.User;
using StashBot.Module.Secure;
using StashBot.Module.Database;
using Telegram.Bot;

namespace StashBot.Module
{
    internal interface IModulesManager
    {
        ITelegramBotClient GetTelegramBotClient();
        IMessageManager GetMessageManager();
        ISessionManager GetSessionManager();
        IUserManager GetUserManager();
        ISecureManager GetSecureManager();
        IDatabaseManager GetDatabaseManager();
    }
}
