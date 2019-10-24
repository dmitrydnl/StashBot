namespace StashBot.Module.Message.Handler
{
    internal interface IMessageHandler
    {
        void HandleUserMessage(ITelegramUserMessage message);
    }
}
