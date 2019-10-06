using System.Collections.Generic;

namespace StashBot.Module.Message.Handler
{
    internal class MessageHandler : IMessageHandler
    {
        private delegate void HandleCommand(long chatId);
        private readonly Dictionary<string, HandleCommand> commandsHandlers;

        internal MessageHandler()
        {
            commandsHandlers = new Dictionary<string, HandleCommand>();
            InitializeCommandsHandlers();
        }

        private void InitializeCommandsHandlers()
        {
            commandsHandlers.Add("/start", HandleStartCommand);
            commandsHandlers.Add("/reg", HandleRegistrationCommand);
            commandsHandlers.Add("/stash", HandleGetStashCommand);
        }

        public void HandleUserTextMessage(long chatId, int messageId, string textMessage)
        {
            if (string.IsNullOrEmpty(textMessage))
            {
                return;
            }

            if (IsCommand(textMessage))
            {
                HandleCommand handleCommand = commandsHandlers[textMessage];
                handleCommand(chatId);
            }
        }

        private bool IsCommand(string textMessage)
        {
            if (!textMessage.StartsWith('/'))
            {
                return false;
            }

            return commandsHandlers.ContainsKey(textMessage);
        }

        private void HandleStartCommand(long chatId)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            const string answer = 
                "Input command /reg for registration" +
                "\n" +
                "If you already registered, just input your key" +
                "\n" +
                "(WARNING)" +
                "\n" +
                "If you already registration," +
                "\n" +
                "after new registration you'll lose all old data";
            messageManager.SendTextMessage(chatId, answer);
        }

        private void HandleRegistrationCommand(long chatId)
        {
            IMessageManager messageManager = 
                ModulesManager.GetModulesManager().GetMessageManager();

            const string answer = "REGISTRATION COMMAND";
            messageManager.SendTextMessage(chatId, answer);
        }

        private void HandleGetStashCommand(long chatId)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            const string answer = "GET STASH COMMAND";
            messageManager.SendTextMessage(chatId, answer);
        }
    }
}
