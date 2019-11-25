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

        public void HandleUserMessage(ITelegramUserMessage message)
        {
            ISessionManager sessionManager = ModulesManager.GetSessionManager();

            IChatSession chatSession = sessionManager.GetChatSession(message.ChatId);
            if (chatSession == null)
            {
                sessionManager.CreateChatSession(message.ChatId);
                chatSession = sessionManager.GetChatSession(message.ChatId);
            }

            sessionManager.UserSentMessage(message.ChatId, message.MessageId);
            chatStateHandlerFactory.GetChatStateHandler(chatSession.State).HandleUserMessage(message, this);
        }

        public void ChangeChatState(long chatId, ChatSessionState newState)
        {
            ISessionManager sessionManager = ModulesManager.GetSessionManager();

            IChatSession chatSession = sessionManager.GetChatSession(chatId);
            if (chatSession != null)
            {
                chatSession.State = newState;
                chatStateHandlerFactory.GetChatStateHandler(chatSession.State).StartStateMessage(chatId);
            }
        }
    }
}
