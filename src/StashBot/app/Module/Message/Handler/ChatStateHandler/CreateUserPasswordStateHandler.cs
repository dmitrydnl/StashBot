using System.Text.RegularExpressions;
using StashBot.Module.User;
using StashBot.BotResponses;
using StashBot.Module.Session;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal class CreateUserPasswordStateHandler : IChatStateHandler
    {
        private readonly IChatCommands chatCommands;

        internal CreateUserPasswordStateHandler()
        {
            chatCommands = new ChatCommands();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            chatCommands.Add("/cancel", true, Cancel);
            chatCommands.Add("/exit", true, Exit);
        }

        public void StartStateMessage(long chatId)
        {
            IMessageManager messageManager = ModulesManager.GetMessageManager();

            messageManager.SendTextMessage(chatId, TextResponse.Get(ResponseType.RegistrationReady), chatCommands.CreateReplyKeyboard());
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
                    RegistrationUser(message, context);
                }
            }
        }

        private void RegistrationUser(ITelegramUserMessage message, IChatStateHandlerContext context)
        {
            IMessageManager messageManager = ModulesManager.GetMessageManager();
            IUserManager userManager = ModulesManager.GetUserManager();

            messageManager.DeleteMessage(message.ChatId, message.MessageId);

            if (CheckPassword(message.ChatId, message.Message))
            {
                userManager.CreateNewUser(message.ChatId, message.Message);
                messageManager.SendTextMessage(message.ChatId, TextResponse.Get(ResponseType.SuccessRegistration), null);
                context.ChangeChatState(message.ChatId, ChatSessionState.Start);
            }
        }

        private bool CheckPassword(long chatId, string password)
        {
            IMessageManager messageManager = ModulesManager.GetMessageManager();

            if (string.IsNullOrEmpty(password))
            {
                messageManager.SendTextMessage(chatId, TextResponse.Get(ResponseType.PasswordEmpty), null);
                return false;
            }

            if (password.Length < 12)
            {
                messageManager.SendTextMessage(chatId, TextResponse.Get(ResponseType.PasswordMinLength), null);
                return false;
            }

            if (password.Length > 25)
            {
                messageManager.SendTextMessage(chatId, TextResponse.Get(ResponseType.PasswordMaxLength), null);
                return false;
            }

            if (!Regex.IsMatch(password, @"^[a-zA-Z0-9!""#$%&'()*+,-./:;<=>?@[\]^_`{|}~]+$"))
            {
                messageManager.SendTextMessage(chatId, TextResponse.Get(ResponseType.PasswordCharacters), null);
                return false;
            }

            return true;
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
