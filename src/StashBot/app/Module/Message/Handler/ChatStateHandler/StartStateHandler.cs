using System.Collections.Generic;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal class StartStateHandler : IChatStateHandler
    {
        private delegate void Command(long chatId, int messageId, IChatStateHandlerContext context);
        private readonly Dictionary<string, Command> commands;

        internal StartStateHandler()
        {
            commands = new Dictionary<string, Command>();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            commands.Add("/reg", Registration);
            commands.Add("/auth", Authorization);
            commands.Add("/info", Information);
        }

        public void HandleUserMessage(long chatId, int messageId, string message, IChatStateHandlerContext context)
        {
            if (commands.ContainsKey(message))
            {
                commands[message](chatId, messageId, context);
            }
        }

        private void Registration(long chatId, int messageId, IChatStateHandlerContext context)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            messageManager.SendTextMessage(chatId, RegistrationMessage());
            context.ChangeChatState(new RegistrationStateHandler());
        }

        private void Authorization(long chatId, int messageId, IChatStateHandlerContext context)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            messageManager.SendTextMessage(chatId, AuthorizationMessage());
            context.ChangeChatState(new AuthorisationStateHandler());
        }

        private void Information(long chatId, int messageId, IChatStateHandlerContext context)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            messageManager.SendTextMessage(chatId, InformationMessage());
        }

        private string RegistrationMessage()
        {
            return "It is registration, send yes";
        }

        private string AuthorizationMessage()
        {
            return "It is authorization";
        }

        private string InformationMessage()
        {
            return "It is information";
        }
    }
}
