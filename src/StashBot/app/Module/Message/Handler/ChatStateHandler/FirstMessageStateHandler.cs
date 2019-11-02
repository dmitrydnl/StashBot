using StashBot.Module.Session;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal class FirstMessageStateHandler : IChatStateHandler
    {
        public void StartStateMessage(long chatId)
        {

        }

        public void HandleUserMessage(ITelegramUserMessage message, IChatStateHandlerContext context)
        {
            if (message == null || context == null)
            {
                return;
            }

            IMessageManager messageManager = ModulesManager.GetModulesManager().GetMessageManager();

            const string welcomeMessage = "Hi, good to see you!";
            messageManager.SendTextMessage(message.ChatId, welcomeMessage);
            context.ChangeChatState(message.ChatId, ChatSessionState.Start);
        }
    }
}
