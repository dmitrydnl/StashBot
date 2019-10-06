using StashBot.Module.Message.Handler;
using StashBot.Module.Message.Sender;
using StashBot.Module.Message.Delete;
using System.Collections.Generic;

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

        public void HandleUserTextMessage(long chatId, int messageId, string textMessage)
        {
            messageHandler.HandleUserTextMessage(chatId, messageId, textMessage);
        }

        public void SendTextMessage(long chatId, string textMessage)
        {
            messageSender.SendTextMessage(chatId, textMessage);
        }

        public void SendWelcomeMessage(long chatId)
        {
            const string message = "Input command /reg for registration\nIf you already registered, just input your key\n(WARNING)\nIf you already registration,\nafter new registration you'll lose all old data";
            SendTextMessage(chatId, message);
        }

        public void SendRegistrationSuccessMessage(long chatId, string authCode)
        {
            string message = $"Registration success\nUse your code for auth:\n{authCode}";
            SendTextMessage(chatId, message);
        }

        public void SendAuthorisationSuccessMessage(long chatId)
        {
            const string message = "Authorisation success";
            SendTextMessage(chatId, message);
        }

        public void SendAuthorisationFailMessage(long chatId)
        {
            const string message = "Authorisation fail";
            SendTextMessage(chatId, message);
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
