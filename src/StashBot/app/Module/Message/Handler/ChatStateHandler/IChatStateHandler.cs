namespace StashBot.Module.Message.Handler.ChatStateHandler
{
    internal interface IChatStateHandler
    {
        void StartStateMessage(long chatId);
        void HandleUserMessage(long chatId, int messageId, string message, IChatStateHandlerContext context);
    }
}
