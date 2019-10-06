using System.Collections.Generic;
using StashBot.Module.ChatSession;

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
            ISessionsManager sessionsManager =
                ModulesManager.GetModulesManager().GetSessionsManager();
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            if (string.IsNullOrEmpty(textMessage))
            {
                return;
            }

            if (!sessionsManager.ContainsSession(chatId))
            {
                sessionsManager.CreateSession(chatId);
                messageManager.SendWelcomeMessage(chatId);
                return;
            }

            sessionsManager.UserSentMessage(chatId, messageId);

            if (IsCommand(textMessage))
            {
                HandleCommand handleCommand = commandsHandlers[textMessage];
                handleCommand(chatId);
                return;
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
                
            messageManager.SendWelcomeMessage(chatId);
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
