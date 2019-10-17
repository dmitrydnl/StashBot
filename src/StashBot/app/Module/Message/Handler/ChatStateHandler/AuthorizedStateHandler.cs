using System.Collections.Generic;
using StashBot.Module.Database;

namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal class AuthorizedStateHandler : IChatStateHandler
    {
        private delegate void Command(long chatId, int messageId, IChatStateHandlerContext context);
        private readonly Dictionary<string, Command> commands;

        internal AuthorizedStateHandler()
        {
            commands = new Dictionary<string, Command>();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            commands.Add("/stash", GetStash);
        }

        public void HandleUserMessage(long chatId, int messageId, string message, IChatStateHandlerContext context)
        {
            if (commands.ContainsKey(message))
            {
                commands[message](chatId, messageId, context);
            }
            else
            {
                AddDataToStashHandle(chatId, message);
            }
        }

        private void AddDataToStashHandle(long chatId, string message)
        {
            IDatabaseManager databaseManager =
                    ModulesManager.GetModulesManager().GetDatabaseManager();

            databaseManager.SaveMessageToStash(chatId, message);
        }

        private void GetStash(long chatId, int messageId, IChatStateHandlerContext context)
        {
            IMessageManager messageManager =
                ModulesManager.GetModulesManager().GetMessageManager();
            IDatabaseManager databaseManager =
                ModulesManager.GetModulesManager().GetDatabaseManager();

            List<string> messagesFromStash = databaseManager.GetMessagesFromStash(chatId);
            foreach (string textMessage in messagesFromStash)
            {
                messageManager.SendMessage(chatId, textMessage);
            }
        }
    }
}
