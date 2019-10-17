using StashBot.Module.Session;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal interface IChatStateHandlerFactory
    {
        IChatStateHandler GetChatStateHandler(ChatSessionState chatSessionState);
    }
}
