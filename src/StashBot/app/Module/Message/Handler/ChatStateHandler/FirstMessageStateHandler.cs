using StashBot.Module.Session;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal class FirstMessageStateHandler : IChatStateHandler
    {
        public void StartStateMessage(long chatId)
        {

        }

        public void HandleUserMessage(long chatId, int messageId, string message, IChatStateHandlerContext context)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            const string welcomeMessage = "Hi, good to see you!";
            messageManager.SendMessage(chatId, welcomeMessage);
            context.ChangeChatState(chatId, ChatSessionState.Start);
        }
    }
}
