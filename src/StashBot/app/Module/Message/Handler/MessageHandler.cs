using System.Collections.Generic;
using StashBot.Module.Session;
using StashBot.Module.User;

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
            ISessionManager sessionManager =
                ModulesManager.GetModulesManager().GetSessionManager();

            if (string.IsNullOrEmpty(textMessage))
            {
                return;
            }

            if (!sessionManager.ContainsChatSession(chatId))
            {
                FirstMessageHandle(chatId);
                return;
            }

            if (IsCommand(textMessage))
            {
                CommandHandle(chatId, messageId, textMessage);
                return;
            }

            if (!sessionManager.GetChatSession(chatId).IsAuthorized())
            {
                AuthorisationHandle(chatId, messageId, textMessage);
                return;
            }

            if (sessionManager.GetChatSession(chatId).IsAuthorized())
            {
                AddDataToStashHandle(chatId, messageId, textMessage);
                return;
            }
        }

        private void FirstMessageHandle(long chatId)
        {
            ISessionManager sessionManager =
                ModulesManager.GetModulesManager().GetSessionManager();
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            sessionManager.CreateChatSession(chatId);
            messageManager.SendWelcomeMessage(chatId);
        }

        private void CommandHandle(long chatId, int messageId, string textMessage)
        {
            ISessionManager sessionManager =
                ModulesManager.GetModulesManager().GetSessionManager();

            sessionManager.UserSentMessage(chatId, messageId);
            HandleCommand handleCommand = commandsHandlers[textMessage];
            handleCommand(chatId);
        }

        private void AuthorisationHandle(long chatId, int messageId, string textMessage)
        {
            ISessionManager sessionManager =
                ModulesManager.GetModulesManager().GetSessionManager();
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();
            IUserManager userManager =
                ModulesManager.GetModulesManager().GetUserManager();

            sessionManager.UserSentMessage(chatId, messageId);
            if (userManager.AuthorisationUser(chatId, textMessage))
            {
                messageManager.SendAuthorisationSuccessMessage(chatId);
            }
            else
            {
                messageManager.SendAuthorisationFailMessage(chatId);
            }
        }

        private void AddDataToStashHandle(long chatId, int messageId, string textMessage)
        {
            ISessionManager sessionManager =
                ModulesManager.GetModulesManager().GetSessionManager();
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            sessionManager.UserSentMessage(chatId, messageId);
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
            ISessionManager sessionManager =
                ModulesManager.GetModulesManager().GetSessionManager();
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            if (!sessionManager.GetChatSession(chatId).IsAuthorized())
            {
                messageManager.SendWelcomeMessage(chatId);
            }
        }

        private void HandleRegistrationCommand(long chatId)
        {
            ISessionManager sessionManager =
                ModulesManager.GetModulesManager().GetSessionManager();
            IMessageManager messageManager = 
                ModulesManager.GetModulesManager().GetMessageManager();
            IUserManager userManager =
                ModulesManager.GetModulesManager().GetUserManager();

            if (!sessionManager.GetChatSession(chatId).IsAuthorized())
            {
                string authCode = userManager.CreateNewUser(chatId);
                messageManager.SendRegistrationSuccessMessage(chatId, authCode);
            }
        }

        private void HandleGetStashCommand(long chatId)
        {
            ISessionManager sessionManager =
                ModulesManager.GetModulesManager().GetSessionManager();
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            if (sessionManager.GetChatSession(chatId).IsAuthorized())
            {
                //TODO get stash
                const string answer = "GET STASH COMMAND";
                messageManager.SendTextMessage(chatId, answer);
            }
        }
    }
}
