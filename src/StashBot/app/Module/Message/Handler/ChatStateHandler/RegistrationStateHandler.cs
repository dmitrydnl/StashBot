using System.Collections.Generic;

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

            const string warningMessage = "If you have already registered you will lose all your old data!\nAre you sure? /yes or /no";
            messageManager.SendMessage(chatId, warningMessage);
        }

        public void HandleUserMessage(ITelegramUserMessage message, IChatStateHandlerContext context)
        {
            if (commands.ContainsKey(message.Message))
            {
                commands[message.Message](message.ChatId, context);
            }
            else
            {
                StartStateMessage(message.ChatId);
            }
        }

        private void Registration(long chatId, IChatStateHandlerContext context)
        {
            context.ChangeChatState(chatId, Session.ChatSessionState.CreateUserPassword);
        }

        private void Cancel(long chatId, IChatStateHandlerContext context)
        {
            context.ChangeChatState(chatId, Session.ChatSessionState.Start);
        }
    }
}
