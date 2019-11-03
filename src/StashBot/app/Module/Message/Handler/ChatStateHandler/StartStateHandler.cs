using System.Collections.Generic;
using StashBot.BotResponses;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal class StartStateHandler : IChatStateHandler
    {
        private delegate void Command(long chatId, IChatStateHandlerContext context);
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

        public void StartStateMessage(long chatId)
        {
            IMessageManager messageManager = ModulesManager.GetModulesManager().GetMessageManager();

            messageManager.SendTextMessage(chatId, TextResponse.Get(ResponseType.MainCommands));
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
                StartStateMessage(message.ChatId);
            }
        }

        private bool IsCommand(string message)
        {
            return !string.IsNullOrEmpty(message) && commands.ContainsKey(message);
        }

        private void Registration(long chatId, IChatStateHandlerContext context)
        {
            context.ChangeChatState(chatId, Session.ChatSessionState.Registration);
        }

        private void Authorization(long chatId, IChatStateHandlerContext context)
        {
            context.ChangeChatState(chatId, Session.ChatSessionState.Authorisation);
        }

        private void Information(long chatId, IChatStateHandlerContext context)
        {
            IMessageManager messageManager = ModulesManager.GetModulesManager().GetMessageManager();

            messageManager.SendTextMessage(chatId, TextResponse.Get(ResponseType.Information));
        }
    }
}
