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

        public void HandleUserMessage(long chatId, int messageId, string message, IChatStateHandlerContext context)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();
            IUserManager userManager =
                ModulesManager.GetModulesManager().GetUserManager();

            if (commands.ContainsKey(message))
            {
                commands[message](chatId, context);
            }
            else
            {
                bool success = userManager.LoginUser(chatId, message);
                if (success)
                {
                    const string successMessage = "Success!";
                    messageManager.SendMessage(chatId, successMessage);
                    context.ChangeChatState(chatId, Session.ChatSessionState.Authorized);
                }
                else
                {
                    const string wrongMessage = "WRONG";
                    messageManager.SendMessage(chatId, wrongMessage);
                }
            }
        }

        private void Cancel(long chatId, IChatStateHandlerContext context)
        {
            context.ChangeChatState(chatId, Session.ChatSessionState.Start);
        }
    }
}
