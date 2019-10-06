using StashBot.Module.Message.Handler;
using StashBot.Module.Message.Sender;

namespace StashBot.Module.Message
{
    internal class MessageManager : IMessageManager
    {
        private readonly IMessageHandler messageHandler;
        private readonly IMessageSender messageSender;

        internal MessageManager()
        {
            messageHandler = new MessageHandler();
            messageSender = new MessageSender();
        }

        public void SendTextMessage(long chatId, string textMessage)
        {
            messageSender.SendTextMessage(chatId, textMessage);
        }

        public void UserSentTextMessage(long chatId, int messageId, string textMessage)
        {
            messageHandler.HandleUserTextMessage(chatId, messageId, textMessage);
        }
    }
}
