using System.Collections.Generic;
using System.Threading.Tasks;
using StashBot.Module.Database;
using StashBot.Module.Database.Stash;
using StashBot.Module.User;
using StashBot.BotResponses;
using StashBot.Module.Session;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal class AuthorizedStateHandler : IChatStateHandler
    {
        private readonly IChatCommands chatCommands;

        internal AuthorizedStateHandler()
        {
            chatCommands = new ChatCommands();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            chatCommands.Add("/Stash", true, GetStash);
            chatCommands.Add("/logout", true, Logout);
            chatCommands.AddExitCommand(true);
        }

        public void StartStateMessage(long chatId)
        {
            IMessageManager messageManager = ModulesManager.GetMessageManager();

            messageManager.SendTextMessage(chatId, TextResponse.Get(ResponseType.Login), chatCommands.CreateReplyKeyboard());
        }

        public void HandleUserMessage(ITelegramUserMessage message, IChatStateHandlerContext context)
        {
            if (message == null || context == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(message.Message) && chatCommands.ContainsCommand(message.Message))
            {
                chatCommands.Get(message.Message)(message.ChatId, context);
            }
            else
            {
                if (!message.IsEmpty())
                {
                    _ = SaveMessageToStash(message);
                }
            }
        }

        private async Task SaveMessageToStash(ITelegramUserMessage message)
        {
            IDatabaseManager databaseManager = ModulesManager.GetDatabaseManager();

            IUser user = databaseManager.GetUser(message.ChatId);
            if (user != null && user.IsAuthorized)
            {
                IStashMessage stashMessage = databaseManager.CreateStashMessage(message);
                await stashMessage.Download();
                stashMessage.Encrypt(user);
                IDatabaseError databaseError = databaseManager.SaveMessageToStash(stashMessage);
                databaseError.Handle(message.ChatId);
            }
        }

        private void GetStash(long chatId, IChatStateHandlerContext context)
        {
            IDatabaseManager databaseManager = ModulesManager.GetDatabaseManager();
            IMessageManager messageManager = ModulesManager.GetMessageManager();

            IUser user = databaseManager.GetUser(chatId);
            if (user != null && user.IsAuthorized)
            {
                ICollection<IStashMessage> messagesFromStash = databaseManager.GetMessagesFromStash(chatId);
                if (messagesFromStash.Count == 0)
                {
                    messageManager.SendTextMessage(chatId, TextResponse.Get(ResponseType.EmptyStash), null);
                }
                else
                {
                    foreach (IStashMessage stashMessage in messagesFromStash)
                    {
                        stashMessage.Decrypt(user);
                        stashMessage.Send();
                    }
                }
            }
        }

        private void Logout(long chatId, IChatStateHandlerContext context)
        {
            IMessageManager messageManager = ModulesManager.GetMessageManager();
            IUserManager userManager = ModulesManager.GetUserManager();

            userManager.LogoutUser(chatId);
            messageManager.SendTextMessage(chatId, TextResponse.Get(ResponseType.Logout), null);
            context.ChangeChatState(chatId, ChatSessionState.Start);
        }
    }
}
