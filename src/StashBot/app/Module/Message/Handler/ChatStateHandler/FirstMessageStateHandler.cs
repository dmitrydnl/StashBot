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
            const string welcomeMessage = "Hi, good to see you!";
            messageManager.SendMessage(chatId, welcomeMessage);
            context.ChangeChatState(new StartStateHandler(chatId));
        }
    }
}
