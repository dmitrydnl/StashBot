using System.Collections.Generic;
using StashBot.Module.User;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal class RegistrationStateHandler : IChatStateHandler
    {
        private delegate void Command(long chatId, IChatStateHandlerContext context);
        private readonly Dictionary<string, Command> commands;

        internal RegistrationStateHandler()
        {
            commands = new Dictionary<string, Command>();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            commands.Add("/yes", Registration);
            commands.Add("/no", Cancel);
        }

        public void StartStateMessage(long chatId)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            const string warningMessage = "If you have already registered you will lose all your old data!";
            messageManager.SendMessage(chatId, warningMessage);

            const string questionMessage = "Are you sure? /yes or /no";
            messageManager.SendMessage(chatId, questionMessage);
        }

        public void HandleUserMessage(long chatId, int messageId, string message, IChatStateHandlerContext context)
        {
            if (commands.ContainsKey(message))
            {
                commands[message](chatId, context);
            }
            else
            {
                StartStateMessage(chatId);
            }
        }

        private void Registration(long chatId, IChatStateHandlerContext context)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();
            IUserManager userManager =
                ModulesManager.GetModulesManager().GetUserManager();

            string authCode = userManager.CreateNewUser(chatId);
            string successMessage = $"Success!\nUse your code for auth:\n{authCode}";
            messageManager.SendMessage(chatId, successMessage);
            context.ChangeChatState(chatId, Session.ChatSessionState.Start);
        }

        private void Cancel(long chatId, IChatStateHandlerContext context)
        {
            context.ChangeChatState(chatId, Session.ChatSessionState.Start);
        }
    }
}
