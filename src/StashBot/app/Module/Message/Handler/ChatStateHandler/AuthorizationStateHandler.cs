using System.Collections.Generic;
using StashBot.Module.User;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal class AuthorisationStateHandler : IChatStateHandler
    {
        private delegate void Command(long chatId, IChatStateHandlerContext context);
        private readonly Dictionary<string, Command> commands;

        internal AuthorisationStateHandler()
        {
            commands = new Dictionary<string, Command>();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            commands.Add("/back", Cancel);
        }

        public void StartStateMessage(long chatId)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            const string warningMessage = "Input your password or /back";
            messageManager.SendMessage(chatId, warningMessage);
        }

        public void HandleUserMessage(ITelegramUserMessage message, IChatStateHandlerContext context)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();
            IUserManager userManager =
                ModulesManager.GetModulesManager().GetUserManager();

            if (IsCommand(message.Message))
            {
                commands[message.Message](message.ChatId, context);
            }
            else
            {
                if (!string.IsNullOrEmpty(message.Message))
                {
                    LoginUser(message, context);
                }
            }
        }

        private bool IsCommand(string message)
        {
            return !string.IsNullOrEmpty(message) && commands.ContainsKey(message);
        }

        private void LoginUser(ITelegramUserMessage message, IChatStateHandlerContext context)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();
            IUserManager userManager =
                ModulesManager.GetModulesManager().GetUserManager();

            bool success = userManager.LoginUser(message.ChatId, message.Message);
            if (success)
            {
                const string successMessage = "Success!";
                messageManager.SendMessage(message.ChatId, successMessage);
                context.ChangeChatState(message.ChatId, Session.ChatSessionState.Authorized);
            }
            else
            {
                const string wrongMessage = "WRONG";
                messageManager.SendMessage(message.ChatId, wrongMessage);
            }
        }

        private void Cancel(long chatId, IChatStateHandlerContext context)
        {
            context.ChangeChatState(chatId, Session.ChatSessionState.Start);
        }
    }
}
