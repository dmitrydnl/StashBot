using StashBot.Module.Message.Handler.ChatStateHandler;

namespace StashBot.Module.Message.Handler
{
    internal interface IChatStateHandlerContext
    {
        void ChangeChatState(IChatStateHandler newChatStateHandler);
    }
}
