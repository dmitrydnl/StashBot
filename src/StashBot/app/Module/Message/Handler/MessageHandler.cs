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

            if (string.IsNullOrEmpty(textMessage))
            {
                return;
            }

            if (!sessionsManager.ContainsSession(chatId))
            {
                FirstMessageHandle(chatId);
                return;
            }

            if (IsCommand(textMessage))
            {
                CommandHandle(chatId, messageId, textMessage);
                return;
            }

            if (!sessionsManager.GetSession(chatId).IsAuthorized())
            {
                AuthorisationHandle(chatId, messageId, textMessage);
                return;
            }

            if (sessionsManager.GetSession(chatId).IsAuthorized())
            {
                AddDataToStashHandle(chatId, messageId, textMessage);
                return;
            }
        }

        private void FirstMessageHandle(long chatId)
        {
            ISessionsManager sessionsManager =
                ModulesManager.GetModulesManager().GetSessionsManager();
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            sessionsManager.CreateSession(chatId);
            messageManager.SendWelcomeMessage(chatId);
        }

        private void CommandHandle(long chatId, int messageId, string textMessage)
        {
            ISessionsManager sessionsManager =
                ModulesManager.GetModulesManager().GetSessionsManager();

            sessionsManager.UserSentMessage(chatId, messageId);
            HandleCommand handleCommand = commandsHandlers[textMessage];
            handleCommand(chatId);
        }

        private void AuthorisationHandle(long chatId, int messageId, string textMessage)
        {
            ISessionsManager sessionsManager =
                ModulesManager.GetModulesManager().GetSessionsManager();
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            sessionsManager.UserSentMessage(chatId, messageId);
            //TODO authorisation
            const string answer = "Authorisation";
            messageManager.SendTextMessage(chatId, answer);
        }

        private void AddDataToStashHandle(long chatId, int messageId, string textMessage)
        {
            ISessionsManager sessionsManager =
                ModulesManager.GetModulesManager().GetSessionsManager();
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            sessionsManager.UserSentMessage(chatId, messageId);
            //TODO add data to stash
            const string answer = "Add Data To Stash";
            messageManager.SendTextMessage(chatId, answer);
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
            ISessionsManager sessionsManager =
                ModulesManager.GetModulesManager().GetSessionsManager();
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            if (!sessionsManager.GetSession(chatId).IsAuthorized())
            {
                messageManager.SendWelcomeMessage(chatId);
            }
        }

        private void HandleRegistrationCommand(long chatId)
        {
            ISessionsManager sessionsManager =
                ModulesManager.GetModulesManager().GetSessionsManager();
            IMessageManager messageManager = 
                ModulesManager.GetModulesManager().GetMessageManager();

            if (!sessionsManager.GetSession(chatId).IsAuthorized())
            {
                //TODO registration
                const string answer = "REGISTRATION COMMAND";
                messageManager.SendTextMessage(chatId, answer);
            }
        }

        private void HandleGetStashCommand(long chatId)
        {
            ISessionsManager sessionsManager =
                ModulesManager.GetModulesManager().GetSessionsManager();
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            if (sessionsManager.GetSession(chatId).IsAuthorized())
            {
                //TODO get stash
                const string answer = "GET STASH COMMAND";
                messageManager.SendTextMessage(chatId, answer);
            }
        }
    }
}
