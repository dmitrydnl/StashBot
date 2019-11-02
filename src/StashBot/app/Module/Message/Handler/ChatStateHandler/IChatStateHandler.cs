namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal interface IChatStateHandler
    {
        void StartStateMessage(long chatId);
        void HandleUserMessage(
            ITelegramUserMessage message,
            IChatStateHandlerContext context);
    }
}
