using StashBot.Module.Session;

namespace StashBot.Module.Message.Handler
{
    internal interface IChatStateHandlerContext
    {
        void ChangeChatState(long chatId, ChatSessionState newChatSessionState);
    }
}
