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
            chatCommands.AddExitCommand(true);
        }

        public void StartStateMessage(long chatId)
        {
            IMessageManager messageManager = ModulesManager.GetMessageManager();

            messageManager.SendTextMessageAsync(chatId, TextResponse.Get(ResponseType.RegistrationReady), chatCommands.CreateReplyKeyboard());
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
                messageManager.SendTextMessageAsync(message.ChatId, TextResponse.Get(ResponseType.Success), null);
                context.ChangeChatState(message.ChatId, ChatSessionState.Start);
            }
        }

        private bool CheckPassword(long chatId, string password)
        {
            IMessageManager messageManager = ModulesManager.GetMessageManager();

            if (string.IsNullOrEmpty(password))
            {
                messageManager.SendTextMessageAsync(chatId, TextResponse.Get(ResponseType.PasswordEmpty), null);
                return false;
            }

            if (!Regex.IsMatch(password, @"^[a-zA-Z0-9!""#$%&'()*+,-./:;<=>?@[\]^_`{|}~]+$"))
            {
                messageManager.SendTextMessageAsync(chatId, TextResponse.Get(ResponseType.PasswordCharacters), null);
                return false;
            }

            const int MIN_PASSWORD_LENGTH = 12;
            if (password.Length < MIN_PASSWORD_LENGTH)
            {
                messageManager.SendTextMessageAsync(chatId, TextResponse.Get(ResponseType.PasswordMinLength), null);
                return false;
            }

            const int MAX_PASSWORD_LENGTH = 25;
            if (password.Length > MAX_PASSWORD_LENGTH)
            {
                messageManager.SendTextMessageAsync(chatId, TextResponse.Get(ResponseType.PasswordMaxLength), null);
                return false;
            }

            return true;
        }

        private void Cancel(long chatId, IChatStateHandlerContext context)
        {
            context.ChangeChatState(chatId, ChatSessionState.Start);
        }
    }
}
