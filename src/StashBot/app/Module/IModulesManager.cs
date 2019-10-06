using StashBot.Module.Message;
using StashBot.Module.ChatSession;
using Telegram.Bot;

namespace StashBot.Module
{
    internal interface IModulesManager
    {
        ITelegramBotClient GetTelegramBotClient();
        IMessageManager GetMessageManager();
        ISessionsManager GetSessionsManager();
    }
}
