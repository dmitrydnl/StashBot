using System.Collections.Generic;
using StashBot.Module.Database;
using StashBot.Module.Database.Stash;
using StashBot.Module.User;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal class AuthorizedStateHandler : IChatStateHandler
    {
        private delegate void Command(long chatId, IChatStateHandlerContext context);
        private readonly Dictionary<string, Command> commands;
        private readonly IStashMessageFactory stashMessageFactory;

        internal AuthorizedStateHandler()
        {
            stashMessageFactory = new StashMessageFactory();
            commands = new Dictionary<string, Command>();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            commands.Add("/stash", GetStash);
            commands.Add("/logout", Logout);
        }

        public void StartStateMessage(long chatId)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();

            const string loginMessage = "Input message to save it in stash.\nGet messages in stash: /stash\nLogout: /logout";
            messageManager.SendMessage(chatId, loginMessage);
        }

        public void HandleUserMessage(ITelegramUserMessage message, IChatStateHandlerContext context)
        {
            if (IsCommand(message.Message))
            {
                commands[message.Message](message.ChatId, context);
            }
            else
            {
                if (!string.IsNullOrEmpty(message.Message))
                {
                    SaveTextMessageToStash(message);
                }
            }
        }

        private bool IsCommand(string message)
        {
            return !string.IsNullOrEmpty(message) && commands.ContainsKey(message);
        }

        private void SaveTextMessageToStash(ITelegramUserMessage message)
        {
            IDatabaseManager databaseManager =
                    ModulesManager.GetModulesManager().GetDatabaseManager();

            IUser user = databaseManager.GetUser(message.ChatId);
            if (user != null && user.IsAuthorized)
            {
                IStashMessage stashMessage = stashMessageFactory.Create(message);
                stashMessage.Encrypt(user);
                databaseManager.SaveMessageToStash(stashMessage);
            }
        }

        private void GetStash(long chatId, IChatStateHandlerContext context)
        {
            IDatabaseManager databaseManager =
                ModulesManager.GetModulesManager().GetDatabaseManager();


            IUser user = databaseManager.GetUser(chatId);
            if (user != null && user.IsAuthorized)
            {
                List<IStashMessage> messagesFromStash = databaseManager.GetMessagesFromStash(chatId);
                foreach(IStashMessage stashMessage in messagesFromStash)
                {
                    stashMessage.Decrypt(user);
                    stashMessage.Send();
                }
            }
        }

        private void Logout(long chatId, IChatStateHandlerContext context)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();
            IUserManager userManager =
                ModulesManager.GetModulesManager().GetUserManager();

            userManager.LogoutUser(chatId);
            const string logoutMessage = "You're logged out";
            messageManager.SendMessage(chatId, logoutMessage);
            context.ChangeChatState(chatId, Session.ChatSessionState.Start);
        }
    }
}
