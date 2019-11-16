using System.Collections.Generic;
using System.Text.RegularExpressions;
using StashBot.Module.User;
using StashBot.BotResponses;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal class CreateUserPasswordStateHandler : IChatStateHandler
    {
        private delegate void Command(long chatId, IChatStateHandlerContext context);
        private readonly Dictionary<string, Command> commands;

        internal CreateUserPasswordStateHandler()
        {
            commands = new Dictionary<string, Command>();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            commands.Add("/cancel", Cancel);
        }

        public void StartStateMessage(long chatId)
        {
            IMessageManager messageManager = ModulesManager.GetModulesManager().GetMessageManager();

            messageManager.SendTextMessage(chatId, TextResponse.Get(ResponseType.RegistrationReady));
        }

        public void HandleUserMessage(ITelegramUserMessage message, IChatStateHandlerContext context)
        {
            if (message == null || context == null)
            {
                return;
            }

            if (IsCommand(message.Message))
            {
                commands[message.Message](message.ChatId, context);
            }
            else
            {
                if (!string.IsNullOrEmpty(message.Message))
                {
                    RegistrationUser(message, context);
                }
            }
        }

        private bool IsCommand(string message)
        {
            return !string.IsNullOrEmpty(message) && commands.ContainsKey(message);
        }

        private void RegistrationUser(ITelegramUserMessage message, IChatStateHandlerContext context)
        {
            IMessageManager messageManager = ModulesManager.GetModulesManager().GetMessageManager();
            IUserManager userManager = ModulesManager.GetModulesManager().GetUserManager();

            messageManager.DeleteMessage(message.ChatId, message.MessageId);

            if (CheckPassword(message.ChatId, message.Message))
            {
                userManager.CreateNewUser(message.ChatId, message.Message);
                messageManager.SendTextMessage(message.ChatId, TextResponse.Get(ResponseType.SuccessRegistration));
                context.ChangeChatState(message.ChatId, Session.ChatSessionState.Start);
            }
        }

        private bool CheckPassword(long chatId, string password)
        {
            IMessageManager messageManager = ModulesManager.GetModulesManager().GetMessageManager();

            if (string.IsNullOrEmpty(password))
            {
                messageManager.SendTextMessage(chatId, TextResponse.Get(ResponseType.PasswordEmpty));
                return false;
            }

            if (password.Length < 12)
            {
                messageManager.SendTextMessage(chatId, TextResponse.Get(ResponseType.PasswordMinLength));
                return false;
            }

            if (password.Length > 25)
            {
                messageManager.SendTextMessage(chatId, TextResponse.Get(ResponseType.PasswordMaxLength));
                return false;
            }

            if (!Regex.IsMatch(password, @"^[a-zA-Z0-9!""#$%&'()*+,-./:;<=>?@[\]^_`{|}~]+$"))
            {
                messageManager.SendTextMessage(chatId, TextResponse.Get(ResponseType.PasswordCharacters));
                return false;
            }

            return true;
        }

        private void Cancel(long chatId, IChatStateHandlerContext context)
        {
            context.ChangeChatState(chatId, Session.ChatSessionState.Start);
        }
    }
}
