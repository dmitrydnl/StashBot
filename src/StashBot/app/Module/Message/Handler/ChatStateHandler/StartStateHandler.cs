using System.Collections.Generic;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal class StartStateHandler : IChatStateHandler
    {
        private delegate void Command(long chatId, IChatStateHandlerContext context);
        private readonly Dictionary<string, Command> commands;

        internal StartStateHandler(long chatId)
        {
            commands = new Dictionary<string, Command>();
            InitializeCommands();
            StartState(chatId);
        }

        private void InitializeCommands()
        {
            commands.Add("/reg", Registration);
            commands.Add("/auth", Authorization);
            commands.Add("/info", Information);
        }

        private void StartState(long chatId)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            const string mainCommandsMessage = "Registration: /reg\nAuthorization: /auth\nInformation about me: /info";
            messageManager.SendMessage(chatId, mainCommandsMessage);
        }

        public void HandleUserMessage(long chatId, int messageId, string message, IChatStateHandlerContext context)
        {
            if (commands.ContainsKey(message))
            {
                commands[message](chatId, context);
            }
            else
            {
                StartState(chatId);
            }
        }

        private void Registration(long chatId, IChatStateHandlerContext context)
        {
            context.ChangeChatState(new RegistrationStateHandler(chatId));
        }

        private void Authorization(long chatId, IChatStateHandlerContext context)
        {
            context.ChangeChatState(new AuthorisationStateHandler(chatId));
        }

        private void Information(long chatId, IChatStateHandlerContext context)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            const string informationMessage = "This is information";
            messageManager.SendMessage(chatId, informationMessage);
        }
    }
}
