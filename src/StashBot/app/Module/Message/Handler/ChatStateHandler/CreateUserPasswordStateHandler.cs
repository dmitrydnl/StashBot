using System.Collections.Generic;
using System.Text.RegularExpressions;
using StashBot.Module.User;

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
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            const string warningMessage = "Input your password or /cancel";
            messageManager.SendMessage(chatId, warningMessage);
        }

        public void HandleUserMessage(long chatId, int messageId, string message, IChatStateHandlerContext context)
        {
            if (commands.ContainsKey(message))
            {
                commands[message](chatId, context);
            }
            else
            {
                if (CheckPassword(chatId, message))
                {
                    RegistrationUser(chatId, message, context);
                }
            }
        }

        private void Cancel(long chatId, IChatStateHandlerContext context)
        {
            context.ChangeChatState(chatId, Session.ChatSessionState.Start);
        }

        private bool CheckPassword(long chatId, string password)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            if (string.IsNullOrEmpty(password))
            {
                const string warningMessage = "Input password";
                messageManager.SendMessage(chatId, warningMessage);
                return false;
            }

            if (password.Length < 12)
            {
                const string warningMessage = "Password min length 12!";
                messageManager.SendMessage(chatId, warningMessage);
                return false;
            }

            if (password.Length > 25)
            {
                const string warningMessage = "Password max length 25!";
                messageManager.SendMessage(chatId, warningMessage);
                return false;
            }

            if (!Regex.IsMatch(password, @"^[a-zA-Z0-9!""#$%&'()*+,-./:;<=>?@[\]^_`{|}~]+$"))
            {
                const string warningMessage = "Password can contain only letters, numbers and special characters!";
                messageManager.SendMessage(chatId, warningMessage);
                return false;
            }

            return true;
        }

        private void RegistrationUser(long chatId, string password, IChatStateHandlerContext context)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();
            IUserManager userManager =
                ModulesManager.GetModulesManager().GetUserManager();

            userManager.CreateNewUser(chatId, password);
            string successMessage = "Success!\nNow you can auth with password";
            messageManager.SendMessage(chatId, successMessage);
            context.ChangeChatState(chatId, Session.ChatSessionState.Start);
        }
    }
}
