namespace StashBot.Module.Message.Handler
{
    internal interface IMessageHandler
    {
        void HandleUserMessage(long chatId, int messageId, string message);
    }
}
