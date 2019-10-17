using System.Collections.Generic;
using StashBot.Module.Message.Handler;
using StashBot.Module.Message.Sender;
using StashBot.Module.Message.Delete;

namespace StashBot.Module.Message
{
    internal class MessageManager : IMessageManager
    {
        private readonly IMessageHandler messageHandler;
        private readonly IMessageSender messageSender;
        private readonly IMessageDelete messageDelete;

        internal MessageManager()
        {
            messageHandler = new MessageHandler();
            messageSender = new MessageSender();
            messageDelete = new MessageDelete();
        }

        public void HandleUserMessage(long chatId, int messageId, string message)
        {
            messageHandler.HandleUserMessage(chatId, messageId, message);
        }

        public void SendTextMessage(long chatId, string textMessage)
        {
            messageSender.SendTextMessage(chatId, textMessage);
        }

        public void DeleteBotMessage(long chatId, int messageId)
        {
            messageDelete.DeleteBotMessage(chatId, messageId);
        }

        public void DeleteListBotMessages(long chatId, List<int> messagesId)
        {
            messageDelete.DeleteListBotMessages(chatId, messagesId);
        }
    }
}
