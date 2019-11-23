using StashBot.BotResponses;
using StashBot.Module.Session;
using StashBot.Module.User;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal class AuthorisationStateHandler : IChatStateHandler
    {
        private readonly IChatCommands chatCommands;

        internal AuthorisationStateHandler()
        {
            chatCommands = new ChatCommands();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            chatCommands.Add("/back", Cancel);
            chatCommands.Add("/exit", Exit);
        }

        public void StartStateMessage(long chatId)
        {
            IMessageManager messageManager = ModulesManager.GetMessageManager();

            messageManager.SendTextMessage(chatId, TextResponse.Get(ResponseType.AuthorisationReady), chatCommands.CreateReplyKeyboard());
        }

        public void HandleUserMessage(ITelegramUserMessage message, IChatStateHandlerContext context)
        {
            if (message == null || context == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(message.Message) && chatCommands.ContainsCommand(message.Message))
            {
                chatCommands.Get(message.Message)(message.ChatId, context);
            }
            else
            {
                if (!string.IsNullOrEmpty(message.Message))
                {
                    LoginUser(message, context);
                }
            }
        }

        private void LoginUser(ITelegramUserMessage message, IChatStateHandlerContext context)
        {
            IMessageManager messageManager = ModulesManager.GetMessageManager();
            IUserManager userManager = ModulesManager.GetUserManager();

            messageManager.DeleteMessage(message.ChatId, message.MessageId);

            bool success = userManager.LoginUser(message.ChatId, message.Message);
            if (success)
            {
                messageManager.SendTextMessage(message.ChatId, TextResponse.Get(ResponseType.SuccessAuthorisation), null);
                context.ChangeChatState(message.ChatId, ChatSessionState.Authorized);
            }
            else
            {
                messageManager.SendTextMessage(message.ChatId, TextResponse.Get(ResponseType.FailAuthorisation), null);
            }
        }

        private void Cancel(long chatId, IChatStateHandlerContext context)
        {
            context.ChangeChatState(chatId, ChatSessionState.Start);
        }

        private void Exit(long chatId, IChatStateHandlerContext context)
        {
            ISessionManager sessionManager = ModulesManager.GetSessionManager();

            sessionManager.KillChatSession(chatId);
        }
    }
}
