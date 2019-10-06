namespace StashBot.Module.Message.Handler
{
    internal interface IMessageHandler
    {
        void HandleUserTextMessage(long chatId, int messageId, string textMessage);
    }
}
