using System.Collections.Generic;
using System.Threading.Tasks;
using StashBot.Module.Database;
using StashBot.Module.Database.Stash;
using StashBot.Module.User;
using StashBot.BotResponses;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal class AuthorizedStateHandler : IChatStateHandler
    {
        private delegate void Command(long chatId, IChatStateHandlerContext context);
        private readonly Dictionary<string, Command> commands;

        internal AuthorizedStateHandler()
        {
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
            IMessageManager messageManager = ModulesManager.GetModulesManager().GetMessageManager();

            messageManager.SendTextMessage(chatId, TextResponse.Get(ResponseType.Login));
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
                if (!message.IsEmpty())
                {
                    _ = SaveMessageToStash(message);
                }
            }
        }

        private bool IsCommand(string message)
        {
            return !string.IsNullOrEmpty(message) && commands.ContainsKey(message);
        }

        private async Task SaveMessageToStash(ITelegramUserMessage message)
        {
            IDatabaseManager databaseManager = ModulesManager.GetModulesManager().GetDatabaseManager();

            IUser user = databaseManager.GetUser(message.ChatId);
            if (user != null && user.IsAuthorized)
            {
                IStashMessage stashMessage = databaseManager.CreateStashMessage(message);
                await stashMessage.Download();
                stashMessage.Encrypt(user);
                databaseManager.SaveMessageToStash(stashMessage);
            }
        }

        private void GetStash(long chatId, IChatStateHandlerContext context)
        {
            IDatabaseManager databaseManager = ModulesManager.GetModulesManager().GetDatabaseManager();

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
            IMessageManager messageManager = ModulesManager.GetModulesManager().GetMessageManager();
            IUserManager userManager = ModulesManager.GetModulesManager().GetUserManager();

            userManager.LogoutUser(chatId);
            messageManager.SendTextMessage(chatId, TextResponse.Get(ResponseType.Logout));
            context.ChangeChatState(chatId, Session.ChatSessionState.Start);
        }
    }
}
