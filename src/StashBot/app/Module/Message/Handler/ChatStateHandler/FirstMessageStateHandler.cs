using StashBot.Module.Session;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal class FirstMessageStateHandler : IChatStateHandler
    {
        public void HandleUserMessage(long chatId, int messageId, string message, IChatStateHandlerContext context)
        {
            ISessionManager sessionManager =
                ModulesManager.GetModulesManager().GetSessionManager();
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            sessionManager.CreateChatSession(chatId);
            messageManager.SendTextMessage(chatId, WelcomeMessage());
            context.ChangeChatState(new StartStateHandler());
        }

        private string WelcomeMessage()
        {
            return "Hi, good to see you!" +
                "\n" +
                "For registration: /reg" +
                "\n" +
                "For authorization: /auth" +
                "\n" +
                "For information about me: /info";
        }
    }
}
