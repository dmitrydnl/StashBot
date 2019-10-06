using StashBot.Module.Message;
using StashBot.Module.ChatSession;
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
        ISessionsManager GetSessionsManager();
        IUserManager GetUserManager();
        ISecureManager GetSecureManager();
        IDatabaseManager GetDatabaseManager();
    }
}
