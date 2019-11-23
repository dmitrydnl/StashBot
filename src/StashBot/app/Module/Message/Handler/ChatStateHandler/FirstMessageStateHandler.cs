using StashBot.Module.Session;
using StashBot.BotResponses;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal class FirstMessageStateHandler : IChatStateHandler
    {
        public void StartStateMessage(long chatId)
        {
            // Do nothing
        }

        public void HandleUserMessage(ITelegramUserMessage message, IChatStateHandlerContext context)
        {
            if (message == null || context == null)
            {
                return;
            }

            IMessageManager messageManager = ModulesManager.GetMessageManager();

            messageManager.SendTextMessageAsync(message.ChatId, TextResponse.Get(ResponseType.WelcomeMessage), null);
            context.ChangeChatState(message.ChatId, ChatSessionState.Start);
        }
    }
}
