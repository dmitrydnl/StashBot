using StashBot.Module.Session;
using StashBot.Module.Message.Handler.ChatStateHandler;

namespace StashBot.Module.Message.Handler
{
    internal class MessageHandler : IMessageHandler, IChatStateHandlerContext
    {
        private IChatStateHandler chatStateHandler;

        public void HandleUserMessage(long chatId, int messageId, string message)
        {
            ISessionManager sessionManager =
                ModulesManager.GetModulesManager().GetSessionManager();

            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            if (!sessionManager.IsChatSessionExist(chatId))
            {
                chatStateHandler = new FirstMessageStateHandler();
            }

            chatStateHandler.HandleUserMessage(chatId, messageId, message, this);
            sessionManager.UserSentMessage(chatId, messageId);
        }

        public void ChangeChatState(IChatStateHandler newChatStateHandler)
        {
            chatStateHandler = newChatStateHandler;
        }
    }
}
