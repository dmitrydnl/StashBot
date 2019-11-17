using StashBot.BotResponses;
using StashBot.Module.Session;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal class StartStateHandler : IChatStateHandler
    {
        private readonly IChatCommands chatCommands;

        internal StartStateHandler()
        {
            chatCommands = new ChatCommands();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            chatCommands.Add("/SignUp", Registration);
            chatCommands.Add("/SignIn", Authorization);
            chatCommands.Add("/Info", Information);
            chatCommands.Add("/exit", Exit);
        }

        public void StartStateMessage(long chatId)
        {
            IMessageManager messageManager = ModulesManager.GetModulesManager().GetMessageManager();

            messageManager.SendTextMessage(chatId, TextResponse.Get(ResponseType.MainCommands), chatCommands.CreateReplyKeyboard());
        }

        public void HandleUserMessage(ITelegramUserMessage message, IChatStateHandlerContext context)
        {
            if (message == null || context == null)
            {
                return;
            }

            if (chatCommands.ContainsCommand(message.Message))
            {
                chatCommands.Get(message.Message)(message.ChatId, context);
            }
            else
            {
                StartStateMessage(message.ChatId);
            }
        }

        private void Registration(long chatId, IChatStateHandlerContext context)
        {
            context.ChangeChatState(chatId, Session.ChatSessionState.Registration);
        }

        private void Authorization(long chatId, IChatStateHandlerContext context)
        {
            context.ChangeChatState(chatId, Session.ChatSessionState.Authorisation);
        }

        private void Information(long chatId, IChatStateHandlerContext context)
        {
            IMessageManager messageManager = ModulesManager.GetModulesManager().GetMessageManager();

            messageManager.SendTextMessage(chatId, TextResponse.Get(ResponseType.Information), chatCommands.CreateReplyKeyboard());
        }

        private void Exit(long chatId, IChatStateHandlerContext context)
        {
            ISessionManager sessionManager = ModulesManager.GetModulesManager().GetSessionManager();

            sessionManager.KillChatSession(chatId);
        }
    }
}
