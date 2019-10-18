using StashBot.Module.Session;
using StashBot.Module.Message.Handler.ChatStateHandler;

namespace StashBot.Module.Message.Handler
{
    internal class MessageHandler : IMessageHandler, IChatStateHandlerContext
    {
        private readonly IChatStateHandlerFactory chatStateHandlerFactory;

        internal MessageHandler()
        {
            chatStateHandlerFactory = new ChatStateHandlerFactory();
        }

        public void HandleUserMessage(long chatId, int messageId, string message)
        {
            ISessionManager sessionManager =
                ModulesManager.GetModulesManager().GetSessionManager();

            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            IChatSession chatSession = sessionManager.GetChatSession(chatId);
            if (chatSession == null)
            {
                sessionManager.CreateChatSession(chatId);
                chatSession = sessionManager.GetChatSession(chatId);
            }

            sessionManager.UserSentMessage(chatId, messageId);
            if (string.Equals(message, "/e") || string.Equals(message, "/exit"))
            {
                sessionManager.KillChatSession(chatId);
            }
            else
            {
                chatStateHandlerFactory.GetChatStateHandler(chatSession.State).HandleUserMessage(chatId, messageId, message, this);
            }
        }

        public void ChangeChatState(long chatId, ChatSessionState newChatSessionState)
        {
            ISessionManager sessionManager =
                ModulesManager.GetModulesManager().GetSessionManager();

            IChatSession chatSession = sessionManager.GetChatSession(chatId);
            if (chatSession != null)
            {
                chatSession.State = newChatSessionState;
                chatStateHandlerFactory.GetChatStateHandler(chatSession.State).StartStateMessage(chatId);
            }
        }
    }
}
